﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class frmDynamicChart : Form
    {
        private Connect connect = null;
        private string filePath;
        DumpDataSet ds = null;
        DataView view = null;
        string lastDumpDate = string.Empty;

        public frmDynamicChart(string mdbFilePath, Connect conn, int areaId, string projectName) : this()
        {
            this.filePath = mdbFilePath;
            this.connect = conn;
            ds = VSCommon.LoadDumpDataset(filePath);

            lastDumpDate = ds.Config.Where(x => x.Key == "DumpDate").First().Val;

            dtStartDt.Value = new DateTime(2017,9,1);

            BuildCheckboxes();

        }

        private void BuildCheckboxes()
        {
            Control lastControl = groupBox1;

            foreach (DataColumn col in ds.VW_CUM_FLOW_SUMMARY.Columns)
            {
                if (col.DataType.Name == "Double")
                {
                    string newName = BuildLabelName(col.ColumnName);

                    if (newName.Trim() == "Was In" || newName.Trim() == "Entered") continue;

                    CheckBox newcb = new CheckBox();
                    newcb.Name = col.ColumnName;
                    newcb.Tag = col.ColumnName;
                    newcb.Text = newName;
                    tabControl1.TabPages[0].Controls.Add(newcb);
                    newcb.Left = lastControl.Left;
                    newcb.Top = lastControl.Top + lastControl.Height + 5;
                    newcb.AutoSize = true;
                    lastControl = newcb;
                }
            }
        }

        private string BuildLabelName(string colColumnName)
        {
            var row = ds.VW_CUM_FLOW_SUMMARY.First();

            for (int i = 1; i <= 15; i++)
            {
                string descColName = string.Format("State{0}Desc", i);
                string descColValue = row[descColName].ToString();

                if (colColumnName == string.Format("State{0}", i))
                {
                    return string.Format("Was In {0}", descColValue);
                }
                else if (colColumnName == string.Format("State{0}Entered", i))
                {
                    return string.Format("Entered {0}", descColValue);
                }
            }

            return colColumnName;
        }


        public frmDynamicChart()
        {
            InitializeComponent();
        }

        public void RenderDynamicChart()
        {
            EnsureStartDateIsFriday();
            ClearChartSeries();

            DataTable table = ds.VW_CUM_FLOW_SUMMARY;

            string filter = BuildFilter();

            Series series = null;
            DataView devview = null;

            ClearChartTitles();

            SetChartTitle();

            foreach (Control ctl in tpConfig.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked)
                    {
                        string fieldName = ctl.Tag.ToString();
                        string friendyName = ctl.Text;

                        Color colorF = ColorTranslator.FromHtml(string.Format("#{0}",StaticUtils.ColorValues[chartDynamic.Series.Count]));

                        series = CreateSeries(fieldName, SeriesChartType.Line, 2, colorF, ChartDashStyle.Solid,
                            ChartValueType.DateTime, friendyName);
                        chartDynamic.Series.Add(series);
                        devview = new DataView(table, filter, "WeekEnding", DataViewRowState.CurrentRows);
                        chartDynamic.Series[fieldName].Points.DataBind(devview, "WeekEnding", fieldName, "Tooltip=WeekEnding");

                    }
                }
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


        private string BuildFilter()
        {
            string filter = string.Format("WeekEnding >= #{0}#", dtStartDt.Value.ToShortDateString());

            if (rbBug.Checked) filter = filter + " AND [Type] = 'Bug'";
            if (rbUserStory.Checked) filter = filter + " AND [Type] = 'User Story'";

            return filter;
        }

        private void SetChartTitle()
        {
            if (rbBug.Checked)
            {
                chartDynamic.Titles.Add(string.Format("Bug Summary From {0} to {1}", dtStartDt.Value.ToShortDateString(),
                    DateTime.Today.ToShortDateString()));
            }
            else if (rbUserStory.Checked)
            {
                chartDynamic.Titles.Add(string.Format("User Story Summary From {0} to {1}", dtStartDt.Value.ToShortDateString(),
                    DateTime.Today.ToShortDateString()));
            }
        }

        private void ClearChartTitles()
        {
            while (true)
            {
                if (chartDynamic.Titles.Any())
                {
                    chartDynamic.Titles.Remove(chartDynamic.Titles.First());
                }
                else
                {
                    break;
                }
            }
        }

        private void EnsureStartDateIsFriday()
        {
            DateTime startDt = dtStartDt.Value.Date;
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
        }

        private void ClearChartSeries()
        {
            while (true)
            {
                if (chartDynamic.Series.Count == 0)
                {
                    break;
                }
                chartDynamic.Series.Remove(chartDynamic.Series.FirstOrDefault());
            }
        }

        private void cmdRender_Click(object sender, EventArgs e)
        {
            RenderDynamicChart();

            tpConfig.TabIndex = 1;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintHelper.SetOrientation(chartDynamic, true);
            PrintHelper.SetMargins(chartDynamic, 10, 10, 10, 10);

            chartDynamic.Printing.PrintPreview();

        }
    }
}
