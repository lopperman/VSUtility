namespace TFSUtilities.Forms
{
    partial class InfoFrm
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblCN = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdNewJoke = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(730, 112);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "label1";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCN
            // 
            this.lblCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCN.Location = new System.Drawing.Point(0, 112);
            this.lblCN.Name = "lblCN";
            this.lblCN.Size = new System.Drawing.Size(730, 222);
            this.lblCN.TabIndex = 1;
            this.lblCN.Text = "label1";
            this.lblCN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(655, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdNewJoke
            // 
            this.cmdNewJoke.Location = new System.Drawing.Point(655, 29);
            this.cmdNewJoke.Name = "cmdNewJoke";
            this.cmdNewJoke.Size = new System.Drawing.Size(75, 23);
            this.cmdNewJoke.TabIndex = 3;
            this.cmdNewJoke.Text = "New Joke";
            this.cmdNewJoke.UseVisualStyleBackColor = true;
            this.cmdNewJoke.Click += new System.EventHandler(this.cmdNewJoke_Click);
            // 
            // InfoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 334);
            this.ControlBox = false;
            this.Controls.Add(this.cmdNewJoke);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblCN);
            this.Controls.Add(this.lblInfo);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notify";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblCN;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdNewJoke;
    }
}