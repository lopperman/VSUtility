﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.TeamFoundation.Client;

namespace VSConnect.Lifecycle
{
    public class UserStoryCumulativeFlow
    {
        public static void CreateUserStoryCumulativeData(Connect connect, DumpDataSet ds, int areaId)
        {

            List<int> workItemClosed = new List<int>();

            var workItemRevisions = WorkItemUtil.GetWorkItemRevisions(ds.WorkItemRevision);

            if (StaticUtil.CurrentFuzzFile.MaxStateIndex > 15) throw new Exception("UserStoryCumulativeFlow.CreateUserStoryCumulativeData cannot support more than 15 states.");

            DateTime currentWeekEnding = FirstFriday(ds.WorkItem);

            while (currentWeekEnding <= LastFriday())
            {


                DateTime searchDate = new DateTime(currentWeekEnding.Year,currentWeekEnding.Month,currentWeekEnding.Day,23,59,59);

                DateTime searchBeginning = searchDate.AddDays(-7);

                foreach (var workItem in Enumerable.Where(ds.WorkItem, x => x.CreatedDate <= searchDate))
                {

                    if (workItem.Type != "User Story" && workItem.Type != "Bug")
                    {
                        continue;
                    }

                    //all revisions up to searchDate
                    var revisions = workItemRevisions.Where(x => x.ID == workItem.ID && x.ChangedDate <= searchDate).OrderBy(y => y.Rev).ToList();

                    if (!revisions.Any())
                    {
                        continue;
                    }

                    DumpDataSet.UserStoryFlowRow row = ds.UserStoryFlow.NewUserStoryFlowRow();

                    row.ID = workItem.ID;
                    row.Title = workItem.Title;
                    row.Type = workItem.Type;
                    row.WeekEndingState = StaticUtil.CurrentFuzzFile.GetCurrentWorkItemState(revisions.Last()).CategoryIndex;

                    if (workItem.CreatedDate <= searchDate && workItem.CreatedDate >= searchBeginning)
                    {
                        row.NewThisWeek = 1;
                    }


                    for (int i = StaticUtil.CurrentFuzzFile.MinStateIndex; i <= StaticUtil.CurrentFuzzFile.MaxStateIndex; i++)
                    {
                        row[string.Format("State{0}Desc", i)] = StaticUtil.CurrentFuzzFile.GetStates(i).First().Category;

                        if (StaticUtil.CurrentFuzzFile.WasInState(revisions, i, searchBeginning, searchDate))
                        {
                            row[string.Format("State{0}", i)] = 1;
                        }
                        else
                        {
                            row[string.Format("State{0}", i)] = 0;
                        }

                        if (StaticUtil.CurrentFuzzFile.EnteredState(revisions, i, searchBeginning, searchDate))
                        {
                            row[string.Format("State{0}Entered", i)] = 1;
                        }
                        else 
                        {
                            row[string.Format("State{0}Entered", i)] = 0;
                        }
                    }

                    row.WeekEnding = currentWeekEnding;

                    if (row.IsNewThisWeekNull()) row.NewThisWeek = 0;

                    if (!row.IsState2Null() && row.State2 == 1) row.ActiveThisWeek = 1;
                    if (!row.IsState4Null() && row.State4 == 1) row.ActiveThisWeek = 1;

                    if (row.IsActiveThisWeekNull()) row.ActiveThisWeek = 0;

                    //closed logic should be:  state was <=4 before this week, and >=5 by the end of this week
                    bool wasInClosedDuringThisWeek = row.WeekEndingState >= 5;
                    bool wasClosedBeforeThisWeek = false;

                    var latestRevFromLastWeek = revisions.Where(x => x.ChangedDate < searchBeginning)
                        .OrderBy(x => x.Rev).LastOrDefault();
                    if (latestRevFromLastWeek != null)
                    {
                        if (latestRevFromLastWeek.LifecycleState >= 5)
                        {
                            wasClosedBeforeThisWeek = true;
                        }
                    }

                    row.Closed = (wasInClosedDuringThisWeek || wasClosedBeforeThisWeek) ? 1 : 0;

                    if (!wasClosedBeforeThisWeek && wasInClosedDuringThisWeek)
                    {
                        row.ClosedThisWeek = 1;
                    }
                    else 
                    {
                        row.ClosedThisWeek = 0;
                    }


                    ds.UserStoryFlow.AddUserStoryFlowRow(row);
                }

                currentWeekEnding = currentWeekEnding.AddDays(7);
            }
        }

        public static void CreateUserStoryCumulativeDataByDay(Connect connect, DumpDataSet ds, int areaId)
        {

            List<int> workItemClosed = new List<int>();

            var workItemRevisions = WorkItemUtil.GetWorkItemRevisions(ds.WorkItemRevision);

            if (StaticUtil.CurrentFuzzFile.MaxStateIndex > 15) throw new Exception("UserStoryCumulativeFlow.CreateUserStoryCumulativeData cannot support more than 15 states.");

            DateTime currentDayEnding = FirstFriday(ds.WorkItem);


            SortedList<int,int> lastState = new SortedList<int, int>(); //key = work item ID, value = last day state

            while (currentDayEnding <= new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59))
            {


                DateTime searchDate = new DateTime(currentDayEnding.Year, currentDayEnding.Month, currentDayEnding.Day, 23, 59, 59);

                DateTime searchBeginning = searchDate.Date;


                foreach (var workItem in Enumerable.Where(ds.WorkItem, x => x.CreatedDate <= searchDate))
                {
                    if (workItem.Type != "User Story" && workItem.Type != "Bug")
                    {
                        continue;
                    }

                    //all revisions up to searchDate
                    var revisions = workItemRevisions.Where(x => x.ID == workItem.ID && x.ChangedDate <= searchDate).OrderBy(y => y.Rev).ToList();

                    if (!revisions.Any())
                    {
                        continue;
                    }



                    DumpDataSet.UserStoryFlow_ByDayRow row = ds.UserStoryFlow_ByDay.NewUserStoryFlow_ByDayRow();
                    row.ID = workItem.ID;
                    row.Title = workItem.Title;
                    row.Type = workItem.Type;
                    row.DayEndingState = StaticUtil.CurrentFuzzFile.GetCurrentWorkItemState(revisions.Last()).CategoryIndex;


                    if (lastState.Any(x => x.Key == workItem.ID))
                    {
                        if (lastState[workItem.ID] == row.DayEndingState)
                        {
                            continue;
                        }
                    }

                    if (workItem.CreatedDate <= searchDate && workItem.CreatedDate >= searchBeginning)
                    {
                        row.NewThisDay = 1;
                    }


                    for (int i = StaticUtil.CurrentFuzzFile.MinStateIndex; i <= StaticUtil.CurrentFuzzFile.MaxStateIndex; i++)
                    {
                        row[string.Format("State{0}Desc", i)] = StaticUtil.CurrentFuzzFile.GetStates(i).First().Category;

                        if (StaticUtil.CurrentFuzzFile.WasInState(revisions, i, searchBeginning, searchDate))
                        {
                            row[string.Format("State{0}", i)] = 1;
                        }
                        else
                        {
                            row[string.Format("State{0}", i)] = 0;
                        }

                        if (StaticUtil.CurrentFuzzFile.EnteredState(revisions, i, searchBeginning, searchDate))
                        {
                            row[string.Format("State{0}Entered", i)] = 1;
                        }
                        else
                        {
                            row[string.Format("State{0}Entered", i)] = 0;
                        }
                    }

                    row.DayEnding = currentDayEnding;

                    if (row.IsNewThisDayNull()) row.NewThisDay = 0;

                    if (!row.IsState2Null() && row.State2 == 1) row.ActiveThisDay = 1;
                    if (!row.IsState4Null() && row.State4 == 1) row.ActiveThisDay = 1;

                    if (row.IsActiveThisDayNull()) row.ActiveThisDay = 0;

                    //closed logic should be:  state was <=4 before this week, and >=5 by the end of this week
                    bool wasInClosedDuringThisWeek = row.DayEndingState >= 5;
                    bool wasClosedBeforeThisWeek = false;

                    var latestRevFromLastWeek = revisions.Where(x => x.ChangedDate < searchBeginning)
                        .OrderBy(x => x.Rev).LastOrDefault();
                    if (latestRevFromLastWeek != null)
                    {
                        if (latestRevFromLastWeek.LifecycleState >= 5)
                        {
                            wasClosedBeforeThisWeek = true;
                        }
                    }

                    row.Closed = (wasInClosedDuringThisWeek || wasClosedBeforeThisWeek) ? 1 : 0;

                    if (!wasClosedBeforeThisWeek && wasInClosedDuringThisWeek)
                    {
                        row.ClosedThisDay = 1;
                    }
                    else
                    {
                        row.ClosedThisDay = 0;
                    }

                    if (lastState.Any(x => x.Key == workItem.ID))
                    {
                        lastState[workItem.ID] = row.DayEndingState;
                    }
                    else
                    {
                        lastState.Add(workItem.ID,row.DayEndingState);
                    }

                    ds.UserStoryFlow_ByDay.AddUserStoryFlow_ByDayRow(row);
                }

                currentDayEnding = currentDayEnding.AddDays(1);
            }
        }

        public static void BuildStoryFlowDump(DumpDataSet ds, string saveFilePath)
        {
            XLWorkbook wb = new XLWorkbook();

            DataTable table = CreateTransformedUserFlowTable(ds);

            wb.Worksheets.Add(table, "UserStoryFlowByDay");

            wb.SaveAs(saveFilePath);
        }

        private static DataTable CreateTransformedUserFlowTable(DumpDataSet ds)
        {
            DataTable table = new DataTable("Table1");


            table.Columns.Add(new DataColumn("ID", typeof(Int32)));
            table.Columns.Add(new DataColumn("Type", typeof(string)));
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("Tags", typeof(string)));
            table.Columns.Add(new DataColumn("DayEnding", typeof(DateTime)));
            table.Columns.Add(new DataColumn("NewThisDay", typeof(bool)));
            table.Columns.Add(new DataColumn("ActiveThisDay", typeof(bool)));
            table.Columns.Add(new DataColumn("DayEndingState", typeof(string)));

            DumpDataSet.UserStoryFlow_ByDayRow row = ds.UserStoryFlow_ByDay.FirstOrDefault(x => x.ID > 0);

            for (int i = 1; i <= 15; i++)
            {
                string colName1 = string.Format("State{0}Desc", i);
                if (row.IsNull(colName1))
                {
                    continue;
                }
                else
                {
                    string newColName1 = $"Was_{row[colName1]}";
                    string newColName2 = $"Entered_{row[colName1]}";
                    table.Columns.Add(new DataColumn(newColName1,typeof(bool)));
                    table.Columns.Add(new DataColumn(newColName2,typeof(bool)));
                }
            }

            foreach (DataColumn col in table.Columns)
            {
                col.AllowDBNull = true;
            }

            foreach (DumpDataSet.UserStoryFlow_ByDayRow dayRow in ds.UserStoryFlow_ByDay)
            {
                DataRow newRow = table.NewRow();
                newRow["ID"] = dayRow.ID;
                newRow["Type"] = dayRow.Type;
                newRow["Title"] = dayRow.Title;
                newRow["Tags"] = GetTags(ds,dayRow.ID);
                newRow["DayEnding"] = dayRow.DayEnding;
                newRow["NewThisDay"] = dayRow.NewThisDay == 1;
                newRow["ActiveThisDay"] = dayRow.ActiveThisDay == 1;
                newRow["DayEndingState"] = StaticUtil.CurrentFuzzFile.GetStates(dayRow.DayEndingState).First().Category;

                for (int i = 1; i <= 15; i++)
                {
                    string colName1 = string.Format("State{0}Desc", i);
                    string colNameBase = dayRow[colName1].ToString();

                    if (dayRow.IsNull(colName1))
                    {
                        continue;
                    }
                    else
                    {
                        string wasInCol = string.Format("State{0}", i);
                        string enteredCol = string.Format("State{0}Entered", i);

                        string newColName1 = $"Was_{colNameBase}";
                        string newColName2 = $"Entered_{colNameBase}";

                        if (!dayRow.IsNull(wasInCol))
                        {
                            newRow[newColName1] = Convert.ToBoolean(dayRow[wasInCol]);
                        }
                        if (!dayRow.IsNull(enteredCol))
                        {
                            newRow[newColName2] = Convert.ToBoolean(dayRow[enteredCol]);
                        }

                    }
                }

                table.Rows.Add(newRow);

            }

            table.AcceptChanges();

            FillInBetweenStates(table);

            return table;


        }

        private static void FillInBetweenStates(DataTable table)
        {
//            var distinctIds = table.AsEnumerable()
//                .Select(s => new {
//                    id = s.Field<int>("ID"),
//                })
//                .Distinct().ToList();


            List<int> distinctIds = table.AsEnumerable()
                .Select(s => s.Field<int>("ID"))
                .Distinct().ToList();
            

            foreach (int id in distinctIds)
            {
                DataRow[] array = table.Select("ID = " + id, "Order By DayEnding Asc");

                for (int i = array.Length - 1; i > 0; i--)
                {
                    
                }
            }


        }

        private static string GetTags(DumpDataSet ds, int id)
        {
            string ret = "none";

            DumpDataSet.WorkItemRow wiRow = ds.WorkItem.FirstOrDefault(x => x.ID == id);
            if (wiRow != null && !wiRow.IsTagsNull())
            {
                ret = wiRow.Tags;
            }

            return ret;

        }

        private static DateTime FirstFriday(DumpDataSet.WorkItemDataTable table)
        {
            DateTime ret = Enumerable.OrderBy(table, x => x.CreatedDate).First().CreatedDate;

            if (ret.DayOfWeek == DayOfWeek.Friday)
            {
                return new DateTime(ret.Year,ret.Month,ret.Day);
            }

            while (ret.DayOfWeek != DayOfWeek.Friday)
            {
                ret = ret.AddDays(-1);
                if (ret.DayOfWeek == DayOfWeek.Friday)
                {
                    break;
                }
            }

            return new DateTime(ret.Year, ret.Month, ret.Day);
        }

        private static DateTime LastFriday()
        {
            DateTime ret = DateTime.Today;

            if (ret.DayOfWeek == DayOfWeek.Friday)
            {
                return ret;
            }

            while (ret.DayOfWeek != DayOfWeek.Friday)
            {
                ret = ret.AddDays(1);
                if (ret.DayOfWeek == DayOfWeek.Friday)
                {
                    break;
                }

            }

            return new DateTime(ret.Year, ret.Month, ret.Day);

        }

    }
}
