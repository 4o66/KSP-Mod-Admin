using System;
using System.ComponentModel;
using System.Windows.Forms;
using KSPModAdmin.Utils;
using System.Collections.Generic;
using KSPModAdmin.Utils.CommonTools;

namespace KSPModAdmin.Views
{
    public partial class frmCopyModInfo : Form
    {
        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TreeNodeMod SourceMod 
        {
            get
            {
                return (tbSourceMod.Tag != null) ? (TreeNodeMod)tbSourceMod.Tag : null;
            }
            set
            {
                tbSourceMod.Tag = value;
                if (value != null)
                    tbSourceMod.Text = value.ToString();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TreeNodeMod DestMod 
        {
            get 
            {
                return (cbDestMod.SelectedItem != null) ? (TreeNodeMod)cbDestMod.SelectedItem : null;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TreeListViewNodes Mods
        {
            set
            {
                if (value != null)
                {
                    foreach (TreeNodeMod mod in value)
                    {
                        if (SourceMod != null && SourceMod.Name != mod.Name)
                            cbDestMod.Items.Add(mod);
                    }

                    cbDestMod.SelectedItem = cbDestMod.Items[0];
                }
            } 
        }

        #endregion


        public frmCopyModInfo()
        {
            InitializeComponent();

            SourceMod = null;
        }


        private void btnCopy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (SourceMod != null)
            { 
                DestMod.ModInfo = SourceMod.ModInfo;
                DestMod.Note = SourceMod.Note;
            } 

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
