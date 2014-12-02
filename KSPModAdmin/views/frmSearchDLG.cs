using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmSearchDLG : frmBase
    {
        #region Members

        /// <summary>
        /// Reference to the calling form.
        /// </summary>
        private MainForm m_MainForm = null;

        /// <summary>
        /// The list of matching nodes. (Search result)
        /// </summary>
        private List<TreeNodeMod> m_MatchingNodes = new List<TreeNodeMod>();

        /// <summary>
        /// The background searcher.
        /// </summary>
        private AsyncTask<bool> m_SearchAsync = null;

        /// <summary>
        /// The last selected node that matched to the searchstring.
        /// </summary>
        private int m_LastMatchedNodeIndex;

        #endregion

        #region Constructors

#if DESIGNER
        /// <summary>
        /// Default constructor for the VS Designer.
        /// </summary>
        public SearchDLG()
        {
            InitializeComponent();
        }
#else
        /// <summary>
        /// Creates a instance of the frmSearchDLG class.
        /// </summary>
        /// <param name="mainForm">Reference to the MainForm.</param>
        public frmSearchDLG(MainForm mainForm)
        {
            InitializeComponent();

            m_MainForm = mainForm;
        }
#endif

#endregion

        #region Event handling

        /// <summary>
        /// Handles the Load event of the Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSearchDLG_Load(object sender, EventArgs e)
        {
            tbSearchString.Select();
        }

        /// <summary>
        /// Handles the FormClosing event of the Form.
        /// Cancles backgroundworker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSearchDLG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SearchAsync != null)
                m_SearchAsync.Cancel();
        }

        /// <summary>
        /// Handles the click event of the btn_Search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (tbSearchString.Text != string.Empty && m_MainForm != null)
                StartSearch();
        }

        /// <summary>
        /// Handles the click event for the btnNext.
        /// Jumps to the next match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            m_LastMatchedNodeIndex = ++m_LastMatchedNodeIndex;

            if (m_LastMatchedNodeIndex >= m_MatchingNodes.Count)
                m_LastMatchedNodeIndex = 0;

            m_MainForm.ModSelection.SelectedNode = m_MatchingNodes[m_LastMatchedNodeIndex];
            m_MainForm.Focus();
        }

        /// <summary>
        /// Handles the click event for the btnPrev.
        /// Jumps to the previous match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            m_LastMatchedNodeIndex = --m_LastMatchedNodeIndex;

            if (m_LastMatchedNodeIndex < 0)
                m_LastMatchedNodeIndex = m_MatchingNodes.Count - 1;

            m_MainForm.ModSelection.SelectedNode = m_MatchingNodes[m_LastMatchedNodeIndex];
            m_MainForm.Focus();
        }

#endregion

        /// <summary>
        /// Starts the search.
        /// </summary>
        private void StartSearch()
        {
            if (m_SearchAsync != null)
                m_SearchAsync.Cancel();

            m_MatchingNodes.Clear();
            lblMatchCount.Text = "Matches: 0";
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            picLoading.Visible = true;
            string searchString = tbSearchString.Text;

            m_SearchAsync = new AsyncTask<bool>(delegate()
                                                {
                                                    foreach (TreeNodeMod node in m_MainForm.ModSelection.Nodes)
                                                        SearchNodes(node, searchString);
                                                
                                                    return m_MatchingNodes.Count > 0;
                                                },
                                                delegate(bool result, Exception ex)
                                                {
                                                    if (ex != null)
                                                        MessageBox.Show(this, ex.Message, "Error");

                                                    picLoading.Visible = false;
                                                },
                                                null,
                                                true);
            m_SearchAsync.Run();
        }

        /// <summary>
        /// Compares the passed node and its childs displayname with the search string.
        /// On a match the node or its child will be added to the m_MatchingNodes list.
        /// </summary>
        /// <param name="node">The node to start the search from.</param>
        /// <param name="searchString"></param>
        private void SearchNodes(TreeNodeMod node, string searchString)
        {
            if (node.Text.ToLower().Contains(searchString.ToLower()))
                InvokeIfRequired(() => ProceedMatch(node));

            foreach (TreeNodeMod child in node.Nodes)
                SearchNodes(child, searchString);
        }

        /// <summary>
        /// Called when the second metching node was found.
        /// </summary>
        private void ProceedMatch(TreeNodeMod matchingNode)
        {
            m_MatchingNodes.Add(matchingNode);

            lblMatchCount.Visible = true;
            lblMatchCount.Text = string.Format("Metches: {0}", m_MatchingNodes.Count);

            btnNext.Enabled = true;
            btnPrev.Enabled = true;

            if (m_MatchingNodes.Count == 1)
            {
                m_LastMatchedNodeIndex = 0;
                m_MainForm.ModSelection.SelectedNode = m_MatchingNodes[m_LastMatchedNodeIndex];
                m_MainForm.Focus();
            }
        }
    }
}
