namespace KSPModAdmin.Views
{
    partial class ucModSelection
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucModSelection));
            this.ttModTab = new System.Windows.Forms.ToolTip(this.components);
            this.cbBorderlessWin = new System.Windows.Forms.CheckBox();
            this.btnLunchKSP = new System.Windows.Forms.Button();
            this.cbOverride = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnScanGameData = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnExecuteMod = new System.Windows.Forms.Button();
            this.btnExecuteAllMods = new System.Windows.Forms.Button();
            this.ctxtMenuMods = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMod_Destination = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_Path = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_SelectDestination = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_ResetDestination = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_ZipSource = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_SourcePath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_SelectNewSourceFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_SelectNewSourceFolderAllMods = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_CreateZipOfInstalledItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_DefaultName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_chooseName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_OpenModFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_ProceedSelectedMod = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_CheckForModUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_SolveConflicts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_VisitSpaceport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_VisitForum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_VisitCurseForge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_EditModInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_CopyModInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_ExpandAllNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_CollapsNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_ResetCheckedStates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_CheckAllMods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMod_UncheckAllMods = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMod_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.ilMods = new System.Windows.Forms.ImageList(this.components);
            this.grpModSelection = new System.Windows.Forms.GroupBox();
            this.btnImExport = new System.Windows.Forms.Button();
            this.tvModSelection = new KSPModAdmin.Utils.CommonTools.TreeListViewEx();
            this.grpProceed = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pBarModProcessing = new System.Windows.Forms.ProgressBar();
            this.ctxtMenuMods.SuspendLayout();
            this.grpModSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvModSelection)).BeginInit();
            this.grpProceed.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbBorderlessWin
            // 
            this.cbBorderlessWin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBorderlessWin.AutoSize = true;
            this.cbBorderlessWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBorderlessWin.Location = new System.Drawing.Point(24, 471);
            this.cbBorderlessWin.Name = "cbBorderlessWin";
            this.cbBorderlessWin.Size = new System.Drawing.Size(114, 17);
            this.cbBorderlessWin.TabIndex = 7;
            this.cbBorderlessWin.Text = "Borderless window";
            this.ttModTab.SetToolTip(this.cbBorderlessWin, "If checked the \"Launch KSP\" button will start KSP with a flat borderless window.");
            this.cbBorderlessWin.UseVisualStyleBackColor = true;
            // 
            // btnLunchKSP
            // 
            this.btnLunchKSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLunchKSP.Enabled = false;
            this.btnLunchKSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLunchKSP.Location = new System.Drawing.Point(10, 429);
            this.btnLunchKSP.Name = "btnLunchKSP";
            this.btnLunchKSP.Size = new System.Drawing.Size(429, 40);
            this.btnLunchKSP.TabIndex = 11;
            this.btnLunchKSP.Text = "Launch Kerbal Space Program";
            this.ttModTab.SetToolTip(this.btnLunchKSP, "Starts Kerbal Space Program.");
            this.btnLunchKSP.UseVisualStyleBackColor = true;
            this.btnLunchKSP.Click += new System.EventHandler(this.btnLunchKSP_Click);
            // 
            // cbOverride
            // 
            this.cbOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOverride.AutoSize = true;
            this.cbOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOverride.Location = new System.Drawing.Point(20, 21);
            this.cbOverride.Name = "cbOverride";
            this.cbOverride.Size = new System.Drawing.Size(125, 17);
            this.cbOverride.TabIndex = 7;
            this.cbOverride.Text = "Override existing files";
            this.ttModTab.SetToolTip(this.cbOverride, "Flag for override mode. When active existing files will be replaced");
            this.cbOverride.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::KSPModAdmin.Properties.Resources.component_add;
            this.btnAdd.Location = new System.Drawing.Point(10, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 24);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnAdd, "Opens the \"Add mod\" dialog, where you can add a mod with its HD path, Spaceport U" +
        "RL or KSP Forum URL.");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnScanGameData
            // 
            this.btnScanGameData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanGameData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanGameData.Image = global::KSPModAdmin.Properties.Resources.components_find;
            this.btnScanGameData.Location = new System.Drawing.Point(362, 17);
            this.btnScanGameData.Name = "btnScanGameData";
            this.btnScanGameData.Size = new System.Drawing.Size(80, 24);
            this.btnScanGameData.TabIndex = 4;
            this.btnScanGameData.Text = "Scan";
            this.btnScanGameData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScanGameData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnScanGameData, "Scans the GameData folder for mods that are not in the selection tree and adds th" +
        "em");
            this.btnScanGameData.UseVisualStyleBackColor = true;
            this.btnScanGameData.Click += new System.EventHandler(this.btnScanGameData_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::KSPModAdmin.Properties.Resources.components_delete;
            this.btnClear.Location = new System.Drawing.Point(182, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(85, 24);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Remove All";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnClear, "Deletes and removes all mods from the selection tree.");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = global::KSPModAdmin.Properties.Resources.component_delete;
            this.btnRemove.Location = new System.Drawing.Point(96, 17);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(80, 24);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnRemove, "Deletes and removes the selected mod(s) from the selection tree.");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnExecuteMod
            // 
            this.btnExecuteMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteMod.Image = global::KSPModAdmin.Properties.Resources.component_gearwheel;
            this.btnExecuteMod.Location = new System.Drawing.Point(3, 3);
            this.btnExecuteMod.Name = "btnExecuteMod";
            this.btnExecuteMod.Size = new System.Drawing.Size(212, 39);
            this.btnExecuteMod.TabIndex = 6;
            this.btnExecuteMod.Text = "Selected Mods";
            this.btnExecuteMod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecuteMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnExecuteMod, "Proceeds (installs/deinstalls) the selected (blue highlighted) mod(s).");
            this.btnExecuteMod.UseVisualStyleBackColor = true;
            this.btnExecuteMod.Click += new System.EventHandler(this.btnExecuteMod_Click);
            // 
            // btnExecuteAllMods
            // 
            this.btnExecuteAllMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteAllMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteAllMods.Image = global::KSPModAdmin.Properties.Resources.components_gearwheel;
            this.btnExecuteAllMods.Location = new System.Drawing.Point(221, 3);
            this.btnExecuteAllMods.Name = "btnExecuteAllMods";
            this.btnExecuteAllMods.Size = new System.Drawing.Size(212, 39);
            this.btnExecuteAllMods.TabIndex = 6;
            this.btnExecuteAllMods.Text = "All Mods";
            this.btnExecuteAllMods.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecuteAllMods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnExecuteAllMods, "Proceeds (installs/deinstalls) all mods in the selection tree.");
            this.btnExecuteAllMods.UseVisualStyleBackColor = true;
            this.btnExecuteAllMods.Click += new System.EventHandler(this.btnExecuteAllMods_Click);
            // 
            // ctxtMenuMods
            // 
            this.ctxtMenuMods.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMod_Destination,
            this.tsmiMod_ZipSource,
            this.tsmiMod_OpenModFolder,
            this.toolStripSeparator8,
            this.tsmiMod_ProceedSelectedMod,
            this.toolStripSeparator3,
            this.tsmiMod_CheckForModUpdate,
            this.tsmiMod_SolveConflicts,
            this.tsmiMod_VisitSpaceport,
            this.tsmiMod_VisitForum,
            this.tsmiMod_VisitCurseForge,
            this.toolStripSeparator9,
            this.tsmiMod_EditModInfos,
            this.tsmiMod_CopyModInfos,
            this.toolStripSeparator1,
            this.tsmiMod_ExpandAllNodes,
            this.tsmiMod_CollapsNodes,
            this.toolStripSeparator4,
            this.tsmiMod_ResetCheckedStates,
            this.tsmiMod_CheckAllMods,
            this.tsmiMod_UncheckAllMods,
            this.toolStripSeparator5,
            this.tsmiMod_Search});
            this.ctxtMenuMods.Name = "contextMenuStrip1";
            this.ctxtMenuMods.Size = new System.Drawing.Size(223, 414);
            this.ctxtMenuMods.Opening += new System.ComponentModel.CancelEventHandler(this.ctxtMenuModSelection_Opening);
            // 
            // tsmiMod_Destination
            // 
            this.tsmiMod_Destination.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMod_Path,
            this.toolStripSeparator2,
            this.tsmiMod_SelectDestination,
            this.tsmiMod_ResetDestination});
            this.tsmiMod_Destination.Name = "tsmiMod_Destination";
            this.tsmiMod_Destination.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_Destination.Text = "Destination";
            // 
            // tsmiMod_Path
            // 
            this.tsmiMod_Path.Enabled = false;
            this.tsmiMod_Path.Name = "tsmiMod_Path";
            this.tsmiMod_Path.Size = new System.Drawing.Size(192, 22);
            this.tsmiMod_Path.Text = "<No path selected>";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // tsmiMod_SelectDestination
            // 
            this.tsmiMod_SelectDestination.Name = "tsmiMod_SelectDestination";
            this.tsmiMod_SelectDestination.Size = new System.Drawing.Size(192, 22);
            this.tsmiMod_SelectDestination.Text = "Select new destination";
            this.tsmiMod_SelectDestination.Click += new System.EventHandler(this.tsmiMod_SelectDestination_Click);
            // 
            // tsmiMod_ResetDestination
            // 
            this.tsmiMod_ResetDestination.Name = "tsmiMod_ResetDestination";
            this.tsmiMod_ResetDestination.Size = new System.Drawing.Size(192, 22);
            this.tsmiMod_ResetDestination.Text = "Reset destination";
            this.tsmiMod_ResetDestination.Click += new System.EventHandler(this.tsmiMod_ResetDestination_Click);
            // 
            // tsmiMod_ZipSource
            // 
            this.tsmiMod_ZipSource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMod_SourcePath,
            this.toolStripSeparator7,
            this.tsmiMod_SelectNewSourceFolder,
            this.tsmiMod_SelectNewSourceFolderAllMods,
            this.toolStripSeparator6,
            this.tsmiMod_CreateZipOfInstalledItems});
            this.tsmiMod_ZipSource.Name = "tsmiMod_ZipSource";
            this.tsmiMod_ZipSource.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_ZipSource.Text = "Zip Source";
            // 
            // tsmiMod_SourcePath
            // 
            this.tsmiMod_SourcePath.Enabled = false;
            this.tsmiMod_SourcePath.Name = "tsmiMod_SourcePath";
            this.tsmiMod_SourcePath.Size = new System.Drawing.Size(294, 22);
            this.tsmiMod_SourcePath.Text = "<Zip not found>";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(291, 6);
            // 
            // tsmiMod_SelectNewSourceFolder
            // 
            this.tsmiMod_SelectNewSourceFolder.Name = "tsmiMod_SelectNewSourceFolder";
            this.tsmiMod_SelectNewSourceFolder.Size = new System.Drawing.Size(294, 22);
            this.tsmiMod_SelectNewSourceFolder.Text = "Select new source folder for selected mod";
            this.tsmiMod_SelectNewSourceFolder.Click += new System.EventHandler(this.tsmiMod_SelectNewSourceFolder_Click);
            // 
            // tsmiMod_SelectNewSourceFolderAllMods
            // 
            this.tsmiMod_SelectNewSourceFolderAllMods.Name = "tsmiMod_SelectNewSourceFolderAllMods";
            this.tsmiMod_SelectNewSourceFolderAllMods.Size = new System.Drawing.Size(294, 22);
            this.tsmiMod_SelectNewSourceFolderAllMods.Text = "Select new source folder for all mods";
            this.tsmiMod_SelectNewSourceFolderAllMods.Click += new System.EventHandler(this.tsmiMod_SelectNewSourceFolderForAllMods_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(291, 6);
            // 
            // tsmiMod_CreateZipOfInstalledItems
            // 
            this.tsmiMod_CreateZipOfInstalledItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMod_DefaultName,
            this.tsmiMod_chooseName});
            this.tsmiMod_CreateZipOfInstalledItems.Name = "tsmiMod_CreateZipOfInstalledItems";
            this.tsmiMod_CreateZipOfInstalledItems.Size = new System.Drawing.Size(294, 22);
            this.tsmiMod_CreateZipOfInstalledItems.Text = "Create Zip of installed items";
            // 
            // tsmiMod_DefaultName
            // 
            this.tsmiMod_DefaultName.Name = "tsmiMod_DefaultName";
            this.tsmiMod_DefaultName.Size = new System.Drawing.Size(170, 22);
            this.tsmiMod_DefaultName.Text = "with default name";
            this.tsmiMod_DefaultName.Click += new System.EventHandler(this.tsmiMod_CreateZipOfInstalledItems_Click);
            // 
            // tsmiMod_chooseName
            // 
            this.tsmiMod_chooseName.Name = "tsmiMod_chooseName";
            this.tsmiMod_chooseName.Size = new System.Drawing.Size(170, 22);
            this.tsmiMod_chooseName.Text = "choose name";
            this.tsmiMod_chooseName.Click += new System.EventHandler(this.tsmiMod_ChooseName_Click);
            // 
            // tsmiMod_OpenModFolder
            // 
            this.tsmiMod_OpenModFolder.Name = "tsmiMod_OpenModFolder";
            this.tsmiMod_OpenModFolder.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_OpenModFolder.Text = "Open mod install folder";
            this.tsmiMod_OpenModFolder.Click += new System.EventHandler(this.tsmiMod_OpenModFolder_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_ProceedSelectedMod
            // 
            this.tsmiMod_ProceedSelectedMod.Name = "tsmiMod_ProceedSelectedMod";
            this.tsmiMod_ProceedSelectedMod.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_ProceedSelectedMod.Text = "Proceed selected mod";
            this.tsmiMod_ProceedSelectedMod.Click += new System.EventHandler(this.btnExecuteMod_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_CheckForModUpdate
            // 
            this.tsmiMod_CheckForModUpdate.Name = "tsmiMod_CheckForModUpdate";
            this.tsmiMod_CheckForModUpdate.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_CheckForModUpdate.Text = "Check for mod update";
            this.tsmiMod_CheckForModUpdate.Click += new System.EventHandler(this.tsmiMod_CheckForModUpdate_Click);
            // 
            // tsmiMod_SolveConflicts
            // 
            this.tsmiMod_SolveConflicts.Name = "tsmiMod_SolveConflicts";
            this.tsmiMod_SolveConflicts.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_SolveConflicts.Text = "Solve conflicts";
            this.tsmiMod_SolveConflicts.Click += new System.EventHandler(this.tsmiMod_SolveConflicts_Click);
            // 
            // tsmiMod_VisitSpaceport
            // 
            this.tsmiMod_VisitSpaceport.Name = "tsmiMod_VisitSpaceport";
            this.tsmiMod_VisitSpaceport.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_VisitSpaceport.Text = "Visit Spacport site of mod";
            this.tsmiMod_VisitSpaceport.Click += new System.EventHandler(this.tsmiMod_VisitSpaceport_Click);
            // 
            // tsmiMod_VisitForum
            // 
            this.tsmiMod_VisitForum.Name = "tsmiMod_VisitForum";
            this.tsmiMod_VisitForum.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_VisitForum.Text = "Visit Forum site of mod";
            this.tsmiMod_VisitForum.Click += new System.EventHandler(this.tsmiMod_VistForum_Click);
            // 
            // tsmiMod_VisitCurseForge
            // 
            this.tsmiMod_VisitCurseForge.Name = "tsmiMod_VisitCurseForge";
            this.tsmiMod_VisitCurseForge.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_VisitCurseForge.Text = "Visit CurseForge site of mod";
            this.tsmiMod_VisitCurseForge.Click += new System.EventHandler(this.tsmiMod_VistCurseForge_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_EditModInfos
            // 
            this.tsmiMod_EditModInfos.Name = "tsmiMod_EditModInfos";
            this.tsmiMod_EditModInfos.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_EditModInfos.Text = "Edit ModInfos";
            this.tsmiMod_EditModInfos.Click += new System.EventHandler(this.tsmiMod_EditModInfos_Click);
            // 
            // tsmiMod_CopyModInfos
            // 
            this.tsmiMod_CopyModInfos.Name = "tsmiMod_CopyModInfos";
            this.tsmiMod_CopyModInfos.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_CopyModInfos.Text = "Copy ModInfos";
            this.tsmiMod_CopyModInfos.Click += new System.EventHandler(this.tsmiMod_CopyModInfos_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_ExpandAllNodes
            // 
            this.tsmiMod_ExpandAllNodes.Name = "tsmiMod_ExpandAllNodes";
            this.tsmiMod_ExpandAllNodes.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_ExpandAllNodes.Text = "Expand all mod nodes";
            this.tsmiMod_ExpandAllNodes.Click += new System.EventHandler(this.tsmiMod_ExpandAllNodes_Click);
            // 
            // tsmiMod_CollapsNodes
            // 
            this.tsmiMod_CollapsNodes.Name = "tsmiMod_CollapsNodes";
            this.tsmiMod_CollapsNodes.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_CollapsNodes.Text = "Collapse all mod nodes";
            this.tsmiMod_CollapsNodes.Click += new System.EventHandler(this.tsmiMod_CollapsNodes_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_ResetCheckedStates
            // 
            this.tsmiMod_ResetCheckedStates.Name = "tsmiMod_ResetCheckedStates";
            this.tsmiMod_ResetCheckedStates.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_ResetCheckedStates.Text = "Refresh checked states";
            this.tsmiMod_ResetCheckedStates.Click += new System.EventHandler(this.tsmiMod_ResetCheckedStates_Click);
            // 
            // tsmiMod_CheckAllMods
            // 
            this.tsmiMod_CheckAllMods.Name = "tsmiMod_CheckAllMods";
            this.tsmiMod_CheckAllMods.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_CheckAllMods.Text = "Check all mods";
            this.tsmiMod_CheckAllMods.Click += new System.EventHandler(this.tsmiMod_SelectAllMods_Click);
            // 
            // tsmiMod_UncheckAllMods
            // 
            this.tsmiMod_UncheckAllMods.Name = "tsmiMod_UncheckAllMods";
            this.tsmiMod_UncheckAllMods.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_UncheckAllMods.Text = "Uncheck all mods";
            this.tsmiMod_UncheckAllMods.Click += new System.EventHandler(this.tsmiMod_UnselectAllMods_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiMod_Search
            // 
            this.tsmiMod_Search.Name = "tsmiMod_Search";
            this.tsmiMod_Search.Size = new System.Drawing.Size(222, 22);
            this.tsmiMod_Search.Text = "Search";
            this.tsmiMod_Search.Click += new System.EventHandler(this.tsmiMod_Search_Click);
            // 
            // ilMods
            // 
            this.ilMods.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMods.ImageStream")));
            this.ilMods.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMods.Images.SetKeyName(0, "folder.png");
            this.ilMods.Images.SetKeyName(1, "folder_add.png");
            this.ilMods.Images.SetKeyName(2, "page_white.png");
            this.ilMods.Images.SetKeyName(3, "page_white_add.png");
            // 
            // grpModSelection
            // 
            this.grpModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModSelection.Controls.Add(this.btnImExport);
            this.grpModSelection.Controls.Add(this.tvModSelection);
            this.grpModSelection.Controls.Add(this.btnAdd);
            this.grpModSelection.Controls.Add(this.btnScanGameData);
            this.grpModSelection.Controls.Add(this.btnClear);
            this.grpModSelection.Controls.Add(this.btnRemove);
            this.grpModSelection.Enabled = false;
            this.grpModSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpModSelection.Location = new System.Drawing.Point(0, 0);
            this.grpModSelection.Name = "grpModSelection";
            this.grpModSelection.Size = new System.Drawing.Size(448, 337);
            this.grpModSelection.TabIndex = 9;
            this.grpModSelection.TabStop = false;
            this.grpModSelection.Text = "Mod selection:";
            // 
            // btnImExport
            // 
            this.btnImExport.Image = global::KSPModAdmin.Properties.Resources.components_scroll_replace;
            this.btnImExport.Location = new System.Drawing.Point(273, 17);
            this.btnImExport.Name = "btnImExport";
            this.btnImExport.Size = new System.Drawing.Size(83, 24);
            this.btnImExport.TabIndex = 7;
            this.btnImExport.Text = "Ex/Import";
            this.btnImExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModTab.SetToolTip(this.btnImExport, "Ex/Import:\r\nExports or imports a ModPack.");
            this.btnImExport.UseVisualStyleBackColor = true;
            this.btnImExport.Click += new System.EventHandler(this.btnImExport_Click);
            // 
            // tvModSelection
            // 
            this.tvModSelection.AllowDrop = true;
            this.tvModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvModSelection.ColumnsOptions.AutoSize = true;
            this.tvModSelection.ContextMenuStrip = this.ctxtMenuMods;
            this.tvModSelection.ExpandOnDoubleClick = false;
            this.tvModSelection.Images = this.ilMods;
            this.tvModSelection.Location = new System.Drawing.Point(6, 47);
            this.tvModSelection.Name = "tvModSelection";
            this.tvModSelection.ReadOnlyBGColor = System.Drawing.SystemColors.ControlLight;
            this.tvModSelection.RecursiveChildCheckBoxChecking = true;
            this.tvModSelection.RecursiveParentCheckBoxChecking = true;
            this.tvModSelection.RowOptions.ShowHeader = false;
            this.tvModSelection.Size = new System.Drawing.Size(436, 284);
            this.tvModSelection.TabIndex = 6;
            this.tvModSelection.Text = "ModSelection";
            this.tvModSelection.ViewOptions.ShowCheckBoxes = true;
            this.tvModSelection.CheckedChanging += new KSPModAdmin.Utils.CommonTools.CheckedChangingHandler(this.tvModSelection_CheckedChanging);
            this.tvModSelection.CustomDrawCell += new KSPModAdmin.Utils.CommonTools.CustomDrawCellHandler(this.tvModSelection_CustomDrawCell);
            this.tvModSelection.Click += new System.EventHandler(this.tvModSelection_Click);
            this.tvModSelection.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvModSelection_DragDrop);
            this.tvModSelection.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvModSelection_DragEnter);
            this.tvModSelection.DoubleClick += new System.EventHandler(this.tvModSelection_DoubleClick);
            // 
            // grpProceed
            // 
            this.grpProceed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProceed.Controls.Add(this.tableLayoutPanel1);
            this.grpProceed.Controls.Add(this.cbOverride);
            this.grpProceed.Controls.Add(this.pBarModProcessing);
            this.grpProceed.Enabled = false;
            this.grpProceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProceed.Location = new System.Drawing.Point(0, 337);
            this.grpProceed.Name = "grpProceed";
            this.grpProceed.Size = new System.Drawing.Size(448, 91);
            this.grpProceed.TabIndex = 10;
            this.grpProceed.TabStop = false;
            this.grpProceed.Text = "Process mod selection:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnExecuteMod, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExecuteAllMods, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 45);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // pBarModProcessing
            // 
            this.pBarModProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarModProcessing.Location = new System.Drawing.Point(160, 18);
            this.pBarModProcessing.Name = "pBarModProcessing";
            this.pBarModProcessing.Size = new System.Drawing.Size(277, 19);
            this.pBarModProcessing.TabIndex = 8;
            this.pBarModProcessing.Visible = false;
            // 
            // ucModSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBorderlessWin);
            this.Controls.Add(this.btnLunchKSP);
            this.Controls.Add(this.grpModSelection);
            this.Controls.Add(this.grpProceed);
            this.Name = "ucModSelection";
            this.Size = new System.Drawing.Size(448, 488);
            this.Load += new System.EventHandler(this.ucModSelection_Load);
            this.ctxtMenuMods.ResumeLayout(false);
            this.grpModSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvModSelection)).EndInit();
            this.grpProceed.ResumeLayout(false);
            this.grpProceed.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttModTab;
        private System.Windows.Forms.ContextMenuStrip ctxtMenuMods;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_Destination;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_Path;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_SelectDestination;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_ResetDestination;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_ZipSource;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_SourcePath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_SelectNewSourceFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_SelectNewSourceFolderAllMods;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_ProceedSelectedMod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_ExpandAllNodes;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_CollapsNodes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_CheckAllMods;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_UncheckAllMods;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_Search;
        private System.Windows.Forms.ImageList ilMods;
        private System.Windows.Forms.ProgressBar pBarModProcessing;
        private System.Windows.Forms.CheckBox cbOverride;
        private System.Windows.Forms.Button btnExecuteAllMods;
        private System.Windows.Forms.Button btnExecuteMod;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        public System.Windows.Forms.GroupBox grpProceed;
        public System.Windows.Forms.GroupBox grpModSelection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_CreateZipOfInstalledItems;
        public System.Windows.Forms.Button btnLunchKSP;
        public System.Windows.Forms.Button btnScanGameData;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_ResetCheckedStates;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_DefaultName;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_chooseName;
        internal Utils.CommonTools.TreeListViewEx tvModSelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_OpenModFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_CheckForModUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_VisitSpaceport;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_EditModInfos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_CopyModInfos;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_VisitForum;
        private System.Windows.Forms.CheckBox cbBorderlessWin;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_VisitCurseForge;
        private System.Windows.Forms.ToolStripMenuItem tsmiMod_SolveConflicts;
        private System.Windows.Forms.Button btnImExport;
    }
}
