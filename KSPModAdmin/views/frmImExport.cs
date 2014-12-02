using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmImExport : frmBase
    {
        public frmImExport()
        {
            InitializeComponent();
        }

        #region private

        #region Event handling

        private void frmImExport_Load(object sender, EventArgs e)
        {
            lblCurrentAction.Text = string.Empty;

            foreach (TreeNodeMod mod in MainForm.Instance.ModSelection.tvModSelection.Nodes)
            {
                cbModSelection.Items.Add(mod);
                cbModSelection.CheckBoxItems[cbModSelection.CheckBoxItems.Count - 1].Checked = true;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void rbExportSelectedOnly_CheckedChanged(object sender, EventArgs e)
        {
            cbModSelection.Enabled = rbExportSelectedOnly.Checked;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void cbDownloadIfNeeded_CheckedChanged(object sender, EventArgs e)
        {
            rbInstall.Enabled = (cbExtract.Checked) && (cbDownloadIfNeeded.Checked);
            rbAddOnly.Enabled = rbInstall.Enabled;
            btnImport.Enabled = rbInstall.Enabled;
        }

        private void cbExtract_CheckedChanged(object sender, EventArgs e)
        {
            rbInstall.Enabled = (cbExtract.Checked) && (cbDownloadIfNeeded.Checked);
            rbAddOnly.Enabled = rbInstall.Enabled;
            btnImport.Enabled = rbInstall.Enabled;
        }

        #endregion

        #region Export

        private void Export()
        {
            List<TreeNodeMod> modsToExport = GetModsToExport();
            if (modsToExport.Count <= 0)
                MessageBox.Show(this, "There are no mods to export.", "");
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = MainForm.Instance.Options.DownloadPath;
                dlg.FileName = RemoveInvalidCharsFromPath(string.Format("ModPack_{0}.modpack", DateTime.Now.ToShortDateString()));
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.FileName = RemoveInvalidCharsFromPath(dlg.FileName);

                    AddMessage(string.Format("Exporting to \"{0}\".", dlg.FileName));
                    new AsyncTask<bool>(() =>
                                        {
                                            ModPackHandler.Export(modsToExport, dlg.FileName, cbIncludeMods.Checked);
                                            return true;
                                        },
                                        (b, ex) =>
                                        {
                                            if (ex != null) 
                                                MessageBox.Show(this, ex.Message, "Error");
                                        }).Run();
                    AddMessage("Export done.");
                    Close();
                }
                else
                    AddMessage("Export aborted.");
            }
        }

        private string RemoveInvalidCharsFromPath(string path)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (path.Contains(c))
                    path = path.Replace(c, '_');
            }

            return path;
        }

        private List<TreeNodeMod> GetModsToExport()
        {
            if (rbExportAll.Checked)
                return MainForm.Instance.ModSelection.Nodes.ToList();
            else if (rbExportSelectedOnly.Checked)
                return (from e in cbModSelection.CheckBoxItems where e.Checked select (TreeNodeMod) e.ComboBoxItem).ToList();

            return new List<TreeNodeMod>();
        }

        #endregion

        #region Import

        private void Import()
        {
            AddMessage("Starting import.");
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = MainForm.Instance.Options.DownloadPath;
            dlg.Filter = Constants.MODPACK_FILTER;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                new AsyncTask<bool>(() =>
                                    {
                                        if (cbClearModSelection.Checked)
                                        {
                                            AddMessage("Clearing ModSelection.");
                                            InvokeIfRequired(() => MainForm.Instance.ModSelection.RemoveAllMods(false));
                                        }
                                        InvokeIfRequired(() => MainForm.Instance.Options.GetValidDownloadPath());
                                        AddMessage(string.Format("Importing from \"{0}\".", dlg.FileName));
                                        ModPackHandler.Import(this, dlg.FileName, MainForm.Instance.Options.DownloadPath, cbExtract.Checked,
                                                              cbDownloadIfNeeded.Checked, rbCopyDestination.Checked, rbAddOnly.Checked,
                                                              (o, msg) => AddMessage(msg));
                                        return true;
                                    },
                                    (b, ex) =>
                                    {
                                        if (ex != null)
                                        {
                                            AddMessage("Import failed!"); 
                                            MessageBox.Show(this, ex.Message, "Error");
                                        }
                                        else
                                        {
                                            AddMessage("Import done.");
                                            Close();
                                        }
                                    }).Run();
            }
            else
                AddMessage("Import aborted.");

        }

        #endregion

        private void AddMessage(string msg)
        {
            InvokeIfRequired(() => lblCurrentAction.Text = msg);
            MainForm.Instance.AddInfo(msg);
        }

        #endregion
    }
}
