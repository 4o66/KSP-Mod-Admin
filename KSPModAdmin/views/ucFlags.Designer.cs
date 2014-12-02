namespace KSPModAdmin.Views
{
    partial class ucFlags
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFlags));
            this.ttFlagsTab = new System.Windows.Forms.ToolTip(this.components);
            this.btnFlagImport = new System.Windows.Forms.Button();
            this.cbFlagFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvFlags = new System.Windows.Forms.ListView();
            this.ilFlags = new System.Windows.Forms.ImageList(this.components);
            this.btnFlagDelete = new System.Windows.Forms.Button();
            this.btnFlagRefresh = new System.Windows.Forms.Button();
            this.pbFlagsLoad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlagsLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFlagImport
            // 
            this.btnFlagImport.Image = global::KSPModAdmin.Properties.Resources.flag_scotland_add;
            this.btnFlagImport.Location = new System.Drawing.Point(9, 33);
            this.btnFlagImport.Name = "btnFlagImport";
            this.btnFlagImport.Size = new System.Drawing.Size(113, 23);
            this.btnFlagImport.TabIndex = 17;
            this.btnFlagImport.Text = "Import new Flag";
            this.btnFlagImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttFlagsTab.SetToolTip(this.btnFlagImport, "Browse your computer for a picture to add as a new Flag.\r\nThe picture will be res" +
        "ized to 256x160 and saved as a png.");
            this.btnFlagImport.UseVisualStyleBackColor = true;
            this.btnFlagImport.Click += new System.EventHandler(this.btnFlagImport_Click);
            // 
            // cbFlagFilter
            // 
            this.cbFlagFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFlagFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlagFilter.FormattingEnabled = true;
            this.cbFlagFilter.Items.AddRange(new object[] {
            "All",
            "MyFlags"});
            this.cbFlagFilter.Location = new System.Drawing.Point(108, 6);
            this.cbFlagFilter.Name = "cbFlagFilter";
            this.cbFlagFilter.Size = new System.Drawing.Size(113, 21);
            this.cbFlagFilter.TabIndex = 14;
            this.ttFlagsTab.SetToolTip(this.cbFlagFilter, "Filter for the flag list view.");
            this.cbFlagFilter.SelectedIndexChanged += new System.EventHandler(this.cbFlagFilter_SelectedIndexChanged);
            this.cbFlagFilter.Click += new System.EventHandler(this.cbFlagFilter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Folder/Mod Filter:";
            // 
            // lvFlags
            // 
            this.lvFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFlags.LargeImageList = this.ilFlags;
            this.lvFlags.Location = new System.Drawing.Point(3, 62);
            this.lvFlags.Name = "lvFlags";
            this.lvFlags.Size = new System.Drawing.Size(337, 368);
            this.lvFlags.SmallImageList = this.ilFlags;
            this.lvFlags.TabIndex = 13;
            this.lvFlags.UseCompatibleStateImageBehavior = false;
            // 
            // ilFlags
            // 
            this.ilFlags.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.ilFlags.ImageSize = new System.Drawing.Size(128, 80);
            this.ilFlags.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnFlagDelete
            // 
            this.btnFlagDelete.Image = global::KSPModAdmin.Properties.Resources.flag_scotland_delete;
            this.btnFlagDelete.Location = new System.Drawing.Point(128, 33);
            this.btnFlagDelete.Name = "btnFlagDelete";
            this.btnFlagDelete.Size = new System.Drawing.Size(93, 23);
            this.btnFlagDelete.TabIndex = 16;
            this.btnFlagDelete.Text = "Delete Flag";
            this.btnFlagDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttFlagsTab.SetToolTip(this.btnFlagDelete, "Deletes a flag and removes it from the mod selection tree.");
            this.btnFlagDelete.UseVisualStyleBackColor = true;
            this.btnFlagDelete.Click += new System.EventHandler(this.btnFlagDelete_Click);
            // 
            // btnFlagRefresh
            // 
            this.btnFlagRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlagRefresh.Image = global::KSPModAdmin.Properties.Resources.refresh;
            this.btnFlagRefresh.Location = new System.Drawing.Point(227, 5);
            this.btnFlagRefresh.Name = "btnFlagRefresh";
            this.btnFlagRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnFlagRefresh.TabIndex = 15;
            this.btnFlagRefresh.Text = "Refresh";
            this.btnFlagRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttFlagsTab.SetToolTip(this.btnFlagRefresh, "Rescan KSP for new flags.");
            this.btnFlagRefresh.UseVisualStyleBackColor = true;
            this.btnFlagRefresh.Click += new System.EventHandler(this.btnFlagRefresh_Click);
            // 
            // pbFlagsLoad
            // 
            this.pbFlagsLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFlagsLoad.Image = ((System.Drawing.Image)(resources.GetObject("pbFlagsLoad.Image")));
            this.pbFlagsLoad.Location = new System.Drawing.Point(308, 7);
            this.pbFlagsLoad.Name = "pbFlagsLoad";
            this.pbFlagsLoad.Size = new System.Drawing.Size(20, 20);
            this.pbFlagsLoad.TabIndex = 18;
            this.pbFlagsLoad.TabStop = false;
            this.pbFlagsLoad.Visible = false;
            // 
            // ucFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFlagDelete);
            this.Controls.Add(this.btnFlagImport);
            this.Controls.Add(this.btnFlagRefresh);
            this.Controls.Add(this.cbFlagFilter);
            this.Controls.Add(this.pbFlagsLoad);
            this.Controls.Add(this.lvFlags);
            this.Name = "ucFlags";
            this.Size = new System.Drawing.Size(343, 433);
            this.Load += new System.EventHandler(this.ucFlags_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFlagsLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttFlagsTab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFlagDelete;
        private System.Windows.Forms.Button btnFlagImport;
        private System.Windows.Forms.Button btnFlagRefresh;
        private System.Windows.Forms.ComboBox cbFlagFilter;
        private System.Windows.Forms.PictureBox pbFlagsLoad;
        private System.Windows.Forms.ListView lvFlags;
        private System.Windows.Forms.ImageList ilFlags;
    }
}
