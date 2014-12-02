using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using FolderSelect;
using KSPModAdmin.Utils;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace KSPModAdmin.Views
{
    public partial class ucBackup : ucBase
    {
        #region Members

        /// <summary>
        /// Auto backup timer.
        /// </summary>
        Timer mAutoBackupTimer = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the enablestate of the UcBackUp
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool EnableControls
        {
            get
            {
                return tvBackup.Enabled;
            }
            set
            {
                if (value)
                {
                    bool exists = false;
                    try
                    {
                        if (!tbBackupPath.Text.Contains("->"))
                            exists = Directory.Exists(tbBackupPath.Text);
                    }
                    catch (Exception ex)
                    {
                        string noWarnigPls = ex.Message;
                    }

                    if (exists && value)
                    {
                        tvBackup.Enabled = true;
                        btnBackup.Enabled = true;
                        btnBackupPath.Enabled = true;
                        btnBackupSaves.Enabled = true;
                        btnClearBackups.Enabled = true;
                        //btnReinstallBackup.Enabled = true;
                        btnRemoveBackup.Enabled = true;
                        btnOpenBackupDir.Enabled = true;
                    }
                    else
                    {
                        tvBackup.Enabled = false;
                        btnBackup.Enabled = false;
                        btnBackupPath.Enabled = true;
                        btnBackupSaves.Enabled = false;
                        btnClearBackups.Enabled = false;
                        btnReinstallBackup.Enabled = false;
                        btnRemoveBackup.Enabled = false;
                        btnOpenBackupDir.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// The path where the backups where saved to.
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, string> BackupNotes
        {
            get
            {
                return tvBackup.Nodes.Cast<TreeNodeNote>().ToDictionary(node => node.Text, node => node.Note);
            }
        }

        #endregion

        #region Contructors

        public ucBackup()
        {
            InitializeComponent();

            //FireBackupEvents = true;

            mAutoBackupTimer = new Timer();
            mAutoBackupTimer.Tick += new EventHandler(mAutoBackupTimer_Tick);
        }

        #endregion

        #region Public

        /// <summary>
        /// Reads the backup directory and fills the backup TreeView.
        /// </summary>
        public void LoadBackups()
        {
            ScanBackupDirectory();
        }

        /// <summary>
        /// Clears the tvBackups.
        /// </summary>
        public void ClearNodes()
        {
            tvBackup.Nodes.Clear();
        }

        /// <summary>
        /// Toggles the on off state of the auto backup function.
        /// </summary>
        public void ToggleAutoBackupOnOff(bool on)
        {
            if (on)
            {
                mAutoBackupTimer.Stop();
                mAutoBackupTimer.Interval = (int)(MainForm.Options.BackupInterval * 60 * 1000); // minutes to milisecs.
                mAutoBackupTimer.Start();
            }
            else
            {
                mAutoBackupTimer.Stop();
            }
        }

        /// <summary>
        /// Creates a Backup of the saves folder with the name "StartupBackup_{yyyyMMdd_HHmm}.zip".
        /// </summary>
        public void StartupBackup()
        {
            MainForm.AddInfo("Backup on startup started.");
            string dir = MainForm.GetPath(KSP_Paths.Saves);
            string zipPath = dir.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", "");
            string name = String.Format("StartupBackup_{0}{1}", DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
        }

        /// <summary>
        /// Creates a Backup of the saves folder with the name "KSPLaunchBackup_{yyyyMMdd_HHmm}.zip".
        /// </summary>
        public void KSPLaunchBackup()
        {
            MainForm.AddInfo("Backup on KSP launch started.");
            string dir = MainForm.GetPath(KSP_Paths.Saves);
            string zipPath = dir.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", "");
            string name = String.Format("KSPLaunchBackup_{0}{1}", DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
        }

        public void UnloadAll()
        {
            //FireBackupEvents = false;
            ClearNodes();

            tbBackupPath.Text = string.Empty;

            tvBackup.Enabled = false;
            btnBackup.Enabled = false;
            btnBackupPath.Enabled = false;
            btnBackupSaves.Enabled = false;
            btnClearBackups.Enabled = false;
            btnReinstallBackup.Enabled = false;
            btnRemoveBackup.Enabled = false;

            //FireBackupEvents = true;
        }
        
        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucBackups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBackups_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            tvBackup.AddActionKey(VirtualKey.VK_DELETE, HandleDeleteKeys, null, true);
            tvBackup.AddActionKey(VirtualKey.VK_BACK, HandleDeleteKeys, null, true);

            tvBackup.AddColumn("name", "Name", 230, 230, false, true);
            tvBackup.AddColumn("note", "Note", 150, 150);
        }

        /// <summary>
        /// Handels the Click event of the btnBackupPath button.
        /// Opens a folder dialog and sets the backup path to the choosen destination.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackupPath_Click(object sender, EventArgs e)
        {
            SetBackupEnableState(false);

            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "Backup folder selection";
            dlg.InitialDirectory = MainForm.GetPath(KSP_Paths.KSPRoot);
            if (dlg.ShowDialog(this.ParentForm.Handle))
            {
                string backupPath = dlg.FileName;
                if (tbBackupPath.Text != backupPath)
                {
                    tbBackupPath.Text = Constants.MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX;

                    if (backupPath == string.Empty)
                        MessageBox.Show(this, Constants.MESSAGE_BACKUP_SELECT_FOLDER);

                    else if (!Directory.Exists(backupPath))
                        MessageBox.Show(this, string.Format(Constants.MESSAGE_FOLDER_NOT_FOUND, backupPath));

                    else
                    {
                        BackupPath = backupPath;
                        MainForm.Options.BackupPath = backupPath;
                        MainForm.AddInfo(string.Format("Backup path changed to \"{0}\"", backupPath));
                    }
                }
            }

            if (Directory.Exists(BackupPath))
                SetBackupEnableState(true);
        }

        /// <summary>
        /// Handels the Click event of the btnBackup button.
        /// Opens a folder dialog and backups the choosen destination.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (!ValidBackupDirectory(tbBackupPath.Text))
                return;

            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "Source folder selection";
            dlg.InitialDirectory = MainForm.GetPath(KSP_Paths.KSPRoot);
            if (dlg.ShowDialog(this.Handle))
                BackupDirectoryAsync(dlg.FileName);
        }

        /// <summary>
        /// Handels the Click event of the btnBackupSaves button.
        /// Backups the KSP saves folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackupSaves_Click(object sender, EventArgs e)
        {
            if (ValidBackupDirectory(tbBackupPath.Text))
                BackupDirectoryAsync(MainForm.GetPath(KSP_Paths.Saves));
        }

        /// <summary>
        /// Handels the Click event of the btnRemoveBackup button.
        /// Deletes the selected backup file and removes it from the backup tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveBackup_Click(object sender, EventArgs e)
        {
            RemoveBackup();
        }

        /// <summary>
        /// Handels the Click event of the btnClearBackups button.
        /// Deletes all backup files and clears the backup tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearBackups_Click(object sender, EventArgs e)
        {
            if (ValidBackupDirectory(tbBackupPath.Text))
            {
                if (DialogResult.Yes != MessageBox.Show(this, Constants.MESSAGE_BACKUP_DELETE_ALL_QUESTION, "", MessageBoxButtons.YesNo))
                    return;

                if (Directory.Exists(BackupPath))
                {
                    foreach (string file in Directory.EnumerateFiles(BackupPath, "*" + Constants.EXT_ZIP))
                    {
                        if (File.Exists(file))
                        {
                            try
                            {
                                File.Delete(file);
                                MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_DELETED, Path.GetFileName(file)));
                            }
                            catch (Exception ex)
                            {
                                MainForm.AddError(string.Format(Constants.MESSAGE_BACKUP_DELETED_ERROR, Path.GetFileName(file)), ex);
                            }
                        }
                        else
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_NOT_FOUND, Path.GetFileName(file)));
                    }

                    tvBackup.Nodes.Clear();
                }
            }
        }

        /// <summary>
        /// Handels the Click event of the btnReinstallBackup button.
        /// Recovers the selected backup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReinstallBackup_Click(object sender, EventArgs e)
        {
            if (tvBackup.FocusedNode != null && ValidBackupDirectory(tbBackupPath.Text))
            {
                string file = tvBackup.FocusedNode.Name;
                if (File.Exists(file))
                {
                    string savesPath = MainForm.GetPath(KSP_Paths.Saves);
                    if (Directory.Exists(savesPath))
                    {
                        string kspPath = MainForm.GetPath(KSP_Paths.KSPRoot);
                        using (IArchive archive = ArchiveFactory.Open(file))
                        {
                            foreach (IArchiveEntry entry in archive.Entries)
                            {
                                string dir = Path.GetDirectoryName(entry.FilePath);
                                CreateNeededDir(dir);
                                entry.WriteToDirectory(Path.Combine(kspPath, dir));
                            }
                        }
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_REINSTALLED, tvBackup.FocusedNode.Text));
                    }
                    else
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_FOLDER_NOT_FOUND, savesPath));
                }
            }
            else
                MessageBox.Show(this, Constants.MESSAGE_BACKUP_SRC_MISSING);
        }

        /// <summary>
        /// Handels the Click event of the btnOpenBackupDir button.
        /// Opens the selected backup directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBackupDir_Click(object sender, EventArgs e)
        {
            if (tbBackupPath.Text == Constants.MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX)
            {
                MessageBox.Show(ParentForm, "Please select a backup directory first.");
                return;
            }

            string fullpath = BackupPath;
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
                MainForm.AddError("Open backup folder faild.", ex);
            }
        }

        /// <summary>
        /// Handels the Tick event of the mAutoBackupTimer timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mAutoBackupTimer_Tick(object sender, EventArgs e)
        {
            DoAutoBackup();
        }

        /// <summary>
        /// Handels the MouseUp event of the tvBackups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvBackups_MouseUp(object sender, MouseEventArgs e)
        {
            tvBackups_AfterSelect(sender, null);
        }

        /// <summary>
        /// Handels the AfterSelect event of the tvBackups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvBackups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnReinstallBackup.Enabled = (tvBackup.FocusedNode != null);
        }

        #endregion

        /// <summary>
        /// Creates the directory if it not exists.
        /// </summary>
        /// <param name="directory">The directory to create.</param>
        private void CreateNeededDir(string directory)
        {
            try
            {
                string path = MainForm.GetPath(KSP_Paths.KSPRoot);
                if (!Directory.Exists(Path.Combine(path, directory)))
                {
                    string[] dirs = directory.Split('\\');
                    foreach (string dir in dirs)
                    {
                        path = Path.Combine(path, dir);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                    }
                }
            }
            catch (Exception ex)
            {
                string NowarningPLS = ex.Message;
            }
        }

        /// <summary>
        /// Deletes the selected backup file and removes it from the backup tree.
        /// </summary>
        private bool HandleDeleteKeys(ActionKeyInfo actionKeyInfo)
        {
            RemoveBackup();
            return true;
        }

        /// <summary>
        /// Deletes the selected backup file and removes it from the backup tree.
        /// </summary>
        private void RemoveBackup()
        {
            if (ValidBackupDirectory(tbBackupPath.Text))
            {
                if (DialogResult.Yes != MessageBox.Show(this, string.Format(Constants.MESSAGE_BACKUP_DELETE_QUESTION, tvBackup.FocusedNode.Text), "", MessageBoxButtons.YesNo))
                    return;

                if (Directory.Exists(BackupPath))
                {
                    string file = tvBackup.FocusedNode.Name;
                    if (File.Exists(file))
                    {
                        try
                        {
                            File.Delete(file);
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_DELETED, Path.GetFileName(file)));
                        }
                        catch (Exception ex)
                        {
                            MainForm.AddError(string.Format(Constants.MESSAGE_BACKUP_DELETED_ERROR, Path.GetFileName(file)), ex);
                        }
                    }
                    else
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_NOT_FOUND, Path.GetFileName(file)));

                    tvBackup.Nodes.Remove(tvBackup.FocusedNode);
                }
            }
        }

        /// <summary>
        /// Reads the backup directory and fills the backup TreeView.
        /// </summary>
        private void ScanBackupDirectory()
        {
            tvBackup.Nodes.Clear();

            try
            {
                if (Directory.Exists(BackupPath))
                {
                    foreach (string file in Directory.EnumerateFiles(BackupPath, "*" + Constants.EXT_ZIP))
                    {
                        string dispTxt = GetBackupDisplayName(file);
                        string note = GetNote(dispTxt);
                        tvBackup.Nodes.Add(new TreeNodeNote(file, dispTxt, note));
                    }
                }
                else
                {
                    if (BackupPath != Constants.MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX)
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_FOLDER_NOT_FOUND, BackupPath));
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError(string.Format(Constants.MESSAGE_BACKUP_LOAD_ERROR, ex.Message), ex);
            }
        }

        /// <summary>
        /// Returns the note to te corresponding display text.
        /// </summary>
        /// <param name="dispTxt">The display text of the backup file.</param>
        /// <returns></returns>
        private string GetNote(string dispTxt)
        {
            return MainForm.KSPConfig.BackupNotes.ContainsKey(dispTxt) ? MainForm.KSPConfig.BackupNotes[dispTxt] : string.Empty;
        }

        /// <summary>
        /// Starts the backup of a directory.
        /// </summary>
        /// <param name="dir">The directory to backup.</param>
        /// <param name="name"></param>
        /// <param name="backupPath"></param>
        /// <param name="zipPath"></param>
        private void BackupDirectoryAsync(string dir, string name = "", string backupPath = "", string zipPath = "")
        {
            try
            {
                string nameAuto;
                string backupPathAuto;
                string zipPathAuto;
                nameAuto = CreateBackupFilename(dir, out backupPathAuto, out zipPathAuto);

                if (string.IsNullOrEmpty(name))
                    name = nameAuto;

                if (string.IsNullOrEmpty(backupPath))
                    backupPath = backupPathAuto;

                if (string.IsNullOrEmpty(zipPath))
                    zipPath = zipPathAuto;

                if (!Directory.Exists(BackupPath))
                    Directory.CreateDirectory(BackupPath);

                if (Directory.Exists(dir))
                {
                    MainForm.AddInfo("Backup started.");
                    new AsyncTask<string>(() => BackupDir(dir, name, backupPath), BackupDirFinished).Run();
                }
                else
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_SRC_FOLDER_NOT_FOUND, dir));
            }
            catch (Exception ex)
            {
                MainForm.AddError(string.Format(Constants.MESSAGE_BACKUP_ERROR, ex.Message), ex);
            }
        }

        private string BackupDir(string dir, string name, string backupPath)
        {
            using (var archive = ZipArchive.Create())
            {
                if (name != string.Empty && backupPath != string.Empty)
                {
                    // TODO: Add file/dir wise, not whole dir at once ...
                    //foreach (string file in Directory.GetFiles(dir))
                    //{
                    //    string directoryName = Path.GetDirectoryName(file);
                    //    if (directoryName != null)
                    //    {
                    //        string temp =
                    //            Path.Combine(directoryName.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", ""),
                    //                Path.GetFileName(file));
                    //        archive.AddEntry(temp, file);
                    //    }
                    //}

                    archive.AddAllFromDirectory(dir);

                    //AddSubDirs(dir, archive);

                    archive.SaveTo(backupPath, CompressionType.None);
                }
                else
                    MainForm.AddInfo(Constants.MESSAGE_BACKUP_CREATION_ERROR);
            }

            return name;
        }

        private void BackupDirFinished(string name, Exception ex)
        {
            if (ex != null)
            {
                MainForm.AddError(Constants.MESSAGE_BACKUP_CREATION_ERROR, ex);
            }
            else
            {
                LoadBackups();

                MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_COMPLETE, name));
            }
        }

        /// <summary>
        /// Adds the sub directory to the Archive.
        /// </summary>
        /// <param name="parentDir">Parent directory to scenn for sub dirs & files.</param>
        /// <param name="archive">The Archive to add the dirs & files to.</param>
        private void AddSubDirs(string parentDir, ZipArchive archive)
        {
            foreach (string subDir in Directory.GetDirectories(parentDir))
            {
                foreach (string file in Directory.GetFiles(subDir))
                {
                    string directoryName = Path.GetDirectoryName(file);
                    if (directoryName != null)
                    {
                        string temp = Path.Combine(directoryName.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", ""), Path.GetFileName(file));
                        archive.AddEntry(temp, file);
                    }
                }

                AddSubDirs(subDir, archive);
            }
        }

        /// <summary>
        /// Creates the name for a backup file.
        /// </summary>
        /// <param name="dir">The path to backup to.</param>
        /// <param name="fullpath">Full path of the created backup file.</param>
        /// <param name="zipPath"></param>
        /// <returns>The created name of the Backupfile.</returns>
        private string CreateBackupFilename(string dir, out string fullpath, out string zipPath)
        {
            // create the name of the backupfile from the backup directory
            string name = dir.Substring(dir.LastIndexOf(Constants.PATHSEPERATOR) + 1, dir.Length - (dir.LastIndexOf(Constants.PATHSEPERATOR) + 1));
            string filename = String.Format("{0}_{1}{2}", name, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            fullpath = Path.Combine(BackupPath, filename);

            // get zip intern dir
            zipPath = dir.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", "");

            // add _(x) to the filename if the file already exists.
            int i = 1;
            while (File.Exists(fullpath))
                fullpath = Path.GetDirectoryName(fullpath) + Constants.PATHSEPERATOR + Path.GetFileNameWithoutExtension(fullpath) + "_" + i++.ToString("00") + Path.GetExtension(fullpath);

            return name;
        }

        /// <summary>
        /// Creates the display name of a backup file.
        /// </summary>
        /// <param name="fullpath">Full path of the backup file.</param>
        /// <returns>The display name of a backup file.</returns>
        private string GetBackupDisplayName(string fullpath)
        {
            string filename = Path.GetFileName(fullpath);
            string dispTxt = filename;
            if (filename != null && filename.StartsWith("AutoBackup_"))
            {
                FileInfo fi = GetFileInfoFromFilename(filename);
                dispTxt = fi.DateTime.ToString("MMM dd. yyyy HH:mm") + " - ";
                dispTxt += filename.Substring(0, filename.IndexOf("_"));
                string temp = filename.Substring(filename.IndexOf("_")+1);
                dispTxt += " " + temp.Substring(0, temp.IndexOf("_"));
            }
            else if (filename != null && filename.Contains("_"))
            {
                string[] temp = filename.Split('_');
                if (temp.Count() >= 3 && temp[1].Count() == 8 && temp[2].Count() >= 4)
                {
                    int y = 0;
                    int m = 0;
                    int d = 0;
                    int h = 0;
                    int min = 0;

                    if (int.TryParse(temp[1].Substring(0, 4), out y) && int.TryParse(temp[1].Substring(4, 2), out m) &&
                        int.TryParse(temp[1].Substring(6, 2), out d) && int.TryParse(temp[2].Substring(0, 2), out h) &&
                        int.TryParse(temp[2].Substring(2, 2), out min))
                    {
                        DateTime time = new DateTime(y, m, d, h, min, 00);
                        dispTxt = time.ToString("MMM dd. yyyy HH:mm") + " - " + temp[0];
                    }
                }
            }
            return dispTxt;
        }

        /// <summary>
        /// Validates the backup directory.
        /// </summary>
        /// <param name="dir">The directory to check for existance</param>
        /// <returns>True if hte passed path exists.</returns>
        private bool ValidBackupDirectory(string dir)
        {
            bool result = false;
            try
            {
                if (dir != string.Empty && Directory.Exists(dir))
                    result = true;
            }
            catch (Exception ex)
            {
                string NoWarningPLS = ex.Message;
            }

            if (!result)
                MessageBox.Show(this, Constants.MESSAGE_BACKUP_SELECT_FOLDER);

            return true;
        }

        /// <summary>
        /// Sets the Backup tab controls enable state to the passes value.
        /// </summary>
        /// <param name="enable">State to set the Backup Tab controls to.</param>
        public void SetBackupEnableState(bool enable)
        {
            btnBackup.Enabled = enable;
            btnBackupSaves.Enabled = enable;
            btnRemoveBackup.Enabled = enable;
            btnReinstallBackup.Enabled = (tvBackup.FocusedNode != null);
            btnClearBackups.Enabled = enable;
            tvBackup.Enabled = enable;
        }

        /// <summary>
        /// Creates a auto backup of the saves folder.
        /// </summary>
        private void DoAutoBackup()
        {
            int count = 1;
            string name = string.Format("AutoBackup_{0}_{1}{2}", count, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            string dir = MainForm.GetPath(KSP_Paths.Saves);
            string zipPath = dir.Replace(MainForm.GetPath(KSP_Paths.KSPRoot) + "\\", "");

            if (Directory.Exists(BackupPath))
            {
                FileInfo fileInfo = null;
                string[] autoBackups = Directory.EnumerateFiles(BackupPath, "AutoBackup_*" + Constants.EXT_ZIP).ToArray();
                List<FileInfo> list = new List<FileInfo>();
                foreach (string file in autoBackups)
                {
                    FileInfo fi = GetFileInfoFromFilename(file);
                    if (fi == null) continue;

                    list.Add(fi);
                    list.Sort(delegate(FileInfo a, FileInfo b) { return a.Number.CompareTo(b.Number); });
                }

                while (list.Count > MainForm.Options.MaxBackupFiles)
                    list.RemoveAt(list.Count - 1);

                if (list.Count == MainForm.Options.MaxBackupFiles)
                {
                    list.Sort(delegate(FileInfo a, FileInfo b)
                    {
                        if (b.DateTime.CompareTo(a.DateTime) == 0)
                            return b.Number.CompareTo(a.Number);
                        
                        return b.DateTime.CompareTo(a.DateTime);
                    });

                    fileInfo = list.Last();
                    string fname = string.Format("AutoBackup_{0}_{1}{2}", fileInfo.Number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                    if (File.Exists(fileInfo.FullPath))
                    {
                        File.Delete(fileInfo.FullPath);
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                    }
                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, fileInfo.Number);
                }
                else if (list.Count > 0)
                {
                    int number = 1;
                    string fname = string.Empty;
                    for (; number <= MainForm.Options.MaxBackupFiles; ++number)
                    {
                        bool found = false;
                        string key = string.Format("AutoBackup_{0}", number);
                        foreach (FileInfo fi in list)
                        {
                            var fileName = Path.GetFileName(fi.FullPath);
                            if (fileName == null || !fileName.StartsWith(key)) 
                                continue;

                            found = true;
                            break;
                        }

                        if (found)
                            continue;

                        fname = string.Format("AutoBackup_{0}_{1}{2}", number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                        break;
                    }

                    if (fname == string.Empty)
                    {
                        fileInfo = list.Last();
                        number = fileInfo.Number;
                        fname = string.Format("AutoBackup_{0}_{1}{2}", number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                        if (File.Exists(fileInfo.FullPath))
                        {
                            File.Delete(fileInfo.FullPath);
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                        }
                    }

                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, number);
                }
                
                if (fileInfo == null)
                {
                    string fname = string.Format("AutoBackup_{0}_{1}{2}", 1, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, 1);
                }

                if (File.Exists(fileInfo.FullPath))
                {
                    File.Delete(fileInfo.FullPath);
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                }

                name = Path.GetFileName(fileInfo.FullPath);

                if (name != null)
                {
                    MainForm.AddInfo("Autobackup started.");
                    BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
                }
            }
            else
            {
                if (BackupPath != Constants.MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX)
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_BACKUP_FOLDER_NOT_FOUND, BackupPath));
            }
        }

        /// <summary>
        /// Creates a FileInfo from a file.
        /// </summary>
        /// <param name="file">Full path to the file to create the FileInfo of.</param>
        /// <returns>The FileInfo of the passed file.</returns>
        private static FileInfo GetFileInfoFromFilename(string file)
        {
            FileInfo fi = null;
            try
            {
                string filename = Path.GetFileNameWithoutExtension(file);

                int index = filename.IndexOf("_");
                string temp = filename.Substring(index + 1);

                index = temp.IndexOf("_");
                string num = temp.Substring(0, index);
                int number = int.Parse(num);
                temp = temp.Substring(index + 1);

                index = temp.IndexOf("_");
                string date = temp.Substring(0, index);
                string time = temp.Substring(index + 1);
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(4, 2));
                int day = int.Parse(date.Substring(6, 2));
                int hour = int.Parse(time.Substring(0, 2));
                int min = int.Parse(time.Substring(2, 2));

                DateTime dateTime = new DateTime(year, month, day, hour, min, 0);

                fi = new FileInfo(file, dateTime, number);
            }
            catch (Exception ex)
            {
                string NoWaringPLS = ex.Message;
            }

            return fi;
        }

        #endregion

        class FileInfo
        {
            private string mFullPath = string.Empty;
            private DateTime mDateTime = DateTime.MinValue;
            private int mNumber = -1;

            public string FullPath
            {
                get
                {
                    return mFullPath;
                }
                set
                {
                    mFullPath = value;
                }
            }

            public DateTime DateTime
            {
                get
                {
                    return mDateTime;
                }
                set
                {
                    mDateTime = value;
                }
            }

            public int Number
            {
                get
                {
                    return mNumber;
                }
                set
                {
                    mNumber = value;
                }
            }

            public FileInfo(string fullPath, DateTime dateTime, int number)
            {
                FullPath = fullPath;
                DateTime = dateTime;
                Number = number;
            }
        }
    }
}
