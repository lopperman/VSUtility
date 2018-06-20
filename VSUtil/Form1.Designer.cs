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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workItemHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentMetricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboRealProject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTeam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTeamAreas = new System.Windows.Forms.CheckedListBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lstFuzzFile = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstIterationPaths = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(886, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Dump Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.developmentMetricsToolStripMenuItem,
            this.dynamicChartToolStripMenuItem});
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
            // dynamicChartToolStripMenuItem
            // 
            this.dynamicChartToolStripMenuItem.Name = "dynamicChartToolStripMenuItem";
            this.dynamicChartToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.dynamicChartToolStripMenuItem.Text = "Dynamic Chart";
            this.dynamicChartToolStripMenuItem.Click += new System.EventHandler(this.dynamicChartToolStripMenuItem_Click);
            // 
            // cboRealProject
            // 
            this.cboRealProject.FormattingEnabled = true;
            this.cboRealProject.Location = new System.Drawing.Point(80, 92);
            this.cboRealProject.Name = "cboRealProject";
            this.cboRealProject.Size = new System.Drawing.Size(220, 21);
            this.cboRealProject.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Project:";
            // 
            // cboTeam
            // 
            this.cboTeam.FormattingEnabled = true;
            this.cboTeam.Location = new System.Drawing.Point(80, 119);
            this.cboTeam.Name = "cboTeam";
            this.cboTeam.Size = new System.Drawing.Size(261, 21);
            this.cboTeam.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Team:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Areas:";
            // 
            // lstTeamAreas
            // 
            this.lstTeamAreas.FormattingEnabled = true;
            this.lstTeamAreas.Location = new System.Drawing.Point(80, 146);
            this.lstTeamAreas.Name = "lstTeamAreas";
            this.lstTeamAreas.Size = new System.Drawing.Size(261, 94);
            this.lstTeamAreas.TabIndex = 34;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblInfo.Location = new System.Drawing.Point(31, 36);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(81, 24);
            this.lblInfo.TabIndex = 35;
            this.lblInfo.Text = "Project:";
            // 
            // lstFuzzFile
            // 
            this.lstFuzzFile.FormattingEnabled = true;
            this.lstFuzzFile.Location = new System.Drawing.Point(80, 293);
            this.lstFuzzFile.Name = "lstFuzzFile";
            this.lstFuzzFile.Size = new System.Drawing.Size(842, 212);
            this.lstFuzzFile.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(76, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 24);
            this.label4.TabIndex = 51;
            this.label4.Text = "Current Fuzz File";
            // 
            // lstIterationPaths
            // 
            this.lstIterationPaths.FormattingEnabled = true;
            this.lstIterationPaths.Location = new System.Drawing.Point(453, 146);
            this.lstIterationPaths.Name = "lstIterationPaths";
            this.lstIterationPaths.Size = new System.Drawing.Size(386, 94);
            this.lstIterationPaths.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Iteration Paths:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 550);
            this.Controls.Add(this.lstIterationPaths);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstFuzzFile);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lstTeamAreas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTeam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboRealProject);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workItemHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developmentMetricsToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboRealProject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTeam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem dynamicChartToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox lstTeamAreas;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ListBox lstFuzzFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox lstIterationPaths;
        private System.Windows.Forms.Label label5;
    }
}

