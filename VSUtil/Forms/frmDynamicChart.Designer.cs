namespace VSUtil.Forms
{
    partial class frmDynamicChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.cmdRender = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtStartDt = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbBug = new System.Windows.Forms.RadioButton();
            this.rbUserStory = new System.Windows.Forms.RadioButton();
            this.tpRender = new System.Windows.Forms.TabPage();
            this.chartDynamic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDynamic)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Controls.Add(this.tpRender);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1112, 669);
            this.tabControl1.TabIndex = 0;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.cmdRender);
            this.tpConfig.Controls.Add(this.label5);
            this.tpConfig.Controls.Add(this.dtStartDt);
            this.tpConfig.Controls.Add(this.groupBox1);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(1104, 643);
            this.tpConfig.TabIndex = 0;
            this.tpConfig.Text = "Configure";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // cmdRender
            // 
            this.cmdRender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRender.Location = new System.Drawing.Point(496, 42);
            this.cmdRender.Name = "cmdRender";
            this.cmdRender.Size = new System.Drawing.Size(129, 23);
            this.cmdRender.TabIndex = 50;
            this.cmdRender.Text = "Update Chart";
            this.cmdRender.UseVisualStyleBackColor = true;
            this.cmdRender.Click += new System.EventHandler(this.cmdRender_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Show Data From:";
            // 
            // dtStartDt
            // 
            this.dtStartDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDt.Location = new System.Drawing.Point(338, 45);
            this.dtStartDt.Name = "dtStartDt";
            this.dtStartDt.Size = new System.Drawing.Size(123, 20);
            this.dtStartDt.TabIndex = 48;
            this.dtStartDt.Value = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBug);
            this.groupBox1.Controls.Add(this.rbUserStory);
            this.groupBox1.Location = new System.Drawing.Point(20, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Work Item Type";
            // 
            // rbBug
            // 
            this.rbBug.AutoSize = true;
            this.rbBug.Location = new System.Drawing.Point(120, 33);
            this.rbBug.Name = "rbBug";
            this.rbBug.Size = new System.Drawing.Size(44, 17);
            this.rbBug.TabIndex = 1;
            this.rbBug.Text = "Bug";
            this.rbBug.UseVisualStyleBackColor = true;
            // 
            // rbUserStory
            // 
            this.rbUserStory.AutoSize = true;
            this.rbUserStory.Checked = true;
            this.rbUserStory.Location = new System.Drawing.Point(23, 33);
            this.rbUserStory.Name = "rbUserStory";
            this.rbUserStory.Size = new System.Drawing.Size(74, 17);
            this.rbUserStory.TabIndex = 0;
            this.rbUserStory.TabStop = true;
            this.rbUserStory.Text = "User Story";
            this.rbUserStory.UseVisualStyleBackColor = true;
            // 
            // tpRender
            // 
            this.tpRender.Controls.Add(this.chartDynamic);
            this.tpRender.Controls.Add(this.panel1);
            this.tpRender.Location = new System.Drawing.Point(4, 22);
            this.tpRender.Name = "tpRender";
            this.tpRender.Padding = new System.Windows.Forms.Padding(3);
            this.tpRender.Size = new System.Drawing.Size(1104, 643);
            this.tpRender.TabIndex = 1;
            this.tpRender.Text = "Render";
            this.tpRender.UseVisualStyleBackColor = true;
            // 
            // chartDynamic
            // 
            chartArea1.AxisX.Interval = 7D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea1.AxisX.MajorTickMark.Interval = 0D;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea1.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea1.Name = "ChartArea1";
            this.chartDynamic.ChartAreas.Add(chartArea1);
            this.chartDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDynamic.Legends.Add(legend1);
            this.chartDynamic.Location = new System.Drawing.Point(3, 71);
            this.chartDynamic.Name = "chartDynamic";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.MediumBlue;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.LegendText = "New";
            series1.Name = "countNew";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartDynamic.Series.Add(series1);
            this.chartDynamic.Size = new System.Drawing.Size(1098, 569);
            this.chartDynamic.TabIndex = 5;
            this.chartDynamic.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 68);
            this.panel1.TabIndex = 4;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Location = new System.Drawing.Point(961, 39);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(129, 23);
            this.cmdPrint.TabIndex = 10;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // frmDynamicChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 669);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmDynamicChart";
            this.Text = "Dynamic Chart";
            this.tabControl1.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDynamic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBug;
        private System.Windows.Forms.RadioButton rbUserStory;
        private System.Windows.Forms.TabPage tpRender;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDynamic;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtStartDt;
        private System.Windows.Forms.Button cmdRender;
    }
}