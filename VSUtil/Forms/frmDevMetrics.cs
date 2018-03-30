using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VSConnect;
using VSUtil.Classes.Util;

namespace VSUtil.Forms
{
    public partial class frmDevMetrics : Form
    {
        private Connect connect = null;
        private string filePath;
        DumpDataSet ds = null;
        DataView view = null;
        string lastDumpDate = string.Empty;
        private bool renderedBlockers = false;
        private int AreaID = 0;
        private string ProjectName = string.Empty;
        private bool renderedBugs = false;
        public frmDevMetrics()
        {
            InitializeComponent();
        }

        public frmDevMetrics(string mdbFilePath, Connect conn, int areaId, string projectName) : this()
        {
            this.ProjectName = projectName;
            this.AreaID = areaId;
            this.filePath = mdbFilePath;
            this.connect = conn;
            ds = VSCommon.LoadDumpDataset(filePath);

            lastDumpDate = ds.Config.Where(x => x.Key == "DumpDate").First().Val;

            chartDevelopment.Titles.Add(string.Format("{0} Stories (as of {1})", ProjectName, lastDumpDate));

            GetFeatureOptions();

            RenderBugs();

            Render();

            SetStoryCumulativeFlowDefaultColors();
        }

        private void RenderBugs()
        {
            DateTime startDt = dtBugStartDate.Value.Date;
            if (startDt.DayOfWeek != DayOfWeek.Friday)
            {
                while (true)
                {
                    startDt = startDt.AddDays(-1);
                    if (startDt.DayOfWeek == DayOfWeek.Friday)
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                if (chartBugs.Series.Count == 0)
                {
                    break;
                }
                chartBugs.Series.Remove(chartBugs.Series.FirstOrDefault());
            }

            Series series = CreateSeries("countNew", SeriesChartType.Line, 2, Color.Blue, ChartDashStyle.Solid,
                ChartValueType.DateTime, "New Bugs");
            chartBugs.Series.Add(series);

            DataTable table = ds.VW_BUG_CUMULATIVE_FLOW;

            if (!table.Columns.Contains("Remaining"))
            {
                table.Columns.Add(new DataColumn("Remaining", typeof(Int32)));
                table.Columns["Remaining"].AllowDBNull = true;
                foreach (DataRow row in table.Rows)
                {
                    int count = 0;
                    int closed = 0;
                    if (!row.IsNull("Count"))
                    {
                        count = Convert.ToInt32(row["Count"]);
                    }
                    if (!row.IsNull("Closed"))
                    {
                        closed = Convert.ToInt32(row["Closed"]);
                    }
                    row["Remaining"] = count - closed;
                }
                table.AcceptChanges();

            }



            string filter = string.Format("WeekEnding >= #{0}#", startDt.ToShortDateString());

            DataView devview = new DataView(table, filter, "WeekEnding", DataViewRowState.CurrentRows);
            chartBugs.Series["countNew"].Points.DataBind(devview, "WeekEnding", "NewThisWeek", "Tooltip=WeekEnding");

            series = CreateSeries("countActive", SeriesChartType.Line, 2, Color.Green, ChartDashStyle.Solid,
                ChartValueType.DateTime, "Active Bugs");
            chartBugs.Series.Add(series);
            devview = new DataView(ds.VW_BUG_CUMULATIVE_FLOW, filter, "WeekEnding", DataViewRowState.CurrentRows);
            chartBugs.Series["countActive"].Points.DataBind(devview, "WeekEnding", "ActiveThisWeek", "Tooltip=WeekEnding");

//            series = CreateSeries("count", SeriesChartType.Line, 2, Color.DarkSlateGray, ChartDashStyle.Solid,
//                ChartValueType.DateTime, "Total Bugs");
//            chartBugs.Series.Add(series);
//            devview = new DataView(ds.VW_BUG_CUMULATIVE_FLOW, "", "WeekEnding", DataViewRowState.CurrentRows);
//            chartBugs.Series["count"].Points.DataBind(devview, "WeekEnding", "Count", "Tooltip=WeekEnding");

            series = CreateSeries("remaining", SeriesChartType.Line, 2, Color.DarkRed, ChartDashStyle.Solid,
                ChartValueType.DateTime, "Remaining Bugs");
            chartBugs.Series.Add(series);
            devview = new DataView(table, filter, "WeekEnding", DataViewRowState.CurrentRows);
            chartBugs.Series["remaining"].Points.DataBind(devview, "WeekEnding", "Remaining", "Tooltip=WeekEnding");

            series = CreateSeries("closedThisWeek", SeriesChartType.Line, 2, Color.Goldenrod, ChartDashStyle.Solid,
                ChartValueType.DateTime, "Closed");
            chartBugs.Series.Add(series);
            devview = new DataView(ds.VW_BUG_CUMULATIVE_FLOW, filter, "WeekEnding", DataViewRowState.CurrentRows);
            chartBugs.Series["closedThisWeek"].Points.DataBind(devview, "WeekEnding", "ClosedThisWeek", "Tooltip=WeekEnding");

//            if (chkShowTrends.Checked)
//            {
//                Series trendline = new Series("CountTrendline");
//                trendline.ChartType = SeriesChartType.Line;
//                trendline.BorderWidth = 1;
//                trendline.BorderDashStyle = ChartDashStyle.Dash;
//                trendline.Color = Color.Blue;
//                chartDevelopment.Series.Add(trendline);
//                string forecast = string.Format("Linear,{0},false,false", Convert.ToInt32(forecastWeeks.Value));
//                chartDevelopment.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, forecast, chartDevelopment.Series["count"], chartDevelopment.Series["CountTrendline"]);
//                chartDevelopment.Series["CountTrendline"].LegendText = "Dev Complete Trend";
//            }


            chartBugs.Update();
        }

        private void SetStoryCumulativeFlowDefaultColors()
        {
            clrBacklog.BackColor = Color.Yellow;
            clrDevelopment.BackColor = Color.Blue;
            clrDevelopmentDone.BackColor = Color.Blue;
            clrAsynchronyQA.BackColor = Color.DarkGreen;
            clrAsynchronyQADone.BackColor = Color.DarkGreen;
            clrWegmansQA.BackColor = Color.DarkGray;
            clrWegmansQADone.BackColor = Color.DarkGray;
            clrPOReview.BackColor = Color.DarkOrange;
            clrPOReviewDone.BackColor = Color.DarkOrange;
            clrSmoketest.BackColor = Color.Brown;
            clrSmoketestDone.BackColor = Color.Brown;
            clrClosed.BackColor = Color.SpringGreen;
        }

        private void Render()
        {
            try
            {
                bool excludeCurrentWeek = chkExcludeCurrentWeek.Checked;

                chartDevelopment.ChartAreas[0].BackColor = Color.LightGray;

                string date = DateTime.Today.AddMonths(-Convert.ToInt32(lastXMonths.Value)).ToShortDateString();
                string dateEnding = VSCommon.GetLastDayOfWeek(DateTime.Today, VSCommon.LastDayOfWeek).ToShortDateString();
                if (excludeCurrentWeek)
                {
                    dateEnding = VSCommon.GetLastDayOfWeek(DateTime.Today, VSCommon.LastDayOfWeek).AddDays(-7).ToShortDateString();
                }



                while (true)
                {
                    chartDevelopment.Series.Remove(chartDevelopment.Series.FirstOrDefault());
                    if (chartDevelopment.Series.Count == 0)
                    {
                        break;
                    }
                }

                if (chkDevComplete.Checked)
                {
                    string s = "";
                    s = "(((WeekEnding)>='" + date + "') AND ((WeekEnding)<='" + dateEnding + "') AND ((WorkItemType)<>'Bug') AND ((State)='Active'))";

                    Series series = CreateSeries("count", SeriesChartType.Line, 2, Color.Blue, ChartDashStyle.Solid,
                        ChartValueType.DateTime, "Dev Complete");
                    chartDevelopment.Series.Add(series);
                    DataView devview = new DataView(ds.WorkItemMetrics, s, "WeekEnding", DataViewRowState.CurrentRows);
                    chartDevelopment.Series["count"].Points.DataBind(devview, "WeekEnding", "StoryCount", "Tooltip=WeekEnding");

                    if (chkShowTrends.Checked)
                    {
                        Series trendline = new Series("CountTrendline");
                        trendline.ChartType = SeriesChartType.Line;
                        trendline.BorderWidth = 1;
                        trendline.BorderDashStyle = ChartDashStyle.Dash;
                        trendline.Color = Color.Blue;
                        chartDevelopment.Series.Add(trendline);
                        string forecast = string.Format("Linear,{0},false,false", Convert.ToInt32(forecastWeeks.Value));
                        chartDevelopment.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, forecast, chartDevelopment.Series["count"], chartDevelopment.Series["CountTrendline"]);
                        chartDevelopment.Series["CountTrendline"].LegendText = "Dev Complete Trend";
                    }


                }
                if (chkQAComplete.Checked)
                {
                    string s = "";
                    s = "(((WeekEnding)>='" + date + "') AND ((WeekEnding)<='" + dateEnding + "') AND ((WorkItemType)<>'Bug') AND ((State)='QA'))";


                    Series series = CreateSeries("qacount", SeriesChartType.Line, 2, Color.Red, ChartDashStyle.Solid,
                        ChartValueType.DateTime, "QA Complete");

                    chartDevelopment.Series.Add(series);
                    DataView qaview = new DataView(ds.WorkItemMetrics, s, "WeekEnding", DataViewRowState.CurrentRows);
                    chartDevelopment.Series["qacount"].Points.DataBind(qaview, "WeekEnding", "StoryCount", "Tooltip=WeekEnding");

                    if (chkShowTrends.Checked)
                    {
                        Series trendline = new Series("QACountTrendline");
                        trendline.ChartType = SeriesChartType.Line;
                        trendline.BorderWidth = 1;
                        trendline.BorderDashStyle = ChartDashStyle.Dash;
                        trendline.Color = Color.Red;
                        chartDevelopment.Series.Add(trendline);
                        string forecast = string.Format("Linear,{0},false,false", Convert.ToInt32(forecastWeeks.Value));
                        chartDevelopment.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, forecast, chartDevelopment.Series["qacount"], chartDevelopment.Series["QACountTrendline"]);
                        chartDevelopment.Series["QACountTrendline"].LegendText = "QA Complete Trend";

                    }
                }
                if (chkUatComplete.Checked)
                {
                    string s = "";
                    s = "(((WeekEnding)>='" + date + "') AND ((WeekEnding)<='" + dateEnding + "') AND ((WorkItemType)<>'Bug') AND ((State)='UAT'))";

                    Series series = CreateSeries("uatcount", SeriesChartType.Line, 2, Color.Green, ChartDashStyle.Solid,
                        ChartValueType.DateTime, "UAT Complete");

                    chartDevelopment.Series.Add(series);
                    DataView uatview = new DataView(ds.WorkItemMetrics, s, "WeekEnding", DataViewRowState.CurrentRows);
                    chartDevelopment.Series["uatcount"].Points.DataBind(uatview, "WeekEnding", "StoryCount", "Tooltip=WeekEnding");


                    if (chkShowTrends.Checked)
                    {
                        Series trendline = new Series("UATCountTrendline");
                        trendline.ChartType = SeriesChartType.Line;
                        trendline.BorderWidth = 1;
                        trendline.BorderDashStyle = ChartDashStyle.Dash;
                        trendline.Color = Color.Green;
                        chartDevelopment.Series.Add(trendline);
                        string forecast = string.Format("Linear,{0},false,false", Convert.ToInt32(forecastWeeks.Value));
                        chartDevelopment.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, forecast, chartDevelopment.Series["uatcount"], chartDevelopment.Series["UATCountTrendline"]);
                        chartDevelopment.Series["UATCountTrendline"].LegendText = "UAT Complete Trend";

                    }

                }

                chartDevelopment.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Series CreateSeries(string name, SeriesChartType chartType, int borderWidth, Color color, ChartDashStyle borderDashStyle,
            ChartValueType valueType, string legendText)
        {
            Series series = new Series(name);
            series.ChartType = chartType;
            series.BorderWidth = borderWidth;
            series.BorderDashStyle = borderDashStyle;
            series.Color = color;
            series.XValueType = valueType;
            series.IsValueShownAsLabel = true;
            series.LegendText = legendText;

            return series;
        }

        private void chkDevComplete_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void chkQAComplete_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void chkUatComplete_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void chkShowTrends_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void chkAndroid_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void chkiOS_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void chkExcludeCurrentWeek_CheckedChanged(object sender, EventArgs e)
        {
            Render();

        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintHelper.SetOrientation(chartDevelopment, true);
            PrintHelper.SetMargins(chartDevelopment, 10, 10, 10, 10);

            chartDevelopment.Printing.PrintPreview();

        }

        private void cmdRender_Click(object sender, EventArgs e)
        {
            Render();
        }

        private void cmdUpdateCompletedWork_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(int)));
            table.Columns.Add(new DataColumn("Type", typeof(string)));
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("CompletedDt", typeof(DateTime)));
            table.Columns.Add(new DataColumn("ParentID", typeof(int)));
            table.Columns.Add(new DataColumn("Parent", typeof(string)));
            table.Columns.Add(new DataColumn("Priority", typeof(int)));

            string filter = string.Empty;
            if (chkBaseOffQA.Checked)
            {
                filter = "QAEnd";
            }
            else
            {
                filter = "ActiveEnd";
            }

            DataView view = new DataView(ds.WorkItemLife,
                filter + ">= #" + dtCompletedWorkStart.Value.Date.ToShortDateString() + "# and " + filter + " < #" +
                dtCompletedWorkEnd.Value.Date.AddDays(1).AddMinutes(-1) + "#", "WorkItemType asc", DataViewRowState.CurrentRows);

            foreach (DataRowView drv in view)
            {
                table.Rows.Add(new object[] { drv["ID"], drv["WorkItemType"], drv["Title"], drv[filter] });
            }

            view = new DataView(ds.WorkItemLife_Task,
                "Closed >= #" + dtCompletedWorkStart.Value.Date.ToShortDateString() + "# and Closed < #" +
                dtCompletedWorkEnd.Value.Date.AddDays(1).AddMinutes(-1) + "#", "WorkItemType asc", DataViewRowState.CurrentRows);

            foreach (DataRowView drv in view)
            {
                table.Rows.Add(new object[] { drv["ID"], drv["WorkItemType"], drv["Title"], drv["Closed"] });
            }

            view = new DataView(ds.WorkItem,
                "Type='Feature' and State = 'Done' and StateChangeDate >=#" +
                dtCompletedWorkStart.Value.Date.ToShortDateString() + "# and StateChangeDate < #" +
                dtCompletedWorkEnd.Value.Date.AddDays(1).AddMinutes(-1) + "#", "StateChangeDate asc",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in view)
            {
                table.Rows.Add(new object[] { drv["ID"], drv["Type"], drv["Title"], drv["StateChangeDate"] });
            }

            foreach (DataRow row in table.Rows)
            {
                int id = Convert.ToInt32(row["ID"].ToString());
                //get parent
                DumpDataSet.VW_All_CurrentRow cr = ds.VW_All_Current.Where(x => x.ID == id).FirstOrDefault();
                if (cr != null)
                {
                    row["ParentID"] = cr.ParentID;
                    row["Parent"] = string.Format("({0})-{1}", cr.ParentType, cr.ParentTitle);
                    row["Priority"] = cr.StackRank;
                }
            }

            table.AcceptChanges();

            table.DefaultView.Sort = "Priority";

            gridCompletedWork.DataSource = table.DefaultView;
        }

        private void cmdUpdateFeatures_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateFeatures();

                SaveFeatureOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetFeatureOptions()
        {
            txtFeatures.Text = TFSRegistry.GetRegistryKey("DevMetrics_FeatureSearch");
        }

        private void SaveFeatureOptions()
        {
            TFSRegistry.SetRegistryKey("DevMetrics_FeatureSearch",txtFeatures.Text);
        }

        private void UpdateFeatures()
        {
            HelperDS helper = new HelperDS();

            txtFeatures.Text = txtFeatures.Text.Replace("; ", ";");

            var values = txtFeatures.Text.ToLower().Replace(" ,", ",").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            if (rbTags.Checked)
            {
                var results = ds.WorkItem.Where(x => x.Type == "Feature" && x.State != "Removed" && (x.Tags.ToLower().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Any(y => values.Contains(y.Trim().ToLower()))));

                foreach (var row in results)
                {
                    helper.FeatureBreakdown.AddFeatureBreakdownRow(row.ID, row.Title, 0, 0, 0, 0, 0, 0, 0m, 0m, 0);
                }
            }
            else if (rbFeatureIDs.Checked)
            {
                var ids = txtFeatures.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList();

                foreach (var row in ds.WorkItem.Where(x => ids.Contains(x.ID)))
                {
                    helper.FeatureBreakdown.AddFeatureBreakdownRow(row.ID, row.Title, 0, 0, 0, 0, 0, 0, 0m, 0m, 0);
                }

            }

            List<DumpDataSet.VW_All_CurrentRow> workItems = new List<DumpDataSet.VW_All_CurrentRow>();

            foreach (HelperDS.FeatureBreakdownRow row in helper.FeatureBreakdown.Rows)
            {
                workItems.Clear();

                bool includeUserStory = chkUserStory.Checked;
                bool includeBug = chkBugStory.Checked;


                if (includeUserStory)
                {
                    workItems.AddRange(ds.VW_All_Current.Where(x => x.Type == "User Story" && x.State.ToLower() != "removed").ToList());
                }
                if (includeBug)
                {
                    workItems.AddRange(ds.VW_All_Current.Where(x => x.Type == "Bug" && x.State.ToLower() != "removed").ToList());
                }

                row.Closed = workItems.Where(x => x.ParentID == row.FeatureId && x.State == "Closed").Count();
                row.InDevelopment = workItems.Where(x => x.ParentID == row.FeatureId && x.State == "Active").Count();
                row.InQA = workItems.Where(x => x.ParentID == row.FeatureId && (x.State == "QA" || x.State == "Ready for QA")).Count();
                row.InUAT = workItems.Where(x => x.ParentID == row.FeatureId && (x.State == "UAT" || x.State == "Ready for UAT")).Count();
                row.TotalStories = workItems.Where(x => x.ParentID == row.FeatureId && x.State != "Removed").Count();
                row.PercentComplete = GetPercentComplete(row.TotalStories, row.Closed);
                row.DevComplete = GetPercentComplete(row.TotalStories, row.InQA + row.InUAT + row.Closed);
                row.Priority = GetBusinessPriority(row.FeatureId);
                row.NotStarted = row.TotalStories - (row.Closed + row.InDevelopment + row.InQA + row.InUAT);
            }

            int totalFeatures = helper.FeatureBreakdown.Rows.Count;
            HelperDS.FeatureBreakdownRow totalsRow = helper.FeatureBreakdown.NewFeatureBreakdownRow();
            totalsRow.FeatureId = 99999;
            totalsRow.Title = "Totals";
            List<Decimal> decimals = new List<decimal>();
            foreach (HelperDS.FeatureBreakdownRow row in helper.FeatureBreakdown.Rows)
            {
                if (row.TotalStories == 0)
                {
                    decimals.Add(0m);
                }
                else
                {
                    decimals.Add(row.PercentComplete);
                }
            }

            totalsRow.PercentComplete = Math.Round(decimals.Average(), 2);
            totalsRow.Closed = helper.FeatureBreakdown.Sum(x => x.Closed);
            totalsRow.DevComplete = helper.FeatureBreakdown.Sum(x => x.DevComplete);
            totalsRow.InDevelopment = helper.FeatureBreakdown.Sum(x => x.InDevelopment);
            totalsRow.InQA = helper.FeatureBreakdown.Sum(x => x.InQA);
            totalsRow.InUAT = helper.FeatureBreakdown.Sum(x => x.InUAT);
            totalsRow.TotalStories = helper.FeatureBreakdown.Sum(x => x.TotalStories);
            totalsRow.NotStarted = helper.FeatureBreakdown.Sum(x => x.NotStarted);
            totalsRow.Priority = 1000;
            totalsRow.Title = string.Format("Totals ({0} stories not started)", totalsRow.TotalStories - totalsRow.InDevelopment - totalsRow.InQA - totalsRow.InUAT - totalsRow.Closed);

            decimals.Clear();
            foreach (HelperDS.FeatureBreakdownRow row in helper.FeatureBreakdown.Rows)
            {
                if (row.TotalStories == 0)
                {
                    decimals.Add(0m);
                }
                else
                {
                    decimals.Add(row.DevComplete);
                }
            }
            totalsRow.DevComplete = Math.Round(decimals.Average(), 2);
            helper.FeatureBreakdown.AddFeatureBreakdownRow(totalsRow);

            helper.AcceptChanges();

            helper.FeatureBreakdown.DefaultView.Sort = "Priority";

            gridFeatures.DataSource = helper.FeatureBreakdown.DefaultView;
//            gridFeatures.AllowEditing = false;
//            gridFeatures.Cols["DevComplete"].Format = "P";
//            gridFeatures.Cols["PercentComplete"].Format = "P";
            //            c1FlexGrid1.Cols["Priority"].Visible = false;

//            for (int i = 1; i < gridFeatures.Rows.Count - 1; i++)
//            {
//                int featureid = 0;
//                Int32.TryParse(gridFeatures.GetData(i, "FeatureId").ToString(), out featureid);
//                if (featureid > 0)
//                {
//                    DumpDataSet.WorkItemRow row = ds.WorkItem.Where(x => x.ID == featureid).FirstOrDefault();
//                    if (row != null)
//                    {
//                        if (row.State == "Done")
//                        {
//                            gridFeatures.Rows[i].Style = gridFeatures.Styles["Done"];
//                        }
//                        else if (row.Tags.ToLower().Contains("storied"))
//                        {
//                            gridFeatures.Rows[i].Style = gridFeatures.Styles["Storied"];
//                        }
//                        else
//                        {
//                            gridFeatures.Rows[i].Style = gridFeatures.Styles["MissingStories"];
//
//                        }
//                    }
//
//                }
//            }

//            gridFeatures.Rows[gridFeatures.Rows.Count - 1].Style = gridFeatures.Styles["GrandTotal"];

//            gridFeatures.AutoSizeCols();

            StringBuilder sb = new StringBuilder();

//            foreach (var wi in workItems.OrderBy(x => x.ParentID).ThenBy(y => y.State))
//            {
//                sb.AppendFormat("{0}\t{1}\t{2}\t{3}{4}", wi.ID, wi.ParentID, wi.State, wi.ParentTitle,
//                    Environment.NewLine);
//            }
//
//            var text = sb.ToString();
        }

        private int GetBusinessPriority(int ID)
        {
            var row = ds.WorkItem.Where(x => x.ID == ID).FirstOrDefault();
            if (row != null)
            {
                return Convert.ToInt32(row.BusinessPriority);
            }
            else return 1000;
        }

        private decimal GetPercentComplete(int totalStories, int closedStories)
        {
            if (totalStories == 0) return 0m;

            return Math.Round(Convert.ToDecimal(closedStories) / Convert.ToDecimal(totalStories), 2);
        }

        private void cmdUpdateStoryCumulativeFlow_Click(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();
        }

        private void chkFlow_ShowBacklog_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();
        }

        private void UpdateStoryCumulativeFlow()
        {

            chartStoryCumulativeFlow.ChartAreas[0].BackColor = Color.LightGray;

            string date = DateTime.Today.AddMonths(-Convert.ToInt32(lastXMonths.Value)).ToShortDateString();

            while (true)
            {
                chartStoryCumulativeFlow.Series.Remove(chartStoryCumulativeFlow.Series.FirstOrDefault());
                if (chartStoryCumulativeFlow.Series.Count == 0)
                {
                    break;
                }
            }

            DataView fview = new DataView(ds.VW_STORY_CUMULATIVE_FLOW, "", "WeekEnding", DataViewRowState.CurrentRows);

            Series series = null;

            if (chkFlow_ShowBacklog.Checked)
            {
                series = CreateSeries("backlog", SeriesChartType.StackedArea, 1, clrBacklog.BackColor, ChartDashStyle.Solid, ChartValueType.DateTime, "Backlog");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["backlog"].Points.DataBind(fview, "WeekEnding", "Backlog", "Tooltip=WeekEnding");

            }

            if (chkFlow_ShowDevelopment.Checked)
            {
                series = CreateSeries("development", SeriesChartType.StackedArea, 1, clrDevelopment.BackColor, ChartDashStyle.Solid,
                    ChartValueType.DateTime, "Development");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["development"].Points
                    .DataBind(fview, "WeekEnding", "Development", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowDevelopmentDone.Checked)
            {

                series = CreateSeries("developmentdone", SeriesChartType.StackedArea, 1, clrDevelopmentDone.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "DevelopmentDone");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["developmentdone"].Points
                    .DataBind(fview, "WeekEnding", "DevelopmentDone", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowAsynchronyQA.Checked)
            {

                series = CreateSeries("asynchronyqa", SeriesChartType.StackedArea, 1, clrAsynchronyQA.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "AsynchronyQA");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["asynchronyqa"].Points
                    .DataBind(fview, "WeekEnding", "AsynchronyQA", "Tooltip=WeekEnding");
            }


            if (chkFlow_ShowAsynchronyQADone.Checked)
            {

                series = CreateSeries("asynchronyqadone", SeriesChartType.StackedArea, 1, clrAsynchronyQADone.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "AsynchronyQADone");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["asynchronyqadone"].Points
                    .DataBind(fview, "WeekEnding", "AsynchronyQADone", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowWegmansQA.Checked)
            {

                series = CreateSeries("wegmansqa", SeriesChartType.StackedArea, 1, clrWegmansQA.BackColor, ChartDashStyle.Solid,
                    ChartValueType.DateTime, "WegmansQA");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["wegmansqa"].Points
                    .DataBind(fview, "WeekEnding", "WegmansQA", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowWegmansQADone.Checked)
            {

                series = CreateSeries("wegmansqadone", SeriesChartType.StackedArea, 1, clrWegmansQADone.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "WegmansQADone");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["wegmansqadone"].Points
                    .DataBind(fview, "WeekEnding", "WegmansQADone", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowPOReview.Checked)
            {

                series = CreateSeries("poreview", SeriesChartType.StackedArea, 1, clrPOReview.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "POReview");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["poreview"].Points
                    .DataBind(fview, "WeekEnding", "POReview", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowPOReviewDone.Checked)
            {

                series = CreateSeries("poreviewdone", SeriesChartType.StackedArea, 1, clrPOReviewDone.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "POReviewDone");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["poreviewdone"].Points
                    .DataBind(fview, "WeekEnding", "POReviewDone", "Tooltip=WeekEnding");
            }


            if (chkFlow_ShowSmoketest.Checked)
            {

                series = CreateSeries("Smoketest", SeriesChartType.StackedArea, 1, clrSmoketest.BackColor, ChartDashStyle.Solid,
                    ChartValueType.DateTime, "Smoketest");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["Smoketest"].Points
                    .DataBind(fview, "WeekEnding", "Smoketest", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowSmoketestDone.Checked)
            {

                series = CreateSeries("SmoketestDone", SeriesChartType.StackedArea, 1, clrSmoketestDone.BackColor,
                    ChartDashStyle.Solid, ChartValueType.DateTime, "SmoketestDone");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["SmoketestDone"].Points
                    .DataBind(fview, "WeekEnding", "SmoketestDone", "Tooltip=WeekEnding");
            }

            if (chkFlow_ShowClosed.Checked)
            {

                series = CreateSeries("closed", SeriesChartType.StackedArea, 1, clrClosed.BackColor, ChartDashStyle.Solid,
                    ChartValueType.DateTime, "Closed");
                chartStoryCumulativeFlow.Series.Add(series);
                chartStoryCumulativeFlow.Series["closed"].Points
                    .DataBind(fview, "WeekEnding", "Closed", "Tooltip=WeekEnding");
            }















            chartStoryCumulativeFlow.Update();
        }

        private void chkFlow_ShowDevelopment_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowDevelopmentDone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowAsynchronyQA_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowAsynchronyQADone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowWegmansQA_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowWegmansQADone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowPOReview_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowPOReviewDone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowSmoketest_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowSmoketestDone_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void chkFlow_ShowClosed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStoryCumulativeFlow();

        }

        private void cmdFlowCheckAll_Click(object sender, EventArgs e)
        {
            foreach (Control control in pnlStoryCumFlowTop.Controls)
            {
                if (control.GetType() == typeof(CheckBox) && control.Name.StartsWith("chkFlow"))
                {
                    (control as CheckBox).Checked = true;
                }
            }
        }

        private void cmdFlowUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (Control control in pnlStoryCumFlowTop.Controls)
            {
                if (control.GetType() == typeof(CheckBox) && control.Name.StartsWith("chkFlow"))
                {
                    (control as CheckBox).Checked = false;
                }
            }

        }

        private void clrBacklog_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrBacklog.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }
        }

        private void clrDevelopment_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrDevelopment.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrDevelopmentDone_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrDevelopmentDone.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrAsynchronyQA_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrAsynchronyQA.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrAsynchronyQADone_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrAsynchronyQADone.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrWegmansQA_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrWegmansQA.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrWegmansQADone_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrWegmansQADone.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrPOReview_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrPOReview.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrPOReviewDone_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrPOReviewDone.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrSmoketest_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrSmoketest.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrSmoketestDone_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrSmoketestDone.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void clrClosed_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clrClosed.BackColor = colorDialog1.Color;
                UpdateStoryCumulativeFlow();
            }

        }

        private void tcMain_Click(object sender, EventArgs e)
        {

        }

        private void cmdRenderBugs_Click(object sender, EventArgs e)
        {
            RenderBugs();
        }
    }
}
