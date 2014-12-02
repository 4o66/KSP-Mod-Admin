namespace KSPModAdmin
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ttMainForm = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucWelcome1 = new KSPModAdmin.Views.ucWelcome();
            this.cbKSPPath = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMods = new System.Windows.Forms.TabPage();
            this.ucModSelection1 = new KSPModAdmin.Views.ucModSelection();
            this.tabPageModBrowser = new System.Windows.Forms.TabPage();
            this.ucModBrowser1 = new KSPModAdmin.Views.ucModBrowser();
            this.tabPageParts = new System.Windows.Forms.TabPage();
            this.ucParts1 = new KSPModAdmin.Views.ucParts();
            this.tabPageCrafts = new System.Windows.Forms.TabPage();
            this.ucCrafts1 = new KSPModAdmin.Views.ucCrafts();
            this.tabPageFlags = new System.Windows.Forms.TabPage();
            this.ucFlags1 = new KSPModAdmin.Views.ucFlags();
            this.tabPageBackup = new System.Windows.Forms.TabPage();
            this.ucBackup1 = new KSPModAdmin.Views.ucBackup();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.ucOptions1 = new KSPModAdmin.Views.ucOptions();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.ucHelp1 = new KSPModAdmin.Views.ucHelp();
            this.lblKSPPath = new System.Windows.Forms.Label();
            this.txtBoxMessages = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageMods.SuspendLayout();
            this.tabPageModBrowser.SuspendLayout();
            this.tabPageParts.SuspendLayout();
            this.tabPageCrafts.SuspendLayout();
            this.tabPageFlags.SuspendLayout();
            this.tabPageBackup.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Yellow;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.ucWelcome1);
            this.splitContainer1.Panel1.Controls.Add(this.cbKSPPath);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.lblKSPPath);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.txtBoxMessages);
            this.splitContainer1.Size = new System.Drawing.Size(464, 612);
            this.splitContainer1.SplitterDistance = 547;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // ucWelcome1
            // 
            this.ucWelcome1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWelcome1.Location = new System.Drawing.Point(0, 0);
            this.ucWelcome1.MainForm = null;
            this.ucWelcome1.Name = "ucWelcome1";
            this.ucWelcome1.Size = new System.Drawing.Size(464, 547);
            this.ucWelcome1.TabIndex = 0;
            this.ucWelcome1.Version = "";
            // 
            // cbKSPPath
            // 
            this.cbKSPPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbKSPPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKSPPath.FormattingEnabled = true;
            this.cbKSPPath.Location = new System.Drawing.Point(99, 11);
            this.cbKSPPath.Name = "cbKSPPath";
            this.cbKSPPath.Size = new System.Drawing.Size(353, 21);
            this.cbKSPPath.TabIndex = 9;
            this.cbKSPPath.SelectedIndexChanged += new System.EventHandler(this.cbKSPPath_SelectedIndexChanged);
            this.cbKSPPath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbKSPPath_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageMods);
            this.tabControl1.Controls.Add(this.tabPageModBrowser);
            this.tabControl1.Controls.Add(this.tabPageParts);
            this.tabControl1.Controls.Add(this.tabPageCrafts);
            this.tabControl1.Controls.Add(this.tabPageFlags);
            this.tabControl1.Controls.Add(this.tabPageBackup);
            this.tabControl1.Controls.Add(this.tabPageOptions);
            this.tabControl1.Controls.Add(this.tabPageHelp);
            this.tabControl1.Location = new System.Drawing.Point(3, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(461, 504);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPageMods
            // 
            this.tabPageMods.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMods.Controls.Add(this.ucModSelection1);
            this.tabPageMods.Location = new System.Drawing.Point(4, 22);
            this.tabPageMods.Name = "tabPageMods";
            this.tabPageMods.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMods.Size = new System.Drawing.Size(453, 478);
            this.tabPageMods.TabIndex = 0;
            this.tabPageMods.Text = "Mods";
            // 
            // ucModSelection1
            // 
            this.ucModSelection1.BorderlessWin = false;
            this.ucModSelection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModSelection1.EnableControls = false;
            this.ucModSelection1.Location = new System.Drawing.Point(3, 3);
            this.ucModSelection1.MainForm = null;
            this.ucModSelection1.Name = "ucModSelection1";
            this.ucModSelection1.Override = false;
            this.ucModSelection1.SelectedNode = null;
            this.ucModSelection1.Size = new System.Drawing.Size(447, 472);
            this.ucModSelection1.TabIndex = 6;
            // 
            // tabPageModBrowser
            // 
            this.tabPageModBrowser.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageModBrowser.Controls.Add(this.ucModBrowser1);
            this.tabPageModBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabPageModBrowser.Name = "tabPageModBrowser";
            this.tabPageModBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageModBrowser.Size = new System.Drawing.Size(423, 478);
            this.tabPageModBrowser.TabIndex = 5;
            this.tabPageModBrowser.Text = "ModBrowser";
            // 
            // ucModBrowser1
            // 
            this.ucModBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModBrowser1.Location = new System.Drawing.Point(3, 3);
            this.ucModBrowser1.MainForm = null;
            this.ucModBrowser1.Name = "ucModBrowser1";
            this.ucModBrowser1.Size = new System.Drawing.Size(417, 472);
            this.ucModBrowser1.TabIndex = 0;
            // 
            // tabPageParts
            // 
            this.tabPageParts.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageParts.Controls.Add(this.ucParts1);
            this.tabPageParts.Location = new System.Drawing.Point(4, 22);
            this.tabPageParts.Name = "tabPageParts";
            this.tabPageParts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParts.Size = new System.Drawing.Size(423, 478);
            this.tabPageParts.TabIndex = 7;
            this.tabPageParts.Text = "Parts";
            // 
            // ucParts1
            // 
            this.ucParts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucParts1.Enabled = false;
            this.ucParts1.Location = new System.Drawing.Point(3, 3);
            this.ucParts1.MainForm = null;
            this.ucParts1.Name = "ucParts1";
            this.ucParts1.Size = new System.Drawing.Size(417, 472);
            this.ucParts1.TabIndex = 0;
            // 
            // tabPageCrafts
            // 
            this.tabPageCrafts.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCrafts.Controls.Add(this.ucCrafts1);
            this.tabPageCrafts.Location = new System.Drawing.Point(4, 22);
            this.tabPageCrafts.Name = "tabPageCrafts";
            this.tabPageCrafts.Size = new System.Drawing.Size(423, 478);
            this.tabPageCrafts.TabIndex = 6;
            this.tabPageCrafts.Text = "Crafts";
            // 
            // ucCrafts1
            // 
            this.ucCrafts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCrafts1.Enabled = false;
            this.ucCrafts1.Location = new System.Drawing.Point(0, 0);
            this.ucCrafts1.MainForm = null;
            this.ucCrafts1.Name = "ucCrafts1";
            this.ucCrafts1.Size = new System.Drawing.Size(423, 478);
            this.ucCrafts1.TabIndex = 0;
            // 
            // tabPageFlags
            // 
            this.tabPageFlags.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageFlags.Controls.Add(this.ucFlags1);
            this.tabPageFlags.Location = new System.Drawing.Point(4, 22);
            this.tabPageFlags.Name = "tabPageFlags";
            this.tabPageFlags.Size = new System.Drawing.Size(423, 478);
            this.tabPageFlags.TabIndex = 4;
            this.tabPageFlags.Text = "Flags";
            // 
            // ucFlags1
            // 
            this.ucFlags1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFlags1.Enabled = false;
            this.ucFlags1.Location = new System.Drawing.Point(0, 0);
            this.ucFlags1.MainForm = null;
            this.ucFlags1.Name = "ucFlags1";
            this.ucFlags1.Size = new System.Drawing.Size(423, 478);
            this.ucFlags1.TabIndex = 0;
            // 
            // tabPageBackup
            // 
            this.tabPageBackup.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBackup.Controls.Add(this.ucBackup1);
            this.tabPageBackup.Location = new System.Drawing.Point(4, 22);
            this.tabPageBackup.Name = "tabPageBackup";
            this.tabPageBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackup.Size = new System.Drawing.Size(423, 478);
            this.tabPageBackup.TabIndex = 1;
            this.tabPageBackup.Text = "Backups";
            // 
            // ucBackup1
            // 
            this.ucBackup1.BackupPath = "                                                                Select a backup f" +
    "older ------>";
            this.ucBackup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBackup1.EnableControls = true;
            this.ucBackup1.Location = new System.Drawing.Point(3, 3);
            this.ucBackup1.MainForm = null;
            this.ucBackup1.Name = "ucBackup1";
            this.ucBackup1.Size = new System.Drawing.Size(417, 472);
            this.ucBackup1.TabIndex = 0;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageOptions.Controls.Add(this.ucOptions1);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(423, 478);
            this.tabPageOptions.TabIndex = 3;
            this.tabPageOptions.Text = "Options";
            // 
            // ucOptions1
            // 
            this.ucOptions1.AdminVersion = null;
            this.ucOptions1.BackupPath = "";
            this.ucOptions1.CheckForUpdates = false;
            this.ucOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOptions1.DownloadPath = "";
            this.ucOptions1.EnablePathButtons = false;
            this.ucOptions1.KSPPath = "";
            this.ucOptions1.LastModUpdateTry = new System.DateTime(((long)(0)));
            this.ucOptions1.Location = new System.Drawing.Point(3, 3);
            this.ucOptions1.MainForm = null;
            this.ucOptions1.ModUpdateBehavior = KSPModAdmin.ModUpdateBehavior.CopyDestination;
            this.ucOptions1.ModUpdateInterval = KSPModAdmin.ModUpdateInterval.Manualy;
            this.ucOptions1.Name = "ucOptions1";
            this.ucOptions1.PostDownloadAction = KSPModAdmin.PostDownloadAction.Ignore;
            this.ucOptions1.SelectedKSPPath = "";
            this.ucOptions1.Size = new System.Drawing.Size(417, 472);
            this.ucOptions1.TabIndex = 0;
            this.ucOptions1.Version = "";
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageHelp.Controls.Add(this.ucHelp1);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Size = new System.Drawing.Size(423, 478);
            this.tabPageHelp.TabIndex = 8;
            this.tabPageHelp.Text = "Help";
            // 
            // ucHelp1
            // 
            this.ucHelp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucHelp1.Location = new System.Drawing.Point(0, 0);
            this.ucHelp1.MainForm = null;
            this.ucHelp1.Name = "ucHelp1";
            this.ucHelp1.Size = new System.Drawing.Size(423, 478);
            this.ucHelp1.TabIndex = 0;
            // 
            // lblKSPPath
            // 
            this.lblKSPPath.AutoSize = true;
            this.lblKSPPath.Location = new System.Drawing.Point(11, 14);
            this.lblKSPPath.Name = "lblKSPPath";
            this.lblKSPPath.Size = new System.Drawing.Size(89, 13);
            this.lblKSPPath.TabIndex = 0;
            this.lblKSPPath.Text = "KSP install folder:";
            // 
            // txtBoxMessages
            // 
            this.txtBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.txtBoxMessages.Multiline = true;
            this.txtBoxMessages.Name = "txtBoxMessages";
            this.txtBoxMessages.ReadOnly = true;
            this.txtBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxMessages.Size = new System.Drawing.Size(464, 60);
            this.txtBoxMessages.TabIndex = 0;
            this.txtBoxMessages.TabStop = false;
            this.txtBoxMessages.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(464, 612);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KSP MOD Admin V";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMods.ResumeLayout(false);
            this.tabPageModBrowser.ResumeLayout(false);
            this.tabPageParts.ResumeLayout(false);
            this.tabPageCrafts.ResumeLayout(false);
            this.tabPageFlags.ResumeLayout(false);
            this.tabPageBackup.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtBoxMessages;
        private System.Windows.Forms.Label lblKSPPath;
        private Views.ucModBrowser ucModBrowser1;
        private System.Windows.Forms.ToolTip ttMainForm;
        private Views.ucModSelection ucModSelection1;
        private Views.ucFlags ucFlags1;
        private Views.ucBackup ucBackup1;
        private Views.ucOptions ucOptions1;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPageMods;
        public System.Windows.Forms.TabPage tabPageBackup;
        public System.Windows.Forms.TabPage tabPageOptions;
        public System.Windows.Forms.TabPage tabPageFlags;
        public System.Windows.Forms.TabPage tabPageModBrowser;
        private System.Windows.Forms.ComboBox cbKSPPath;
        private System.Windows.Forms.TabPage tabPageCrafts;
        private System.Windows.Forms.TabPage tabPageParts;
        private System.Windows.Forms.TabPage tabPageHelp;
        private Views.ucHelp ucHelp1;
        private Views.ucParts ucParts1;
        private Views.ucCrafts ucCrafts1;
        private Views.ucWelcome ucWelcome1;
    }
}

