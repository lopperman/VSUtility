namespace VSUtil.Forms
{
    partial class frmDevMetrics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series27 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series28 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series31 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series32 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series33 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series34 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series35 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series36 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series37 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series38 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series39 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpDevMetrics = new System.Windows.Forms.TabPage();
            this.chartDevelopment = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlDevMetricsTop = new System.Windows.Forms.Panel();
            this.chkUatComplete = new System.Windows.Forms.CheckBox();
            this.chkQAComplete = new System.Windows.Forms.CheckBox();
            this.chkDevComplete = new System.Windows.Forms.CheckBox();
            this.chkExcludeCurrentWeek = new System.Windows.Forms.CheckBox();
            this.chkShowTrends = new System.Windows.Forms.CheckBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdRender = new System.Windows.Forms.Button();
            this.lastXMonths = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.forecastWeeks = new System.Windows.Forms.NumericUpDown();
            this.tpFeatures = new System.Windows.Forms.TabPage();
            this.gridFeatures = new System.Windows.Forms.DataGridView();
            this.pnlFeaturesTop = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdUpdateFeatures = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBugStory = new System.Windows.Forms.CheckBox();
            this.chkUserStory = new System.Windows.Forms.CheckBox();
            this.gbFeaturesSearch = new System.Windows.Forms.GroupBox();
            this.rbFeatureIDs = new System.Windows.Forms.RadioButton();
            this.rbTags = new System.Windows.Forms.RadioButton();
            this.txtFeatures = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpCompletedWork = new System.Windows.Forms.TabPage();
            this.gridCompletedWork = new System.Windows.Forms.DataGridView();
            this.pnlCompletedWorkTop = new System.Windows.Forms.Panel();
            this.cmdUpdateCompletedWork = new System.Windows.Forms.Button();
            this.dtCompletedWorkEnd = new System.Windows.Forms.DateTimePicker();
            this.dtCompletedWorkStart = new System.Windows.Forms.DateTimePicker();
            this.tpStoryCumulativeFlow = new System.Windows.Forms.TabPage();
            this.chartStoryCumulativeFlow = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlStoryCumFlowTop = new System.Windows.Forms.Panel();
            this.cmdFlowUncheckAll = new System.Windows.Forms.Button();
            this.cmdFlowCheckAll = new System.Windows.Forms.Button();
            this.clrClosed = new System.Windows.Forms.Panel();
            this.clrPOReviewDone = new System.Windows.Forms.Panel();
            this.clrSmoketestDone = new System.Windows.Forms.Panel();
            this.clrWegmansQA = new System.Windows.Forms.Panel();
            this.clrSmoketest = new System.Windows.Forms.Panel();
            this.clrPOReview = new System.Windows.Forms.Panel();
            this.clrAsynchronyQADone = new System.Windows.Forms.Panel();
            this.clrWegmansQADone = new System.Windows.Forms.Panel();
            this.clrDevelopmentDone = new System.Windows.Forms.Panel();
            this.clrAsynchronyQA = new System.Windows.Forms.Panel();
            this.clrDevelopment = new System.Windows.Forms.Panel();
            this.clrBacklog = new System.Windows.Forms.Panel();
            this.chkFlow_ShowClosed = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowSmoketestDone = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowSmoketest = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowPOReviewDone = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowPOReview = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowWegmansQADone = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowWegmansQA = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowAsynchronyQADone = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowAsynchronyQA = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowDevelopmentDone = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowDevelopment = new System.Windows.Forms.CheckBox();
            this.chkFlow_ShowBacklog = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdUpdateStoryCumulativeFlow = new System.Windows.Forms.Button();
            this.cmdPrintStoryCumulativeFlow = new System.Windows.Forms.Button();
            this.tpBugs = new System.Windows.Forms.TabPage();
            this.chartBugs = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkBugsExcludeCurrentWeek = new System.Windows.Forms.CheckBox();
            this.chkShowNewWorkedClosedBugs = new System.Windows.Forms.CheckBox();
            this.chkShowRemainingBugsBySeverity = new System.Windows.Forms.CheckBox();
            this.chkShowRemainingBugs = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtBugStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmdRenderBugs = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.lstBoxCompletedWorkTFSStateList = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tcMain.SuspendLayout();
            this.tpDevMetrics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDevelopment)).BeginInit();
            this.pnlDevMetricsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastXMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forecastWeeks)).BeginInit();
            this.tpFeatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatures)).BeginInit();
            this.pnlFeaturesTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbFeaturesSearch.SuspendLayout();
            this.tpCompletedWork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCompletedWork)).BeginInit();
            this.pnlCompletedWorkTop.SuspendLayout();
            this.tpStoryCumulativeFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStoryCumulativeFlow)).BeginInit();
            this.pnlStoryCumFlowTop.SuspendLayout();
            this.tpBugs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBugs)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpDevMetrics);
            this.tcMain.Controls.Add(this.tpFeatures);
            this.tcMain.Controls.Add(this.tpCompletedWork);
            this.tcMain.Controls.Add(this.tpStoryCumulativeFlow);
            this.tcMain.Controls.Add(this.tpBugs);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1008, 701);
            this.tcMain.TabIndex = 0;
            this.tcMain.Click += new System.EventHandler(this.tcMain_Click);
            // 
            // tpDevMetrics
            // 
            this.tpDevMetrics.Controls.Add(this.chartDevelopment);
            this.tpDevMetrics.Controls.Add(this.pnlDevMetricsTop);
            this.tpDevMetrics.Location = new System.Drawing.Point(4, 22);
            this.tpDevMetrics.Name = "tpDevMetrics";
            this.tpDevMetrics.Padding = new System.Windows.Forms.Padding(3);
            this.tpDevMetrics.Size = new System.Drawing.Size(1000, 675);
            this.tpDevMetrics.TabIndex = 0;
            this.tpDevMetrics.Text = "Development Metrics";
            this.tpDevMetrics.UseVisualStyleBackColor = true;
            // 
            // chartDevelopment
            // 
            chartArea7.AxisX.Interval = 7D;
            chartArea7.AxisX.MajorGrid.Enabled = false;
            chartArea7.AxisX.MajorGrid.Interval = 0D;
            chartArea7.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea7.AxisX.MajorTickMark.Interval = 0D;
            chartArea7.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea7.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea7.Name = "ChartArea1";
            this.chartDevelopment.ChartAreas.Add(chartArea7);
            this.chartDevelopment.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chartDevelopment.Legends.Add(legend7);
            this.chartDevelopment.Location = new System.Drawing.Point(3, 71);
            this.chartDevelopment.Name = "chartDevelopment";
            series27.ChartArea = "ChartArea1";
            series27.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series27.Color = System.Drawing.Color.MediumBlue;
            series27.IsValueShownAsLabel = true;
            series27.Legend = "Legend1";
            series27.LegendText = "Dev Completed";
            series27.Name = "count";
            series27.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series28.ChartArea = "ChartArea1";
            series28.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series28.Color = System.Drawing.Color.Red;
            series28.IsValueShownAsLabel = true;
            series28.Legend = "Legend1";
            series28.LegendText = "QA Completed";
            series28.Name = "qacount";
            series28.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series29.ChartArea = "ChartArea1";
            series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series29.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series29.IsValueShownAsLabel = true;
            series29.Legend = "Legend1";
            series29.LegendText = "UAT Completed";
            series29.Name = "uatcount";
            series29.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartDevelopment.Series.Add(series27);
            this.chartDevelopment.Series.Add(series28);
            this.chartDevelopment.Series.Add(series29);
            this.chartDevelopment.Size = new System.Drawing.Size(994, 601);
            this.chartDevelopment.TabIndex = 1;
            this.chartDevelopment.Text = "chart1";
            // 
            // pnlDevMetricsTop
            // 
            this.pnlDevMetricsTop.Controls.Add(this.chkUatComplete);
            this.pnlDevMetricsTop.Controls.Add(this.chkQAComplete);
            this.pnlDevMetricsTop.Controls.Add(this.chkDevComplete);
            this.pnlDevMetricsTop.Controls.Add(this.chkExcludeCurrentWeek);
            this.pnlDevMetricsTop.Controls.Add(this.chkShowTrends);
            this.pnlDevMetricsTop.Controls.Add(this.cmdPrint);
            this.pnlDevMetricsTop.Controls.Add(this.cmdRender);
            this.pnlDevMetricsTop.Controls.Add(this.lastXMonths);
            this.pnlDevMetricsTop.Controls.Add(this.label2);
            this.pnlDevMetricsTop.Controls.Add(this.label1);
            this.pnlDevMetricsTop.Controls.Add(this.forecastWeeks);
            this.pnlDevMetricsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDevMetricsTop.Location = new System.Drawing.Point(3, 3);
            this.pnlDevMetricsTop.Name = "pnlDevMetricsTop";
            this.pnlDevMetricsTop.Size = new System.Drawing.Size(994, 68);
            this.pnlDevMetricsTop.TabIndex = 0;
            // 
            // chkUatComplete
            // 
            this.chkUatComplete.AutoSize = true;
            this.chkUatComplete.Location = new System.Drawing.Point(203, 9);
            this.chkUatComplete.Name = "chkUatComplete";
            this.chkUatComplete.Size = new System.Drawing.Size(95, 17);
            this.chkUatComplete.TabIndex = 18;
            this.chkUatComplete.Text = "UAT Complete";
            this.chkUatComplete.UseVisualStyleBackColor = true;
            this.chkUatComplete.CheckedChanged += new System.EventHandler(this.chkUatComplete_CheckedChanged);
            // 
            // chkQAComplete
            // 
            this.chkQAComplete.AutoSize = true;
            this.chkQAComplete.Checked = true;
            this.chkQAComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQAComplete.Location = new System.Drawing.Point(109, 8);
            this.chkQAComplete.Name = "chkQAComplete";
            this.chkQAComplete.Size = new System.Drawing.Size(88, 17);
            this.chkQAComplete.TabIndex = 17;
            this.chkQAComplete.Text = "QA Complete";
            this.chkQAComplete.UseVisualStyleBackColor = true;
            this.chkQAComplete.CheckedChanged += new System.EventHandler(this.chkQAComplete_CheckedChanged);
            // 
            // chkDevComplete
            // 
            this.chkDevComplete.AutoSize = true;
            this.chkDevComplete.Checked = true;
            this.chkDevComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDevComplete.Location = new System.Drawing.Point(10, 8);
            this.chkDevComplete.Name = "chkDevComplete";
            this.chkDevComplete.Size = new System.Drawing.Size(93, 17);
            this.chkDevComplete.TabIndex = 16;
            this.chkDevComplete.Text = "Dev Complete";
            this.chkDevComplete.UseVisualStyleBackColor = true;
            this.chkDevComplete.CheckedChanged += new System.EventHandler(this.chkDevComplete_CheckedChanged);
            // 
            // chkExcludeCurrentWeek
            // 
            this.chkExcludeCurrentWeek.AutoSize = true;
            this.chkExcludeCurrentWeek.Checked = true;
            this.chkExcludeCurrentWeek.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeCurrentWeek.Location = new System.Drawing.Point(415, 8);
            this.chkExcludeCurrentWeek.Name = "chkExcludeCurrentWeek";
            this.chkExcludeCurrentWeek.Size = new System.Drawing.Size(133, 17);
            this.chkExcludeCurrentWeek.TabIndex = 15;
            this.chkExcludeCurrentWeek.Text = "Exclude Current Week";
            this.chkExcludeCurrentWeek.UseVisualStyleBackColor = true;
            this.chkExcludeCurrentWeek.CheckedChanged += new System.EventHandler(this.chkExcludeCurrentWeek_CheckedChanged);
            // 
            // chkShowTrends
            // 
            this.chkShowTrends.AutoSize = true;
            this.chkShowTrends.Checked = true;
            this.chkShowTrends.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTrends.Location = new System.Drawing.Point(304, 9);
            this.chkShowTrends.Name = "chkShowTrends";
            this.chkShowTrends.Size = new System.Drawing.Size(105, 17);
            this.chkShowTrends.TabIndex = 12;
            this.chkShowTrends.Text = "Show Trendlines";
            this.chkShowTrends.UseVisualStyleBackColor = true;
            this.chkShowTrends.CheckedChanged += new System.EventHandler(this.chkShowTrends_CheckedChanged);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(488, 32);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(129, 23);
            this.cmdPrint.TabIndex = 11;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdRender
            // 
            this.cmdRender.Location = new System.Drawing.Point(353, 31);
            this.cmdRender.Name = "cmdRender";
            this.cmdRender.Size = new System.Drawing.Size(129, 23);
            this.cmdRender.TabIndex = 10;
            this.cmdRender.Text = "Update Chart";
            this.cmdRender.UseVisualStyleBackColor = true;
            this.cmdRender.Click += new System.EventHandler(this.cmdRender_Click);
            // 
            // lastXMonths
            // 
            this.lastXMonths.Enabled = false;
            this.lastXMonths.Location = new System.Drawing.Point(285, 32);
            this.lastXMonths.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.lastXMonths.Name = "lastXMonths";
            this.lastXMonths.Size = new System.Drawing.Size(51, 20);
            this.lastXMonths.TabIndex = 9;
            this.lastXMonths.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Show Last \'X\' Months:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Forecast Weeks:";
            // 
            // forecastWeeks
            // 
            this.forecastWeeks.Enabled = false;
            this.forecastWeeks.Location = new System.Drawing.Point(99, 31);
            this.forecastWeeks.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.forecastWeeks.Name = "forecastWeeks";
            this.forecastWeeks.Size = new System.Drawing.Size(51, 20);
            this.forecastWeeks.TabIndex = 6;
            this.forecastWeeks.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // tpFeatures
            // 
            this.tpFeatures.Controls.Add(this.gridFeatures);
            this.tpFeatures.Controls.Add(this.pnlFeaturesTop);
            this.tpFeatures.Location = new System.Drawing.Point(4, 22);
            this.tpFeatures.Name = "tpFeatures";
            this.tpFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.tpFeatures.Size = new System.Drawing.Size(1000, 675);
            this.tpFeatures.TabIndex = 1;
            this.tpFeatures.Text = "Features";
            this.tpFeatures.UseVisualStyleBackColor = true;
            // 
            // gridFeatures
            // 
            this.gridFeatures.AllowUserToAddRows = false;
            this.gridFeatures.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gridFeatures.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFeatures.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridFeatures.Location = new System.Drawing.Point(3, 98);
            this.gridFeatures.Name = "gridFeatures";
            this.gridFeatures.Size = new System.Drawing.Size(994, 574);
            this.gridFeatures.TabIndex = 2;
            // 
            // pnlFeaturesTop
            // 
            this.pnlFeaturesTop.Controls.Add(this.button1);
            this.pnlFeaturesTop.Controls.Add(this.cmdUpdateFeatures);
            this.pnlFeaturesTop.Controls.Add(this.groupBox1);
            this.pnlFeaturesTop.Controls.Add(this.gbFeaturesSearch);
            this.pnlFeaturesTop.Controls.Add(this.txtFeatures);
            this.pnlFeaturesTop.Controls.Add(this.label3);
            this.pnlFeaturesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFeaturesTop.Location = new System.Drawing.Point(3, 3);
            this.pnlFeaturesTop.Name = "pnlFeaturesTop";
            this.pnlFeaturesTop.Size = new System.Drawing.Size(994, 95);
            this.pnlFeaturesTop.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(901, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdUpdateFeatures
            // 
            this.cmdUpdateFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateFeatures.Location = new System.Drawing.Point(811, 9);
            this.cmdUpdateFeatures.Name = "cmdUpdateFeatures";
            this.cmdUpdateFeatures.Size = new System.Drawing.Size(84, 23);
            this.cmdUpdateFeatures.TabIndex = 14;
            this.cmdUpdateFeatures.Text = "Update";
            this.cmdUpdateFeatures.UseVisualStyleBackColor = true;
            this.cmdUpdateFeatures.Click += new System.EventHandler(this.cmdUpdateFeatures_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBugStory);
            this.groupBox1.Controls.Add(this.chkUserStory);
            this.groupBox1.Location = new System.Drawing.Point(486, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 58);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Include Work Item Types";
            // 
            // chkBugStory
            // 
            this.chkBugStory.AutoSize = true;
            this.chkBugStory.Location = new System.Drawing.Point(87, 19);
            this.chkBugStory.Name = "chkBugStory";
            this.chkBugStory.Size = new System.Drawing.Size(45, 17);
            this.chkBugStory.TabIndex = 2;
            this.chkBugStory.Text = "Bug";
            this.chkBugStory.UseVisualStyleBackColor = true;
            // 
            // chkUserStory
            // 
            this.chkUserStory.AutoSize = true;
            this.chkUserStory.Checked = true;
            this.chkUserStory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserStory.Location = new System.Drawing.Point(6, 19);
            this.chkUserStory.Name = "chkUserStory";
            this.chkUserStory.Size = new System.Drawing.Size(75, 17);
            this.chkUserStory.TabIndex = 0;
            this.chkUserStory.Text = "User Story";
            this.chkUserStory.UseVisualStyleBackColor = true;
            // 
            // gbFeaturesSearch
            // 
            this.gbFeaturesSearch.Controls.Add(this.rbFeatureIDs);
            this.gbFeaturesSearch.Controls.Add(this.rbTags);
            this.gbFeaturesSearch.Location = new System.Drawing.Point(268, 25);
            this.gbFeaturesSearch.Name = "gbFeaturesSearch";
            this.gbFeaturesSearch.Size = new System.Drawing.Size(212, 58);
            this.gbFeaturesSearch.TabIndex = 12;
            this.gbFeaturesSearch.TabStop = false;
            this.gbFeaturesSearch.Text = "Search Type";
            // 
            // rbFeatureIDs
            // 
            this.rbFeatureIDs.AutoSize = true;
            this.rbFeatureIDs.Checked = true;
            this.rbFeatureIDs.Location = new System.Drawing.Point(77, 14);
            this.rbFeatureIDs.Name = "rbFeatureIDs";
            this.rbFeatureIDs.Size = new System.Drawing.Size(82, 17);
            this.rbFeatureIDs.TabIndex = 2;
            this.rbFeatureIDs.TabStop = true;
            this.rbFeatureIDs.Text = "Feature ID\'s";
            this.rbFeatureIDs.UseVisualStyleBackColor = true;
            // 
            // rbTags
            // 
            this.rbTags.AutoSize = true;
            this.rbTags.Location = new System.Drawing.Point(6, 14);
            this.rbTags.Name = "rbTags";
            this.rbTags.Size = new System.Drawing.Size(49, 17);
            this.rbTags.TabIndex = 0;
            this.rbTags.Text = "Tags";
            this.rbTags.UseVisualStyleBackColor = true;
            // 
            // txtFeatures
            // 
            this.txtFeatures.Location = new System.Drawing.Point(14, 25);
            this.txtFeatures.Multiline = true;
            this.txtFeatures.Name = "txtFeatures";
            this.txtFeatures.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFeatures.Size = new System.Drawing.Size(248, 61);
            this.txtFeatures.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(363, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Enter feature IDs or Feature Tags or Target Release , separated by comma:";
            // 
            // tpCompletedWork
            // 
            this.tpCompletedWork.Controls.Add(this.gridCompletedWork);
            this.tpCompletedWork.Controls.Add(this.pnlCompletedWorkTop);
            this.tpCompletedWork.Location = new System.Drawing.Point(4, 22);
            this.tpCompletedWork.Name = "tpCompletedWork";
            this.tpCompletedWork.Size = new System.Drawing.Size(1000, 675);
            this.tpCompletedWork.TabIndex = 2;
            this.tpCompletedWork.Text = "Completed Work";
            this.tpCompletedWork.UseVisualStyleBackColor = true;
            // 
            // gridCompletedWork
            // 
            this.gridCompletedWork.AllowUserToAddRows = false;
            this.gridCompletedWork.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gridCompletedWork.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridCompletedWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCompletedWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCompletedWork.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridCompletedWork.Location = new System.Drawing.Point(0, 172);
            this.gridCompletedWork.Name = "gridCompletedWork";
            this.gridCompletedWork.Size = new System.Drawing.Size(1000, 503);
            this.gridCompletedWork.TabIndex = 1;
            // 
            // pnlCompletedWorkTop
            // 
            this.pnlCompletedWorkTop.Controls.Add(this.label6);
            this.pnlCompletedWorkTop.Controls.Add(this.lstBoxCompletedWorkTFSStateList);
            this.pnlCompletedWorkTop.Controls.Add(this.cmdUpdateCompletedWork);
            this.pnlCompletedWorkTop.Controls.Add(this.dtCompletedWorkEnd);
            this.pnlCompletedWorkTop.Controls.Add(this.dtCompletedWorkStart);
            this.pnlCompletedWorkTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCompletedWorkTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCompletedWorkTop.Name = "pnlCompletedWorkTop";
            this.pnlCompletedWorkTop.Size = new System.Drawing.Size(1000, 172);
            this.pnlCompletedWorkTop.TabIndex = 0;
            // 
            // cmdUpdateCompletedWork
            // 
            this.cmdUpdateCompletedWork.Location = new System.Drawing.Point(367, 14);
            this.cmdUpdateCompletedWork.Name = "cmdUpdateCompletedWork";
            this.cmdUpdateCompletedWork.Size = new System.Drawing.Size(84, 23);
            this.cmdUpdateCompletedWork.TabIndex = 47;
            this.cmdUpdateCompletedWork.Text = "Update";
            this.cmdUpdateCompletedWork.UseVisualStyleBackColor = true;
            this.cmdUpdateCompletedWork.Click += new System.EventHandler(this.cmdUpdateCompletedWork_Click);
            // 
            // dtCompletedWorkEnd
            // 
            this.dtCompletedWorkEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCompletedWorkEnd.Location = new System.Drawing.Point(195, 14);
            this.dtCompletedWorkEnd.Name = "dtCompletedWorkEnd";
            this.dtCompletedWorkEnd.Size = new System.Drawing.Size(123, 20);
            this.dtCompletedWorkEnd.TabIndex = 46;
            // 
            // dtCompletedWorkStart
            // 
            this.dtCompletedWorkStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCompletedWorkStart.Location = new System.Drawing.Point(9, 14);
            this.dtCompletedWorkStart.Name = "dtCompletedWorkStart";
            this.dtCompletedWorkStart.Size = new System.Drawing.Size(123, 20);
            this.dtCompletedWorkStart.TabIndex = 45;
            // 
            // tpStoryCumulativeFlow
            // 
            this.tpStoryCumulativeFlow.Controls.Add(this.chartStoryCumulativeFlow);
            this.tpStoryCumulativeFlow.Controls.Add(this.pnlStoryCumFlowTop);
            this.tpStoryCumulativeFlow.Location = new System.Drawing.Point(4, 22);
            this.tpStoryCumulativeFlow.Name = "tpStoryCumulativeFlow";
            this.tpStoryCumulativeFlow.Size = new System.Drawing.Size(1000, 675);
            this.tpStoryCumulativeFlow.TabIndex = 3;
            this.tpStoryCumulativeFlow.Text = "Story Cumulative Flow";
            this.tpStoryCumulativeFlow.UseVisualStyleBackColor = true;
            // 
            // chartStoryCumulativeFlow
            // 
            chartArea8.AxisX.Interval = 7D;
            chartArea8.AxisX.MajorGrid.Enabled = false;
            chartArea8.AxisX.MajorGrid.Interval = 0D;
            chartArea8.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea8.AxisX.MajorTickMark.Interval = 0D;
            chartArea8.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea8.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea8.Name = "ChartArea1";
            this.chartStoryCumulativeFlow.ChartAreas.Add(chartArea8);
            this.chartStoryCumulativeFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            legend8.Name = "Legend1";
            this.chartStoryCumulativeFlow.Legends.Add(legend8);
            this.chartStoryCumulativeFlow.Location = new System.Drawing.Point(0, 103);
            this.chartStoryCumulativeFlow.Name = "chartStoryCumulativeFlow";
            series30.ChartArea = "ChartArea1";
            series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series30.Color = System.Drawing.Color.DarkSeaGreen;
            series30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series30.IsValueShownAsLabel = true;
            series30.Legend = "Legend1";
            series30.LegendText = "Done";
            series30.Name = "done";
            series30.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series30.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series31.ChartArea = "ChartArea1";
            series31.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series31.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series31.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series31.IsValueShownAsLabel = true;
            series31.Legend = "Legend1";
            series31.LegendText = "In Progress";
            series31.Name = "inProgress";
            series31.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series31.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series32.ChartArea = "ChartArea1";
            series32.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series32.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series32.IsValueShownAsLabel = true;
            series32.Legend = "Legend1";
            series32.LegendText = "Committed";
            series32.Name = "committed";
            series32.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series32.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series33.ChartArea = "ChartArea1";
            series33.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series33.Color = System.Drawing.Color.LightCoral;
            series33.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series33.IsValueShownAsLabel = true;
            series33.Legend = "Legend1";
            series33.LegendText = "Approved";
            series33.Name = "approved";
            series33.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series33.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series34.ChartArea = "ChartArea1";
            series34.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series34.Color = System.Drawing.Color.LightSlateGray;
            series34.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series34.IsValueShownAsLabel = true;
            series34.LabelToolTip = "Backlog";
            series34.Legend = "Legend1";
            series34.LegendText = "Backlog";
            series34.Name = "backlog";
            series34.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series34.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series35.BorderWidth = 2;
            series35.ChartArea = "ChartArea1";
            series35.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series35.Color = System.Drawing.Color.Yellow;
            series35.Legend = "Legend1";
            series35.LegendText = "New Features";
            series35.Name = "newFeatures";
            series35.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series36.ChartArea = "ChartArea1";
            series36.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series36.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            series36.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series36.IsValueShownAsLabel = true;
            series36.LabelToolTip = "Backlog_HighValue";
            series36.Legend = "Legend1";
            series36.LegendText = "Backlog High Value";
            series36.Name = "Backlog_HighValue";
            series36.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series37.ChartArea = "ChartArea1";
            series37.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series37.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series37.IsValueShownAsLabel = true;
            series37.LabelToolTip = "Analysis";
            series37.Legend = "Legend1";
            series37.LegendText = "Analysis";
            series37.Name = "analysis";
            series37.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series38.ChartArea = "ChartArea1";
            series38.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            series38.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series38.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            series38.IsValueShownAsLabel = true;
            series38.LabelToolTip = "Verification";
            series38.Legend = "Legend1";
            series38.LegendText = "Verification";
            series38.Name = "verification";
            series38.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartStoryCumulativeFlow.Series.Add(series30);
            this.chartStoryCumulativeFlow.Series.Add(series31);
            this.chartStoryCumulativeFlow.Series.Add(series32);
            this.chartStoryCumulativeFlow.Series.Add(series33);
            this.chartStoryCumulativeFlow.Series.Add(series34);
            this.chartStoryCumulativeFlow.Series.Add(series35);
            this.chartStoryCumulativeFlow.Series.Add(series36);
            this.chartStoryCumulativeFlow.Series.Add(series37);
            this.chartStoryCumulativeFlow.Series.Add(series38);
            this.chartStoryCumulativeFlow.Size = new System.Drawing.Size(1000, 572);
            this.chartStoryCumulativeFlow.TabIndex = 5;
            this.chartStoryCumulativeFlow.Text = "chart1";
            // 
            // pnlStoryCumFlowTop
            // 
            this.pnlStoryCumFlowTop.Controls.Add(this.cmdFlowUncheckAll);
            this.pnlStoryCumFlowTop.Controls.Add(this.cmdFlowCheckAll);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrClosed);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrPOReviewDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrSmoketestDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrWegmansQA);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrSmoketest);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrPOReview);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrAsynchronyQADone);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrWegmansQADone);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrDevelopmentDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrAsynchronyQA);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrDevelopment);
            this.pnlStoryCumFlowTop.Controls.Add(this.clrBacklog);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowClosed);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowSmoketestDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowSmoketest);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowPOReviewDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowPOReview);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowWegmansQADone);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowWegmansQA);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowAsynchronyQADone);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowAsynchronyQA);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowDevelopmentDone);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowDevelopment);
            this.pnlStoryCumFlowTop.Controls.Add(this.chkFlow_ShowBacklog);
            this.pnlStoryCumFlowTop.Controls.Add(this.label4);
            this.pnlStoryCumFlowTop.Controls.Add(this.cmdUpdateStoryCumulativeFlow);
            this.pnlStoryCumFlowTop.Controls.Add(this.cmdPrintStoryCumulativeFlow);
            this.pnlStoryCumFlowTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStoryCumFlowTop.Location = new System.Drawing.Point(0, 0);
            this.pnlStoryCumFlowTop.Name = "pnlStoryCumFlowTop";
            this.pnlStoryCumFlowTop.Size = new System.Drawing.Size(1000, 103);
            this.pnlStoryCumFlowTop.TabIndex = 4;
            // 
            // cmdFlowUncheckAll
            // 
            this.cmdFlowUncheckAll.Location = new System.Drawing.Point(657, 57);
            this.cmdFlowUncheckAll.Name = "cmdFlowUncheckAll";
            this.cmdFlowUncheckAll.Size = new System.Drawing.Size(102, 23);
            this.cmdFlowUncheckAll.TabIndex = 35;
            this.cmdFlowUncheckAll.Text = "Uncheck All";
            this.cmdFlowUncheckAll.UseVisualStyleBackColor = true;
            this.cmdFlowUncheckAll.Click += new System.EventHandler(this.cmdFlowUncheckAll_Click);
            // 
            // cmdFlowCheckAll
            // 
            this.cmdFlowCheckAll.Location = new System.Drawing.Point(657, 34);
            this.cmdFlowCheckAll.Name = "cmdFlowCheckAll";
            this.cmdFlowCheckAll.Size = new System.Drawing.Size(102, 23);
            this.cmdFlowCheckAll.TabIndex = 34;
            this.cmdFlowCheckAll.Text = "Check All";
            this.cmdFlowCheckAll.UseVisualStyleBackColor = true;
            this.cmdFlowCheckAll.Click += new System.EventHandler(this.cmdFlowCheckAll_Click);
            // 
            // clrClosed
            // 
            this.clrClosed.Location = new System.Drawing.Point(495, 80);
            this.clrClosed.Name = "clrClosed";
            this.clrClosed.Size = new System.Drawing.Size(24, 17);
            this.clrClosed.TabIndex = 33;
            this.clrClosed.Click += new System.EventHandler(this.clrClosed_Click);
            // 
            // clrPOReviewDone
            // 
            this.clrPOReviewDone.Location = new System.Drawing.Point(334, 80);
            this.clrPOReviewDone.Name = "clrPOReviewDone";
            this.clrPOReviewDone.Size = new System.Drawing.Size(24, 17);
            this.clrPOReviewDone.TabIndex = 33;
            this.clrPOReviewDone.Click += new System.EventHandler(this.clrPOReviewDone_Click);
            // 
            // clrSmoketestDone
            // 
            this.clrSmoketestDone.Location = new System.Drawing.Point(495, 57);
            this.clrSmoketestDone.Name = "clrSmoketestDone";
            this.clrSmoketestDone.Size = new System.Drawing.Size(24, 17);
            this.clrSmoketestDone.TabIndex = 32;
            this.clrSmoketestDone.Click += new System.EventHandler(this.clrSmoketestDone_Click);
            // 
            // clrWegmansQA
            // 
            this.clrWegmansQA.Location = new System.Drawing.Point(162, 80);
            this.clrWegmansQA.Name = "clrWegmansQA";
            this.clrWegmansQA.Size = new System.Drawing.Size(24, 17);
            this.clrWegmansQA.TabIndex = 33;
            this.clrWegmansQA.Click += new System.EventHandler(this.clrWegmansQA_Click);
            // 
            // clrSmoketest
            // 
            this.clrSmoketest.Location = new System.Drawing.Point(495, 34);
            this.clrSmoketest.Name = "clrSmoketest";
            this.clrSmoketest.Size = new System.Drawing.Size(24, 17);
            this.clrSmoketest.TabIndex = 31;
            this.clrSmoketest.Click += new System.EventHandler(this.clrSmoketest_Click);
            // 
            // clrPOReview
            // 
            this.clrPOReview.Location = new System.Drawing.Point(334, 57);
            this.clrPOReview.Name = "clrPOReview";
            this.clrPOReview.Size = new System.Drawing.Size(24, 17);
            this.clrPOReview.TabIndex = 32;
            this.clrPOReview.Click += new System.EventHandler(this.clrPOReview_Click);
            // 
            // clrAsynchronyQADone
            // 
            this.clrAsynchronyQADone.Location = new System.Drawing.Point(162, 57);
            this.clrAsynchronyQADone.Name = "clrAsynchronyQADone";
            this.clrAsynchronyQADone.Size = new System.Drawing.Size(24, 17);
            this.clrAsynchronyQADone.TabIndex = 32;
            this.clrAsynchronyQADone.Click += new System.EventHandler(this.clrAsynchronyQADone_Click);
            // 
            // clrWegmansQADone
            // 
            this.clrWegmansQADone.Location = new System.Drawing.Point(334, 34);
            this.clrWegmansQADone.Name = "clrWegmansQADone";
            this.clrWegmansQADone.Size = new System.Drawing.Size(24, 17);
            this.clrWegmansQADone.TabIndex = 31;
            this.clrWegmansQADone.Click += new System.EventHandler(this.clrWegmansQADone_Click);
            // 
            // clrDevelopmentDone
            // 
            this.clrDevelopmentDone.Location = new System.Drawing.Point(8, 80);
            this.clrDevelopmentDone.Name = "clrDevelopmentDone";
            this.clrDevelopmentDone.Size = new System.Drawing.Size(24, 17);
            this.clrDevelopmentDone.TabIndex = 30;
            this.clrDevelopmentDone.Click += new System.EventHandler(this.clrDevelopmentDone_Click);
            // 
            // clrAsynchronyQA
            // 
            this.clrAsynchronyQA.Location = new System.Drawing.Point(162, 34);
            this.clrAsynchronyQA.Name = "clrAsynchronyQA";
            this.clrAsynchronyQA.Size = new System.Drawing.Size(24, 17);
            this.clrAsynchronyQA.TabIndex = 31;
            this.clrAsynchronyQA.Click += new System.EventHandler(this.clrAsynchronyQA_Click);
            // 
            // clrDevelopment
            // 
            this.clrDevelopment.Location = new System.Drawing.Point(8, 57);
            this.clrDevelopment.Name = "clrDevelopment";
            this.clrDevelopment.Size = new System.Drawing.Size(24, 17);
            this.clrDevelopment.TabIndex = 29;
            this.clrDevelopment.Click += new System.EventHandler(this.clrDevelopment_Click);
            // 
            // clrBacklog
            // 
            this.clrBacklog.Location = new System.Drawing.Point(8, 34);
            this.clrBacklog.Name = "clrBacklog";
            this.clrBacklog.Size = new System.Drawing.Size(24, 17);
            this.clrBacklog.TabIndex = 28;
            this.clrBacklog.Click += new System.EventHandler(this.clrBacklog_Click);
            // 
            // chkFlow_ShowClosed
            // 
            this.chkFlow_ShowClosed.AutoSize = true;
            this.chkFlow_ShowClosed.Checked = true;
            this.chkFlow_ShowClosed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowClosed.Location = new System.Drawing.Point(525, 80);
            this.chkFlow_ShowClosed.Name = "chkFlow_ShowClosed";
            this.chkFlow_ShowClosed.Size = new System.Drawing.Size(58, 17);
            this.chkFlow_ShowClosed.TabIndex = 27;
            this.chkFlow_ShowClosed.Text = "Closed";
            this.chkFlow_ShowClosed.UseVisualStyleBackColor = true;
            this.chkFlow_ShowClosed.CheckedChanged += new System.EventHandler(this.chkFlow_ShowClosed_CheckedChanged);
            // 
            // chkFlow_ShowSmoketestDone
            // 
            this.chkFlow_ShowSmoketestDone.AutoSize = true;
            this.chkFlow_ShowSmoketestDone.Checked = true;
            this.chkFlow_ShowSmoketestDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowSmoketestDone.Location = new System.Drawing.Point(525, 57);
            this.chkFlow_ShowSmoketestDone.Name = "chkFlow_ShowSmoketestDone";
            this.chkFlow_ShowSmoketestDone.Size = new System.Drawing.Size(105, 17);
            this.chkFlow_ShowSmoketestDone.TabIndex = 26;
            this.chkFlow_ShowSmoketestDone.Text = "Smoketest Done";
            this.chkFlow_ShowSmoketestDone.UseVisualStyleBackColor = true;
            this.chkFlow_ShowSmoketestDone.CheckedChanged += new System.EventHandler(this.chkFlow_ShowSmoketestDone_CheckedChanged);
            // 
            // chkFlow_ShowSmoketest
            // 
            this.chkFlow_ShowSmoketest.AutoSize = true;
            this.chkFlow_ShowSmoketest.Checked = true;
            this.chkFlow_ShowSmoketest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowSmoketest.Location = new System.Drawing.Point(525, 34);
            this.chkFlow_ShowSmoketest.Name = "chkFlow_ShowSmoketest";
            this.chkFlow_ShowSmoketest.Size = new System.Drawing.Size(76, 17);
            this.chkFlow_ShowSmoketest.TabIndex = 25;
            this.chkFlow_ShowSmoketest.Text = "Smoketest";
            this.chkFlow_ShowSmoketest.UseVisualStyleBackColor = true;
            this.chkFlow_ShowSmoketest.CheckedChanged += new System.EventHandler(this.chkFlow_ShowSmoketest_CheckedChanged);
            // 
            // chkFlow_ShowPOReviewDone
            // 
            this.chkFlow_ShowPOReviewDone.AutoSize = true;
            this.chkFlow_ShowPOReviewDone.Checked = true;
            this.chkFlow_ShowPOReviewDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowPOReviewDone.Location = new System.Drawing.Point(364, 80);
            this.chkFlow_ShowPOReviewDone.Name = "chkFlow_ShowPOReviewDone";
            this.chkFlow_ShowPOReviewDone.Size = new System.Drawing.Size(109, 17);
            this.chkFlow_ShowPOReviewDone.TabIndex = 24;
            this.chkFlow_ShowPOReviewDone.Text = "PO Review Done";
            this.chkFlow_ShowPOReviewDone.UseVisualStyleBackColor = true;
            this.chkFlow_ShowPOReviewDone.CheckedChanged += new System.EventHandler(this.chkFlow_ShowPOReviewDone_CheckedChanged);
            // 
            // chkFlow_ShowPOReview
            // 
            this.chkFlow_ShowPOReview.AutoSize = true;
            this.chkFlow_ShowPOReview.Checked = true;
            this.chkFlow_ShowPOReview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowPOReview.Location = new System.Drawing.Point(364, 57);
            this.chkFlow_ShowPOReview.Name = "chkFlow_ShowPOReview";
            this.chkFlow_ShowPOReview.Size = new System.Drawing.Size(80, 17);
            this.chkFlow_ShowPOReview.TabIndex = 23;
            this.chkFlow_ShowPOReview.Text = "PO Review";
            this.chkFlow_ShowPOReview.UseVisualStyleBackColor = true;
            this.chkFlow_ShowPOReview.CheckedChanged += new System.EventHandler(this.chkFlow_ShowPOReview_CheckedChanged);
            // 
            // chkFlow_ShowWegmansQADone
            // 
            this.chkFlow_ShowWegmansQADone.AutoSize = true;
            this.chkFlow_ShowWegmansQADone.Checked = true;
            this.chkFlow_ShowWegmansQADone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowWegmansQADone.Location = new System.Drawing.Point(364, 34);
            this.chkFlow_ShowWegmansQADone.Name = "chkFlow_ShowWegmansQADone";
            this.chkFlow_ShowWegmansQADone.Size = new System.Drawing.Size(121, 17);
            this.chkFlow_ShowWegmansQADone.TabIndex = 22;
            this.chkFlow_ShowWegmansQADone.Text = "Wegmans QA Done";
            this.chkFlow_ShowWegmansQADone.UseVisualStyleBackColor = true;
            this.chkFlow_ShowWegmansQADone.CheckedChanged += new System.EventHandler(this.chkFlow_ShowWegmansQADone_CheckedChanged);
            // 
            // chkFlow_ShowWegmansQA
            // 
            this.chkFlow_ShowWegmansQA.AutoSize = true;
            this.chkFlow_ShowWegmansQA.Checked = true;
            this.chkFlow_ShowWegmansQA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowWegmansQA.Location = new System.Drawing.Point(192, 80);
            this.chkFlow_ShowWegmansQA.Name = "chkFlow_ShowWegmansQA";
            this.chkFlow_ShowWegmansQA.Size = new System.Drawing.Size(92, 17);
            this.chkFlow_ShowWegmansQA.TabIndex = 21;
            this.chkFlow_ShowWegmansQA.Text = "Wegmans QA";
            this.chkFlow_ShowWegmansQA.UseVisualStyleBackColor = true;
            this.chkFlow_ShowWegmansQA.CheckedChanged += new System.EventHandler(this.chkFlow_ShowWegmansQA_CheckedChanged);
            // 
            // chkFlow_ShowAsynchronyQADone
            // 
            this.chkFlow_ShowAsynchronyQADone.AutoSize = true;
            this.chkFlow_ShowAsynchronyQADone.Checked = true;
            this.chkFlow_ShowAsynchronyQADone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowAsynchronyQADone.Location = new System.Drawing.Point(192, 57);
            this.chkFlow_ShowAsynchronyQADone.Name = "chkFlow_ShowAsynchronyQADone";
            this.chkFlow_ShowAsynchronyQADone.Size = new System.Drawing.Size(128, 17);
            this.chkFlow_ShowAsynchronyQADone.TabIndex = 20;
            this.chkFlow_ShowAsynchronyQADone.Text = "Asynchrony QA Done";
            this.chkFlow_ShowAsynchronyQADone.UseVisualStyleBackColor = true;
            this.chkFlow_ShowAsynchronyQADone.CheckedChanged += new System.EventHandler(this.chkFlow_ShowAsynchronyQADone_CheckedChanged);
            // 
            // chkFlow_ShowAsynchronyQA
            // 
            this.chkFlow_ShowAsynchronyQA.AutoSize = true;
            this.chkFlow_ShowAsynchronyQA.Checked = true;
            this.chkFlow_ShowAsynchronyQA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowAsynchronyQA.Location = new System.Drawing.Point(192, 34);
            this.chkFlow_ShowAsynchronyQA.Name = "chkFlow_ShowAsynchronyQA";
            this.chkFlow_ShowAsynchronyQA.Size = new System.Drawing.Size(99, 17);
            this.chkFlow_ShowAsynchronyQA.TabIndex = 19;
            this.chkFlow_ShowAsynchronyQA.Text = "Asynchrony QA";
            this.chkFlow_ShowAsynchronyQA.UseVisualStyleBackColor = true;
            this.chkFlow_ShowAsynchronyQA.CheckedChanged += new System.EventHandler(this.chkFlow_ShowAsynchronyQA_CheckedChanged);
            // 
            // chkFlow_ShowDevelopmentDone
            // 
            this.chkFlow_ShowDevelopmentDone.AutoSize = true;
            this.chkFlow_ShowDevelopmentDone.Checked = true;
            this.chkFlow_ShowDevelopmentDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowDevelopmentDone.Location = new System.Drawing.Point(38, 80);
            this.chkFlow_ShowDevelopmentDone.Name = "chkFlow_ShowDevelopmentDone";
            this.chkFlow_ShowDevelopmentDone.Size = new System.Drawing.Size(118, 17);
            this.chkFlow_ShowDevelopmentDone.TabIndex = 18;
            this.chkFlow_ShowDevelopmentDone.Text = "Development Done";
            this.chkFlow_ShowDevelopmentDone.UseVisualStyleBackColor = true;
            this.chkFlow_ShowDevelopmentDone.CheckedChanged += new System.EventHandler(this.chkFlow_ShowDevelopmentDone_CheckedChanged);
            // 
            // chkFlow_ShowDevelopment
            // 
            this.chkFlow_ShowDevelopment.AutoSize = true;
            this.chkFlow_ShowDevelopment.Checked = true;
            this.chkFlow_ShowDevelopment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowDevelopment.Location = new System.Drawing.Point(38, 57);
            this.chkFlow_ShowDevelopment.Name = "chkFlow_ShowDevelopment";
            this.chkFlow_ShowDevelopment.Size = new System.Drawing.Size(89, 17);
            this.chkFlow_ShowDevelopment.TabIndex = 17;
            this.chkFlow_ShowDevelopment.Text = "Development";
            this.chkFlow_ShowDevelopment.UseVisualStyleBackColor = true;
            this.chkFlow_ShowDevelopment.CheckedChanged += new System.EventHandler(this.chkFlow_ShowDevelopment_CheckedChanged);
            // 
            // chkFlow_ShowBacklog
            // 
            this.chkFlow_ShowBacklog.AutoSize = true;
            this.chkFlow_ShowBacklog.Checked = true;
            this.chkFlow_ShowBacklog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlow_ShowBacklog.Location = new System.Drawing.Point(38, 34);
            this.chkFlow_ShowBacklog.Name = "chkFlow_ShowBacklog";
            this.chkFlow_ShowBacklog.Size = new System.Drawing.Size(65, 17);
            this.chkFlow_ShowBacklog.TabIndex = 16;
            this.chkFlow_ShowBacklog.Text = "Backlog";
            this.chkFlow_ShowBacklog.UseVisualStyleBackColor = true;
            this.chkFlow_ShowBacklog.CheckedChanged += new System.EventHandler(this.chkFlow_ShowBacklog_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Note:  This chart is tuned for a specific Project.";
            // 
            // cmdUpdateStoryCumulativeFlow
            // 
            this.cmdUpdateStoryCumulativeFlow.Location = new System.Drawing.Point(8, 5);
            this.cmdUpdateStoryCumulativeFlow.Name = "cmdUpdateStoryCumulativeFlow";
            this.cmdUpdateStoryCumulativeFlow.Size = new System.Drawing.Size(129, 23);
            this.cmdUpdateStoryCumulativeFlow.TabIndex = 11;
            this.cmdUpdateStoryCumulativeFlow.Text = "Update Chart";
            this.cmdUpdateStoryCumulativeFlow.UseVisualStyleBackColor = true;
            this.cmdUpdateStoryCumulativeFlow.Click += new System.EventHandler(this.cmdUpdateStoryCumulativeFlow_Click);
            // 
            // cmdPrintStoryCumulativeFlow
            // 
            this.cmdPrintStoryCumulativeFlow.Location = new System.Drawing.Point(143, 5);
            this.cmdPrintStoryCumulativeFlow.Name = "cmdPrintStoryCumulativeFlow";
            this.cmdPrintStoryCumulativeFlow.Size = new System.Drawing.Size(129, 23);
            this.cmdPrintStoryCumulativeFlow.TabIndex = 5;
            this.cmdPrintStoryCumulativeFlow.Text = "Print";
            this.cmdPrintStoryCumulativeFlow.UseVisualStyleBackColor = true;
            // 
            // tpBugs
            // 
            this.tpBugs.Controls.Add(this.chartBugs);
            this.tpBugs.Controls.Add(this.panel1);
            this.tpBugs.Location = new System.Drawing.Point(4, 22);
            this.tpBugs.Name = "tpBugs";
            this.tpBugs.Size = new System.Drawing.Size(1000, 675);
            this.tpBugs.TabIndex = 4;
            this.tpBugs.Text = "Bugs";
            this.tpBugs.UseVisualStyleBackColor = true;
            // 
            // chartBugs
            // 
            chartArea9.AxisX.Interval = 7D;
            chartArea9.AxisX.MajorGrid.Enabled = false;
            chartArea9.AxisX.MajorGrid.Interval = 0D;
            chartArea9.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea9.AxisX.MajorTickMark.Interval = 0D;
            chartArea9.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea9.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chartArea9.Name = "ChartArea1";
            this.chartBugs.ChartAreas.Add(chartArea9);
            this.chartBugs.Dock = System.Windows.Forms.DockStyle.Fill;
            legend9.Name = "Legend1";
            this.chartBugs.Legends.Add(legend9);
            this.chartBugs.Location = new System.Drawing.Point(0, 68);
            this.chartBugs.Name = "chartBugs";
            series39.ChartArea = "ChartArea1";
            series39.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series39.Color = System.Drawing.Color.MediumBlue;
            series39.IsValueShownAsLabel = true;
            series39.Legend = "Legend1";
            series39.LegendText = "New";
            series39.Name = "countNew";
            series39.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartBugs.Series.Add(series39);
            this.chartBugs.Size = new System.Drawing.Size(1000, 607);
            this.chartBugs.TabIndex = 2;
            this.chartBugs.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkBugsExcludeCurrentWeek);
            this.panel1.Controls.Add(this.chkShowNewWorkedClosedBugs);
            this.panel1.Controls.Add(this.chkShowRemainingBugsBySeverity);
            this.panel1.Controls.Add(this.chkShowRemainingBugs);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtBugStartDate);
            this.panel1.Controls.Add(this.cmdRenderBugs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 68);
            this.panel1.TabIndex = 3;
            // 
            // chkBugsExcludeCurrentWeek
            // 
            this.chkBugsExcludeCurrentWeek.AutoSize = true;
            this.chkBugsExcludeCurrentWeek.Checked = true;
            this.chkBugsExcludeCurrentWeek.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBugsExcludeCurrentWeek.Location = new System.Drawing.Point(448, 43);
            this.chkBugsExcludeCurrentWeek.Name = "chkBugsExcludeCurrentWeek";
            this.chkBugsExcludeCurrentWeek.Size = new System.Drawing.Size(133, 17);
            this.chkBugsExcludeCurrentWeek.TabIndex = 51;
            this.chkBugsExcludeCurrentWeek.Text = "Exclude Current Week";
            this.chkBugsExcludeCurrentWeek.UseVisualStyleBackColor = true;
            this.chkBugsExcludeCurrentWeek.CheckedChanged += new System.EventHandler(this.chkBugsExcludeCurrentWeek_CheckedChanged);
            // 
            // chkShowNewWorkedClosedBugs
            // 
            this.chkShowNewWorkedClosedBugs.AutoSize = true;
            this.chkShowNewWorkedClosedBugs.Checked = true;
            this.chkShowNewWorkedClosedBugs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNewWorkedClosedBugs.Location = new System.Drawing.Point(267, 43);
            this.chkShowNewWorkedClosedBugs.Name = "chkShowNewWorkedClosedBugs";
            this.chkShowNewWorkedClosedBugs.Size = new System.Drawing.Size(160, 17);
            this.chkShowNewWorkedClosedBugs.TabIndex = 50;
            this.chkShowNewWorkedClosedBugs.Text = "Show New, Worked, Closed";
            this.chkShowNewWorkedClosedBugs.UseVisualStyleBackColor = true;
            // 
            // chkShowRemainingBugsBySeverity
            // 
            this.chkShowRemainingBugsBySeverity.AutoSize = true;
            this.chkShowRemainingBugsBySeverity.Checked = true;
            this.chkShowRemainingBugsBySeverity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRemainingBugsBySeverity.Location = new System.Drawing.Point(448, 11);
            this.chkShowRemainingBugsBySeverity.Name = "chkShowRemainingBugsBySeverity";
            this.chkShowRemainingBugsBySeverity.Size = new System.Drawing.Size(162, 17);
            this.chkShowRemainingBugsBySeverity.TabIndex = 49;
            this.chkShowRemainingBugsBySeverity.Text = "Show Remaining By Severity";
            this.chkShowRemainingBugsBySeverity.UseVisualStyleBackColor = true;
            // 
            // chkShowRemainingBugs
            // 
            this.chkShowRemainingBugs.AutoSize = true;
            this.chkShowRemainingBugs.Checked = true;
            this.chkShowRemainingBugs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRemainingBugs.Location = new System.Drawing.Point(267, 11);
            this.chkShowRemainingBugs.Name = "chkShowRemainingBugs";
            this.chkShowRemainingBugs.Size = new System.Drawing.Size(160, 17);
            this.chkShowRemainingBugs.TabIndex = 48;
            this.chkShowRemainingBugs.Text = "Show Remaining Bugs Total";
            this.chkShowRemainingBugs.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Show Bugs From:";
            // 
            // dtBugStartDate
            // 
            this.dtBugStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtBugStartDate.Location = new System.Drawing.Point(128, 9);
            this.dtBugStartDate.Name = "dtBugStartDate";
            this.dtBugStartDate.Size = new System.Drawing.Size(123, 20);
            this.dtBugStartDate.TabIndex = 46;
            this.dtBugStartDate.Value = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            // 
            // cmdRenderBugs
            // 
            this.cmdRenderBugs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRenderBugs.Location = new System.Drawing.Point(863, 39);
            this.cmdRenderBugs.Name = "cmdRenderBugs";
            this.cmdRenderBugs.Size = new System.Drawing.Size(129, 23);
            this.cmdRenderBugs.TabIndex = 10;
            this.cmdRenderBugs.Text = "Update Chart";
            this.cmdRenderBugs.UseVisualStyleBackColor = true;
            this.cmdRenderBugs.Click += new System.EventHandler(this.cmdRenderBugs_Click);
            // 
            // lstBoxCompletedWorkTFSStateList
            // 
            this.lstBoxCompletedWorkTFSStateList.FormattingEnabled = true;
            this.lstBoxCompletedWorkTFSStateList.Location = new System.Drawing.Point(9, 71);
            this.lstBoxCompletedWorkTFSStateList.Name = "lstBoxCompletedWorkTFSStateList";
            this.lstBoxCompletedWorkTFSStateList.Size = new System.Drawing.Size(971, 95);
            this.lstBoxCompletedWorkTFSStateList.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(430, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Select Work Items That Entered Into State (Selected Below) between Start and End " +
    "date.";
            // 
            // frmDevMetrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 701);
            this.Controls.Add(this.tcMain);
            this.Name = "frmDevMetrics";
            this.Text = "frmDevMetrics";
            this.tcMain.ResumeLayout(false);
            this.tpDevMetrics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDevelopment)).EndInit();
            this.pnlDevMetricsTop.ResumeLayout(false);
            this.pnlDevMetricsTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastXMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forecastWeeks)).EndInit();
            this.tpFeatures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFeatures)).EndInit();
            this.pnlFeaturesTop.ResumeLayout(false);
            this.pnlFeaturesTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFeaturesSearch.ResumeLayout(false);
            this.gbFeaturesSearch.PerformLayout();
            this.tpCompletedWork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCompletedWork)).EndInit();
            this.pnlCompletedWorkTop.ResumeLayout(false);
            this.pnlCompletedWorkTop.PerformLayout();
            this.tpStoryCumulativeFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStoryCumulativeFlow)).EndInit();
            this.pnlStoryCumFlowTop.ResumeLayout(false);
            this.pnlStoryCumFlowTop.PerformLayout();
            this.tpBugs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBugs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpDevMetrics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDevelopment;
        private System.Windows.Forms.Panel pnlDevMetricsTop;
        private System.Windows.Forms.TabPage tpFeatures;
        private System.Windows.Forms.CheckBox chkShowTrends;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdRender;
        private System.Windows.Forms.NumericUpDown lastXMonths;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown forecastWeeks;
        private System.Windows.Forms.CheckBox chkExcludeCurrentWeek;
        private System.Windows.Forms.CheckBox chkUatComplete;
        private System.Windows.Forms.CheckBox chkQAComplete;
        private System.Windows.Forms.CheckBox chkDevComplete;
        private System.Windows.Forms.TabPage tpCompletedWork;
        private System.Windows.Forms.Panel pnlCompletedWorkTop;
        private System.Windows.Forms.Button cmdUpdateCompletedWork;
        private System.Windows.Forms.DateTimePicker dtCompletedWorkEnd;
        private System.Windows.Forms.DateTimePicker dtCompletedWorkStart;
        private System.Windows.Forms.DataGridView gridCompletedWork;
        private System.Windows.Forms.DataGridView gridFeatures;
        private System.Windows.Forms.Panel pnlFeaturesTop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdUpdateFeatures;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkBugStory;
        private System.Windows.Forms.CheckBox chkUserStory;
        private System.Windows.Forms.GroupBox gbFeaturesSearch;
        private System.Windows.Forms.RadioButton rbFeatureIDs;
        private System.Windows.Forms.RadioButton rbTags;
        private System.Windows.Forms.TextBox txtFeatures;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tpStoryCumulativeFlow;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStoryCumulativeFlow;
        private System.Windows.Forms.Panel pnlStoryCumFlowTop;
        private System.Windows.Forms.Button cmdUpdateStoryCumulativeFlow;
        private System.Windows.Forms.Button cmdPrintStoryCumulativeFlow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFlow_ShowClosed;
        private System.Windows.Forms.CheckBox chkFlow_ShowSmoketestDone;
        private System.Windows.Forms.CheckBox chkFlow_ShowSmoketest;
        private System.Windows.Forms.CheckBox chkFlow_ShowPOReviewDone;
        private System.Windows.Forms.CheckBox chkFlow_ShowPOReview;
        private System.Windows.Forms.CheckBox chkFlow_ShowWegmansQADone;
        private System.Windows.Forms.CheckBox chkFlow_ShowWegmansQA;
        private System.Windows.Forms.CheckBox chkFlow_ShowAsynchronyQADone;
        private System.Windows.Forms.CheckBox chkFlow_ShowAsynchronyQA;
        private System.Windows.Forms.CheckBox chkFlow_ShowDevelopmentDone;
        private System.Windows.Forms.CheckBox chkFlow_ShowDevelopment;
        private System.Windows.Forms.CheckBox chkFlow_ShowBacklog;
        private System.Windows.Forms.Panel clrClosed;
        private System.Windows.Forms.Panel clrPOReviewDone;
        private System.Windows.Forms.Panel clrSmoketestDone;
        private System.Windows.Forms.Panel clrWegmansQA;
        private System.Windows.Forms.Panel clrSmoketest;
        private System.Windows.Forms.Panel clrPOReview;
        private System.Windows.Forms.Panel clrAsynchronyQADone;
        private System.Windows.Forms.Panel clrWegmansQADone;
        private System.Windows.Forms.Panel clrDevelopmentDone;
        private System.Windows.Forms.Panel clrAsynchronyQA;
        private System.Windows.Forms.Panel clrDevelopment;
        private System.Windows.Forms.Panel clrBacklog;
        private System.Windows.Forms.Button cmdFlowUncheckAll;
        private System.Windows.Forms.Button cmdFlowCheckAll;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TabPage tpBugs;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBugs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtBugStartDate;
        private System.Windows.Forms.Button cmdRenderBugs;
        private System.Windows.Forms.CheckBox chkShowRemainingBugs;
        private System.Windows.Forms.CheckBox chkShowRemainingBugsBySeverity;
        private System.Windows.Forms.CheckBox chkShowNewWorkedClosedBugs;
        private System.Windows.Forms.CheckBox chkBugsExcludeCurrentWeek;
        private System.Windows.Forms.ListBox lstBoxCompletedWorkTFSStateList;
        private System.Windows.Forms.Label label6;
    }
}