using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmAddMod : frmBase
    {
        public frmAddMod()
        {
            InitializeComponent();
        }


        private void frmAddMod_Load(object sender, EventArgs e)
        {
            tbModPath.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbModPath.Text) || (!CurseForgeHelper.IsValidURL(tbModPath.Text) && !SpaceportHelper.IsValidURL(tbModPath.Text) &&
                !KSPForumHelper.IsValidURL(tbModPath.Text) && !ValidModPath(tbModPath.Text)))
            {
                MessageBox.Show(this, "Please enter a valid archive path or Spaceport/KSPForum URL.", "");
                return;
            }

            tbModName.Enabled = false;
            tbModPath.Enabled = false;
            btnAdd.Enabled = false;
            btnCancel.Enabled = false;
            btnFolderSearch.Enabled = false;
            cbInstallAfterAdd.Enabled = false;
            picLoading.Visible = true;

            string modPath = tbModPath.Text;
            new AsyncTask<bool>(() =>
                                {
                                    TreeNodeMod newMod = null;
                                    InvokeIfRequired(() =>
                                        {
                                            if (SpaceportHelper.IsValidURL(modPath))
                                                newMod = MainForm.Instance.ModSelection.HandleModAddViaSpaceport(modPath, tbModName.Text, cbInstallAfterAdd.Checked);
                                            else if (KSPForumHelper.IsValidURL(modPath))
                                                newMod = MainForm.Instance.ModSelection.HandleModAddViaKSPForum(modPath, tbModName.Text, cbInstallAfterAdd.Checked);
                                            else if (CurseForgeHelper.IsValidURL(modPath))
                                                newMod = MainForm.Instance.ModSelection.HandleModAddViaCurseForge(modPath, tbModName.Text, cbInstallAfterAdd.Checked);
                                            else if (ValidModPath(modPath))
                                                newMod = MainForm.Instance.ModSelection.HandleModAddViaPath(modPath, tbModName.Text, cbInstallAfterAdd.Checked);
                                        });
                                    return (newMod != null);
                                }, (success, ex) =>
                                {
                                    if (ex != null)
                                        MessageBox.Show(this, ex.Message, "Error!");

                                    tbModName.Enabled = true;
                                    tbModPath.Enabled = true;
                                    btnAdd.Enabled = true;
                                    btnCancel.Enabled = true;
                                    btnFolderSearch.Enabled = true;
                                    cbInstallAfterAdd.Enabled = true;
                                    picLoading.Visible = false;

                                    if (success)
                                        Close();
                                }).Run();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFolderSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Constants.ADD_DLG_FILTER;
            dlg.Multiselect = false; // TODO: alow multiselect.

            string path = MainForm.Instance.Options.DownloadPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                dlg.InitialDirectory = path;

            if (dlg.ShowDialog() == DialogResult.OK)
                tbModPath.Text = dlg.FileName;
        }


        private bool ValidModPath(string path)
        {
            string ext = Path.GetExtension(tbModPath.Text);
            if (string.IsNullOrEmpty(ext))
                return false;

            ext = ext.ToLower();
            return ((ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR ||
                     ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT) && 
                     File.Exists(tbModPath.Text));
        }
    }
}
