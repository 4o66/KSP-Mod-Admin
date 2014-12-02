namespace KSPModAdmin.Views
{
    partial class ucCrafts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCrafts));
            this.cmsCrafts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSwapBuilding = new System.Windows.Forms.Button();
            this.ttCrafts = new System.Windows.Forms.ToolTip(this.components);
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.cbBuildingFilter = new System.Windows.Forms.ComboBox();
            this.lblBuildingFilter = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tvCrafts = new KSPModAdmin.Utils.CommonTools.TreeListViewEx();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cmsCrafts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvCrafts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsCrafts
            // 
            this.cmsCrafts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.swapToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeToolStripMenuItem});
            this.cmsCrafts.Name = "contextMenuStrip1";
            this.cmsCrafts.Size = new System.Drawing.Size(150, 126);
            this.cmsCrafts.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCrafts_Opening);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // swapToolStripMenuItem
            // 
            this.swapToolStripMenuItem.Name = "swapToolStripMenuItem";
            this.swapToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.swapToolStripMenuItem.Text = "Swap building";
            this.swapToolStripMenuItem.Click += new System.EventHandler(this.btnSwapBuilding_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSwapBuilding
            // 
            this.btnSwapBuilding.Enabled = false;
            this.btnSwapBuilding.Image = global::KSPModAdmin.Properties.Resources.airplane_replace;
            this.btnSwapBuilding.Location = new System.Drawing.Point(99, 33);
            this.btnSwapBuilding.Name = "btnSwapBuilding";
            this.btnSwapBuilding.Size = new System.Drawing.Size(102, 26);
            this.btnSwapBuilding.TabIndex = 1;
            this.btnSwapBuilding.Text = "Swap building";
            this.btnSwapBuilding.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnSwapBuilding, "Swaps the selected craft to the other vehicle building.");
            this.btnSwapBuilding.UseVisualStyleBackColor = true;
            this.btnSwapBuilding.Click += new System.EventHandler(this.btnSwapBuilding_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Image = global::KSPModAdmin.Properties.Resources.airplane_checkbox_checked;
            this.btnValidate.Location = new System.Drawing.Point(216, 33);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(71, 26);
            this.btnValidate.TabIndex = 1;
            this.btnValidate.Text = "Validate";
            this.btnValidate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnValidate, "Checks if all parts for the crafts are installed.");
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnRename
            // 
            this.btnRename.Enabled = false;
            this.btnRename.Image = global::KSPModAdmin.Properties.Resources.airplane_scroll;
            this.btnRename.Location = new System.Drawing.Point(17, 33);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(76, 26);
            this.btnRename.TabIndex = 1;
            this.btnRename.Text = "Rename";
            this.btnRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnRename, "Renames the selected craft.");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // cbBuildingFilter
            // 
            this.cbBuildingFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBuildingFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBuildingFilter.FormattingEnabled = true;
            this.cbBuildingFilter.Items.AddRange(new object[] {
            "All",
            "VAB",
            "SPH"});
            this.cbBuildingFilter.Location = new System.Drawing.Point(125, 3);
            this.cbBuildingFilter.Name = "cbBuildingFilter";
            this.cbBuildingFilter.Size = new System.Drawing.Size(167, 21);
            this.cbBuildingFilter.TabIndex = 2;
            this.ttCrafts.SetToolTip(this.cbBuildingFilter, "Filters the craft list by vehicle building.");
            this.cbBuildingFilter.SelectedIndexChanged += new System.EventHandler(this.cbBuildingFilter_SelectedIndexChanged);
            // 
            // lblBuildingFilter
            // 
            this.lblBuildingFilter.AutoSize = true;
            this.lblBuildingFilter.Location = new System.Drawing.Point(14, 6);
            this.lblBuildingFilter.Name = "lblBuildingFilter";
            this.lblBuildingFilter.Size = new System.Drawing.Size(106, 13);
            this.lblBuildingFilter.TabIndex = 3;
            this.lblBuildingFilter.Text = "Vehicle building filter:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(3, 485);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(47, 13);
            this.lblCount.TabIndex = 3;
            this.lblCount.Text = "Count: 0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 485);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "NOTE: Crafts with invalid parts are red.";
            // 
            // tvCrafts
            // 
            this.tvCrafts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCrafts.ExpandOnDoubleClick = false;
            this.tvCrafts.Images = null;
            this.tvCrafts.Location = new System.Drawing.Point(3, 65);
            this.tvCrafts.Name = "tvCrafts";
            this.tvCrafts.ReadOnlyBGColor = System.Drawing.SystemColors.Window;
            this.tvCrafts.RowOptions.ShowHeader = false;
            this.tvCrafts.Size = new System.Drawing.Size(398, 417);
            this.tvCrafts.TabIndex = 16;
            this.tvCrafts.Text = "treeListViewEx1";
            this.tvCrafts.CustomDrawCell += new KSPModAdmin.Utils.CommonTools.CustomDrawCellHandler(this.tvCrafts_CustomDrawCell);
            this.tvCrafts.FocusedNodeChanged += new KSPModAdmin.Utils.CommonTools.FocusedNodeChangedHandler(this.tvCrafts_FocusedNodeChanged);
            this.tvCrafts.DoubleClick += new System.EventHandler(this.tvCrafts_DoubleClick);
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(378, 4);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(20, 20);
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::KSPModAdmin.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(298, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnRefresh, "Reloads all crafts from the KSP install folder.");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Image = global::KSPModAdmin.Properties.Resources.airplane_delete;
            this.btnRemove.Location = new System.Drawing.Point(325, 33);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(73, 26);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnRemove, "Removes a ship from the list and unchecks it in the mod selection.");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // ucCrafts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvCrafts);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblBuildingFilter);
            this.Controls.Add(this.cbBuildingFilter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnSwapBuilding);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnRemove);
            this.Name = "ucCrafts";
            this.Size = new System.Drawing.Size(404, 503);
            this.Load += new System.EventHandler(this.ucCrafts_Load);
            this.cmsCrafts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvCrafts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSwapBuilding;
        private System.Windows.Forms.ToolTip ttCrafts;
        private System.Windows.Forms.ComboBox cbBuildingFilter;
        private System.Windows.Forms.Label lblBuildingFilter;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lblCount;
        public System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsCrafts;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        public Utils.CommonTools.TreeListViewEx tvCrafts;
    }
}
