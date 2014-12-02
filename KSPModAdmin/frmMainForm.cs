using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using KSPModAdmin.Config;
using KSPModAdmin.Utils;
using KSPModAdmin.Views;
using System.Collections.Generic;
using System.Text;

namespace KSPModAdmin
{
    /// <summary>
    /// Main Form of the KSP MOD Admin.
    /// </summary>
    public partial class MainForm : frmBase
    {
        #region Members

        static MainForm mMainForm = null;

        /// <summary>
        /// Current Version (x.x.x).
        /// </summary>
        string mVersion = string.Empty;

        /// <summary>
        /// Current Version (x.x.x.x).
        /// </summary>
        string mAdminVersion = string.Empty;

        /// <summary>
        /// Min splitter distance from the bottom.
        /// </summary>
        int mMinSplitterDistance = 55;

        /// <summary>
        /// Min splitter distance from the top.
        /// </summary>
        int mMaxSplitterDistance = 250;

        /// <summary>
        /// Config of the MODAdmin.
        /// </summary>
        AdminConfig mAppConfig = null;

        /// <summary>
        /// Config of the selected KSP installation.
        /// </summary>
        KSPConfig mKSPConfig = null;

        /// <summary>
        /// A instance of the search dialog.
        /// </summary>
        frmSearchDLG m_SearchDLG = null;

        bool mIgnoreKSPPath = false;

        #endregion

        #region Properties

        public static MainForm Instance
        {
            get { return mMainForm; }
        }

        //public AdminConfig AppConfig { get { return mAppConfig; } }

        public KSPConfig KSPConfig { get { return mKSPConfig; } }

        public ucWelcome Welcome { get { return ucWelcome1; } }

        public ucModSelection ModSelection { get { return ucModSelection1; } }

        public ucModBrowser ModBrowser { get { return ucModBrowser1; } }

        public ucParts Parts { get { return ucParts1; } }

        public ucCrafts Crafts { get { return ucCrafts1; } }

        public ucFlags Flags { get { return ucFlags1; } }

        public ucBackup Backup { get { return ucBackup1; } }

        public ucOptions Options { get { return ucOptions1; } }

        public ucHelp Help { get { return ucHelp1; } }

        public string KSPPath 
        {
            get
            { 
                return cbKSPPath.SelectedItem.ToString(); 
            }
            set
            { 
                SetKSPInstallPath(value); 
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the KSPMODOrgenaizer MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            mMainForm = this;
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets the path of the passed FolderType.
        /// </summary>
        /// <param name="kpsPath">FolderType of the folder to get the path of.</param>
        /// <returns>The HD path to the passed folder name.</returns>
        public string GetPath(KSP_Paths kpsPath)
        {
            string path = string.Empty;
            if (kpsPath == KSP_Paths.AppConfig)
                path = Path.Combine(Application.CommonAppDataPath.Replace(mAdminVersion, ""), Constants.CONFIG_FILE);

            else if (mAppConfig != null && !string.IsNullOrEmpty(mAppConfig.KSP_Path) && Directory.Exists(mAppConfig.KSP_Path))
            {
                switch (kpsPath)
                {
                    case KSP_Paths.KSPRoot:
                        path = mAppConfig.KSP_Path;
                        break;
                    case KSP_Paths.KSPExe:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSP_EXE);
                        break;
                    case KSP_Paths.KSP64Exe:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSP_X64_EXE);
                        break;
                    case KSP_Paths.KSPConfig:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.CONFIG_FILE);
                        break;
                    case KSP_Paths.Saves:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.SAVES);
                        break;
                    case KSP_Paths.Parts:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PARTS);
                        break;
                    case KSP_Paths.Plugins:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PLUGINS);
                        break;
                    case KSP_Paths.PluginData:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PLUGINDATA);
                        break;
                    case KSP_Paths.Resources:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.RESOURCES);
                        break;
                    case KSP_Paths.GameData:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.GAMEDATA);
                        break;
                    case KSP_Paths.Ships:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.SHIPS);
                        break;
                    case KSP_Paths.VAB:
                        path = Path.Combine(Path.Combine(mAppConfig.KSP_Path, Constants.SHIPS), Constants.VAB);
                        break;
                    case KSP_Paths.SPH:
                        path = Path.Combine(Path.Combine(mAppConfig.KSP_Path, Constants.SHIPS), Constants.SPH);
                        break;
                    case KSP_Paths.Internals:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.INTERNALS);
                        break;
                    case KSP_Paths.KSPData:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSPDATA);
                        break;
                }
            }

            return path;
        }

        /// <summary>
        /// Gets the HD path of the passed folder name.
        /// </summary>
        /// <param name="pathName">Name of the folder to get the path of.</param>
        /// <returns>The HD path to the passed folder name.</returns>
        public string GetPathByName(string pathName)
        {
            string path = string.Empty;
            if (mAppConfig != null && !string.IsNullOrEmpty(mAppConfig.KSP_Path) && Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX != mAppConfig.KSP_Path && Directory.Exists(mAppConfig.KSP_Path))
            {
                switch (pathName)
                {
                    case Constants.SAVES:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.SAVES);
                        break;
                    case Constants.PARTS:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PARTS);
                        break;
                    case Constants.PLUGINS:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PLUGINS);
                        break;
                    case Constants.PLUGINDATA:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.PLUGINDATA);
                        break;
                    case Constants.RESOURCES:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.RESOURCES);
                        break;
                    case Constants.GAMEDATA:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.GAMEDATA);
                        break;
                    case Constants.SHIPS:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.SHIPS);
                        break;
                    case Constants.INTERNALS:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.INTERNALS);
                        break;
                    case Constants.VAB:
                        path = Path.Combine(mAppConfig.KSP_Path, Path.Combine(Constants.SHIPS, Constants.VAB));
                        break;
                    case Constants.SPH:
                        path = Path.Combine(mAppConfig.KSP_Path, Path.Combine(Constants.SHIPS, Constants.SPH));
                        break;
                    case Constants.KSPDATA:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSPDATA);
                        break;
                    case Constants.KSP_ROOT:
                        path = mAppConfig.KSP_Path;
                        break;
                    case Constants.KSP_EXE:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSP_EXE);
                        break;
                    case Constants.KSP_X64_EXE:
                        path = Path.Combine(mAppConfig.KSP_Path, Constants.KSP_X64_EXE);
                        break;
                    case Constants.MYFLAGS:
                        path = Path.Combine(Path.Combine(Path.Combine(mAppConfig.KSP_Path, Constants.GAMEDATA), Constants.MYFLAGS), Constants.FLAGS);
                        break;
                }
            }

            return path;
        }

        /// <summary>
        /// Checks if the passed path is a KSP folder path.
        /// </summary>
        /// <param name="dir">The directory to check.</param>
        /// <returns>True if the passed path is a KSP folder path.</returns>
        public bool IsKSPDir(string dir)
        {
            foreach (var path in Constants.KSPFolders)
            {
                if (path.ToLower() == dir.ToLower() || GetPathByName(path).ToLower() == dir.ToLower())
                    return true;
            }
            return false;
            //return Constants.KSPFolders.Any(path => GetPathByName(path.ToLower()) == dir.ToLower());
        }

        ///// <summary>
        ///// Checks if the passed folder is a KSP folder.
        ///// </summary>
        ///// <param name="folderName">The foldername to check.</param>
        ///// <returns>True if the passed folder is a KSP folder.</returns>
        //public bool IsKSPFolder(string folderName)
        //{
        //    bool result = false;
        //    switch (folderName.ToLower())
        //    {
        //        case Constants.INTERNALS:
        //            result = true;
        //            break;

        //        case Constants.KSPDATA:
        //            result = true;
        //            break;

        //        case Constants.PARTS:
        //            result = true;
        //            break;

        //        case Constants.PLUGINS:
        //            result = true;
        //            break;

        //        case Constants.PLUGINDATA:
        //            result = true;
        //            break;

        //        case Constants.RESOURCES:
        //            result = true;
        //            break;

        //        case Constants.SHIPS:
        //            result = true;
        //            break;

        //        case Constants.VAB:
        //            result = true;
        //            break;

        //        case Constants.SPH:
        //            result = true;
        //            break;

        //        case Constants.GAMEDATA:
        //            result = true;
        //            break;

        //        case Constants.SAVES:
        //            result = true;
        //            break;
        //    }

        //    return result;
        //}

        /// <summary>
        /// Checks if the passed folder is the install folder of KSP.
        /// </summary>
        /// <param name="kspPath">The path to the KSP install folder.</param>
        /// <returns>True if the passed folder is the install folder of KSP otherwise false.</returns>
        public bool IsKSPInstallFolder(string kspPath)
        {
            try
            {
                if (string.IsNullOrEmpty(kspPath))
                    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PARTS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.KSPDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.GAMEDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.RESOURCES)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.INTERNALS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.SHIPS)))
                //    return false;
                if (!Directory.Exists(kspPath) && (!File.Exists(Path.Combine(kspPath, Constants.KSP_EXE)) && !File.Exists(Path.Combine(kspPath, Constants.KSP_X64_EXE))))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                string NoWarningPLS = ex.Message;
            }

            return false;
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The message to add.</param>
        public void AddMessage(string msg)
        {
            InvokeIfRequired(() => txtBoxMessages.AppendText(msg + Environment.NewLine));
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The info message to add.</param>
        /// <param name="callingFileLineNumber">Parameter for the line number of the calling function.</param>
        public void AddInfo(string msg, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            InvokeIfRequired(()=> txtBoxMessages.AppendText(msg + Environment.NewLine));
            Log.AddInfoS(msg, callingFileLineNumber);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The debug message to add.</param>
        /// <param name="callingFileLineNumber">Parameter for the line number of the calling function.</param>
        public void AddDebug(string msg, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            InvokeIfRequired(()=> txtBoxMessages.AppendText(msg + Environment.NewLine));
            Log.AddDebugS(msg, callingFileLineNumber);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The warning message to add.</param>
        /// <param name="callingFileLineNumber">Parameter for the line number of the calling function.</param>
        public void AddWarning(string msg, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            InvokeIfRequired(()=> txtBoxMessages.AppendText(msg + Environment.NewLine));
            Log.AddWarningS(msg, callingFileLineNumber);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The error message to add.</param>
        /// <param name="ex">The exception to add to the error message.</param>
        /// <param name="callingFileLineNumber">Parameter for the line number of the calling function.</param>
        public void AddError(string msg, Exception ex = null, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            InvokeIfRequired(()=> txtBoxMessages.AppendText(msg + Environment.NewLine));
            Log.AddErrorS(msg, ex, callingFileLineNumber);
        }

        /// <summary>
        /// Unloads all infos and resets the App to default values.
        /// </summary>
        public void UnloadAll()
        {
            ResetKSPPaths(true);
            SetModAdminEnableState(false);

            Backup.UnloadAll();

            Options.BackupPath = string.Empty;
            Options.DownloadPath = string.Empty;
        }

        #endregion

        #region Private

        #region Form event handling

        /// <summary>
        /// Handles the Load event of the MainForm.
        /// Loads AppConfig and sets form title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // Load plugins
            List<IKSPMAPlugin> plugins = PluginLoader.LoadPlugins<IKSPMAPlugin>("");

            // Get assembly version.
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            mVersion = string.Format("{0}.{1}.{2}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
            mAdminVersion = string.Format("{0}.{1}.{2}.{3}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
            Text = Text + mAdminVersion + " *UNOFFICIAL RELEASE";
            Options.Version = mVersion;
            Options.AdminVersion = mAdminVersion;

            SetupLogFile();

            HandleCommandLineArgs(Environment.GetCommandLineArgs());

            // Set parent form and event handling of UserControls (tabs view).
            Welcome.MainForm = this;
            ModSelection.MainForm = this;
            ModBrowser.MainForm = this;
            Parts.MainForm = this;
            Crafts.MainForm = this;
            Flags.MainForm = this;
            Backup.MainForm = this;
            Options.MainForm = this;
            Help.MainForm = this;

            // Load configs
            LoadAppConfig();

            SetDefaultKSPPath();

            if (mAppConfig != null)
            {
                //adjust pos of main form.
                if (mAppConfig.Maximized)
                    WindowState = FormWindowState.Maximized;
                else
                {
                    int width = Screen.PrimaryScreen.Bounds.Width;
                    int height = Screen.PrimaryScreen.Bounds.Height;

                    Point pos = mAppConfig.Position;
                    if (pos.X < 0 || pos.Y < 0 ||
                        pos.X > width || pos.X + this.Width > width ||
                        pos.Y > height || pos.Y + this.Height > height)
                        mAppConfig.Position = Point.Empty;

                    if (mAppConfig.Position != Point.Empty)
                        Location = mAppConfig.Position;

                    width = mAppConfig.Size.Width;
                    height = mAppConfig.Size.Height;
                    if (width < Constants.WIN_MIN_WIDTH)
                        width = Constants.WIN_MIN_WIDTH;
                    if (height < Constants.WIN_MIN_HEIGHT)
                        height = Constants.WIN_MIN_HEIGHT;

                    Size = mAppConfig.Size = new Size(width, height);
                }

                Welcome.MainForm = this;
                Welcome.Version = mVersion;
                if (mAppConfig.HasValidKSP_Path)
                    Welcome.Visible = false;

                if (mAppConfig.CheckForUpdates)
                    Options.CheckForNewVersion();
            }

            // Help not done yet -> so "hide" it ;)
            tabControl1.TabPages.RemoveByKey(tabPageHelp.Name);

            // auto mod update check.
            Options.AutoModUpdateCheck(true);
        }

        /// <summary>
        /// Handles the Resize event of the MainForm.
        /// Saves the Minimized and Maximized state of the MainForm to AppConfig.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (mAppConfig != null)
            {
                mAppConfig.Minimized = false;
                mAppConfig.Maximized = false;

                if (WindowState == FormWindowState.Minimized)
                    mAppConfig.Minimized = true;

                else if (WindowState == FormWindowState.Maximized)
                    mAppConfig.Maximized = true;
            }
        }

        /// <summary>
        /// Handels the FromClosing event of the MainForm.
        /// Saves config files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mAppConfig.Position = Location;
            mAppConfig.Size = Size;

            SaveAppConfig();
            SaveKSPConfig();
            ucModSelection1.ClearNodes();
            ucBackup1.ClearNodes();

            if (m_SearchDLG != null)
                m_SearchDLG.Close();

            Log.AddInfoS(string.Format("---> KSP MA v{0} closed <---{1}", mAdminVersion, Environment.NewLine));
        }

        /// <summary>
        /// Handels the MouseDown event of the cbKSPPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKSPPath_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbKSPPath.Items.Count == 1 && ((string)cbKSPPath.Items[0]) == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
            {
                MessageBox.Show(this, "Please select a KSP install path on the Options tab.");
                tabControl1.SelectedTab = tabPageOptions;
                Options.TabControl1.SelectedTab = Options.tabPagePaths;
            }
        }

        /// <summary>
        /// Handels the SelectedIndexChanged event of the cbKSPPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKSPPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKSPPath.SelectedIndex > -1 && !mIgnoreKSPPath)
                SetKSPInstallPath(cbKSPPath.Items[cbKSPPath.SelectedIndex].ToString());
        }

        /// <summary>
        /// Handels the TabIndexChanged event of the tabControl1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == null) return;

            bool validKSPPath = IsKSPDir((string)cbKSPPath.SelectedItem);
            if (tabControl1.SelectedTab.Name == tabPageFlags.Name && Flags.FlagCount == 0 && Flags.Enabled)
                Flags.RefreshFlagTab();
            else if (tabControl1.SelectedTab.Name == tabPageModBrowser.Name && ModBrowser.URL == string.Empty && ModBrowser.Enabled)
                ModBrowser.RefreshModBrowser();
            else if (validKSPPath && tabControl1.SelectedTab.Name == tabPageParts.Name && Parts.tvParts.Nodes.Count == 0 && Parts.Enabled)
                Parts.RefreshParts();
            else if (validKSPPath && tabControl1.SelectedTab.Name == tabPageCrafts.Name && Crafts.tvCrafts.Nodes.Count == 0 && Crafts.Enabled)
                Crafts.RefreshCrafts();
        }

        /// <summary>
        /// Handels the SplitterMoved event of the SplitContainer.
        /// Min and max splitter distance adjustments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (e.SplitY < mMaxSplitterDistance)
                splitContainer1.SplitterDistance = mMaxSplitterDistance;
            if (e.SplitY > splitContainer1.Size.Height - mMinSplitterDistance)
                splitContainer1.SplitterDistance = splitContainer1.Size.Height - mMinSplitterDistance;
        }

        #endregion

        #region Config

        /// <summary>
        /// Sets the KSP install path.
        /// </summary>
        /// <param name="kspPath">The new KSP install path.</param>
        private void SetKSPInstallPath(string kspPath)
        {
            if (mIgnoreKSPPath || string.IsNullOrEmpty(kspPath) || kspPath == Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX) 
                return;

            ModRegister.Clear();

            SaveAppConfig();
            SaveKSPConfig();

            mAppConfig.KSP_Path = kspPath;
            SaveAppConfig();
            LoadAppConfig();

            Flags.ClearFlags();
        }

        /// <summary>
        /// Sets the default KSP path.
        /// </summary>
        private void SetDefaultKSPPath()
        {
            if (cbKSPPath.Items.Count == 0)
            {
                cbKSPPath.Items.Add(Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX);
                ucOptions1.cbKSPPath.Items.Add(Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX);
                mIgnoreKSPPath = true;
                cbKSPPath.SelectedIndex = 0;
                ucOptions1.cbKSPPath.SelectedIndex = 0;
                mIgnoreKSPPath = false;
            }
        }

        /// <summary>
        /// Clears all KSPPaths and set default paths
        /// </summary>
        /// <param name="setDefaultPath">Parameter to determin if the default KSP path should be set or not.</param>
        private void ResetKSPPaths(bool setDefaultPath = false)
        {
            cbKSPPath.Items.Clear();
            ucOptions1.tvPaths.Nodes.Clear();
            ucOptions1.cbKSPPath.Items.Clear();

            ModSelection.ClearNodes();

            if (setDefaultPath)
                SetDefaultKSPPath();
        }

        /// <summary>
        /// Sets the ModAdmin tab controls enable state to the passes value.
        /// </summary>
        /// <param name="enable">State to set the MODAdmin Tab controls to.</param>
        private void SetModAdminEnableState(bool enable)
        {
            ucModSelection1.EnableControls = enable;
            ucParts1.Enabled = enable;
            ucCrafts1.Enabled = enable;
            ucFlags1.Enabled = enable;
            ucBackup1.EnableControls = enable;
            ucOptions1.EnablePathButtons = enable;
            //ucHelp1.Enabled = enable;
        }

        // App Config //
        /// <summary>
        /// Creates the default AppConfig.
        /// </summary>
        private void CreateDefaultAppConfig()
        {
            mAppConfig = new AdminConfig();
            mAppConfig.Save(GetPath(KSP_Paths.AppConfig));

            SetAppConfigValues();
        }

        /// <summary>
        /// Loads the AppConfig from "c:\ProgramData\...".
        /// </summary>
        private void LoadAppConfig()
        {
            AddInfo("Loading KSP Mod Admin settings ...");
            string path = GetPath(KSP_Paths.AppConfig);
            if (File.Exists(path))
            {
                mAppConfig = new AdminConfig();
                if (mAppConfig.Load(path))
                {
                    SetAppConfigValues();
                    AddInfo("Done.");

                    if (IsKSPInstallFolder(mAppConfig.KSP_Path))
                        LoadKSPConfig();
                }
                else
                {
                    AddInfo("Creating default KSP Mod Admin settings ...");
                    CreateDefaultAppConfig();
                    AddInfo("Done.");
                }
            }
            else // try to find older version config files.
            {
                AddInfo("KSP Mod Admin config not found. Searching config files of older versions ...");
                bool loaded = false;
                int count = Constants.RELEASE_VERSIONS.Count();
                for (int i = count - 1; i > 0; --i)
                {
                    if (i == 0) break;

                    string version = Constants.RELEASE_VERSIONS[i];
                    string oldPath = Path.Combine(Path.Combine(Path.GetDirectoryName(path), version), Constants.CONFIG_FILE);

                    if (!File.Exists(oldPath)) 
                        continue;

                    AddInfo(string.Format("Older version ({0}) of KSP Mod Admin config detected.", version));
                    mAppConfig = new AdminConfig();
                    if (mAppConfig.Load(oldPath))
                    {
                        SetAppConfigValues();
                        SaveAppConfig();
                        AddInfo("Done.");

                        if (IsKSPInstallFolder(mAppConfig.KSP_Path))
                            LoadKSPConfig();
                    }

                    loaded = true;

                    break;
                }
                
                if (!loaded)
                {
                    AddInfo("No older config file found.");
                    AddInfo("Creating default KSP Mod Admin settings ...");
                    CreateDefaultAppConfig();
                    AddInfo("Done.");
                }
            }

            DeleteOldAppConfigs();
        }

        /// <summary>
        /// Deletes older config paths and files.
        /// </summary>
        private void DeleteOldAppConfigs()
        {
            AddInfo("Deleting old versions of KSP Mod Admin config.");
            string path = GetPath(KSP_Paths.AppConfig);
            int count = Constants.RELEASE_VERSIONS.Count();
            for (int i = count - 1; i > 0; --i)
            {
                string version = Constants.RELEASE_VERSIONS[i];
                string oldPath = Path.Combine(Path.GetDirectoryName(path), version);

                //try
                //{
                    try
                    {
                        if (!Directory.Exists(oldPath))
                            continue;

                        Directory.Delete(oldPath, true);
                        AddInfo(string.Format("Config \"{0}\" deleted.", version));
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("Error during deleting of \"{0}\".", oldPath), ex);
                    }
                //}
                //catch (Exception ex)
                //{
                //    AddError(string.Format("Error during deleting of \"{0}\".", oldPath), ex);
                //}
            }
            AddInfo("Done.");
        }

        /// <summary>
        /// Saves the AppConfig to "c:\ProgramData\..."
        /// </summary>
        public void SaveAppConfig()
        {
            try
            {
                AddInfo("Saving new KSP Mod Admin settings.");
                string path = GetPath(KSP_Paths.AppConfig);
                if (path != string.Empty && mAppConfig != null)
                {
                    mAppConfig.CheckForUpdates = Options.CheckForUpdates;
                    mAppConfig.PostDownloadAction = Options.PostDownloadAction;
                    mAppConfig.LastModUpdateTry = Options.LastModUpdateTry;
                    mAppConfig.ModUpdateInterval = Options.ModUpdateInterval;
                    mAppConfig.ModUpdateBehavior = Options.ModUpdateBehavior;
                    mAppConfig.KnownPaths = Options.KnownPaths;
                    mAppConfig.ColorDestinationConflict = Options.ColorDestinationConflict;
                    mAppConfig.ColorDestinationDetected = Options.ColorDestinationDetected;
                    mAppConfig.ColorDestinationMissing = Options.ColorDestinationMissing;
                    mAppConfig.ColorModArchiveMissing = Options.ColorModArchiveMissing;
                    mAppConfig.ColorModInstalled = Options.ColorModInstalled;
                    mAppConfig.ColorModOutdated = Options.ColorModOutdated;
                    mAppConfig.ConflictDetection = Options.ConflictDetection;
                    mAppConfig.FolderConflictDetection = Options.FolderConflictDetection;
                    mAppConfig.ShowConflictSolver = Options.ShowConflictSolver;
                    mAppConfig.Save(path);
                }
            }
            catch (Exception ex)
            {
                AddError("Error during saving of KSP Mod Admin config!", ex);
                ShowAdminRightsDlg(ex);
            }
        }

        /// <summary>
        /// Sets the AppConfig values to the app.
        /// </summary>
        private void SetAppConfigValues()
        {
            SetKnownPaths();

            if (mAppConfig.KSP_Path != Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX)
                Options.KSPPath = mAppConfig.KSP_Path;
            else
                Options.KSPPath = string.Empty;

            Options.PostDownloadAction = mAppConfig.PostDownloadAction;
            Options.LastModUpdateTry = mAppConfig.LastModUpdateTry;
            Options.ModUpdateInterval = mAppConfig.ModUpdateInterval;
            Options.ModUpdateBehavior = mAppConfig.ModUpdateBehavior;
            Options.ColorDestinationConflict = mAppConfig.ColorDestinationConflict;
            Options.ColorDestinationDetected = mAppConfig.ColorDestinationDetected;
            Options.ColorDestinationMissing = mAppConfig.ColorDestinationMissing;
            Options.ColorModArchiveMissing = mAppConfig.ColorModArchiveMissing;
            Options.ColorModInstalled = mAppConfig.ColorModInstalled;
            Options.ColorModOutdated = mAppConfig.ColorModOutdated;
            Options.ConflictDetection = mAppConfig.ConflictDetection;
            Options.FolderConflictDetection = mAppConfig.FolderConflictDetection;
            Options.ShowConflictSolver = mAppConfig.ShowConflictSolver;
        }

        /// <summary>
        /// Sets the known KSP paths to the UI.
        /// </summary>
        private void SetKnownPaths()
        {
            Options.KnownPaths = mAppConfig.KnownPaths;
            Options.SelectedKSPPath = mAppConfig.KSP_Path;

            Options.cbKSPPath.Items.Clear();
            cbKSPPath.Items.Clear();
            foreach (PathInfo info in mAppConfig.KnownPaths)
            {
                if (info.ValidPath)
                {
                    cbKSPPath.Items.Add(info.FullPath);
                    Options.cbKSPPath.Items.Add(info.FullPath);
                    if (info.FullPath == mAppConfig.KSP_Path)
                    {
                        mIgnoreKSPPath = true;
                        cbKSPPath.SelectedItem = mAppConfig.KSP_Path;
                        Options.cbKSPPath.SelectedItem = mAppConfig.KSP_Path;
                        Options.EnablePathButtons = info.ValidPath;
                        mIgnoreKSPPath = false;
                    }
                }
            }
            Options.tbBackupPath.Text = string.Empty;
            Options.tbDownloadPath.Text = string.Empty;

            Backup.tbBackupPath.Text = string.Empty;
        }

        // KSP Config //
        /// <summary>
        /// Creates the default KSPConfig.
        /// </summary>
        private void CreateDefaultKSPConfig()
        {
            mKSPConfig = new KSPConfig();
            mKSPConfig.Save(GetPath(KSP_Paths.KSPConfig), new TreeNodeMod[] {}, new Dictionary<string,string>());

            SetKSPConfigValues();
        }

        /// <summary>
        /// Loads the KSPConfig from the selected KSP folder.
        /// </summary>
        private void LoadKSPConfig()
        {
            ModSelection.ClearNodes();
            Backup.ClearNodes();
            Parts.tvParts.Nodes.Clear();
            Crafts.tvCrafts.Nodes.Clear();

            if (File.Exists(GetPath(KSP_Paths.KSPConfig)))
            {
                AddInfo("Loading KSP mod configuration ...");
                List<TreeNodeMod> mods = new List<TreeNodeMod>();
                mKSPConfig = new KSPConfig();
                mKSPConfig.Load(GetPath(KSP_Paths.KSPConfig), ref mods);
                ModSelection.AddNodes(mods.ToArray());
                ModSelection.SortModSelection();

                SetKSPConfigValues();
                SetModAdminEnableState(true);
            }
            else
            {
                AddInfo("KSP mod configuration not found!");
                AddInfo("Creating default configuration.");
                CreateDefaultKSPConfig();
                SetModAdminEnableState(true);
            }

            ModSelection.RenewCheckedStateAllNodes();
            AddInfo("Done.");
        }

        /// <summary>
        /// Saves the KSPConfig to the selected KSP folder.
        /// </summary>
        public void SaveKSPConfig()
        {
            try
            {
                string path = GetPath(KSP_Paths.KSPConfig);
                if (mKSPConfig != null && path != string.Empty && Directory.Exists(Path.GetDirectoryName(path)))
                {
                    AddInfo("Saving new KSP Mod settings.");
                    mKSPConfig.BackupPath = Options.BackupPath;
                    mKSPConfig.DownloadPath = Options.DownloadPath;
                    mKSPConfig.Override = ModSelection.Override;
                    mKSPConfig.StartWithBorderlessWindow = ModSelection.BorderlessWin;
                    mKSPConfig.Save(path, ModSelection.Nodes, Backup.BackupNotes);
                }
            }
            catch (Exception ex)
            {
                AddError("Error during saving of KSP Mod config!", ex);
                ShowAdminRightsDlg(ex);
            }
        }

        /// <summary>
        /// Shows a MessageBox with the info, that KSP MA needs admin rights if KSP is installed to c:\Programme
        /// </summary>
        /// <param name="ex">The message of the Exception will be displayed too.</param>
        private void ShowAdminRightsDlg(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Message: {0}", ex.Message));
            sb.AppendLine();
            sb.AppendLine("KSP Mod Admin can't save its config file. The access to the KSP path was denied.");
            sb.AppendLine("If you have install KSP into the Program folder of Windows,");
            sb.AppendLine("KSP MA needs admin rights to manipulate (save/change KSP MA config file).");
            sb.AppendLine("You can move you install dir of KSP to another spot or start KSPMA in admin mode with:");
            sb.AppendLine("  - a right click on the KSPModAdmin.exe and choose \"Run as Admin\"");
            sb.AppendLine("or for a permanent change:");
            sb.AppendLine("  - right click the KSPModAdmin.exe");
            sb.AppendLine("  - choose properties");
            sb.AppendLine("  - choose compatibility");
            sb.AppendLine("  - check the \"Run as Admin\" Checkbox at the bottom.");
            sb.AppendLine("  - press OK.");
            MessageBox.Show(this, sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Sets the KSPConfig values to the app.
        /// </summary>
        private void SetKSPConfigValues()
        {
            Options.DownloadPath = string.Empty;

            Crafts.ClearTreeView();
            Parts.ClearTreeView();

            // Backup Tab
            if (Directory.Exists(mKSPConfig.BackupPath))
            {
                //Backup.FireBackupEvents = false;
                Backup.BackupPath = mKSPConfig.BackupPath;
                Options.BackupOnStartup = mKSPConfig.BackupOnStartup;
                Options.BackupOnKSPLaunch = mKSPConfig.BackupOnKSPLaunch;
                Options.BackupOnInterval = mKSPConfig.BackupOnInterval;
                Options.BackupInterval = mKSPConfig.BackupInterval;
                Options.MaxBackupFiles = mKSPConfig.MaxBackupFiles;

                Backup.LoadBackups();
                Backup.SetBackupEnableState(true);

                if (Options.BackupOnStartup)
                    Backup.StartupBackup();

                Options.BackupPath = Backup.BackupPath;
            }

            if (Directory.Exists(mKSPConfig.DownloadPath))
                Options.DownloadPath = mKSPConfig.DownloadPath;

            ModSelection.BorderlessWin = mKSPConfig.StartWithBorderlessWindow;
            ModSelection.Override = mKSPConfig.Override;
        }

        #endregion

        /// <summary>
        /// Deletes the old content of the log file when file size is above 1mb and creates a new log file.
        /// </summary>
        private void SetupLogFile()
        {
//#if DEBUG
            Log.GlobalInstance.LogMode = LogMode.All;
//#else
//            Log.GlobalInstance.LogMode = LogMode.WarningsAndErrors;
//#endif
            try
            {
                string logPath = Path.Combine(Application.StartupPath, "KSPMA.log");
                if (File.Exists(logPath))
                {
                    FileInfo fInfo = new FileInfo(logPath);
                    if (fInfo.Length > 2097152) // > 2mb
                        File.Delete(logPath);
                }

                Log.GlobalInstance.FullPath = logPath;

                Log.AddInfoS(string.Format("---> KSP MA v{0} started <---", mAdminVersion));

//#if DEBUG
//                Log.AddWarningS("Test Warning.");
//                Log.AddDebugS("Test Debug Info.");
//                Log.AddErrorS("Test Error w/o Exception.");
//                try
//                {
//                    throw new Exception("Test Exception");
//                }
//                catch (Exception ex)
//                {
//                    Log.AddErrorS("Test Error", ex);
//                }
//#endif
            }
            catch (Exception)
            {
                MessageBox.Show("Can't create KSP MA log file. Error logging will be turned off.");
                Log.GlobalInstance.LogMode = LogMode.None;
            }
        }

        /// <summary>
        /// Parses and handles the comandline arguments.
        /// </summary>
        /// <param name="args">Array of command line arguments.</param>
        private void HandleCommandLineArgs(string[] args)
        {
            foreach (string argValuePair in args)
            {
                string[] arg = argValuePair.Split('=');
                if (arg.Length == 2)
                {
                    switch (arg[0].Trim().ToLower())
                    {
                        case "logmode":
                            HandleArgLogMode(arg[1].Trim().ToLower());
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the LogMode commandline argument.
        /// </summary>
        /// <param name="value">The logmode command line argument.</param>
        private void HandleArgLogMode(string value)
        {
            switch (value.ToLower())
            {
                case "4":
                case "all":
                    Log.GlobalInstance.LogMode = LogMode.All;
                    break;
                case "3":
                case "debugwarningsanderrors":
                    Log.GlobalInstance.LogMode = LogMode.DebugWarningsAndErrors;
                    break;
                case "2":
                case "warningsanderrors":
                    Log.GlobalInstance.LogMode = LogMode.WarningsAndErrors;
                    break;
                case "1":
                case "errors":
                    Log.GlobalInstance.LogMode = LogMode.Errors;
                    break;
                case "0":
                case "none":
                    Log.GlobalInstance.LogMode = LogMode.None;
                    break;
            }
        }

        #endregion

        #region WndProc

#if !MONOBUILD
        /// <summary>
        /// Listen to windows messages
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
                ShowMe();
            base.WndProc(ref m);
        }

        /// <summary>
        /// Bring form to front.
        /// </summary>
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }
#endif

        #endregion
    }
}
