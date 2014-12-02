namespace KSPModAdmin.Views
{
    partial class ucWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWelcome));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_FolderBrowser = new System.Windows.Forms.Button();
            this.btn_NormalSearch = new System.Windows.Forms.Button();
            this.btn_SteamSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ttWelcome = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btn_Later = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.tb_KSPInstallFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tlpSearchBG = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tlpSearchBG.SuspendLayout();
            this.tlpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to KSP Mod Admin v1.0.0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "To add a KSP install folder to the list of known paths,";
            // 
            // btn_FolderBrowser
            // 
            this.btn_FolderBrowser.Location = new System.Drawing.Point(33, 63);
            this.btn_FolderBrowser.Name = "btn_FolderBrowser";
            this.btn_FolderBrowser.Size = new System.Drawing.Size(133, 23);
            this.btn_FolderBrowser.TabIndex = 1;
            this.btn_FolderBrowser.Text = "Open folder browser";
            this.ttWelcome.SetToolTip(this.btn_FolderBrowser, "Opens a folder browser dialog to select the KSP install folder manualy.");
            this.btn_FolderBrowser.UseVisualStyleBackColor = true;
            this.btn_FolderBrowser.Click += new System.EventHandler(this.btn_FolderBrowser_Click);
            // 
            // btn_NormalSearch
            // 
            this.btn_NormalSearch.Location = new System.Drawing.Point(33, 108);
            this.btn_NormalSearch.Name = "btn_NormalSearch";
            this.btn_NormalSearch.Size = new System.Drawing.Size(151, 23);
            this.btn_NormalSearch.TabIndex = 1;
            this.btn_NormalSearch.Text = "Search KSP install folder";
            this.ttWelcome.SetToolTip(this.btn_NormalSearch, "Searches the KSP install folder starts at the KSP Mod Admin path and searches the" +
        " parent and subfolders,\r\ndepandent on the count you have selected.");
            this.btn_NormalSearch.UseVisualStyleBackColor = true;
            this.btn_NormalSearch.Click += new System.EventHandler(this.btn_NormalSearch_Click);
            // 
            // btn_SteamSearch
            // 
            this.btn_SteamSearch.Location = new System.Drawing.Point(33, 158);
            this.btn_SteamSearch.Name = "btn_SteamSearch";
            this.btn_SteamSearch.Size = new System.Drawing.Size(226, 23);
            this.btn_SteamSearch.TabIndex = 1;
            this.btn_SteamSearch.Text = "Search KSP install folder (Steam Version)";
            this.ttWelcome.SetToolTip(this.btn_SteamSearch, "Searches the Program folder for the Steam installation of KSP.");
            this.btn_SteamSearch.UseVisualStyleBackColor = true;
            this.btn_SteamSearch.Click += new System.EventHandler(this.btn_SteamSearch_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(20, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(356, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "A. Select a KSP install folder via folder browser dialog.";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(20, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(356, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "B. Autosearch of near folder structure";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "KSP install paths:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(198, 111);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown1.TabIndex = 4;
            this.ttWelcome.SetToolTip(this.numericUpDown1, "The maximum folder up and down where the Admin should search for the KSP install " +
        "folder.\r\n(0 = search the HD - very time comsuming!)");
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btn_Later
            // 
            this.btn_Later.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Later.Location = new System.Drawing.Point(345, 433);
            this.btn_Later.Name = "btn_Later";
            this.btn_Later.Size = new System.Drawing.Size(43, 23);
            this.btn_Later.TabIndex = 1;
            this.btn_Later.Text = "Later";
            this.ttWelcome.SetToolTip(this.btn_Later, "Choose a a KSP path later.");
            this.btn_Later.UseVisualStyleBackColor = true;
            this.btn_Later.Click += new System.EventHandler(this.btn_Later_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Enabled = false;
            this.btn_OK.Location = new System.Drawing.Point(296, 433);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(43, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.ttWelcome.SetToolTip(this.btn_OK, "Sets the current KSP path to the selected KSP install path.");
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // tb_KSPInstallFolder
            // 
            this.tb_KSPInstallFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_KSPInstallFolder.Enabled = false;
            this.tb_KSPInstallFolder.Location = new System.Drawing.Point(44, 272);
            this.tb_KSPInstallFolder.Name = "tb_KSPInstallFolder";
            this.tb_KSPInstallFolder.ReadOnly = true;
            this.tb_KSPInstallFolder.Size = new System.Drawing.Size(303, 20);
            this.tb_KSPInstallFolder.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "C. Autosearch of KSP Steam install folder.";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(239, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "folders up/down";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(370, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "choose one of the following options.";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 438);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(220, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Note: Select a path and click OK to proceed.";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(328, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "D. Drag && Drop a KSP install folder into the known KSP install paths.";
            this.label10.Visible = false;
            // 
            // tlpSearchBG
            // 
            this.tlpSearchBG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSearchBG.ColumnCount = 3;
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.Controls.Add(this.tlpSearch, 1, 1);
            this.tlpSearchBG.Location = new System.Drawing.Point(44, 298);
            this.tlpSearchBG.Name = "tlpSearchBG";
            this.tlpSearchBG.RowCount = 3;
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.Size = new System.Drawing.Size(303, 66);
            this.tlpSearchBG.TabIndex = 18;
            this.tlpSearchBG.Visible = false;
            // 
            // tlpSearch
            // 
            this.tlpSearch.AutoSize = true;
            this.tlpSearch.ColumnCount = 2;
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearch.Controls.Add(this.lblLoading, 1, 0);
            this.tlpSearch.Controls.Add(this.picLoading, 0, 0);
            this.tlpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSearch.Location = new System.Drawing.Point(101, 17);
            this.tlpSearch.Name = "tlpSearch";
            this.tlpSearch.RowCount = 1;
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSearch.Size = new System.Drawing.Size(101, 31);
            this.tlpSearch.TabIndex = 16;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(33, 9);
            this.lblLoading.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(65, 13);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "searching ...";
            // 
            // picLoading
            // 
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(3, 3);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(24, 25);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 358F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(394, 216);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btn_FolderBrowser);
            this.panel1.Controls.Add(this.btn_SteamSearch);
            this.panel1.Controls.Add(this.btn_NormalSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(21, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 210);
            this.panel1.TabIndex = 0;
            // 
            // ucWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tlpSearchBG);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_KSPInstallFolder);
            this.Controls.Add(this.btn_Later);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "ucWelcome";
            this.Size = new System.Drawing.Size(400, 468);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tlpSearchBG.ResumeLayout(false);
            this.tlpSearchBG.PerformLayout();
            this.tlpSearch.ResumeLayout(false);
            this.tlpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_FolderBrowser;
        private System.Windows.Forms.Button btn_NormalSearch;
        private System.Windows.Forms.Button btn_SteamSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip ttWelcome;
        private System.Windows.Forms.Button btn_Later;
        private System.Windows.Forms.TextBox tb_KSPInstallFolder;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tlpSearchBG;
        private System.Windows.Forms.TableLayoutPanel tlpSearch;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
