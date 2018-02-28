namespace VSUtil
{
    partial class frmWorkItemHistory
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
            this.cmdView = new System.Windows.Forms.Button();
            this.txtWorkItemId = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmdCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdView
            // 
            this.cmdView.Location = new System.Drawing.Point(118, 10);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(75, 23);
            this.cmdView.TabIndex = 0;
            this.cmdView.Text = "View";
            this.cmdView.UseVisualStyleBackColor = true;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // txtWorkItemId
            // 
            this.txtWorkItemId.Location = new System.Drawing.Point(12, 12);
            this.txtWorkItemId.Name = "txtWorkItemId";
            this.txtWorkItemId.Size = new System.Drawing.Size(100, 20);
            this.txtWorkItemId.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 47);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1170, 550);
            this.dgvData.TabIndex = 2;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmdCopy);
            this.pnlTop.Controls.Add(this.txtWorkItemId);
            this.pnlTop.Controls.Add(this.cmdView);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1170, 47);
            this.pnlTop.TabIndex = 3;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(199, 10);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdCopy.TabIndex = 2;
            this.cmdCopy.Text = "Export";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // frmWorkItemHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 597);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmWorkItemHistory";
            this.Text = "Work Item History";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.TextBox txtWorkItemId;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button cmdCopy;
    }
}