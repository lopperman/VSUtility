using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

                foreach (var workItem in ds.WorkItem.Where(x => x.CreatedDate <= searchDate))
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

            while (currentDayEnding <= new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59))
            {


                DateTime searchDate = new DateTime(currentDayEnding.Year, currentDayEnding.Month, currentDayEnding.Day, 23, 59, 59);

                DateTime searchBeginning = searchDate.Date;

                foreach (var workItem in ds.WorkItem.Where(x => x.CreatedDate <= searchDate))
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

                    row.WeekEnding = currentDayEnding;

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


                    ds.UserStoryFlow_ByDay.AddUserStoryFlow_ByDayRow(row);
                }

                currentDayEnding = currentDayEnding.AddDays(1);
            }
        }

        private static DateTime FirstFriday(DumpDataSet.WorkItemDataTable table)
        {
            DateTime ret = table.OrderBy(x => x.CreatedDate).First().CreatedDate;

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
