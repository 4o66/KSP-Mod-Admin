namespace KSPModAdmin.Views
{
    partial class frmAddMod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddMod));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbModName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbModPath = new System.Windows.Forms.TextBox();
            this.btnFolderSearch = new System.Windows.Forms.Button();
            this.ttAddMod = new System.Windows.Forms.ToolTip(this.components);
            this.cbInstallAfterAdd = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::KSPModAdmin.Properties.Resources.component_add;
            this.btnAdd.Location = new System.Drawing.Point(212, 170);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttAddMod.SetToolTip(this.btnAdd, "Adds the mod to the ModSelection.");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::KSPModAdmin.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(293, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttAddMod.SetToolTip(this.btnCancel, "Close dialog without adding a mod.");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ModName (leave blank for auto fill):";
            // 
            // tbModName
            // 
            this.tbModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbModName.Location = new System.Drawing.Point(28, 37);
            this.tbModName.Name = "tbModName";
            this.tbModName.Size = new System.Drawing.Size(324, 20);
            this.tbModName.TabIndex = 1;
            this.ttAddMod.SetToolTip(this.tbModName, "Enter a name for the mod or leave it blank for default name.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter mod archive path or Spaceport/CurseForge/KSPForum URL:";
            // 
            // tbModPath
            // 
            this.tbModPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbModPath.Location = new System.Drawing.Point(28, 85);
            this.tbModPath.Name = "tbModPath";
            this.tbModPath.Size = new System.Drawing.Size(293, 20);
            this.tbModPath.TabIndex = 2;
            this.ttAddMod.SetToolTip(this.tbModPath, "Enter a path to a mod archive, a craft, a Spaceport URL or KSP Forum URL\r\n(URL wi" +
        "th http:// or https://).");
            // 
            // btnFolderSearch
            // 
            this.btnFolderSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderSearch.Image = global::KSPModAdmin.Properties.Resources.folder_view;
            this.btnFolderSearch.Location = new System.Drawing.Point(327, 83);
            this.btnFolderSearch.Name = "btnFolderSearch";
            this.btnFolderSearch.Size = new System.Drawing.Size(25, 24);
            this.btnFolderSearch.TabIndex = 3;
            this.btnFolderSearch.Tag = "\"Start\"";
            this.btnFolderSearch.UseVisualStyleBackColor = true;
            this.btnFolderSearch.Click += new System.EventHandler(this.btnFolderSearch_Click);
            // 
            // cbInstallAfterAdd
            // 
            this.cbInstallAfterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInstallAfterAdd.AutoSize = true;
            this.cbInstallAfterAdd.Location = new System.Drawing.Point(28, 174);
            this.cbInstallAfterAdd.Name = "cbInstallAfterAdd";
            this.cbInstallAfterAdd.Size = new System.Drawing.Size(101, 17);
            this.cbInstallAfterAdd.TabIndex = 6;
            this.cbInstallAfterAdd.Text = "Install after add.";
            this.ttAddMod.SetToolTip(this.cbInstallAfterAdd, "Check to let KSP MA install the new mod right after adding.");
            this.cbInstallAfterAdd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(16, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(352, 43);
            this.label3.TabIndex = 1;
            this.label3.Text = "NOTE: After KSP MA has added the mod you have to check / uncheck your wanted part" +
    "s of the mod and press \"Process All\" to install the mod.\r\nOr check the checkbox " +
    "below for immediate install of the mod.";
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(188, 173);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(18, 20);
            this.picLoading.TabIndex = 16;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // frmAddMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 205);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.cbInstallAfterAdd);
            this.Controls.Add(this.btnFolderSearch);
            this.Controls.Add(this.tbModPath);
            this.Controls.Add(this.tbModName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddMod";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adding mod";
            this.Load += new System.EventHandler(this.frmAddMod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbModName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbModPath;
        private System.Windows.Forms.Button btnFolderSearch;
        private System.Windows.Forms.ToolTip ttAddMod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbInstallAfterAdd;
        private System.Windows.Forms.PictureBox picLoading;
    }
}