using System;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    /// <summary>
    /// Dialog to choose a source and destination folder.
    /// </summary>
    public partial class frmDestFolderSelection : Form
    {
        #region Members

        private TreeNodeMod m_SourceNode = null;

        #endregion

        #region Properties

        /// <summary>
        /// The parent main form.
        /// </summary>
        public MainForm MainForm { get; set; }

        /// <summary>
        /// Sets the available destination pahts.
        /// </summary>
        public string[] DestFolders
        {
            set
            {
                if (value == null)
                    CB_Dest.Items.Clear();
                else
                {
                    CB_Dest.Items.Add(new DestInfo("Other folder ...", ""));
                    foreach (string path in value)
                    {
                        int index = path.LastIndexOf("\\");
                        if (index >= 0)
                        {
                            string name = path.Substring(index);
                            name = name[0] + name[1].ToString().ToUpper() + name.Substring(2);
                            CB_Dest.Items.Add(new DestInfo(name, path));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the available source folders.
        /// </summary>
        public TreeNodeMod[] SrcFolders
        {
            set
            {
                if (value == null)
                    CB_Source.Items.Clear();
                else
                {
                    if (value.Length > 0)
                    {
                        m_SourceNode = value[0];
                        foreach (TreeNodeMod child in value)
                            AddSrcFolder(child);

                        CB_ListFoldersOnly.Checked = !m_SourceNode.IsFile;
                        SrcFolder = m_SourceNode.Text;
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets the selected destination folder.
        /// </summary>
        public string DestFolder
        {
            get
            {
                string result = string.Empty;
                if (CB_Dest.SelectedIndex >= 0)
                    result = ((DestInfo)CB_Dest.SelectedItem).Fullpath;

                return result.ToLower();
            }
            set
            {
                foreach (DestInfo entry in CB_Dest.Items)
                {
                    if (entry.Name.ToLower() == value.ToLower())
                    {
                        CB_Dest.SelectedItem = entry;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the selected source folder.
        /// </summary>
        public string SrcFolder
        {
            get
            {
                TreeNodeMod result = null;
                if (CB_Source.SelectedIndex >= 0)
                    result = (TreeNodeMod)CB_Source.SelectedItem;

                if (result != null)
                    return result.Name;
                else
                    return string.Empty;
            }
            set
            {
                foreach (TreeNodeMod entry in CB_Source.Items)
                {
                    if (entry.Text == value)
                        CB_Source.SelectedItem = entry;
                }
            }
        }

        /// <summary>
        /// Gets a value that indicates wether the content of the source should be copied or the selected source itself.
        /// </summary>
        public bool CopyContent { get { return radioButton1.Checked; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DestFolderSelection class.
        /// </summary>
        public frmDestFolderSelection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the DestFolderSelection class.
        /// </summary>
        public frmDestFolderSelection(MainForm mainform)
        {
            InitializeComponent();

            MainForm = mainform;
        }

        #endregion

        #region Eventhandling

        /// <summary>
        /// Handels the Click event of the btnOK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CB_Dest.SelectedIndex >= 0 && CB_Source.SelectedIndex >= 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (CB_Dest.SelectedIndex == -1)
                    MessageBox.Show(Constants.MESSAGE_SELECT_DEST);
                else if (CB_Source.SelectedIndex == -1)
                    MessageBox.Show(Constants.MESSAGE_SELECT_SCOURCE);
            }
        }

        /// <summary>
        /// Handels the Click event of the btnCancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handels the SelectedIndexChanged event of the CB_Dest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Dest_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "Other folder ..." selected?
            if (CB_Dest.SelectedIndex == 0)
            {
                FolderSelectDialog dlg = new FolderSelectDialog();
                dlg.Title = "Destination folder selection";
                dlg.InitialDirectory = MainForm.GetPath(KSP_Paths.KSPRoot);
                if (dlg.ShowDialog(this.Handle))
                {
                    string dest = dlg.FileName;
                    string destName = dest.Substring(dest.LastIndexOf("\\"));
                    CB_Dest.Items.Add(new DestInfo(destName, dlg.FileName));
                    CB_Dest.SelectedIndex = CB_Dest.Items.Count - 1;
                }
                else
                    CB_Dest.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Handels the CheckedChanged event of the CB_ListFoldersOnly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_ListFoldersOnly_CheckedChanged(object sender, EventArgs e)
        {
            CB_Source.Items.Clear();
            SrcFolders = new TreeNodeMod[] { m_SourceNode };
        }

        #endregion

        #region Private

        /// <summary>
        /// Adds a source folder to the CB_Source for the passed node and all its childs.
        /// </summary>
        /// <param name="node">The node to add as source folder.</param>
        /// <param name="depth">The depth of the recursive call.</param>
        /// <note>Recursive function!</note>
        private void AddSrcFolder(TreeNodeMod node, int depth = 0)
        {
            if (!CB_ListFoldersOnly.Checked || (CB_ListFoldersOnly.Checked && !node.IsFile))
                CB_Source.Items.Add(node);

            foreach (TreeNodeMod child in node.Nodes)
                if (!CB_ListFoldersOnly.Checked || (CB_ListFoldersOnly.Checked && !child.IsFile))
                    AddSrcFolder(child, depth + 1);
        }

        #endregion
    }

    /// <summary>
    /// Class for destination paths.
    /// </summary>
    public class DestInfo
    {
        /// <summary>
        /// Display name of the destination path.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full destination path.
        /// </summary>
        public string Fullpath { get; set; }


        /// <summary>
        /// Creates a instance of the DestInfo class.
        /// </summary>
        /// <param name="name">The display name of the destination path.</param>
        /// <param name="fullpath">The full destination path.</param>
        public DestInfo(string name, string fullpath)
        {
            Name = name;
            Fullpath = fullpath;
        }


        /// <summary>
        /// Returns the display name of the destination path.
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return Name; }
    }
}
