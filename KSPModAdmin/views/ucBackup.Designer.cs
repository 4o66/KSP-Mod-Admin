namespace KSPModAdmin.Views
{
    partial class ucBackup
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
            this.ttBackupTab = new System.Windows.Forms.ToolTip(this.components);
            this.btnOpenBackupDir = new System.Windows.Forms.Button();
            this.btnReinstallBackup = new System.Windows.Forms.Button();
            this.btnClearBackups = new System.Windows.Forms.Button();
            this.btnRemoveBackup = new System.Windows.Forms.Button();
            this.btnBackupSaves = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnBackupPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBackupPath = new System.Windows.Forms.TextBox();
            this.tvBackup = new KSPModAdmin.Utils.CommonTools.TreeListViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.tvBackup)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenBackupDir
            // 
            this.btnOpenBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenBackupDir.Image = global::KSPModAdmin.Properties.Resources.folder1;
            this.btnOpenBackupDir.Location = new System.Drawing.Point(390, 45);
            this.btnOpenBackupDir.Name = "btnOpenBackupDir";
            this.btnOpenBackupDir.Size = new System.Drawing.Size(25, 24);
            this.btnOpenBackupDir.TabIndex = 26;
            this.ttBackupTab.SetToolTip(this.btnOpenBackupDir, "Opens the back directory.");
            this.btnOpenBackupDir.UseVisualStyleBackColor = true;
            this.btnOpenBackupDir.Click += new System.EventHandler(this.btnOpenBackupDir_Click);
            // 
            // btnReinstallBackup
            // 
            this.btnReinstallBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReinstallBackup.Enabled = false;
            this.btnReinstallBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReinstallBackup.Image = global::KSPModAdmin.Properties.Resources.disk_black_data_into;
            this.btnReinstallBackup.Location = new System.Drawing.Point(3, 378);
            this.btnReinstallBackup.Name = "btnReinstallBackup";
            this.btnReinstallBackup.Size = new System.Drawing.Size(412, 40);
            this.btnReinstallBackup.TabIndex = 23;
            this.btnReinstallBackup.Text = " Recover selected Backup";
            this.btnReinstallBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReinstallBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttBackupTab.SetToolTip(this.btnReinstallBackup, "Recovers a backup.\r\nNOTE: The old folder will be replaced! (make a Backup first)." +
        "");
            this.btnReinstallBackup.UseVisualStyleBackColor = true;
            this.btnReinstallBackup.Click += new System.EventHandler(this.btnReinstallBackup_Click);
            // 
            // btnClearBackups
            // 
            this.btnClearBackups.Enabled = false;
            this.btnClearBackups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearBackups.Image = global::KSPModAdmin.Properties.Resources.data_copy_delete;
            this.btnClearBackups.Location = new System.Drawing.Point(251, 45);
            this.btnClearBackups.Name = "btnClearBackups";
            this.btnClearBackups.Size = new System.Drawing.Size(95, 24);
            this.btnClearBackups.TabIndex = 21;
            this.btnClearBackups.Text = "Remove all";
            this.btnClearBackups.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearBackups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttBackupTab.SetToolTip(this.btnClearBackups, "Deletes all backups.");
            this.btnClearBackups.UseVisualStyleBackColor = true;
            this.btnClearBackups.Click += new System.EventHandler(this.btnClearBackups_Click);
            // 
            // btnRemoveBackup
            // 
            this.btnRemoveBackup.Enabled = false;
            this.btnRemoveBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveBackup.Image = global::KSPModAdmin.Properties.Resources.data_delete;
            this.btnRemoveBackup.Location = new System.Drawing.Point(174, 45);
            this.btnRemoveBackup.Name = "btnRemoveBackup";
            this.btnRemoveBackup.Size = new System.Drawing.Size(71, 24);
            this.btnRemoveBackup.TabIndex = 20;
            this.btnRemoveBackup.Text = "Remove";
            this.btnRemoveBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttBackupTab.SetToolTip(this.btnRemoveBackup, "Deletes the selecten backup");
            this.btnRemoveBackup.UseVisualStyleBackColor = true;
            this.btnRemoveBackup.Click += new System.EventHandler(this.btnRemoveBackup_Click);
            // 
            // btnBackupSaves
            // 
            this.btnBackupSaves.Enabled = false;
            this.btnBackupSaves.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupSaves.Image = global::KSPModAdmin.Properties.Resources.data_floppy_disk;
            this.btnBackupSaves.Location = new System.Drawing.Point(69, 45);
            this.btnBackupSaves.Name = "btnBackupSaves";
            this.btnBackupSaves.Size = new System.Drawing.Size(99, 24);
            this.btnBackupSaves.TabIndex = 19;
            this.btnBackupSaves.Text = "Backup saves";
            this.btnBackupSaves.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttBackupTab.SetToolTip(this.btnBackupSaves, "Creates a backup of the complete KSP saves folder.");
            this.btnBackupSaves.UseVisualStyleBackColor = true;
            this.btnBackupSaves.Click += new System.EventHandler(this.btnBackupSaves_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Enabled = false;
            this.btnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Image = global::KSPModAdmin.Properties.Resources.data_add;
            this.btnBackup.Location = new System.Drawing.Point(8, 45);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(55, 24);
            this.btnBackup.TabIndex = 18;
            this.btnBackup.Text = "New";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttBackupTab.SetToolTip(this.btnBackup, "Browse the KSP folders for the folder to backup.");
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnBackupPath
            // 
            this.btnBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupPath.Enabled = false;
            this.btnBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupPath.Image = global::KSPModAdmin.Properties.Resources.folder_view;
            this.btnBackupPath.Location = new System.Drawing.Point(390, 16);
            this.btnBackupPath.Name = "btnBackupPath";
            this.btnBackupPath.Size = new System.Drawing.Size(25, 24);
            this.btnBackupPath.TabIndex = 17;
            this.ttBackupTab.SetToolTip(this.btnBackupPath, "Browse your computer to select a folder to write the backups to.");
            this.btnBackupPath.UseVisualStyleBackColor = true;
            this.btnBackupPath.Click += new System.EventHandler(this.btnBackupPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Backup folder:";
            // 
            // tbBackupPath
            // 
            this.tbBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBackupPath.Location = new System.Drawing.Point(8, 18);
            this.tbBackupPath.Name = "tbBackupPath";
            this.tbBackupPath.ReadOnly = true;
            this.tbBackupPath.Size = new System.Drawing.Size(376, 20);
            this.tbBackupPath.TabIndex = 15;
            this.tbBackupPath.TabStop = false;
            this.tbBackupPath.Text = "                                                                Select a backup f" +
    "older ------>";
            // 
            // tvBackup
            // 
            this.tvBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvBackup.ColumnsOptions.AutoSize = true;
            this.tvBackup.EditOnDoubleClick = true;
            this.tvBackup.Images = null;
            this.tvBackup.Location = new System.Drawing.Point(3, 74);
            this.tvBackup.Name = "tvBackup";
            this.tvBackup.ReadOnlyBGColor = System.Drawing.SystemColors.Window;
            this.tvBackup.RowOptions.ShowHeader = false;
            this.tvBackup.Size = new System.Drawing.Size(412, 298);
            this.tvBackup.TabIndex = 27;
            this.tvBackup.Text = "treeListView1";
            this.tvBackup.ViewOptions.ShowLine = false;
            this.tvBackup.ViewOptions.ShowPlusMinus = false;
            this.tvBackup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBackups_AfterSelect);
            this.tvBackup.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvBackups_MouseUp);
            // 
            // ucBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOpenBackupDir);
            this.Controls.Add(this.btnReinstallBackup);
            this.Controls.Add(this.btnClearBackups);
            this.Controls.Add(this.btnRemoveBackup);
            this.Controls.Add(this.btnBackupSaves);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBackupPath);
            this.Controls.Add(this.tbBackupPath);
            this.Controls.Add(this.tvBackup);
            this.Name = "ucBackup";
            this.Size = new System.Drawing.Size(418, 422);
            this.Load += new System.EventHandler(this.ucBackups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tvBackup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttBackupTab;
        private System.Windows.Forms.Button btnReinstallBackup;
        private System.Windows.Forms.Button btnClearBackups;
        private System.Windows.Forms.Button btnRemoveBackup;
        private System.Windows.Forms.Button btnBackupSaves;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnBackupPath;
        private System.Windows.Forms.Button btnOpenBackupDir;
        private Utils.CommonTools.TreeListViewEx tvBackup;
        public System.Windows.Forms.TextBox tbBackupPath;
    }
}
