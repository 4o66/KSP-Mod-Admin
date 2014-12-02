using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmCollisionSolving : Form
    {
        private TreeNodeMod m_CollisionMod = null;


        public TreeNodeMod CollisionMod
        {
            set
            {
                m_CollisionMod = value;

                if (m_CollisionMod != null)
                {
                    List<TreeNodeMod> collisionMods = ModRegister.GetCollisionModsByCollisionMod(m_CollisionMod);
                    cbModSelect.Items.AddRange(collisionMods.ToArray());
                    cbModSelect.SelectedItem = m_CollisionMod.ZipRoot;

                    rbKeepInstalled.Checked = true;
                    rbKeepInstalled.Enabled = true;

                    TreeNodeMod installedMod = GetInstalledMod();
                    if (installedMod != null)
                        tbInstalledMod.Text = GetInstalledMod().ToString();
                    else
                    {
                        tbInstalledMod.Text = string.Empty;
                        rbKeepSelected.Checked = true;
                        rbKeepInstalled.Enabled = false;
                    }
                }
                else
                {
                    cbModSelect.Items.Clear();
                    cbModSelect.SelectedItem = m_CollisionMod;
                }
            }
        }

        public TreeNodeMod SelectedMod { get; set; }


        public frmCollisionSolving()
        {
            InitializeComponent();
        }


        private void rbKeepSelected_CheckedChanged(object sender, EventArgs e)
        {
            cbModSelect.Enabled = rbKeepSelected.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedMod = null;
            if (rbKeepInstalled.Checked)
                SelectedMod = GetInstalledMod();
            else
                SelectedMod = (TreeNodeMod)cbModSelect.SelectedItem;

            ModRegister.SolveCollisions(SelectedMod);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnShowConflicts_Click(object sender, EventArgs e)
        {
            frmShowConflicts dlg = new frmShowConflicts();
            dlg.CollisionMod = m_CollisionMod;
            dlg.ShowDialog();
        }


        private TreeNodeMod GetInstalledMod()
        {
            TreeNodeMod node = null;
            List<TreeNodeMod> collisionModFiles = ModRegister.GetCollisionModFiles(m_CollisionMod);
            foreach (var n in collisionModFiles)
            {
                if (!n.IsInstalled) 
                    continue;

                node = n.ZipRoot;
                break;
            }

            return node;
        }
    }
}
