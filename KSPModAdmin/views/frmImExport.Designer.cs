namespace KSPModAdmin.Views
{
    partial class frmImExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImExport));
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbExport = new System.Windows.Forms.PictureBox();
            this.cbModSelection = new PresentationControls.CheckBoxComboBox();
            this.cbIncludeMods = new System.Windows.Forms.CheckBox();
            this.rbExportSelectedOnly = new System.Windows.Forms.RadioButton();
            this.rbExportAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbCopyDestination = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbInstall = new System.Windows.Forms.RadioButton();
            this.rbAddOnly = new System.Windows.Forms.RadioButton();
            this.cbClearModSelection = new System.Windows.Forms.CheckBox();
            this.pbImport = new System.Windows.Forms.PictureBox();
            this.cbExtract = new System.Windows.Forms.CheckBox();
            this.lblCurrentAction = new System.Windows.Forms.Label();
            this.cbDownloadIfNeeded = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.ttModImExport = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExport)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImport)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbExport);
            this.groupBox1.Controls.Add(this.cbModSelection);
            this.groupBox1.Controls.Add(this.cbIncludeMods);
            this.groupBox1.Controls.Add(this.rbExportSelectedOnly);
            this.groupBox1.Controls.Add(this.rbExportAll);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 131);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export:";
            // 
            // pbExport
            // 
            this.pbExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbExport.Image = ((System.Drawing.Image)(resources.GetObject("pbExport.Image")));
            this.pbExport.Location = new System.Drawing.Point(333, 94);
            this.pbExport.Name = "pbExport";
            this.pbExport.Size = new System.Drawing.Size(20, 20);
            this.pbExport.TabIndex = 13;
            this.pbExport.TabStop = false;
            this.pbExport.Visible = false;
            // 
            // cbModSelection
            // 
            this.cbModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbModSelection.CheckBoxProperties = checkBoxProperties1;
            this.cbModSelection.DisplayMemberSingleItem = "";
            this.cbModSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModSelection.Enabled = false;
            this.cbModSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModSelection.FormattingEnabled = true;
            this.cbModSelection.Location = new System.Drawing.Point(47, 93);
            this.cbModSelection.Name = "cbModSelection";
            this.cbModSelection.Size = new System.Drawing.Size(275, 21);
            this.cbModSelection.TabIndex = 3;
            // 
            // cbIncludeMods
            // 
            this.cbIncludeMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIncludeMods.AutoSize = true;
            this.cbIncludeMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeMods.Location = new System.Drawing.Point(304, 21);
            this.cbIncludeMods.Name = "cbIncludeMods";
            this.cbIncludeMods.Size = new System.Drawing.Size(130, 17);
            this.cbIncludeMods.TabIndex = 2;
            this.cbIncludeMods.Text = "Include mod archives.";
            this.cbIncludeMods.UseVisualStyleBackColor = true;
            // 
            // rbExportSelectedOnly
            // 
            this.rbExportSelectedOnly.AutoSize = true;
            this.rbExportSelectedOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportSelectedOnly.Location = new System.Drawing.Point(28, 70);
            this.rbExportSelectedOnly.Name = "rbExportSelectedOnly";
            this.rbExportSelectedOnly.Size = new System.Drawing.Size(117, 17);
            this.rbExportSelectedOnly.TabIndex = 2;
            this.rbExportSelectedOnly.Text = "Selected mods only";
            this.rbExportSelectedOnly.UseVisualStyleBackColor = true;
            this.rbExportSelectedOnly.CheckedChanged += new System.EventHandler(this.rbExportSelectedOnly_CheckedChanged);
            // 
            // rbExportAll
            // 
            this.rbExportAll.AutoSize = true;
            this.rbExportAll.Checked = true;
            this.rbExportAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportAll.Location = new System.Drawing.Point(28, 47);
            this.rbExportAll.Name = "rbExportAll";
            this.rbExportAll.Size = new System.Drawing.Size(155, 17);
            this.rbExportAll.TabIndex = 2;
            this.rbExportAll.TabStop = true;
            this.rbExportAll.Text = "Complete ModSelection list.";
            this.rbExportAll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Export a ModPack for sharing with other players.";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = global::KSPModAdmin.Properties.Resources.components_scroll_into;
            this.btnExport.Location = new System.Drawing.Point(359, 91);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.cbClearModSelection);
            this.groupBox2.Controls.Add(this.pbImport);
            this.groupBox2.Controls.Add(this.cbExtract);
            this.groupBox2.Controls.Add(this.lblCurrentAction);
            this.groupBox2.Controls.Add(this.cbDownloadIfNeeded);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 245);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rbCopyDestination);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(16, 95);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(418, 52);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Destination options:";
            // 
            // rbCopyDestination
            // 
            this.rbCopyDestination.AutoSize = true;
            this.rbCopyDestination.Checked = true;
            this.rbCopyDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCopyDestination.Location = new System.Drawing.Point(27, 19);
            this.rbCopyDestination.Name = "rbCopyDestination";
            this.rbCopyDestination.Size = new System.Drawing.Size(106, 17);
            this.rbCopyDestination.TabIndex = 2;
            this.rbCopyDestination.TabStop = true;
            this.rbCopyDestination.Text = "Copy destination.";
            this.rbCopyDestination.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(139, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(176, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "User Auto destination detection.";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rbInstall);
            this.groupBox3.Controls.Add(this.rbAddOnly);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(16, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 52);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Install options:";
            // 
            // rbInstall
            // 
            this.rbInstall.AutoSize = true;
            this.rbInstall.Checked = true;
            this.rbInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInstall.Location = new System.Drawing.Point(27, 19);
            this.rbInstall.Name = "rbInstall";
            this.rbInstall.Size = new System.Drawing.Size(83, 17);
            this.rbInstall.TabIndex = 2;
            this.rbInstall.TabStop = true;
            this.rbInstall.Text = "Install mods.";
            this.rbInstall.UseVisualStyleBackColor = true;
            // 
            // rbAddOnly
            // 
            this.rbAddOnly.AutoSize = true;
            this.rbAddOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAddOnly.Location = new System.Drawing.Point(139, 19);
            this.rbAddOnly.Name = "rbAddOnly";
            this.rbAddOnly.Size = new System.Drawing.Size(169, 17);
            this.rbAddOnly.TabIndex = 2;
            this.rbAddOnly.Text = "Add mods only (manual install).";
            this.rbAddOnly.UseVisualStyleBackColor = true;
            // 
            // cbClearModSelection
            // 
            this.cbClearModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClearModSelection.AutoSize = true;
            this.cbClearModSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClearModSelection.Location = new System.Drawing.Point(255, 72);
            this.cbClearModSelection.Name = "cbClearModSelection";
            this.cbClearModSelection.Size = new System.Drawing.Size(179, 17);
            this.cbClearModSelection.TabIndex = 2;
            this.cbClearModSelection.Text = "Clear ModSelection befor import.";
            this.cbClearModSelection.UseVisualStyleBackColor = true;
            // 
            // pbImport
            // 
            this.pbImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImport.Image = ((System.Drawing.Image)(resources.GetObject("pbImport.Image")));
            this.pbImport.Location = new System.Drawing.Point(333, 216);
            this.pbImport.Name = "pbImport";
            this.pbImport.Size = new System.Drawing.Size(20, 20);
            this.pbImport.TabIndex = 13;
            this.pbImport.TabStop = false;
            this.pbImport.Visible = false;
            // 
            // cbExtract
            // 
            this.cbExtract.AutoSize = true;
            this.cbExtract.Checked = true;
            this.cbExtract.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExtract.Location = new System.Drawing.Point(28, 72);
            this.cbExtract.Name = "cbExtract";
            this.cbExtract.Size = new System.Drawing.Size(164, 17);
            this.cbExtract.TabIndex = 2;
            this.cbExtract.Text = "Extract mods (if in ModPack).";
            this.cbExtract.UseVisualStyleBackColor = true;
            this.cbExtract.CheckedChanged += new System.EventHandler(this.cbExtract_CheckedChanged);
            // 
            // lblCurrentAction
            // 
            this.lblCurrentAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAction.Location = new System.Drawing.Point(13, 208);
            this.lblCurrentAction.Name = "lblCurrentAction";
            this.lblCurrentAction.Size = new System.Drawing.Size(314, 34);
            this.lblCurrentAction.TabIndex = 1;
            this.lblCurrentAction.Text = "Current Action...";
            // 
            // cbDownloadIfNeeded
            // 
            this.cbDownloadIfNeeded.AutoSize = true;
            this.cbDownloadIfNeeded.Checked = true;
            this.cbDownloadIfNeeded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDownloadIfNeeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDownloadIfNeeded.Location = new System.Drawing.Point(28, 49);
            this.cbDownloadIfNeeded.Name = "cbDownloadIfNeeded";
            this.cbDownloadIfNeeded.Size = new System.Drawing.Size(199, 17);
            this.cbDownloadIfNeeded.TabIndex = 2;
            this.cbDownloadIfNeeded.Text = "Download needed mods (if possible).";
            this.cbDownloadIfNeeded.UseVisualStyleBackColor = true;
            this.cbDownloadIfNeeded.CheckedChanged += new System.EventHandler(this.cbDownloadIfNeeded_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Import mods from a ModPack file.";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Image = global::KSPModAdmin.Properties.Resources.components_scroll_out;
            this.btnImport.Location = new System.Drawing.Point(359, 213);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 25);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "Import";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // frmImExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 391);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 330);
            this.Name = "frmImExport";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModPack im/exporter";
            this.Load += new System.EventHandler(this.frmImExport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExport)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbExportSelectedOnly;
        private System.Windows.Forms.RadioButton rbExportAll;
        private PresentationControls.CheckBoxComboBox cbModSelection;
        private System.Windows.Forms.CheckBox cbExtract;
        private System.Windows.Forms.CheckBox cbDownloadIfNeeded;
        private System.Windows.Forms.CheckBox cbIncludeMods;
        private System.Windows.Forms.PictureBox pbExport;
        private System.Windows.Forms.PictureBox pbImport;
        private System.Windows.Forms.RadioButton rbAddOnly;
        private System.Windows.Forms.RadioButton rbInstall;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbCopyDestination;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbClearModSelection;
        private System.Windows.Forms.ToolTip ttModImExport;
        private System.Windows.Forms.Label lblCurrentAction;
    }
}