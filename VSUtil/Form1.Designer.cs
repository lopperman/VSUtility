namespace VSUtil
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workItemHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentMetricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboRealProject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(374, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Dump Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 109);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(739, 407);
            this.textBox1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilitiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(973, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workItemHistoryToolStripMenuItem,
            this.developmentMetricsToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.utilitiesToolStripMenuItem.Text = "&Utilities";
            // 
            // workItemHistoryToolStripMenuItem
            // 
            this.workItemHistoryToolStripMenuItem.Name = "workItemHistoryToolStripMenuItem";
            this.workItemHistoryToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.workItemHistoryToolStripMenuItem.Text = "&Work Item History";
            this.workItemHistoryToolStripMenuItem.Click += new System.EventHandler(this.workItemHistoryToolStripMenuItem_Click);
            // 
            // developmentMetricsToolStripMenuItem
            // 
            this.developmentMetricsToolStripMenuItem.Name = "developmentMetricsToolStripMenuItem";
            this.developmentMetricsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.developmentMetricsToolStripMenuItem.Text = "&Development Metrics";
            this.developmentMetricsToolStripMenuItem.Click += new System.EventHandler(this.developmentMetricsToolStripMenuItem_Click);
            // 
            // cboRealProject
            // 
            this.cboRealProject.FormattingEnabled = true;
            this.cboRealProject.Location = new System.Drawing.Point(107, 65);
            this.cboRealProject.Name = "cboRealProject";
            this.cboRealProject.Size = new System.Drawing.Size(220, 21);
            this.cboRealProject.TabIndex = 29;
            this.cboRealProject.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Project:";
            this.label3.Visible = false;
            // 
            // cboProject
            // 
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(107, 38);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(261, 21);
            this.cboProject.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Project/Team:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 550);
            this.Controls.Add(this.cboProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboRealProject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "VSTS Utilities";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workItemHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developmentMetricsToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboRealProject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.Label label2;
    }
}

