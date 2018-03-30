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
                    var revisions = workItemRevisions.Where(x => x.ID == workItem.ID && x.RevisedDate <= searchDate).OrderBy(y => y.Rev).ToList();

                    DumpDataSet.UserStoryFlowRow row = ds.UserStoryFlow.NewUserStoryFlowRow();

                    row.ID = workItem.ID;
                    row.Title = workItem.Title;
                    if (workItem.CreatedDate <= searchDate && workItem.CreatedDate >= searchDate.AddDays(-7))
                    {
                        row.NewThisWeek = 1;
                    }
                    //TODO:  Add ActiveThisWEek
                    for (int i = states.MinStateIndex; i <= states.MaxStateIndex; i++)
                    {
                        row[string.Format("State{0}Desc", i)] = states.GetStates(i).First().StateCategory;
                    }

                    //find the latest state for this item for this week
                    //pass in the latest revision, and determine state
                    var state = states.GetCurrentWorkItemStateCategory(revisions.LastOrDefault());
                    if (state != null)
                    {
                        string columnName = string.Format("State{0}", state.Position);
                        row[columnName] = 1;
                    }
                    row.WeekEnding = currentWeekEnding;

                    if (row.IsNewThisWeekNull()) row.NewThisWeek = 0;

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
