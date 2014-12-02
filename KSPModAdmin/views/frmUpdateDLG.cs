using System;
using System.Windows.Forms;
using FolderSelect;

namespace KSPModAdmin.Views
{
    public partial class frmUpdateDLG : Form
    {
        public MainForm MainForm { get; set; }

        public PostDownloadAction PostDownloadAction
        {
            get
            {
                return (PostDownloadAction)cbPostDownloadAction.SelectedIndex;
            }
            set
            {
                cbPostDownloadAction.SelectedIndex = (int)value;
            }
        }

        public string DownloadPath
        {
            get
            {
                return tbDownloadPath.Text;
            }
            set
            {
                tbDownloadPath.Text = value;
            }
        }

        public string Message 
        {
            get
            {
                return tbMessage.Text;
            }
            set
            {
                tbMessage.Text = value;
            }
        }


        public frmUpdateDLG()
        {
            InitializeComponent();
        }


        private void UpdateDLG_Load(object sender, EventArgs e)
        {
            if (cbPostDownloadAction.SelectedIndex < 0)
                cbPostDownloadAction.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnDownloadPath_Click(object sender, EventArgs e)
        {
            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "Download folder selection";
            dlg.InitialDirectory = DownloadPath;
            if (dlg.ShowDialog(this.Handle))
            {
                DownloadPath = dlg.FileName;
                MainForm.AddInfo(string.Format("Download path changed to \"{0}\".", dlg.FileName));
            }
        }
    }
}
