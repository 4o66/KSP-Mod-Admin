using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class frmSelectDownloadURL : Form
    {
        public List<string> Links
        {
            get
            {
                if (cbLinks.Items.Count > 0)
                    return cbLinks.Items.Cast<string>().ToList();
                else
                    return new List<string>();
            }
            set
            {
                cbLinks.Items.Clear();
                if (value != null && value.Count > 0)
                {
                    foreach (var e in value)
                        cbLinks.Items.Add(e);
                    cbLinks.SelectedItem = cbLinks.Items[0];
                }
            }
        }

        public string SelectedLink
        {
            get
            {
                return (string)cbLinks.SelectedItem;
            }
        }


        public frmSelectDownloadURL()
        {
            InitializeComponent();
        }


        private void frmSelectDownloadURL_Load(object sender, EventArgs e)
        {
            cbLinks.Select();
            cbLinks.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
