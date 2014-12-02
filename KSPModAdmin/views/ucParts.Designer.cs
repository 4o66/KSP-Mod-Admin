namespace KSPModAdmin.Views
{
    partial class ucParts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucParts));
            this.ttCrafts = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnChangeCategory = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.cbModFilter = new System.Windows.Forms.ComboBox();
            this.cbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBuildingFilter = new System.Windows.Forms.Label();
            this.cmsParts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.podsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propulsionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structuralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scienceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvParts = new KSPModAdmin.Utils.CommonTools.TreeListViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.cmsParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvParts)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::KSPModAdmin.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(287, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttCrafts.SetToolTip(this.btnRefresh, "Reloads all parts from KSP install folder.");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnChangeCategory
            // 
            this.btnChangeCategory.Enabled = false;
            this.btnChangeCategory.Location = new System.Drawing.Point(90, 61);
            this.btnChangeCategory.Name = "btnChangeCategory";
            this.btnChangeCategory.Size = new System.Drawing.Size(97, 23);
            this.btnChangeCategory.TabIndex = 1;
            this.btnChangeCategory.Text = "Change category";
            this.ttCrafts.SetToolTip(this.btnChangeCategory, "Change the category of the selected part(s).");
            this.btnChangeCategory.UseVisualStyleBackColor = true;
            this.btnChangeCategory.Click += new System.EventHandler(this.btnChangeCategory_Click);
            // 
            // btnRename
            // 
            this.btnRename.Enabled = false;
            this.btnRename.Location = new System.Drawing.Point(17, 61);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(67, 23);
            this.btnRename.TabIndex = 1;
            this.btnRename.Text = "Rename";
            this.ttCrafts.SetToolTip(this.btnRename, "Renames the selected craft.");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // cbModFilter
            // 
            this.cbModFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModFilter.FormattingEnabled = true;
            this.cbModFilter.Items.AddRange(new object[] {
            "All",
            "Squad"});
            this.cbModFilter.Location = new System.Drawing.Point(94, 30);
            this.cbModFilter.Name = "cbModFilter";
            this.cbModFilter.Size = new System.Drawing.Size(187, 21);
            this.cbModFilter.TabIndex = 2;
            this.ttCrafts.SetToolTip(this.cbModFilter, "Filters the part list by Mod");
            this.cbModFilter.SelectedIndexChanged += new System.EventHandler(this.cbBuildingFilter_SelectedIndexChanged);
            // 
            // cbCategoryFilter
            // 
            this.cbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoryFilter.FormattingEnabled = true;
            this.cbCategoryFilter.Items.AddRange(new object[] {
            "All",
            "Pods",
            "Propulsion",
            "Control",
            "Structural",
            "Aero",
            "Utility",
            "Science"});
            this.cbCategoryFilter.Location = new System.Drawing.Point(94, 3);
            this.cbCategoryFilter.Name = "cbCategoryFilter";
            this.cbCategoryFilter.Size = new System.Drawing.Size(187, 21);
            this.cbCategoryFilter.TabIndex = 2;
            this.ttCrafts.SetToolTip(this.cbCategoryFilter, "Filters the part list by category.");
            this.cbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cbBuildingFilter_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(320, 61);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(67, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.ttCrafts.SetToolTip(this.btnRemove, "Removes a part from the list and unchecks it in the mod selection.");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(3, 417);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(47, 13);
            this.lblCount.TabIndex = 16;
            this.lblCount.Text = "Count: 0";
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(367, 4);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(20, 20);
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mod filter:";
            // 
            // lblBuildingFilter
            // 
            this.lblBuildingFilter.AutoSize = true;
            this.lblBuildingFilter.Location = new System.Drawing.Point(14, 6);
            this.lblBuildingFilter.Name = "lblBuildingFilter";
            this.lblBuildingFilter.Size = new System.Drawing.Size(74, 13);
            this.lblBuildingFilter.TabIndex = 3;
            this.lblBuildingFilter.Text = "Category filter:";
            // 
            // cmsParts
            // 
            this.cmsParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripSeparator2,
            this.renameToolStripMenuItem,
            this.changeCategoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem});
            this.cmsParts.Name = "contextMenuStrip1";
            this.cmsParts.Size = new System.Drawing.Size(165, 104);
            this.cmsParts.Opening += new System.ComponentModel.CancelEventHandler(this.cmsParts_Opening);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // changeCategoryToolStripMenuItem
            // 
            this.changeCategoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.podsToolStripMenuItem,
            this.propulsionToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.structuralToolStripMenuItem,
            this.aeroToolStripMenuItem,
            this.utilityToolStripMenuItem,
            this.scienceToolStripMenuItem});
            this.changeCategoryToolStripMenuItem.Name = "changeCategoryToolStripMenuItem";
            this.changeCategoryToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.changeCategoryToolStripMenuItem.Text = "Change category";
            this.changeCategoryToolStripMenuItem.Click += new System.EventHandler(this.btnChangeCategory_Click);
            // 
            // podsToolStripMenuItem
            // 
            this.podsToolStripMenuItem.Name = "podsToolStripMenuItem";
            this.podsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.podsToolStripMenuItem.Text = "Pods";
            // 
            // propulsionToolStripMenuItem
            // 
            this.propulsionToolStripMenuItem.Name = "propulsionToolStripMenuItem";
            this.propulsionToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.propulsionToolStripMenuItem.Text = "Propulsion";
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // structuralToolStripMenuItem
            // 
            this.structuralToolStripMenuItem.Name = "structuralToolStripMenuItem";
            this.structuralToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.structuralToolStripMenuItem.Text = "Structural";
            // 
            // aeroToolStripMenuItem
            // 
            this.aeroToolStripMenuItem.Name = "aeroToolStripMenuItem";
            this.aeroToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.aeroToolStripMenuItem.Text = "Aero";
            // 
            // utilityToolStripMenuItem
            // 
            this.utilityToolStripMenuItem.Name = "utilityToolStripMenuItem";
            this.utilityToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.utilityToolStripMenuItem.Text = "Utility";
            // 
            // scienceToolStripMenuItem
            // 
            this.scienceToolStripMenuItem.Name = "scienceToolStripMenuItem";
            this.scienceToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.scienceToolStripMenuItem.Text = "Science";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // tvParts
            // 
            this.tvParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvParts.ColumnsOptions.AutoSize = true;
            this.tvParts.ExpandOnDoubleClick = false;
            this.tvParts.Images = null;
            this.tvParts.Location = new System.Drawing.Point(3, 90);
            this.tvParts.Name = "tvParts";
            this.tvParts.ReadOnlyBGColor = System.Drawing.SystemColors.Window;
            this.tvParts.RowOptions.ShowHeader = false;
            this.tvParts.Size = new System.Drawing.Size(388, 324);
            this.tvParts.TabIndex = 17;
            this.tvParts.Text = "treeListViewEx1";
            this.tvParts.FocusedNodeChanged += new KSPModAdmin.Utils.CommonTools.FocusedNodeChangedHandler(this.tvParts_FocusedNodeChanged);
            this.tvParts.DoubleClick += new System.EventHandler(this.tvParts_DoubleClick);
            // 
            // ucParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvParts);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBuildingFilter);
            this.Controls.Add(this.cbModFilter);
            this.Controls.Add(this.cbCategoryFilter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeCategory);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnRemove);
            this.Name = "ucParts";
            this.Size = new System.Drawing.Size(393, 435);
            this.Load += new System.EventHandler(this.ucParts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.cmsParts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangeCategory;
        private System.Windows.Forms.ToolTip ttCrafts;
        private System.Windows.Forms.ComboBox cbCategoryFilter;
        private System.Windows.Forms.Label lblBuildingFilter;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.ComboBox cbModFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ContextMenuStrip cmsParts;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem podsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propulsionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem structuralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scienceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public Utils.CommonTools.TreeListViewEx tvParts;
    }
}
