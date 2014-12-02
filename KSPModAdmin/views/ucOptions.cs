using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class ucOptions : ucBase
    {
        #region Members

        /// <summary>
        /// The current version of the KSP Mod Admin.
        /// </summary>
        private string mVersion = string.Empty;

        /// <summary>
        /// List of known KSP install paths.
        /// </summary>
        private List<PathInfo> mKnownPaths = new List<PathInfo>();

        /// <summary>
        /// Flag to stop the ksp install folder search thread.
        /// </summary>
        private bool mStopSearch = false;

        /// <summary>
        /// Backgroud worker for path search.
        /// </summary>
        private AsyncTask<List<string>> mAsyncTask = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current version of the KSP Mod Admin.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Version 
        {
            get
            {
                return mVersion;
            }
            set
            {
                mVersion = value;
                lblKSPModAdminVersion.Text = "Current version: " + value;
                llblAdminDownload.Text = String.Format(Constants.DOWNLOAD_FILENAME_TEMPLATE, value);
            }
        }

        /// <summary>
        /// Gets or sets the current version of the KSP Mod Admin.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string AdminVersion { get; set; }

        /// <summary>
        /// Gets or sets the cbVersionCheck checkbox.
        /// Determines wether we should check for updates at starup.
        /// </summary>
        public bool CheckForUpdates { get { return cbVersionCheck.Checked; } set { cbVersionCheck.Checked = value; } }

        /// <summary>
        /// Gets or sets the KSP install folder.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string KSPPath
        {
            get
            {
                return tbKSPPath.Text;
            }
            set
            {
                tbKSPPath.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of known KSP install paths.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), ReadOnly(true)]
        public List<PathInfo> KnownPaths
        {
            get
            {
                if (mKnownPaths == null) 
                    mKnownPaths =  new List<PathInfo>();

                mKnownPaths.Clear();
                foreach (TreeNodeNote note in tvPaths.Nodes)
                {
                    if (note.Text != Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
                        mKnownPaths.Add(new PathInfo(note.Text, note.Note, true)); // TODO: set ImageId.
                }

                return mKnownPaths;
            }
            set
            {
                mKnownPaths = value;
                tvPaths.Nodes.Clear();
                if (mKnownPaths != null)
                    foreach (PathInfo info in mKnownPaths)
                    {
                        if (info.FullPath != Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
                            tvPaths.Nodes.Add(new TreeNodeNote(info.FullPath, info.FullPath, info.Note)); // TODO: set ImageId.
                    }
            }
        }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string SelectedKSPPath
        {
            get
            {
                if (tvPaths.FocusedNode != null)
                    return tvPaths.FocusedNode.Name;
                else
                    return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                foreach (TreeNodeNote node in tvPaths.Nodes)
                {
                    if (value == node.Text)
                    {
                        tvPaths.FocusedNode = node;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the backup path.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string BackupPath
        {
            get
            {
                return tbBackupPath.Text;
            }
            set
            {
                tbBackupPath.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the download and mod path.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        /// <summary>
        /// Gets or sets the cbVersionCheck checkbox.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool EnablePathButtons 
        {
            get
            { 
                return btnBackupPath.Enabled;
            } 
            set
            {
                btnBackupPath.Enabled = value;
                btnDownloadPath.Enabled = value;
                //btnOpenKSPRoot.Enabled = value;
                //btnAddPath.Enabled = value;
                //btnRemove.Enabled = value;
                //btnFolderSearch.Enabled = value;
                //btnSteamSearch.Enabled = value; 
            }
        }

        /// <summary>
        /// The action the should be performed after an update download.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public PostDownloadAction PostDownloadAction
        {
            get
            {
                return (PostDownloadAction)cbPostDownloadAction.SelectedIndex;
            }
            set
            {
                if (value >= PostDownloadAction.Ignore && value <= PostDownloadAction.AutoUpdate)
                    cbPostDownloadAction.SelectedIndex = (int)value;
                else
                    cbPostDownloadAction.SelectedIndex = (int)PostDownloadAction.Ask;
            }
        }

        /// <summary>
        /// The interval of mod updating.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModUpdateInterval ModUpdateInterval
        {
            get
            {
                return (ModUpdateInterval)cbModUpdateInterval.SelectedIndex;
            }
            set
            {
                if (value >= ModUpdateInterval.Manualy && value <= ModUpdateInterval.OnceAWeek)
                    cbModUpdateInterval.SelectedIndex = (int)value;
                else
                    cbModUpdateInterval.SelectedIndex = (int)ModUpdateInterval.Manualy;
            }
        }

        /// <summary>
        /// Date of last mod update check
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime LastModUpdateTry { get; set; }

        /// <summary>
        /// Gets or sets the action that should be performed when the mod will be auto updated.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModUpdateBehavior ModUpdateBehavior
        {
            get
            {
                int index = 0;
                InvokeIfRequired(() => index = cbModUpdateBehavior.SelectedIndex);
                return (ModUpdateBehavior)index;
            }
            set
            {
                InvokeIfRequired(() =>
                                 {
                                     if (value >= ModUpdateBehavior.RemoveAndAdd && value <= ModUpdateBehavior.Manualy)
                                         cbModUpdateBehavior.SelectedIndex = (int) value;
                                     else
                                         cbModUpdateBehavior.SelectedIndex = (int) ModUpdateBehavior.RemoveAndAdd;
                                 });
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShowConflictSolver
        {
            get { return cbShowConflicSolver.Checked; }
            set { cbShowConflicSolver.Checked = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool FolderConflictDetection
        {
            get { return cbFolderConflictDetection.Checked; }
            set { cbFolderConflictDetection.Checked = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ConflictDetection
        {
            get { return cbConflictDetectionOnOff.Checked; }
            set { cbConflictDetectionOnOff.Checked = value; }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where a destination was found.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorDestinationDetected
        {
            get
            {
                return pDestinationDetected.BackColor;
            }
            set
            {
                pDestinationDetected.BackColor = value;
                tbDestinationDetectedRed.Text = value.R.ToString();
                tbDestinationDetectedGreen.Text = value.G.ToString();
                tbDestinationDetectedBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where a destination is missing.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorDestinationMissing
        {
            get
            {
                return pDestinationMissing.BackColor;
            }
            set
            {
                pDestinationMissing.BackColor = value;
                tbDestinationMissingRed.Text = value.R.ToString();
                tbDestinationMissingGreen.Text = value.G.ToString();
                tbDestinationMissingBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where the mod has conflicts with other mods.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorDestinationConflict
        {
            get
            {
                return pDestinationConflict.BackColor;
            }
            set
            {
                pDestinationConflict.BackColor = value;
                tbDestinationConflictRed.Text = value.R.ToString();
                tbDestinationConflictGreen.Text = value.G.ToString();
                tbDestinationConflictBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where is installed.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorModInstalled
        {
            get
            {
                return pModInstalled.BackColor;
            }
            set
            {
                pModInstalled.BackColor = value;
                tbModInstalledRed.Text = value.R.ToString();
                tbModInstalledGreen.Text = value.G.ToString();
                tbModInstalledBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where mod archive missing.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorModArchiveMissing
        {
            get
            {
                return pModArchiveMissing.BackColor;
            }
            set
            {
                pModArchiveMissing.BackColor = value;
                tbModArchiveMissingRed.Text = value.R.ToString();
                tbModArchiveMissingGreen.Text = value.G.ToString();
                tbModArchiveMissingBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes with outdated mods.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color ColorModOutdated
        {
            get
            {
                return pModOutdated.BackColor;
            }
            set
            {
                pModOutdated.BackColor = value;
                tbModOutdatedRed.Text = value.R.ToString();
                tbModOutdatedGreen.Text = value.G.ToString();
                tbModOutdatedBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates if a backup should be made on startup.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool BackupOnStartup
        {
            get
            {
                return cbBackupOnStartup.Checked;
            }
            set
            {
                if (cbBackupOnStartup.Checked != value)
                {
                    cbBackupOnStartup.Checked = value;
                    MainForm.KSPConfig.BackupOnStartup = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates if a backup should be made on KSP launch.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool BackupOnKSPLaunch
        {
            get
            {
                return cbBackupOnLaunch.Checked;
            }
            set
            {
                if (cbBackupOnLaunch.Checked != value)
                {
                    cbBackupOnLaunch.Checked = value;
                    MainForm.KSPConfig.BackupOnKSPLaunch = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates if a backup should be made on certain intervals.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool BackupOnInterval
        {
            get
            {
                return cbOnOff.Checked;
            }
            set
            {
                if (cbOnOff.Checked != value)
                {
                    cbOnOff.Checked = value;
                    MainForm.KSPConfig.BackupOnInterval = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the wait time between 2 backups.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int BackupInterval
        {
            get
            {
                return (int)nudInterval.Value;
            }
            set
            {
                if (nudInterval.Value != value)
                {
                    nudInterval.Value = value;
                    MainForm.KSPConfig.BackupInterval = value;
                }
            }
        }

        /// <summary>
        /// Maximum of backupfiles.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int MaxBackupFiles
        {
            get
            {
                return (int)nudMaxFiles.Value;
            }
            set
            {
                if (nudMaxFiles.Value != value)
                {
                    nudMaxFiles.Value = value;
                    MainForm.KSPConfig.MaxBackupFiles = value;
                }
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the ucUpdate class.
        /// </summary>
        public ucOptions()
        {
            InitializeComponent();

            ModUpdateInterval = ModUpdateInterval.Manualy;
            ModUpdateBehavior = ModUpdateBehavior.CopyDestination;
            LastModUpdateTry = DateTime.MinValue;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Starts an async Job.
        /// Gets the current version from "www.services.mactee.de/..."
        /// and asks to start a download if new version is available.
        /// </summary>
        public void CheckForNewVersion()
        {
            CheckVersion();
        }

        /// <summary>
        /// Asks the user for a KSP Install path with a folder browser dialog .
        /// </summary>
        /// <returns>The selected KSPpath or String.Empty.</returns>
        public string AskForKSPInstallFolder()
        {
            string kspPath = string.Empty;

            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "KSP install folder selection";
            if (dlg.ShowDialog(this.ParentForm.Handle))
            {
                if (MainForm.IsKSPInstallFolder(dlg.FileName))
                    kspPath = dlg.FileName;
                else
                    MessageBox.Show("This is not the KSP install path.");
            }

            return kspPath;
        }

        /// <summary>
        /// Checks the string array for valid KSP install paths and adds them to the known paths.
        /// </summary>
        /// <param name="files">The string array of paths to check.</param>
        public void TryAddPaths(string[] files)
        {
            foreach (string file in files)
            {
                bool isKSPDir = false;
                try
                {
                    string dir = file;
                    if (file.EndsWith(Constants.KSP_EXE) || file.EndsWith(Constants.KSP_X64_EXE))
                        dir = Path.GetDirectoryName(file);
                    if (MainForm.IsKSPInstallFolder(dir))
                        isKSPDir = true;
                    if (dir.ToLower().Contains("recycle.bin"))
                        isKSPDir = false;
                }
                catch (Exception ex)
                {
                    string NoWarningPLS = ex.Message;
                }

                if (isKSPDir)
                    AddKSPPath(file);
                else
                    MainForm.AddInfo("The dropped directory is not a KSP install directory. \"" + file + "\"");
            }
        }

        /// <summary>
        /// Starts the search for KSP folders in an extra thread.
        /// </summary>
        /// <param name="searchDepth">The depth to search in the folder structur (up and down).</param>
        /// <param name="finishedCallback">The callback function which should be called after finish ksp install folder search.</param>
        public void SearchFolderStruct(int searchDepth, AsyncResultHandler<List<string>> finishedCallback = null)
        {
            SearchNormal(searchDepth, finishedCallback);
        }

        /// <summary>
        /// Starts the search for Steam KSP folder in an extra thread.
        /// </summary>
        /// <param name="finishedCallback">The callback function which should be called after finish ksp install folder search.</param>
        public void SearchSteamVersion(AsyncResultHandler<List<string>> finishedCallback = null)
        {
            SteamSearch(finishedCallback);
        }

        /// <summary>
        /// Stops the search of ksp install folder.
        /// </summary>
        public void StopSearch()
        {
            mStopSearch = true;
        }

        /// <summary>
        /// Check if the auto mod update is active and starts the modInfo update process.
        /// </summary>
        /// <param name="isStartup">Flag to determine if the call of this function is aon app startup.</param>
        public void AutoModUpdateCheck(bool isStartup = false, bool silent = false)
        {
            switch (ModUpdateInterval)
            {
                case ModUpdateInterval.OnStartup:
                    if (isStartup)
                        Check4ModUpdats(MainForm.ModSelection.Nodes, silent);
                    break;
                case ModUpdateInterval.OnceADay:
                    if (LastModUpdateTry.AddHours(24) < DateTime.Now)
                        Check4ModUpdats(MainForm.ModSelection.Nodes, silent);
                    break;
                case ModUpdateInterval.EveryTwoDays:
                    if (LastModUpdateTry.AddHours(48) < DateTime.Now)
                        Check4ModUpdats(MainForm.ModSelection.Nodes, silent);
                    break;
                case ModUpdateInterval.OnceAWeek:
                    if (LastModUpdateTry.AddDays(7) < DateTime.Now)
                        Check4ModUpdats(MainForm.ModSelection.Nodes, silent);
                    break;
            }
        }

        /// <summary>
        /// Updates the ModInfo
        /// </summary>
        public void Check4ModUpdats(TreeNodeMod[] mods, bool silent = false)
        {
            if (mods == null || mods.Length <= 0) return;

            MainForm.AddInfo("Update check for mods started ...");

            MainForm.ModSelection.EnableControls = false;
            MainForm.ModBrowser.Enabled = false;
            MainForm.Parts.Enabled = false;
            MainForm.Crafts.Enabled = false;
            MainForm.Flags.Enabled = false;
            btnCheckModUpdates.Enabled = false;
            btnCheckModUpdates.Text = Constants.TXT_BTN_UPDATE_UPDATING;
            pbUpdateLoad.Visible = true;
            pbUp2Date.Visible = false;
            cbPostDownloadAction.Enabled = false;
            cbModUpdateInterval.Enabled = false;
            cbModUpdateBehavior.Enabled = false;
            btnUpdate.Enabled = false;
            AsyncTask<bool>.DoWork(
                delegate()
                {
                    bool result = false;
                    foreach (TreeNodeMod mod in mods)
                    {
                        MainForm.AddInfo(string.Format("\"{0}\" checking ...", mod.Text));
                        ModInfo modInfo = null;
                        if (mod.VersionControl == VersionControl.Spaceport && SpaceportHelper.IsValidURL(mod.SpaceportURL))
                            modInfo = SpaceportHelper.GetModInfo(mod.SpaceportURL);
                        else if (mod.VersionControl == VersionControl.KSPForum && KSPForumHelper.IsValidURL(mod.ForumURL))
                            modInfo = KSPForumHelper.GetModInfo(mod.ForumURL);
                        else if (mod.VersionControl == VersionControl.CurseForge && CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(mod.CurseForgeURL)))
                            modInfo = CurseForgeHelper.GetModInfo(CurseForgeHelper.GetCurseForgeModURL(mod.CurseForgeURL));

                        if (modInfo != null)
                        {
                            modInfo.LocalPath = string.Empty;

                            DateTime oldDate = DateTime.MinValue;
                            DateTime newDate = DateTime.MinValue;
                            if (!DateTime.TryParse(mod.AddDate, out oldDate))
                                oldDate = DateTime.MinValue;

                            if (!DateTime.TryParse(modInfo.CreationDate, out newDate))
                                newDate = DateTime.MinValue;

                            bool updateAvailable = false;
                            if (oldDate < newDate)
                                updateAvailable = true;
                            else
                            {
                                if (!DateTime.TryParse(mod.CreationDate, out oldDate))
                                    continue;

                                if (oldDate < newDate)
                                    updateAvailable = true;
                            }

                            if (updateAvailable)
                            {
                                MainForm.AddInfo(string.Format("\"{0}\" is outdated", mod.Text));
                                result = true;
                            }
                            else
                            {
                                MainForm.AddInfo(string.Format("\"{0}\" is up to date.", mod.Text));
                            }

                            mod.IsOutdated = updateAvailable;
                            //mod.CreationDate = modInfo.CreationDate;
                            mod.Rating = modInfo.Rating;
                            mod.Downloads = modInfo.Downloads;
                            mod.SpaceportURL = modInfo.SpaceportURL;
                            mod.ForumURL = modInfo.ForumURL;
                            mod.CurseForgeURL = modInfo.CurseForgeURL;
                            mod.Author = modInfo.Author;
                        }
                        else
                            MainForm.AddInfo(string.Format("\"{0}\" has no valid Spaceport or Forum URL", mod.Text));
                    }

                    return result;
                },
                delegate(bool result, Exception ex)
                {
                    MainForm.ModSelection.EnableControls = true;
                    MainForm.ModBrowser.Enabled = true;
                    MainForm.Parts.Enabled = true;
                    MainForm.Crafts.Enabled = true;
                    MainForm.Flags.Enabled = true;
                    btnCheckModUpdates.Enabled = true;
                    btnCheckModUpdates.Text = Constants.TXT_BTN_UPDATE_MODUPDATE;
                    pbUpdateLoad.Visible = false;
                    cbPostDownloadAction.Enabled = true;
                    cbModUpdateInterval.Enabled = true;
                    cbModUpdateBehavior.Enabled = true;
                    btnUpdate.Enabled = true;

                    if (ex != null)
                    {
                        MessageBox.Show(this, ex.Message);
                        MainForm.AddError("Error during update check.", ex);
                    }
                    else
                    {
                        if (result && !silent)
                        {
                            string msg = "One or more mods are outdated.";
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            if (MainForm.tabControl1.SelectedTab != MainForm.tabPageMods)
                            {
                                msg += "\n\rSwitch to ModSelection?";
                                buttons = MessageBoxButtons.YesNo;
                            }

                            if (MessageBox.Show(MainForm, msg, "Update Info", buttons) == DialogResult.Yes)
                            { 
                                MainForm.tabControl1.SelectedTab = MainForm.tabPageMods;
                                MainForm.tabControl1.Refresh();
                            }
                        }

                        LastModUpdateTry = DateTime.Now;
                    }

                    MainForm.AddInfo("Update check done.");
                });
        }

        /// <summary>
        /// Returns the download path, if its empty the user will be asked to select one.
        /// </summary>
        /// <returns>The download path (could be empty!).</returns>
        public string GetValidDownloadPath(bool forceUserSelect = false)
        {
            if (string.IsNullOrEmpty(DownloadPath) || forceUserSelect)
            {
                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(pathUser, "Downloads");

                FolderSelectDialog dlg = new FolderSelectDialog();
                dlg.Title = "Mod download folder selection";
                dlg.InitialDirectory = pathDownload;

                if (!dlg.ShowDialog(this.ParentForm.Handle))
                    return string.Empty;

                DownloadPath = dlg.FileName;
                MainForm.AddInfo(string.Format("Download path changed to \"{0}\".", dlg.FileName));
            }

            return DownloadPath;
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucOptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOptions_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                cbPostDownloadAction.SelectedIndex = 0;
                return;
            }

            tvPaths.AllowDrop = true;
            tvPaths.AddActionKey(VirtualKey.VK_DELETE, RemovePath, null, true);
            tvPaths.AddActionKey(VirtualKey.VK_BACK, RemovePath, null, true);
            tvPaths.AddActionKey(VirtualKey.VK_V, PastePath, new ModifierKey[] { ModifierKey.AnyControl }, true);

            tvPaths.AddColumn("name", "Path", 280, 280, false, true);
            tvPaths.AddColumn("note", "Note", 100, 100);
        }

        /// <summary>
        /// Handels the Click event of the btnUpdate.
        /// Gets the current version from "www.services.mactee.de/..."
        /// and asks to start a download if new version is available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckVersion();
        }

        /// <summary>
        /// Handels the Click event of the btnCheckModUpdates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckModUpdates_Click(object sender, EventArgs e)
        {
            Check4ModUpdats(MainForm.ModSelection.Nodes);
        }

        /// <summary>
        /// Handels the Click event of the btnOpenDownloads.
        /// Opens the downloads folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDownloads_Click(object sender, EventArgs e)
        {
            OpenFolder(DownloadPath);
        }

        /// <summary>
        /// Handels the Click event of the btnBackupPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackupPath_Click(object sender, EventArgs e)
        {
            if (cbKSPPath.SelectedText == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
            {
                MessageBox.Show(ParentForm, Constants.MESSAGE_SELECT_KSP_FOLDER_FIRST);
                return;
            }

            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "Backup folder selection";
            if (dlg.ShowDialog(this.ParentForm.Handle))
            {
                BackupPath = dlg.FileName;
                MainForm.Backup.BackupPath = dlg.FileName;
                MainForm.AddInfo(string.Format("Backup path changed to \"{0}\"", BackupPath));
            }
        }

        /// <summary>
        /// Handels the Click event of the btnOpenBackupFolder.
        /// Opens the backup folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBackupFolder_Click(object sender, EventArgs e)
        {
            OpenFolder(BackupPath);
        }

        /// <summary>
        /// Handels the Click event of the btnDownloadPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadPath_Click(object sender, EventArgs e)
        {
            if (cbKSPPath.SelectedText == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
            {
                MessageBox.Show(ParentForm, Constants.MESSAGE_SELECT_KSP_FOLDER_FIRST);
                return;
            }

            GetValidDownloadPath(true);
            tbDownloadPath.Text = DownloadPath;
        }
        
        /// <summary>
        /// Handels the Click event of the btnOpenKSPRoot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenKSPRoot_Click(object sender, EventArgs e)
        {
            if (cbKSPPath.SelectedText == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
            {
                MessageBox.Show(ParentForm, Constants.MESSAGE_SELECT_KSP_FOLDER_FIRST);
                return;
            }

            string fullpath = KSPPath;
            try
            {
                if (Directory.Exists(fullpath))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fullpath;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("Open KSP install folder faild.", ex);
            }
        }

        /// <summary>
        /// Handels the Click event of the btnAddPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPath_Click(object sender, EventArgs e)
        {
            OpenFolderBrowser();
        }

        /// <summary>
        /// Handels the Click event of the btnRemove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemovePath();
        }

        /// <summary>
        /// Handels the Click event of the btnSteamSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSteamSearch_Click(object sender, EventArgs e)
        {
            if (btnSteamSearch.Tag != null && (string)btnSteamSearch.Tag == "Stop")
                StopSearch();
            else
                SteamSearch();
        }

        /// <summary>
        /// Handels the Click event of the btnFolderSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolderSearch_Click(object sender, EventArgs e)
        {
            if (btnFolderSearch.Tag != null && (string)btnFolderSearch.Tag == "Stop")
                StopSearch();
            else
            {
                int searchDepth = 3;
                if (!string.IsNullOrEmpty(tbDepth.Text) && !int.TryParse(tbDepth.Text, out searchDepth))
                    tbDepth.Text = "3";

                SearchNormal(searchDepth);
            }
        }

        /// <summary>
        /// Handles the LinkClick event of the llblAdminDownload.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblAdminDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadNewAdminVersion();
        }

        /// <summary>
        /// Handles the TextChanged event of the tbDepth.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDepth_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (!int.TryParse(tbDepth.Text, out i))
                tbDepth.Text = "3";
        }

        /// <summary>
        /// Handels the MouseDown event of the cbKSPPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKSPPath_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbKSPPath.Items.Count == 1 && ((string)cbKSPPath.Items[0]) == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
                MessageBox.Show(this, "Please add a KSP install path below.");
        }

        /// <summary>
        /// Handels the SelectedIndexChanged event of the cbKSPPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKSPPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKSPPath.SelectedIndex > -1)
                MainForm.KSPPath = cbKSPPath.Items[cbKSPPath.SelectedIndex].ToString();
        }

        /// <summary>
        /// Handles the DragEnter event of the tvPaths.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPaths_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) || e.Data.GetDataPresent(DataFormats.Text, false))
                e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// Handles the DragDrop event of the tvPaths.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPaths_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            else if (e.Data.GetDataPresent(DataFormats.Text, false))
                paths = new string[] { e.Data.GetData(DataFormats.Text).ToString() };

            if (paths != null)
                TryAddPaths(paths);
        }

        /// <summary>
        /// Handels the Value- or CheckedChanged event of all backup settings relevant controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupSettings_ValueChanged(object sender, EventArgs e)
        {
            if ((sender.GetType().Name == cbOnOff.GetType().Name && ((CheckBox)sender).Name == cbOnOff.Name) ||
                (sender.GetType().Name == nudInterval.GetType().Name && ((NumericUpDown)sender).Name == nudInterval.Name && cbOnOff.Checked))
                MainForm.Backup.ToggleAutoBackupOnOff(cbOnOff.Checked);

            if ((sender.GetType().Name == cbOnOff.GetType().Name && ((CheckBox)sender).Name == cbOnOff.Name))
                MainForm.KSPConfig.BackupOnInterval = cbOnOff.Checked;
            if ((sender.GetType().Name == cbBackupOnLaunch.GetType().Name && ((CheckBox)sender).Name == cbBackupOnLaunch.Name))
                MainForm.KSPConfig.BackupOnKSPLaunch = cbBackupOnLaunch.Checked;
            if ((sender.GetType().Name == cbBackupOnStartup.GetType().Name && ((CheckBox)sender).Name == cbBackupOnStartup.Name))
                MainForm.KSPConfig.BackupOnStartup = cbBackupOnStartup.Checked;
            if ((sender.GetType().Name == nudInterval.GetType().Name && ((NumericUpDown)sender).Name == nudInterval.Name))
                MainForm.KSPConfig.BackupInterval = (int)nudInterval.Value;
            if ((sender.GetType().Name == nudMaxFiles.GetType().Name && ((NumericUpDown)sender).Name == nudMaxFiles.Name))
                MainForm.KSPConfig.MaxBackupFiles = (int)nudMaxFiles.Value;
        }

        private void tbDestinationDetected_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox) sender).Text))
                ((TextBox) sender).Text = "0";

            if (((TextBox)sender).Name.Contains("DestinationDetected"))
            {
                pDestinationDetected.BackColor = Color.FromArgb(255, int.Parse(tbDestinationDetectedRed.Text),
                    int.Parse(tbDestinationDetectedGreen.Text), int.Parse(tbDestinationDetectedBlue.Text));
            }
            else if (((TextBox)sender).Name.Contains("DestinationMissing"))
            {
                pDestinationMissing.BackColor = Color.FromArgb(255, int.Parse(tbDestinationMissingRed.Text),
                    int.Parse(tbDestinationMissingGreen.Text), int.Parse(tbDestinationMissingBlue.Text));
            }
            else if (((TextBox)sender).Name.Contains("DestinationConflict"))
            {
                pDestinationConflict.BackColor = Color.FromArgb(255, int.Parse(tbDestinationConflictRed.Text),
                    int.Parse(tbDestinationConflictGreen.Text), int.Parse(tbDestinationConflictBlue.Text));
            }
            else if (((TextBox)sender).Name.Contains("ModInstalled"))
            {
                pModInstalled.BackColor = Color.FromArgb(255, int.Parse(tbModInstalledRed.Text),
                    int.Parse(tbModInstalledGreen.Text), int.Parse(tbModInstalledBlue.Text));
            }
            else if (((TextBox)sender).Name.Contains("ModArchiveMissing"))
            {
                pModArchiveMissing.BackColor = Color.FromArgb(255, int.Parse(tbModArchiveMissingRed.Text),
                    int.Parse(tbModArchiveMissingGreen.Text), int.Parse(tbModArchiveMissingBlue.Text));
            }
            else if (((TextBox)sender).Name.Contains("ModOutdated"))
            {
                pModOutdated.BackColor = Color.FromArgb(255, int.Parse(tbModOutdatedRed.Text),
                    int.Parse(tbModOutdatedGreen.Text), int.Parse(tbModOutdatedBlue.Text));
            }

            MainForm.ModSelection.tvModSelection.Invalidate();
        }

        private void btnDestinationDetected_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (((Button)sender).Name.Contains("DestinationDetected"))
            {
                dlg.Color = pDestinationDetected.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationDetected.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains("DestinationMissing"))
            {
                dlg.Color = pDestinationMissing.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationMissing.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains("DestinationConflict"))
            {
                dlg.Color = pDestinationConflict.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationConflict.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains("ModInstalled"))
            {
                dlg.Color = pModInstalled.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModInstalled.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains("ModArchiveMissing"))
            {
                dlg.Color = pModArchiveMissing.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModArchiveMissing.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains("ModOutdated"))
            {
                dlg.Color = pModOutdated.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModOutdated.BackColor = dlg.Color;
            }

            MainForm.ModSelection.tvModSelection.Invalidate();
        }

        private void cbConflictDetectionOnOff_CheckedChanged(object sender, EventArgs e)
        {
            cbShowConflicSolver.Enabled = cbConflictDetectionOnOff.Checked;
            cbFolderConflictDetection.Enabled = cbConflictDetectionOnOff.Checked;
            if (cbConflictDetectionOnOff.Checked)
            {
                bool conflicts = false;
                foreach (TreeNodeMod mod in MainForm.ModSelection.Nodes)
                {
                    if (!ModRegister.RegisterMod(mod))
                        conflicts = true;
                }

                if (conflicts)
                {
                    // TODO: show solver conflict dialog
                }
            }
            else
            {
                ModRegister.Clear();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Starts an async Job.
        /// Gets the current version from "www.services.mactee.de/..."
        /// and asks to start a download if new version is available.
        /// </summary>
        private void CheckVersion()
        {
            btnUpdate.Enabled = false;
            btnUpdate.Text = Constants.TXT_BTN_UPDATE_UPDATING;
            pbUpdateLoad.Visible = true;
            pbUp2Date.Visible = false;
            cbPostDownloadAction.Enabled = false;
            cbModUpdateInterval.Enabled = false;
            btnCheckModUpdates.Enabled = false;
            AsyncTask<WebResponse>.DoWork(
                delegate()
                {
                    WebRequest request = WebRequest.Create(Constants.SERVICE_ADMIN_VERSION);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    return request.GetResponse();
                },
                delegate(WebResponse response, Exception ex)
                {
                    btnUpdate.Enabled = true;
                    btnUpdate.Text = Constants.TXT_BTN_UPDATE_UPDATE;
                    pbUpdateLoad.Visible = false;
                    cbPostDownloadAction.Enabled = true;
                    cbModUpdateInterval.Enabled = true;
                    btnCheckModUpdates.Enabled = true;

                    if (ex != null)
                        MessageBox.Show(this, ex.Message);
                    else
                    {
                        string status = ((HttpWebResponse)response).StatusDescription;
                        Stream dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        Dictionary<string, string> parameter = ToParameterDic(responseFromServer);

                        if (!parameter.ContainsKey(Constants.VERSION)) 
                            return;

                        Version oldVersion = new Version(AdminVersion);
                        Version newVersion = new Version(parameter[Constants.VERSION]);
                        if (oldVersion < newVersion)
                        {
                            llblAdminDownload.Text = String.Format(Constants.DOWNLOAD_FILENAME_TEMPLATE, parameter[Constants.VERSION]);
                            frmUpdateDLG updateDLG = new frmUpdateDLG();
                            updateDLG.MainForm = MainForm;
                            updateDLG.DownloadPath = DownloadPath;
                            updateDLG.PostDownloadAction = PostDownloadAction;
                            updateDLG.Message = GetDownloadMSG(parameter);
                            
                            if (updateDLG.ShowDialog(this) != DialogResult.OK) 
                                return;

                            DownloadPath = updateDLG.DownloadPath;
                            PostDownloadAction = updateDLG.PostDownloadAction;
                            DownloadNewAdminVersion();
                        }
                        else
                            pbUp2Date.Visible = true;
                    }
                });
        }

        /// <summary>
        /// Converts a string (e.g. : "Version=1.0.1;TEST=123123") to a dictionary.
        /// </summary>
        /// <param name="parameterString">A string with ; seperated parameters.</param>
        /// <returns>A dictionary of parameternames and values.</returns>
        private Dictionary<string, string> ToParameterDic(string parameterString)
        {
            parameterString = parameterString.Replace(Environment.NewLine, string.Empty);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] temp = parameterString.Split(';');
            foreach (string entry in temp)
            {
                if (entry == string.Empty) continue;

                string[] keyValue = entry.Split('=');
                dic.Add(keyValue[0], keyValue[1]);
            }

            return dic;
        }

        /// <summary>
        /// Creates the displaymessage for the download / update dialog.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string GetDownloadMSG(Dictionary<string, string> parameter)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(Constants.MESSAGE_NEW_VERSION, parameter[Constants.VERSION]);
            sb.AppendLine();
            sb.AppendLine();
            if (parameter.ContainsKey(Constants.MESSAGE) && parameter[Constants.MESSAGE] != String.Empty)
            {
                string[] lines = parameter[Constants.MESSAGE].Split('#');
                foreach (string line in lines)
                {
                    string tempLine = line.Trim();
                    if (tempLine != string.Empty)
                        sb.AppendLine(tempLine);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Opens a FolderBrowser to select a Download destination. 
        /// Starts a AsyncJob to download the new version.
        /// </summary>
        private void DownloadNewAdminVersion()
        {
            // get valid download path.
            MainForm.Options.GetValidDownloadPath();
            if (!string.IsNullOrEmpty(MainForm.Options.DownloadPath) && Directory.Exists(MainForm.Options.DownloadPath))
            {
                string filename = llblAdminDownload.Text;
                string url = Constants.SERVICE_DOWNLOAD_LINK;

                prgBarAdminDownload.Visible = true;
                llblAdminDownload.Visible = false;

                int index = 1;
                string downloadDest = Path.Combine(MainForm.Options.DownloadPath, filename);
                while (File.Exists(downloadDest))
                {
                    string temp = Path.GetFileNameWithoutExtension(downloadDest).Replace("_(" + index++ + ")", "");
                    string newFilename = String.Format("{0}_({1}){2}", temp, index, Path.GetExtension(downloadDest));
                    downloadDest = Path.Combine(Path.GetDirectoryName(downloadDest), newFilename);
                }

                AsyncTask<bool>.RunDownload(url, downloadDest,
                                            delegate(bool result, Exception ex)
                                            {
                                                llblAdminDownload.Visible = true;
                                                prgBarAdminDownload.Visible = false;
                                                prgBarAdminDownload.Value = 0;

                                                if (ex != null)
                                                    MessageBox.Show(this, ex.Message);
                                                else
                                                {
                                                    switch (PostDownloadAction)
                                                    {
                                                        case PostDownloadAction.Ask:
                                                            if (MessageBox.Show(MainForm, "The download is complete.\n\rDo you want to auto install the update?", "Download complete.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                                AutoUpdateKSPMA(downloadDest);
                                                            break;
                                                        case PostDownloadAction.AutoUpdate:
                                                            AutoUpdateKSPMA(downloadDest);
                                                            break;
                                                        default: // case Views.PostDownloadAction.Ignore:
                                                            break;
                                                    }
                                                }
                                            },
                                            delegate(int progressPercentage)
                                            {
                                                prgBarAdminDownload.Value = progressPercentage;
                                            });
            }
        }

        /// <summary>
        /// Starts the KSPModAdmin_Updater in another process.
        /// </summary>
        /// <param name="archivePath">The path to the new KSPModAdmin archive.</param>
        private void AutoUpdateKSPMA(string archivePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "KSPModAdmin_Updater.exe");
            process.StartInfo.Arguments = string.Format("version={0} process={1} archive={2} dest={3}", 
                                                        GetVersionFromArchive(archivePath), 
                                                        "KSPModAdmin", 
                                                        archivePath,
                                                        Path.GetDirectoryName(Application.ExecutablePath));
            process.Start();

            MainForm.Close();
        }

        /// <summary>
        /// Extracts the version from the fullpath of the KSPModAdmin archive.
        /// </summary>
        /// <param name="archivePath">The path to the KSPModAdmin archive.</param>
        /// <returns>The version of the KSPModAdmin archive.</returns>
        private string GetVersionFromArchive(string archivePath)
        {
            string version = Path.GetFileNameWithoutExtension(archivePath);
            int index = version.IndexOf("_");
            if (index > 0)
            {
                version = version.Substring(version.IndexOf("-v") + 2);
                index = version.IndexOf("_");
                version = version.Substring(0, index);
            }
            else
                version = version.Substring(version.IndexOf("-v") + 2);

            return version;
        }

        #endregion

        #region Mod update

        #endregion

        #region Path

        /// <summary>
        /// Callback of the ctrl + v key (paste) event.
        /// Checks if data in clipboard is a valid ksp install path and adds it to the known paths.
        /// </summary>
        /// <param name="actionKeyInfo">ActionKey event information.</param>
        /// <returns>Ture if event was handled.</returns>
        private bool PastePath(ActionKeyInfo actionKeyInfo)
        {
            string[] path = null;
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                string str = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();
                if (str.Contains(Environment.NewLine))
                    path = str.Replace("\r", "").Split('\n');
                else
                    path = new string[] { str };
            }
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.FileDrop))
                path = (string[])Clipboard.GetDataObject().GetData(DataFormats.FileDrop);

            if (path != null)
                TryAddPaths(path);
            else
                MainForm.AddInfo("No paste info found!");

            return true;
        }

        /// <summary>
        /// Adds a KSP path to the known KSP paths.
        /// </summary>
        /// <param name="kspPath">The full path to add.</param>
        private void AddKSPPath(string kspPath)
        {
            if (kspPath == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX ||
                !MainForm.IsKSPInstallFolder(kspPath))
                return;

            bool found = false;
            foreach (Utils.CommonTools.Node node in tvPaths.Nodes)
            {
                if (node.Text == kspPath)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                PathInfo info = new PathInfo();
                info.FullPath = kspPath;
                info.Note = string.Empty;
                info.ValidPath = true;
                mKnownPaths.Add(info);

                TreeNodeNote newNode = new TreeNodeNote(kspPath, kspPath, string.Empty);
                tvPaths.Nodes.Add(newNode);
                tvPaths.FocusedNode = newNode;
                MainForm.KSPPath = newNode.Text;
            }
        }

        /// <summary>
        /// Removes the selected path from the list.
        /// </summary>
        private bool RemovePath(ActionKeyInfo actionKeyInfo)
        {
            RemovePath();
            return true;
        }

        /// <summary>
        /// Removes the selected path from the list.
        /// </summary>
        private void RemovePath()
        {
            if (tvPaths.FocusedNode != null)
            {
                PathInfo info2Del = null;
                foreach (PathInfo info in mKnownPaths)
                {
                    if (info.FullPath == tvPaths.FocusedNode.Text)
                    {
                        info2Del = info;
                        break;
                    }
                }

                if (info2Del != null)
                    mKnownPaths.Remove(info2Del);

                tvPaths.Nodes.Remove(tvPaths.FocusedNode);

                if (tvPaths.Nodes.Count <= 0)
                    MainForm.UnloadAll();
                else
                    tvPaths.FocusedNode = tvPaths.Nodes[tvPaths.Nodes.Count - 1];
            }
        }

        /// <summary>
        /// Opens a folder browser that the user can select the KSP install folder.
        /// </summary>
        private void OpenFolderBrowser()
        {
            string kspPath = AskForKSPInstallFolder();
            if (!string.IsNullOrEmpty(kspPath))
                AddKSPPath(kspPath);
        }

        /// <summary>
        /// Starts the normal search for the KSP install folder.
        /// </summary>
        private void SearchNormal(int searchDepth, AsyncResultHandler<List<string>> finishedCallback = null)
        {
            tlpSearchBG.Visible = true;
            btnAddPath.Enabled = false;
            btnRemove.Enabled = false;
            //btnFolderSearch.Enabled = false;
            btnFolderSearch.Tag = "Stop";
            btnFolderSearch.Image = global::KSPModAdmin.Properties.Resources.stop;
            string oldTT = ttUpdateTab.GetToolTip(btnFolderSearch);
            ttUpdateTab.SetToolTip(btnFolderSearch, "Stop search.");
            btnSteamSearch.Enabled = false;
            tbDepth.Enabled = false;

            mStopSearch = false;

            mAsyncTask = new AsyncTask<List<string>>();
            mAsyncTask.SetCallbackFunctions(
                delegate()
                {
                    string adminDir = Path.GetDirectoryName(Application.ExecutablePath);

                    int depth = 1;
                    bool done = false;
                    DirectoryInfo dirInfo = Directory.GetParent(adminDir);
                    while ((depth <= searchDepth || 0 == searchDepth) && !done && !mStopSearch)
                    {
                        if (dirInfo == null || dirInfo.Root.FullName == dirInfo.FullName)
                        {
                            done = true;
                            break;
                        }

                        dirInfo = dirInfo.Parent;
                        ++depth;
                    }

                    List<string> list = new List<string>();
                    if (dirInfo != null)
                        SearchSubDirs(dirInfo.FullName, searchDepth * 2, ref list, (finishedCallback != null));

                    return list;
                },
                delegate(List<string> result, Exception ex)
                {
                    TryAddPaths(result.ToArray());

                    tlpSearchBG.Visible = false;
                    btnAddPath.Enabled = true;
                    btnRemove.Enabled = true;
                    //btnFolderSearch.Enabled = true;
                    btnFolderSearch.Tag = "Start";
                    btnFolderSearch.Image = global::KSPModAdmin.Properties.Resources.folder_view;
                    ttUpdateTab.SetToolTip(btnFolderSearch, oldTT);
                    btnSteamSearch.Enabled = true;
                    tbDepth.Enabled = true;

                    if (finishedCallback != null)
                        finishedCallback(result, ex);
                }, null, true);
            mAsyncTask.Run();
        }
        
        /// <summary>
        /// Starts the steam search for the KSP install folder.
        /// </summary>
        private void SteamSearch(AsyncResultHandler<List<string>> finishedCallback = null)
        {
            tlpSearchBG.Visible = true;
            btnAddPath.Enabled = false;
            btnRemove.Enabled = false;
            btnFolderSearch.Enabled = false;
            //btnSteamSearch.Enabled = false;
            btnSteamSearch.Tag = "Stop";
            btnSteamSearch.Image = global::KSPModAdmin.Properties.Resources.stop;
            tbDepth.Enabled = false;

            mAsyncTask = new AsyncTask<List<string>>();
            mAsyncTask.SetCallbackFunctions(
                delegate()
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                    string[] paths = Directory.GetDirectories(path, "Steam", SearchOption.TopDirectoryOnly);
                    if (paths.Length == 0)
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                        paths = Directory.GetDirectories(path, "Steam", SearchOption.TopDirectoryOnly);
                    }

                    List<string> list = new List<string>();
                    foreach (string dir in paths)
                    {
                        if (mStopSearch || !SearchSubDirs(dir, 2, ref list, (finishedCallback != null)))
                            break;
                    }

                    return list;
                },
                delegate(List<string> result, Exception ex)
                {
                    TryAddPaths(result.ToArray());

                    tlpSearchBG.Visible = false;
                    btnAddPath.Enabled = true;
                    btnRemove.Enabled = true;
                    btnFolderSearch.Enabled = true;
                    //btnSteamSearch.Enabled = true;
                    btnSteamSearch.Tag = "Start";
                    btnSteamSearch.Image = global::KSPModAdmin.Properties.Resources.folder_tool;
                    tbDepth.Enabled = true;

                    if (finishedCallback != null)
                        finishedCallback(result, ex);
                }, null, true);
            mAsyncTask.Run();
        }

        /// <summary>
        /// Searches the sub directoies for the KSP install folder.
        /// </summary>
        /// <param name="dirName">The directory to check the subdirectories from.</param>
        /// <param name="depth">The current depth of the sub folder.</param>
        /// <returns></returns>
        private bool SearchSubDirs(string dirName, int searchDepth, ref List<string> foundKSPDirs, bool stopOnFirstHit = false, int depth = 1)
        {
            if ((searchDepth != 0 && depth + 1 > searchDepth) || mStopSearch)
                return true;

            try
            {
                bool continueSearch = true;
                string[] subDirs = Directory.GetDirectories(dirName);
                foreach (string dir in subDirs)
                {
                    if (mStopSearch) 
                        break;

                    if (MainForm.IsKSPInstallFolder(dir) && !dir.ToLower().Contains("recycle.bin"))
                    {
                        switch (AskUser(dir))
                        {
                            case DialogResult.Yes:
                                foundKSPDirs.Add(dir);
                                continueSearch = !stopOnFirstHit;
                                break;
                            case DialogResult.No:
                                continueSearch = true;
                                break;
                            case DialogResult.Cancel:
                                continueSearch = false;
                                break;
                        };

                        if (!continueSearch)
                            return continueSearch;
                    }

                    if (!continueSearch)
                        return continueSearch;
                    else
                        if (!SearchSubDirs(dir, searchDepth, ref foundKSPDirs, stopOnFirstHit, depth + 1))
                            return false;
                }
            }
            catch (Exception ex)
            {
                // ignore directories where we aren't authorized for.
                string NoWarningPLS = ex.Message;
            }

            return true;
        }

        /// <summary>
        /// Opens a MessageBox to ask the user if he wants to take the found KSP folder or continue the search.
        /// </summary>
        /// <param name="dir">The found KSP install direcory.</param>
        /// <returns>True if the User wants to take the directory otherwise false.</returns>
        private DialogResult AskUser(string dir)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KSP folder found.").AppendLine().AppendLine(string.Format("\"{0}\"", dir)).AppendLine().AppendLine();
            sb.AppendLine("Should this folder be added (yes)?").AppendLine("continue search (no)?").AppendLine("or cancel search(cancel)?");

            DialogResult result = DialogResult.Cancel;
            InvokeIfRequired(() => result = MessageBox.Show(ParentForm, sb.ToString(), "KSP folder found.", MessageBoxButtons.YesNoCancel));

            return result;
        }

        #endregion

        private void OpenFolder(string fullpath)
        {
            try
            {
                if (Directory.Exists(fullpath))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = fullpath;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("Open folder faild.", ex);
            }
        }

        #endregion
    }
}
