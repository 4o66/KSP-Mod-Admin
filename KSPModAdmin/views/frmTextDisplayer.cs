using System;
using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class TextDisplayer : Form
    {
        public TextDisplayer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
