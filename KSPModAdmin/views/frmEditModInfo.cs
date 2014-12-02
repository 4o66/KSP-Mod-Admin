using System;
using System.ComponentModel;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmEditModInfo : Form
    {
        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TreeNodeMod ModZipRoot 
        { 
            set
            {
                if (value != null)
                {
                    SpaceportURL = value.SpaceportURL;
                    ForumURL = value.ForumURL;
                    CurseForgeURL = value.CurseForgeURL;

                    ModName = value.Text;
                    tbName.ReadOnly = value.IsInstalled;

                    ProductID = value.ProductID;
                    Version = value.Version;
                    VersionControl = value.VersionControl;
                    Author = value.Author;
                    DownloadDate = value.AddDate;
                    CreationDate = value.CreationDate;
                    Rating = value.Rating;
                    Downloads = value.Downloads;
                    Note = value.Note;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModInfo ModInfo
        {
            get
            {
                ModInfo modInfo = new ModInfo();
                modInfo.Author = Author;
                modInfo.CreationDate = CreationDate;
                modInfo.DownloadDate = DownloadDate;
                modInfo.Downloads = Downloads;
                modInfo.Name = ModName;
                modInfo.ProductID = ProductID;
                modInfo.Rating = Rating;
                modInfo.SpaceportURL = SpaceportURL;
                modInfo.ForumURL = ForumURL;
                modInfo.CurseForgeURL = CurseForgeURL;
                modInfo.VersionControl = VersionControl;
                return modInfo;
            }
            set
            {
                if (value != null)
                {
                    if (!tbName.ReadOnly)
                        ModName = value.Name;

                    Author = value.Author;
                    Downloads = value.Downloads;
                    ProductID = value.ProductID;
                    Rating = value.Rating;
                    SpaceportURL = value.SpaceportURL;
                    ForumURL = value.ForumURL;
                    CurseForgeURL = value.CurseForgeURL;
                    CreationDate = value.CreationDate;
                    DownloadDate = value.DownloadDate;
                    VersionControl = value.VersionControl;
                }
                else
                {
                    ModName = string.Empty;
                    Author = string.Empty;
                    dtpCreation.Value = DateTime.Now.Date;
                    dtpDownload.Value = dtpCreation.Value;
                    Downloads = "0";
                    ProductID = string.Empty;
                    Rating = "0 (0)";
                    SpaceportURL = string.Empty;
                    ForumURL = string.Empty;
                    CurseForgeURL = string.Empty;
                    VersionControl = VersionControl.Spaceport;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Author
        {
            get
            {
                return tbAuthor.Text;
            }
            set
            {
                tbAuthor.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Downloads
        {
            get
            {
                return tbDownloads.Text;
            }
            set
            {
                tbDownloads.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ModName
        {
            get
            {
                return tbName.Text;
            }
            set
            {
                tbName.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Note
        {
            get
            {
                return tbNote.Text;
            }
            set
            {
                tbNote.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ProductID
        {
            get
            {
                return tbProductID.Text;
            }
            set
            {
                tbProductID.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Rating
        {
            get
            {
                return tbRating.Text;
            }
            set
            {
                tbRating.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string SpaceportURL
        {
            get
            {
                return tbSpaceportURL.Text;
            }
            set
            {
                tbSpaceportURL.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ForumURL
        {
            get
            {
                return tbForumURL.Text;
            }
            set
            {
                tbForumURL.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string CurseForgeURL
        {
            get
            {
                return tbCurseForgeURL.Text;
            }
            set
            {
                tbCurseForgeURL.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Version
        {
            get
            {
                return tbVersion.Text;
            }
            set
            {
                tbVersion.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string CreationDate
        {
            get
            {
                return dtpCreation.Value.ToString();
            }
            set
            {
                if (value != null)
                {
                    DateTime dtTemp = DateTime.Now.Date;
                     
                    if (DateTime.TryParse(value, out dtTemp))
                        dtpCreation.Value = dtTemp;
                    else
                        dtpCreation.Value = dtpDownload.Value == DateTime.MinValue ? DateTime.Now.Date : dtpDownload.Value;
                }
                else
                    dtpCreation.Value = dtpDownload.Value == DateTime.MinValue ? DateTime.Now.Date : dtpDownload.Value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string DownloadDate
        {
            get
            {
                return dtpDownload.Value.ToString();
            }
            set
            {
                if (value != null)
                {
                    DateTime dtTemp = DateTime.Now.Date;
                    dtpDownload.Value = (DateTime.TryParse(value, out dtTemp)) ? dtTemp : DateTime.Now.Date;
                }
                else
                    dtpDownload.Value = DateTime.Now.Date;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public VersionControl VersionControl
        {
            get
            {
                if (rbSpaceport.Checked)
                    return VersionControl.Spaceport;
                else if (rbKSPForum.Checked)
                    return VersionControl.KSPForum;
                else if (rbCurseForge.Checked)
                    return VersionControl.CurseForge;

                return VersionControl.None;
            }
            set
            {
                switch (value)
                {
                    default:
                        rbSpaceport.Checked = true;
                        break;
                    case VersionControl.Spaceport:
                        rbSpaceport.Checked = true;
                        break;
                    case VersionControl.KSPForum:
                        rbKSPForum.Checked = true;
                        break;
                    case VersionControl.CurseForge:
                        rbCurseForge.Checked = true;
                        break;
                }
            }
        }

        #endregion


        public frmEditModInfo()
        {
            InitializeComponent();
        }


        private void btnGotoSpaceport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SpaceportURL) && string.IsNullOrEmpty(ForumURL) && string.IsNullOrEmpty(CurseForgeURL))
                MessageBox.Show(this, "Enter a Spaceport, CurseForge or ForumURL fitst please.");
            else
            {
                string url = string.Empty;
                try
                {
                    ModInfo newModInfo = null;
                    if (SpaceportHelper.IsValidURL(SpaceportURL))
                    {
                        newModInfo = SpaceportHelper.GetModInfo(SpaceportURL);
                        url = SpaceportURL;
                    }
                    else if (KSPForumHelper.IsValidURL(ForumURL))
                    {
                        newModInfo = KSPForumHelper.GetModInfo(ForumURL);
                        url = ForumURL;
                    }
                    else if (CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(CurseForgeURL)))
                    {
                        newModInfo = CurseForgeHelper.GetModInfo(CurseForgeHelper.GetCurseForgeModURL(CurseForgeURL));
                        url = CurseForgeURL;
                    }
                    if (newModInfo != null)
                    {
                        VersionControl tempVCtrl = ModInfo.VersionControl;
                        ModInfo = newModInfo;
                        VersionControl = tempVCtrl;
                    }
                }
                catch (Exception ex)
                {
                    string msg = string.Format("Error during load of URL \"{0}\"{1}{1}Error message:{1}{2}", url, Environment.NewLine, ex.Message);
                    MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.AddErrorS(msg, ex);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // check for one valid url or users permission.
            if ((SpaceportHelper.IsValidURL(SpaceportURL) && VersionControl == VersionControl.Spaceport) ||
                (KSPForumHelper.IsValidURL(ForumURL) && VersionControl == VersionControl.KSPForum) ||
                (CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(CurseForgeURL)) && VersionControl == VersionControl.CurseForge) ||
                MessageBox.Show(this, "Invalid Spaceport, CurseForge or Forum URL save anyway?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
