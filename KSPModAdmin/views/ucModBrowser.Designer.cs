namespace KSPModAdmin.Views
{
    partial class ucModBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucModBrowser));
            this.webBrowser1 = new KSPModAdmin.Utils.WebBrowserEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsstbURL = new KSPModAdmin.Utils.Controls.ToolStripSpringTextBox();
            this.tsbNavitageTo = new System.Windows.Forms.ToolStripButton();
            this.tsbForum = new System.Windows.Forms.ToolStripButton();
            this.tsbHome = new System.Windows.Forms.ToolStripButton();
            this.tsbBestRating = new System.Windows.Forms.ToolStripButton();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbKerbalStuff = new System.Windows.Forms.ToolStripButton();
            this.tsbCurseForge = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenDownloads = new System.Windows.Forms.ToolStripButton();
            this.tsbChooseDest = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdateModInfo = new System.Windows.Forms.ToolStripButton();
            this.tsbDownloadMod = new System.Windows.Forms.ToolStripButton();
            this.tsbLoading = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 50);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(558, 286);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.FileDownloading += new System.EventHandler<KSPModAdmin.Utils.FileDownloadEventArgs>(this.webBrowser_FileDownloading);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser_Navigated);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser_ProgressChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(558, 308);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(550, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.AutoSize = false;
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 18);
            this.tsProgressBar.ToolTipText = "Download progression";
            this.tsProgressBar.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbForum,
            this.tsbHome,
            this.tsbBestRating,
            this.tsbNew,
            this.tsbKerbalStuff,
            this.tsbCurseForge,
            this.tsbRefresh,
            this.tsbPrev,
            this.tsbNext,
            this.toolStripSeparator1,
            this.tsbOpenDownloads,
            this.tsbChooseDest,
            this.toolStripSeparator4,
            this.tsbUpdateModInfo,
            this.tsbDownloadMod,
            this.toolStripSeparator2,
            this.tsbLoading,
            this.toolStripSeparator3,
            this.tsProgressBar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(558, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsstbURL,
            this.tsbNavitageTo});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(558, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel1.Text = "URL:";
            // 
            // tsstbURL
            // 
            this.tsstbURL.Name = "tsstbURL";
            this.tsstbURL.Size = new System.Drawing.Size(461, 25);
            this.tsstbURL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tsstbURL_KeyUp);
            // 
            // tsbNavitageTo
            // 
            this.tsbNavitageTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNavitageTo.Image = global::KSPModAdmin.Properties.Resources.download;
            this.tsbNavitageTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNavitageTo.Name = "tsbNavitageTo";
            this.tsbNavitageTo.Size = new System.Drawing.Size(23, 22);
            this.tsbNavitageTo.Text = "Navigate to URL";
            this.tsbNavitageTo.Click += new System.EventHandler(this.tsbNavitageTo_Click);
            // 
            // tsbForum
            // 
            this.tsbForum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForum.Image = ((System.Drawing.Image)(resources.GetObject("tsbForum.Image")));
            this.tsbForum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForum.Name = "tsbForum";
            this.tsbForum.Size = new System.Drawing.Size(23, 22);
            this.tsbForum.Text = "toolStripButton5";
            this.tsbForum.ToolTipText = "Open KSP Forum\r\nOpens the KSP Forum main site.";
            this.tsbForum.Click += new System.EventHandler(this.tsbForum_Click);
            // 
            // tsbHome
            // 
            this.tsbHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHome.Image = global::KSPModAdmin.Properties.Resources.house;
            this.tsbHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHome.Name = "tsbHome";
            this.tsbHome.Size = new System.Drawing.Size(23, 22);
            this.tsbHome.Text = "toolStripButton1";
            this.tsbHome.ToolTipText = "Open KSP Spaceport\r\nOpens the KSP Spaceport main site.\r\n";
            this.tsbHome.Visible = false;
            this.tsbHome.Click += new System.EventHandler(this.tsbHome_Click);
            // 
            // tsbBestRating
            // 
            this.tsbBestRating.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBestRating.Image = global::KSPModAdmin.Properties.Resources.star;
            this.tsbBestRating.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBestRating.Name = "tsbBestRating";
            this.tsbBestRating.Size = new System.Drawing.Size(23, 22);
            this.tsbBestRating.Text = "toolStripButton1";
            this.tsbBestRating.ToolTipText = "Best ratings\r\nOpens the KSP Spaceport site with the highest rated mods.\r\n";
            this.tsbBestRating.Visible = false;
            this.tsbBestRating.Click += new System.EventHandler(this.tsbBestRating_Click);
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::KSPModAdmin.Properties.Resources._new;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "toolStripButton6";
            this.tsbNew.ToolTipText = "Open latest mods\r\nOpens the KSP Spaceport site with the latest added mods.";
            this.tsbNew.Visible = false;
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbKerbalStuff
            // 
            this.tsbKerbalStuff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbKerbalStuff.Image = global::KSPModAdmin.Properties.Resources.KSicon;
            this.tsbKerbalStuff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbKerbalStuff.Name = "tsbKerbalStuff";
            this.tsbKerbalStuff.Size = new System.Drawing.Size(23, 22);
            this.tsbKerbalStuff.Text = "toolStripButton2";
            this.tsbKerbalStuff.ToolTipText = "Open KerbalSuff";
            this.tsbKerbalStuff.Click += new System.EventHandler(this.tsbKerbalStuff_Click);
            // 
            // tsbCurseForge
            // 
            this.tsbCurseForge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCurseForge.Image = global::KSPModAdmin.Properties.Resources.anvil;
            this.tsbCurseForge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCurseForge.Name = "tsbCurseForge";
            this.tsbCurseForge.Size = new System.Drawing.Size(23, 22);
            this.tsbCurseForge.Text = "toolStripButton1";
            this.tsbCurseForge.ToolTipText = "Open CurseForge:\r\nOpens the KSP CurseForge main site.";
            this.tsbCurseForge.Click += new System.EventHandler(this.tsbCurseForge_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::KSPModAdmin.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "toolStripButton2";
            this.tsbRefresh.ToolTipText = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbPrev
            // 
            this.tsbPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrev.Image = global::KSPModAdmin.Properties.Resources.arrow_left_blue;
            this.tsbPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrev.Name = "tsbPrev";
            this.tsbPrev.Size = new System.Drawing.Size(23, 22);
            this.tsbPrev.Text = "toolStripButton3";
            this.tsbPrev.ToolTipText = "Back";
            this.tsbPrev.Click += new System.EventHandler(this.tsbPrev_Click);
            // 
            // tsbNext
            // 
            this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = global::KSPModAdmin.Properties.Resources.arrow_right_blue;
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size(23, 22);
            this.tsbNext.Text = "toolStripButton4";
            this.tsbNext.ToolTipText = "Forward";
            this.tsbNext.Click += new System.EventHandler(this.tsbNext_Click);
            // 
            // tsbOpenDownloads
            // 
            this.tsbOpenDownloads.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenDownloads.Image = global::KSPModAdmin.Properties.Resources.folder1;
            this.tsbOpenDownloads.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenDownloads.Name = "tsbOpenDownloads";
            this.tsbOpenDownloads.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenDownloads.Text = "toolStripButton1";
            this.tsbOpenDownloads.ToolTipText = "Open download folder\r\nOpens the download folder.";
            this.tsbOpenDownloads.Click += new System.EventHandler(this.tsbOpenDownloads_Click);
            // 
            // tsbChooseDest
            // 
            this.tsbChooseDest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChooseDest.Image = global::KSPModAdmin.Properties.Resources.folder_view;
            this.tsbChooseDest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChooseDest.Name = "tsbChooseDest";
            this.tsbChooseDest.Size = new System.Drawing.Size(23, 22);
            this.tsbChooseDest.Text = "toolStripButton5";
            this.tsbChooseDest.ToolTipText = "Select new download folder\r\nOpens a folder select dilaog to select a new download" +
    " directory.";
            this.tsbChooseDest.Click += new System.EventHandler(this.tsbChooseDest_Click);
            // 
            // tsbUpdateModInfo
            // 
            this.tsbUpdateModInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUpdateModInfo.Image = global::KSPModAdmin.Properties.Resources.component_scroll_next;
            this.tsbUpdateModInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdateModInfo.Name = "tsbUpdateModInfo";
            this.tsbUpdateModInfo.Size = new System.Drawing.Size(23, 22);
            this.tsbUpdateModInfo.Text = "toolStripButton1";
            this.tsbUpdateModInfo.ToolTipText = "Update ModInfos\r\nTrys to find a Mod in the ModSelection with the same ProductID\r\n" +
    "as the one parsed from the current displayed WebSite.";
            this.tsbUpdateModInfo.Click += new System.EventHandler(this.tsbUpdateModInfo_Click);
            // 
            // tsbDownloadMod
            // 
            this.tsbDownloadMod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDownloadMod.Image = global::KSPModAdmin.Properties.Resources.component_next;
            this.tsbDownloadMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownloadMod.Name = "tsbDownloadMod";
            this.tsbDownloadMod.Size = new System.Drawing.Size(23, 22);
            this.tsbDownloadMod.ToolTipText = "Download Mod:\r\nTries to download and add the mod from the current web site (SPace" +
    "port & KSP Forum only).";
            this.tsbDownloadMod.Click += new System.EventHandler(this.tsbDownloadMod_Click);
            // 
            // tsbLoading
            // 
            this.tsbLoading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoading.Image = global::KSPModAdmin.Properties.Resources.loader;
            this.tsbLoading.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoading.Name = "tsbLoading";
            this.tsbLoading.Size = new System.Drawing.Size(23, 22);
            this.tsbLoading.ToolTipText = "loading...";
            this.tsbLoading.Visible = false;
            // 
            // ucModBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "ucModBrowser";
            this.Size = new System.Drawing.Size(558, 336);
            this.Load += new System.EventHandler(this.ucModBrowser_Load);
            this.tabControl1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KSPModAdmin.Utils.WebBrowserEx webBrowser1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripButton tsbForum;
        private System.Windows.Forms.ToolStripButton tsbHome;
        private System.Windows.Forms.ToolStripButton tsbBestRating;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbPrev;
        private System.Windows.Forms.ToolStripButton tsbNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbChooseDest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbLoading;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpenDownloads;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbUpdateModInfo;
        private System.Windows.Forms.ToolStripButton tsbDownloadMod;
        private System.Windows.Forms.ToolStripButton tsbCurseForge;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbNavitageTo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private Utils.Controls.ToolStripSpringTextBox tsstbURL;
        private System.Windows.Forms.ToolStripButton tsbKerbalStuff;
    }
}
