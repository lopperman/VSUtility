using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TFSMetrics;

namespace VSConnect
{
    public static class VSCommon
    {

        private static DayOfWeek _lastDayOfWeek = DayOfWeek.Friday;

        public static DayOfWeek LastDayOfWeek
        {
            get
            {
                return _lastDayOfWeek;

            }
            set { _lastDayOfWeek = value; }
        }

        public static int GetWorkingDays(DateTime d0, DateTime d1)
        {
            int ndays = 1 + Convert.ToInt32((d1.Date - d0.Date).TotalDays);
            int nsaturdays = (ndays + Convert.ToInt32(d0.DayOfWeek)) / 7;
            return ndays - 2 * nsaturdays
                   - (d0.DayOfWeek == DayOfWeek.Sunday ? 1 : 0)
                   + (d1.DayOfWeek == DayOfWeek.Saturday ? 1 : 0);
        }

        public static int GetBusinessHours(int startWorkHour, int endWorkHour, DateTime startDt, DateTime endDt)
        {
            //TODO:  confirm no problems after new LastDayOfWeekLogic is in use.

            int ret = 0;

            if (startDt.DayOfWeek == DayOfWeek.Saturday) startDt = startDt.Date.AddDays(2) + new TimeSpan(startWorkHour, 0, 0);
            if (startDt.DayOfWeek == DayOfWeek.Sunday) startDt = startDt.Date.AddDays(1) + new TimeSpan(startWorkHour, 0, 0);
            if (endDt.DayOfWeek == DayOfWeek.Saturday) endDt = endDt.Date.AddDays(-1) + new TimeSpan(endWorkHour, 0, 0);
            if (endDt.DayOfWeek == DayOfWeek.Sunday) endDt = endDt.Date.AddDays(-2) + new TimeSpan(endWorkHour, 0, 0);

            if (startDt.Hour < startWorkHour) startDt = startDt.Date + new TimeSpan(startWorkHour, 0, 0);
            if (endDt.Hour > endWorkHour) endDt = endDt.Date + new TimeSpan(endWorkHour, 0, 0);

            double totalHours = endDt.Subtract(startDt).TotalHours;

            if (startDt.Date.CompareTo(endDt.Date) == 0)
            {
                ret = Convert.ToInt32(Math.Round(totalHours, MidpointRounding.AwayFromZero));
            }
            else
            {
                int hoursInFirstDay = Convert.ToInt32(Math.Round((startDt.Date + new TimeSpan(endWorkHour, 0, 0)).Subtract(startDt).TotalHours));
                int hoursInLastDay = Convert.ToInt32(Math.Round((endDt.Subtract(endDt.Date + new TimeSpan(startWorkHour, 0, 0)).TotalHours)));
                int mondayHours = (endWorkHour - startWorkHour) * CountDays(DayOfWeek.Monday, startDt.AddDays(1), endDt.AddDays(-1));
                int tuesdayHours = (endWorkHour - startWorkHour) * CountDays(DayOfWeek.Tuesday, startDt.AddDays(1), endDt.AddDays(-1));
                int wednesdayHours = (endWorkHour - startWorkHour) * CountDays(DayOfWeek.Wednesday, startDt.AddDays(1), endDt.AddDays(-1));
                int thursdayHours = (endWorkHour - startWorkHour) * CountDays(DayOfWeek.Thursday, startDt.AddDays(1), endDt.AddDays(-1));
                int fridayHours = (endWorkHour - startWorkHour) * CountDays(DayOfWeek.Friday, startDt.AddDays(1), endDt.AddDays(-1));
                ret = hoursInFirstDay + hoursInLastDay + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours;
            }

            return ret;
        }

        public static double GetBusinessHoursFraction(int startWorkHour, int endWorkHour, DateTime startDt, DateTime endDt)
        {
            double ret = 0;

            if (startDt.DayOfWeek == DayOfWeek.Saturday) startDt = startDt.Date.AddDays(2) + new TimeSpan(startWorkHour, 0, 0);
            if (startDt.DayOfWeek == DayOfWeek.Sunday) startDt = startDt.Date.AddDays(1) + new TimeSpan(startWorkHour, 0, 0);
            if (endDt.DayOfWeek == DayOfWeek.Saturday) endDt = endDt.Date.AddDays(-1) + new TimeSpan(endWorkHour, 0, 0);
            if (endDt.DayOfWeek == DayOfWeek.Sunday) endDt = endDt.Date.AddDays(-2) + new TimeSpan(endWorkHour, 0, 0);

            if (startDt.Hour < startWorkHour) startDt = startDt.Date + new TimeSpan(startWorkHour, 0, 0);
            if (startDt.Hour > endWorkHour) startDt = startDt.Date + new TimeSpan(endWorkHour, 0, 0);

            if (endDt.Hour > endWorkHour) endDt = endDt.Date + new TimeSpan(endWorkHour, 0, 0);
            if (endDt.Hour < startWorkHour) endDt = endDt.Date + new TimeSpan(startWorkHour, 0, 0);

            double totalHours = endDt.Subtract(startDt).TotalHours;

            if (startDt.Date.CompareTo(endDt.Date) == 0)
            {
                ret = Math.Round(totalHours, 1);
            }
            else
            {
                double hoursInFirstDay = (startDt.Date + new TimeSpan(endWorkHour, 0, 0)).Subtract(startDt).TotalHours;
                double hoursInLastDay = endDt.Subtract(endDt.Date + new TimeSpan(startWorkHour, 0, 0)).TotalHours;
                double mondayHours = (endWorkHour - startWorkHour) * CountDaysExcludeFirstLast(DayOfWeek.Monday, startDt, endDt);
                double tuesdayHours = (endWorkHour - startWorkHour) * CountDaysExcludeFirstLast(DayOfWeek.Tuesday, startDt, endDt);
                double wednesdayHours = (endWorkHour - startWorkHour) * CountDaysExcludeFirstLast(DayOfWeek.Wednesday, startDt, endDt);
                double thursdayHours = (endWorkHour - startWorkHour) * CountDaysExcludeFirstLast(DayOfWeek.Thursday, startDt, endDt);
                double fridayHours = (endWorkHour - startWorkHour) * CountDaysExcludeFirstLast(DayOfWeek.Friday, startDt, endDt);
                ret = hoursInFirstDay + hoursInLastDay + mondayHours + tuesdayHours + wednesdayHours + thursdayHours + fridayHours;
            }

            return Math.Round(ret, 1);
        }

        private static int CountDaysExcludeFirstLast(DayOfWeek day, DateTime start, DateTime end)
        {
            DateTime first = start;
            DateTime last = end;

            if (first.DayOfWeek == day) first = first.AddDays(1);
            if (last.DayOfWeek == day) last = last.AddDays(-1);

            return CountDays(day, first, last);
        }

        public static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;     // Total duration
            if (ts.TotalHours <= 0) return 0;
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }
        public static bool IsFileLocked(string path)
        {
            FileStream stream = null;

            if (!File.Exists(path))
            {
                return false;
            }

            FileInfo file = new FileInfo(path);

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }

        public static DateTime? GetDumpDate(string mdbPath)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbPath));
            OleDbCommand cmd = new OleDbCommand("select * from config", conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.Config);

            conn.Close();

            DateTime? ret = null;

            if (ds.Config.Any(x => x.Key == "DumpDate"))
            {
                ret = Convert.ToDateTime(ds.Config.Where(x => x.Key == "DumpDate").First().Val);
            }

            return ret;

        }

        public static int GetDumpFileAreaId(string mdbPath)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbPath));
            OleDbCommand cmd = new OleDbCommand("select * from config", conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.Config);

            conn.Close();

            int? ret = null;

            if (ds.Config.Any(x => x.Key == "AreaId"))
            {
                ret = Convert.ToInt32(ds.Config.Where(x => x.Key == "AreaId").First().Val);
            }

            if (ret.HasValue)
            {
                return ret.Value;
            }
            else
            {
                return 0;
            }

        }

        public static DumpDataSet.WorkItemRevisionDataTable LoadWorkItemRevisionTable(string mdbFilePath, string sql)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.WorkItemRevision);

            return ds.WorkItemRevision;
        }

        public static DataTable GeTable(string mdbFilePath, string sql)
        {
            DataTable table = new DataTable();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(table);

            return table;

        }

        public static DumpDataSet.WorkItemDataTable LoadWorkItemTable(string mdbFilePath, string sql)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.WorkItem);

            return ds.WorkItem;
        }

        public static DumpDataSet.VW_OrphansDataTable LoadVW_OrphansView(string mdbFilePath, string sql)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.VW_Orphans);

            return ds.VW_Orphans;
        }

        public static DumpDataSet.WorkItemMetricsDataTable LoadWorkItemMetricsTable(string mdbFilePath, string sql)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            adap.Fill(ds.WorkItemMetrics);

            return ds.WorkItemMetrics;
        }

        public static DumpDataSet LoadDumpDataset(string mdbFilePath)
        {
            DumpDataSet ds = new DumpDataSet();

            foreach (DataTable table in ds.Tables)
            {
                if (table.TableName.ToLower().StartsWith("vw"))
                {
                    table.PrimaryKey = null;
                }

                OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
                OleDbCommand cmd = new OleDbCommand("select * from " + table.TableName, conn);
                OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
                adap.Fill(table);
            }

            return ds;
        }


        public static int GetBusinessDays(DateTime start, DateTime end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                start = start.AddDays(2);
            }
            else if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                start = start.AddDays(1);
            }

            if (end.DayOfWeek == DayOfWeek.Saturday)
            {
                end = end.AddDays(-1);
            }
            else if (end.DayOfWeek == DayOfWeek.Sunday)
            {
                end = end.AddDays(-2);
            }

            int diff = (int)end.Subtract(start).TotalDays;

            int result = diff / 7 * 5 + diff % 7;

            if (end.DayOfWeek < start.DayOfWeek)
            {
                return result - 2;
            }
            else
            {
                return result;
            }
        }

        public static int GetWeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetLastDayOfWeek(DateTime date, DayOfWeek lastDayOfWeek)
        {
            return GetLastDayOfWeek(date.Year, GetWeekOfYear(date));
        }

        public static DateTime GetLastDayOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            result = result.AddDays(-3); //result is now Monday

            int diff = (int)VSCommon.LastDayOfWeek - (int)result.DayOfWeek;

            if (diff > 0)
            {
                result = result.AddDays(diff);
            }

            return result;


        }

        public static void SaveDataTableToCsv(DataTable dt, string filePath, string orderBy)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                Select(column => column.ColumnName);
            sb.AppendLine(string.Join("|", columnNames));

            DataView view = dt.DefaultView;
            view.Sort = orderBy;

            foreach (DataRowView row in view)
            {
                IEnumerable<string> fields = row.Row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join("|", fields));

            }

            //foreach (DataRow row in dt.Rows)
            //{
            //    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            //    sb.AppendLine(string.Join("|", fields));
            //}

            File.WriteAllText(filePath, sb.ToString());
        }

        public static DumpDataSet.VW_All_CurrentDataTable LoadVW_All_CurrentTable(string mdbFilePath, string sql)
        {
            DumpDataSet ds = new DumpDataSet();

            OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", mdbFilePath));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter adap = new OleDbDataAdapter(cmd);
            ds.EnforceConstraints = false;
            adap.Fill(ds.VW_All_Current);

            return ds.VW_All_Current;

        }
    }
}
