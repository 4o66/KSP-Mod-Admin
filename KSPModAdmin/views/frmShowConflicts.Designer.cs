namespace KSPModAdmin.Views
{
    partial class frmShowConflicts
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
            this.dgvConflicts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConflicts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConflicts
            // 
            this.dgvConflicts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConflicts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConflicts.Location = new System.Drawing.Point(0, 0);
            this.dgvConflicts.Name = "dgvConflicts";
            this.dgvConflicts.Size = new System.Drawing.Size(603, 373);
            this.dgvConflicts.TabIndex = 0;
            // 
            // frmShowConflicts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 373);
            this.Controls.Add(this.dgvConflicts);
            this.MinimizeBox = false;
            this.Name = "frmShowConflicts";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conflicting files";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConflicts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConflicts;
    }
}