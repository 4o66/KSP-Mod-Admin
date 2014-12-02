namespace KSPModAdmin
{
    /// <summary>
    /// All constants used for the KSP Mod Admin.
    /// </summary>
    public class Constants
    {
        // General
        public static string[] RELEASE_VERSIONS = new string[] { "1.0.0", 
                                                                 "1.1.0", "1.1.1", 
                                                                 "1.2.0", "1.2.1", "1.2.2", "1.2.3", "1.2.4", "1.2.5", "1.2.6", 
                                                                 "1.3.0", "1.3.1", "1.3.2", "1.3.3", "1.3.4", "1.3.5", "1.3.6", "1.3.7", "1.3.8", "1.3.9", "1.3.10", "1.3.11", 
                                                                 "1.4.0", "1.4.0.5" };

        public const string DOWNLOAD_FILENAME_TEMPLATE = "KSPModAdmin-v{0}.zip";

        public const string SERVICE_DOWNLOAD_LINK = "http://www.services.mactee.de/KSP/getKSP_MA_Zip.php5";
        public const string SERVICE_ADMIN_VERSION = "http://www.services.mactee.de/KSP/getKSP_MA_Version.php5";

        public const string PATHSEPERATOR = "\\";

        public const string TRUE = "true";

        public const string TXT_BTN_UPDATE_UPDATE = "Check for KSP MA updates";
        public const string TXT_BTN_UPDATE_MODUPDATE = "Check for mod updates";
        public const string TXT_BTN_UPDATE_UPDATING = "Checking ...";
        public const string TXT_BTN_UPDATE_LIST_UPDATE = "Update";
        public const string TXT_BTN_UPDATE_LIST_ABORT = "Abort";

        public const string FOLDER_DOWNLOADS = "Downloads";

        public const string DEFAULT_TEXT_DESTINATION_PATH_CONTEXTMENU = "<No path selected>";

        public const int FLAG_WIDTH = 256;
        public const int FLAG_HEIGHT = 160;

        public const int WIN_MIN_WIDTH = 480;
        public const int WIN_MIN_HEIGHT = 650;

        // KSP folders
        public const string KSP_ROOT = "ksp_root";
        public const string PARTS = "Parts";
        public const string PLUGINS = "Plugins";
        public const string PLUGINDATA = "PluginData";
        public const string RESOURCES = "Resources";
        public const string INTERNALS = "Internals";
        public const string SHIPS = "Ships";
        public const string VAB = "VAB";
        public const string SPH = "SPH";
        public const string KSPDATA = "KSP_Data";
        public const string SAVES = "saves";
        public const string GAMEDATA = "GameData";
        public static string[] KSPFolders
        {
            get
            {
                return new string[] { Constants.KSP_ROOT, Constants.PARTS, Constants.PLUGINS, Constants.PLUGINDATA, Constants.RESOURCES, 
                                      Constants.INTERNALS, Constants.SHIPS, Constants.KSPDATA, Constants.SAVES, Constants.GAMEDATA };
            }
        }

        public const string MYFLAGS_GROUP = "MyFlags";
        public const string MYFLAGS = "myflags";
        public const string FLAGS = "Flags";

        // Files
        public const string KSP_EXE = "KSP.exe";
        public const string KSP_X64_EXE = "KSP_x64.exe";
        public const string CONFIG_FILE = "KSPModAdmin.cfg";

        // XMLNode names
        public const string ROOTNODE = "ModAdminConfig";
        public const string ROOT = "Root";
        public const string VERSION = "Version";
        public const string MESSAGE = "Message";
        public const string GENERAL = "General";
        public const string KSP_PATH = "KSP_Path";
        public const string KNOWN_KSP_PATHS = "Known_KSP_Paths";
        public const string KNOWN_KSP_PATH = "Known_KSP_Path";
        public const string CHECKFORUPDATES = "checkforupdates";
        public const string LASTMODUPDATETRY = "LastModUpdateTry";
        public const string MODUPDATEINTERVAL = "ModUpdateInterval";
        public const string MODUPDATEBEHAVIOR = "ModUpdateBehavior";
        public const string BACKUP_PATH = "BackupPath";
        public const string DOWNLOAD_PATH = "DownloadPath";
        public const string NAME = "Name";
        public const string KEY = "key";
        public const string FULLPATH = "FullPath";
        public const string CHECKED = "Checked";
        public const string NODETYPE = "NodeType";
        public const string DESTINATION = "Destination";
        public const string INSTALLROOTKEY = "InstallRootKey";
        public const string MODS = "Mods";
        public const string MOD = "Mod";
        public const string MOD_ENTRY = "ModEntry";
        public const string POSTDOWNLOADACTION = "PostDownloadAction";
        public const string OVERRRIDE = "Override";
        public const string VALUE = "Value";
        public const string AUTOBACKUPSETTING = "AutoBackupSetting";
        public const string BACKUPONSTARTUP = "BackupOnStartup";
        public const string BACKUPONKSPLAUNCH = "BackupOnKSPLaunch";
        public const string BACKUPONINTERVAL = "BackupOnInterval";
        public const string BACKUPINTERVAL = "BackupInterval";
        public const string MAXBACKUPFILES = "MaxBackupFiles";
        public const string ADDDATE = "AddDate";
        public const string NOTE = "Note";
        public const string PRODUCTID = "ProductID";
        public const string CREATIONDATE = "CreationDate";
        public const string AUTHOR = "Author";
        public const string RATING = "Rating";
        public const string DOWNLOADS = "Downloads";
        public const string MODURL = "ModURL";
        public const string FORUMURL = "ForumURL";
        public const string CURSEFORGEURL = "CurseForgeURL";
        public const string VERSIONCONTROL = "VersionControl";
        public const string BACKUPNOTES = "BackupNotes";
        public const string BACKUPNOTE = "BackupNote";
        public const string FILENAME = "Filename";
        public const string KSPSTARTUPOPTIONS = "KSPStartupOptions";
        public const string BORDERLESSWINDOW = "BorderlessWindow";
        public const string ISFILE = "IsFile";
        public const string INSTALL = "Install";
        public const string INSTALLDIR = "InstallDir";
        public const string CONFLICTDETECTIONOPTIONS = "ConflictDetectionOptions";
        public const string ONOFF = "OnOff";
        public const string FOLDERCONFLICTDETECTION = "FolderCOnflictDetection";
        public const string SHOWCONFLICTSOLVER = "ShowConflictSolver";
        public const string NODECOLORS = "NodeColors";
        public const string COLOR = "Color";
        public const string DESTINATIONDETECTED = "DestinationDetection";
        public const string DESTINATIONMISSING = "DetstinationMissing";
        public const string DESTINATIONCONFLICT = "DestinationConflict";
        public const string MODINSTALLED = "ModInstalled";
        public const string MODARCHIVEMISSING = "ModArchiveMissing";
        public const string MODOUTDATED = "ModOutdated";


        // Form related
        public const string POSITION = "Position";
        public const string X = "x";
        public const string Y = "y";
        public const string SIZE = "Size";
        public const string WIDTH = "Width";
        public const string HEIGHT = "Height";
        public const string WINDOWSTATE = "WindowState";
        public const string MINIM = "minimized";
        public const string MAXIM = "maximized";
        public const string MODSELECTIONCOLUMNS = "ModSelectionColumns";
        public const string COLUMN = "Column";
        public const string ID = "ID";

        // File extensions
        public const string EXT_ZIP = ".zip";
        public const string EXT_RAR = ".rar";
        public const string EXT_7ZIP = ".7z";
        public const string EXT_CRAFT = ".craft";
        public const string EXT_KSP_SAVE = ".sfs";
        public const string EXT_CFG = ".cfg";

        // Filter
        public const string ZIP_FILTER1 = "Zip-Files|*.zip";
        public const string ARCHIVE_FILTER = "All|*.zip;*.7z;*.rar;*.craft|Archives|*.zip;*.7z;*.rar|Zip-Files|*.zip|7Zip-Files|*.7z|Rar-Files|*.rar";
        public const string ADD_DLG_FILTER = ARCHIVE_FILTER + "|Craft|*.craft";
        public const string IMAGE_FILTER = "Image files|*.jpeg;*.jpg;*.png;*.gif|JPEG Files (*.jpeg)|*.jpeg;|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
        public const string MODPACK_FILTER = "ModPack|*.modpack";

        // Messages
        public const string MESSAGE_SELECT_KSP_FOLDER_TXTBOX = "                         Select the KSP install folder";
        public const string MESSAGE_SELECT_KSP_FOLDER = "Please select the KSP install folder.";
        public const string MESSAGE_SELECT_KSP_FOLDER_FIRST = "Please select a KSP install folder first.";
        public const string MESSAGE_SELECT_LIST = "Select a list first please.";
        public const string MESSAGE_UNEXPECTED_FOLDERSTRUCTURE = "Folder did not have the expected structure.";

        public const string MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX = "                                                             Select a backup folder ------>";
        public const string MESSAGE_BACKUP_SELECT_FOLDER = "Please select a backup folder.";
        public const string MESSAGE_BACKUP_SELECT_SRC_FOLDER = "Please select a folder to backup.";
        public const string MESSAGE_BACKUP_SRC_MISSING = "Select a Backup first!";
        public const string MESSAGE_BACKUP_DELETE_QUESTION = "Are you sure to delete the backup \"{0}\"?";
        public const string MESSAGE_BACKUP_DELETE_ALL_QUESTION = "Are you sure to delete all backups?";
        public const string MESSAGE_BACKUP_DELETED = "Backup deleted: \"{0}\"";
        public const string MESSAGE_BACKUP_NOT_FOUND = "Backup file not found! \"{0}\"";
        public const string MESSAGE_BACKUP_REINSTALLED = "Backup reinstalled! \"{0}\"";
        public const string MESSAGE_BACKUP_FOLDER_NOT_FOUND = "Backup folder not found! \"{0}\"";
        public const string MESSAGE_BACKUP_COMPLETE = "Backup of \"{0}\" complete.";
        public const string MESSAGE_BACKUP_SRC_FOLDER_NOT_FOUND = "Folder to backup not found! \"{0}\"";
        public const string MESSAGE_BACKUP_DELETED_ERROR = "Can not delete backup \"{0}\".";
        public const string MESSAGE_BACKUP_ERROR = "Error during backup creation: \"{0}\"";
        public const string MESSAGE_BACKUP_CREATION_ERROR = "Error during backup creation.";
        public const string MESSAGE_BACKUP_LOAD_ERROR = "Error during backup loading: \"{0}\"";

        public const string MESSAGE_SELECT_MOD = "Select a mod please.";
        public const string MESSAGE_MOD_ZIP_NOT_FOUND = "Mod Zip-File not found \"{0}\"";
        public const string MESSAGE_MOD_ERROR_WHILE_READ_ZIP = "Error while reading Mod Zip-File \"{0}\"\r\nError message: {1}";
        public const string MESSAGE_MOD_ERROR_WHILE_UPDATING = "Error while updating Mod \"{0}\"\r\nError message: {1}";
        public const string MESSAGE_MOD_ADDED = "Mod added: \"{0}\"";
        public const string MESSAGE_MOD_ALREADY_ADDED = "Mod already added: \"{0}\"";
        public const string MESSAGE_MOD_UPDATED = "Mod updated: \"{0}\"";
        public const string MESSAGE_MOD_DELETE_QUESTION = "Are you sure to deinstall the selected Mod(s)?";
        public const string MESSAGE_MOD_DELETE_ALL_QUESTION = "Are you sure to deinstall all Mod?";
        public const string MESSAGE_MOD_DONE = "Done with \"{0}\"";

        public const string MESSAGE_ROOT_IDENTIFIED = "Root of Zip identified as \"{0}\".";
        public const string MESSAGE_ROOT_NOT_FOUND = "Root of Zip not found! \"{0}\".";

        public const string MESSAGE_DIR_ADDED = "Directory added \"{0}\".";
        public const string MESSAGE_DIR_CREATED = "Directory created \"{0}\".";
        public const string MESSAGE_DIR_DELETED = "Directory deleted \"{0}\".";
        public const string MESSAGE_DIR_CREATED_ERROR = "Can not create directory \"{0}\".";
        public const string MESSAGE_DIR_DELETED_ERROR = "Can not delete directory \"{0}\".";

        public const string MESSAGE_FOLDER_NOT_FOUND = "Folder not found. \"{0}\"";
        public const string MESSAGE_SOURCE_NODE_NOT_FOUND = "Source node not found!\r\nCan not set destination paths.";

        public const string MESSAGE_FILE_ADDED = "File added \"{0}\".";
        public const string MESSAGE_FILE_DELETED = "File deleted \"{0}\".";
        public const string MESSAGE_FILE_EXTRACTED = "File extracted \"{0}\".";
        public const string MESSAGE_FILE_EXTRACTED_ERROR = "Can not extract file \"{0}\".";
        public const string MESSAGE_FILE_DELETED_ERROR = "Can not delete file \"{0}\".";

        public const string MESSAGE_UPDATE_MISMATCH_STRUCTRUE = "Structure mismatch! Updating aborded!";

        public const string MESSAGE_NEW_VERSION_TITLE = "New version available.";
        public const string MESSAGE_NEW_VERSION = "New version {0} available.";

        public const string MESSAGE_SELECT_DEST = "Please select a destination folder fist.";
        public const string MESSAGE_SELECT_SCOURCE = "Please select a source folder fist.";
    }
}