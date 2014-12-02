namespace KSPModAdmin.Views
{
    partial class frmCollisionSolving
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCollisionSolving));
            this.rbKeepInstalled = new System.Windows.Forms.RadioButton();
            this.rbKeepSelected = new System.Windows.Forms.RadioButton();
            this.cbModSelect = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInstalledMod = new System.Windows.Forms.TextBox();
            this.btnShowConflicts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbKeepInstalled
            // 
            this.rbKeepInstalled.AutoSize = true;
            this.rbKeepInstalled.Checked = true;
            this.rbKeepInstalled.Location = new System.Drawing.Point(44, 89);
            this.rbKeepInstalled.Name = "rbKeepInstalled";
            this.rbKeepInstalled.Size = new System.Drawing.Size(175, 17);
            this.rbKeepInstalled.TabIndex = 0;
            this.rbKeepInstalled.TabStop = true;
            this.rbKeepInstalled.Text = "Keep already installed mod files.";
            this.rbKeepInstalled.UseVisualStyleBackColor = true;
            // 
            // rbKeepSelected
            // 
            this.rbKeepSelected.AutoSize = true;
            this.rbKeepSelected.Location = new System.Drawing.Point(44, 148);
            this.rbKeepSelected.Name = "rbKeepSelected";
            this.rbKeepSelected.Size = new System.Drawing.Size(213, 17);
            this.rbKeepSelected.TabIndex = 0;
            this.rbKeepSelected.Text = "Select a mod that should install the files.";
            this.rbKeepSelected.UseVisualStyleBackColor = true;
            this.rbKeepSelected.CheckedChanged += new System.EventHandler(this.rbKeepSelected_CheckedChanged);
            // 
            // cbModSelect
            // 
            this.cbModSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModSelect.Enabled = false;
            this.cbModSelect.FormattingEnabled = true;
            this.cbModSelect.Location = new System.Drawing.Point(62, 173);
            this.cbModSelect.Name = "cbModSelect";
            this.cbModSelect.Size = new System.Drawing.Size(195, 21);
            this.cbModSelect.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::KSPModAdmin.Properties.Resources.component_replace;
            this.btnOK.Location = new System.Drawing.Point(212, 206);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Collision with other mod detected.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(30, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 41);
            this.label2.TabIndex = 4;
            this.label2.Text = "One or more files of the mod are already installed by another mod.\r\nChose one of " +
    "the following options.";
            // 
            // tbInstalledMod
            // 
            this.tbInstalledMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInstalledMod.Enabled = false;
            this.tbInstalledMod.Location = new System.Drawing.Point(62, 113);
            this.tbInstalledMod.Name = "tbInstalledMod";
            this.tbInstalledMod.Size = new System.Drawing.Size(195, 20);
            this.tbInstalledMod.TabIndex = 5;
            // 
            // btnShowConflicts
            // 
            this.btnShowConflicts.Location = new System.Drawing.Point(12, 207);
            this.btnShowConflicts.Name = "btnShowConflicts";
            this.btnShowConflicts.Size = new System.Drawing.Size(105, 23);
            this.btnShowConflicts.TabIndex = 6;
            this.btnShowConflicts.Text = "Show conflicts";
            this.btnShowConflicts.UseVisualStyleBackColor = true;
            this.btnShowConflicts.Click += new System.EventHandler(this.btnShowConflicts_Click);
            // 
            // frmCollisionSolving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 241);
            this.Controls.Add(this.btnShowConflicts);
            this.Controls.Add(this.tbInstalledMod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbModSelect);
            this.Controls.Add(this.rbKeepSelected);
            this.Controls.Add(this.rbKeepInstalled);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 279);
            this.Name = "frmCollisionSolving";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Collision solving";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbKeepInstalled;
        private System.Windows.Forms.RadioButton rbKeepSelected;
        private System.Windows.Forms.ComboBox cbModSelect;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInstalledMod;
        private System.Windows.Forms.Button btnShowConflicts;
    }
}