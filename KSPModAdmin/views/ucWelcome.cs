using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class ucWelcome : ucBase
    {
        #region Members

        /// <summary>
        /// The version of KSP Mod Admin.
        /// </summary>
        private string mVersion = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The version of KSP Mod Admin.
        /// </summary>
        public string Version
        {
            get
            {
                return mVersion;
            }
            set
            {
                mVersion = value;
                label1.Text = string.Format("Welcome to KSP Mod Admin v{0}", mVersion);
            }
        }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        public string SelectedPath
        {
            get
            {
                return tb_KSPInstallFolder.Text;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the class ucKSPPathSelection.
        /// </summary>
        public ucWelcome()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handles the Click event of the btn_FolderBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_FolderBrowser_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            string kspPath = MainForm.Options.AskForKSPInstallFolder();
            if (kspPath != String.Empty)
            {
                tb_KSPInstallFolder.Text = kspPath;
                btn_OK.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_NormalSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NormalSearch_Click(object sender, EventArgs e)
        {
            btn_FolderBrowser.Enabled = false;
            btn_NormalSearch.Enabled = false;
            btn_SteamSearch.Enabled = false;
            tlpSearchBG.Visible = true;

            MainForm.Options.SearchFolderStruct((int)numericUpDown1.Value, SearchFinished);
        }

        /// <summary>
        /// Handles the Click event of the btn_SteamSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SteamSearch_Click(object sender, EventArgs e)
        {
            btn_FolderBrowser.Enabled = false;
            btn_NormalSearch.Enabled = false;
            btn_SteamSearch.Enabled = false;
            tlpSearchBG.Visible = true;

            MainForm.Options.SearchSteamVersion(SearchFinished);
        }

        /// <summary>
        /// Handles the Click event of the btn_OK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_KSPInstallFolder.Text))
                MessageBox.Show(ParentForm, "Please select a KSP install folder first.");
            else
            { 
                MainForm.KSPPath = tb_KSPInstallFolder.Text;

                Visible = false;
                //ParentForm.Controls.Remove(this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_Later.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Later_Click(object sender, EventArgs e)
        {
            Visible = false;
            tlpSearchBG.Visible = false;
            btn_FolderBrowser.Enabled = true;
            btn_NormalSearch.Enabled = true;
            btn_SteamSearch.Enabled = true;

            // TODO: Stop search.
            MainForm.Options.StopSearch();
        }

        /// <summary>
        /// Handles the SearchFinished event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFinished(List<string> result, Exception ex)
        {
            btn_FolderBrowser.Enabled = true;
            btn_NormalSearch.Enabled = true;
            btn_SteamSearch.Enabled = true;
            tlpSearchBG.Visible = false;

            if (result != null && result.Count > 0)
            {
                tb_KSPInstallFolder.Text = result[0];
                btn_OK.Enabled = true;
            }
        }

        #endregion

        #endregion
    }
}
