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
        public static void CreateUserStoryCumulativeData(Connect connect, DumpDataSet ds)
        {

            List<int> workItemClosed = new List<int>();

            var workItemRevisions = WorkItemUtil.GetWorkItemRevisions(ds.WorkItemRevision);

            LifecycleStates states = new LifecycleStates();

            if (states.MaxStateIndex > 15) throw new Exception("UserStoryCumulativeFlow.CreateUserStoryCumulativeData cannot support more than 15 states.");

            DateTime currentWeekEnding = FirstFriday(ds.WorkItem);

            while (currentWeekEnding <= LastFriday())
            {
                DateTime searchDate = new DateTime(currentWeekEnding.Year,currentWeekEnding.Month,currentWeekEnding.Day,23,59,59);

                foreach (var workItem in ds.WorkItem.Where(x => x.CreatedDate <= searchDate))
                {
                    if (workItem.Type != "User Story" && workItem.Type != "Bug")
                    {
                        continue;
                    }

                    //all revisions up to searchDate
                    var revisions = workItemRevisions.Where(x => x.ID == workItem.ID && x.ChangedDate <= searchDate).OrderBy(y => y.Rev).ToList();

                    DumpDataSet.UserStoryFlowRow row = ds.UserStoryFlow.NewUserStoryFlowRow();

                    row.ID = workItem.ID;
                    row.Title = workItem.Title;
                    row.Type = workItem.Type;
                    row.WeekEndingState = states.GetCurrentWorkItemStateCategory(revisions.Last()).Position;

                    if (workItem.CreatedDate <= searchDate && workItem.CreatedDate >= searchDate.AddDays(-7))
                    {
                        row.NewThisWeek = 1;
                    }

                    if (row.ID == 117096)
                    {
                        string asdf = "asdf";
                    }

                    for (int i = states.MinStateIndex; i <= states.MaxStateIndex; i++)
                    {
                        row[string.Format("State{0}Desc", i)] = states.GetStates(i).First().StateCategory;

                        if (states.WasInState(revisions, i, searchDate, searchDate.AddDays(-7)))
                        {
                            row[string.Format("State{0}", i)] = 1;
                        }
                        else
                        {
                            row[string.Format("State{0}", i)] = 0;
                        }
                    }


                    row.WeekEnding = currentWeekEnding;

                    if (row.IsNewThisWeekNull()) row.NewThisWeek = 0;

                    if (!row.IsState2Null() && row.State2 == 1) row.ActiveThisWeek = 1;
                    if (!row.IsState4Null() && row.State4 == 1) row.ActiveThisWeek = 1;
                    if (row.IsActiveThisWeekNull()) row.ActiveThisWeek = 0;

                    //if there is a 1 in any state greater than or equal to 5
                    if (row.WeekEndingState >= 5 && !workItemClosed.Contains(workItem.ID))
                    {
                        workItemClosed.Add(workItem.ID);
                        row.Closed = 1;
                    }
                    else
                    {
                        row.Closed = 0;
                    }
//                    else if (row.IsClosedNull())
                    //                    {
                    //                        if (workItem.State == "Closed" || workItem.State == "Resolved")
                    //                        {
                    //                            row.Closed = 1;
                    //                            workItemClosed.Add(workItem.ID);
                    //                        }
                    //                        else
                    //                        {
                    //                            row.Closed = 0;
                    //                        }
                    //                    }

                    ds.UserStoryFlow.AddUserStoryFlowRow(row);
                }

                currentWeekEnding = currentWeekEnding.AddDays(7);
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
