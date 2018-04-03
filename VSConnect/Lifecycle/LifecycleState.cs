using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect.Lifecycle
{
    public class LifecycleState
    {
        public int Position { get; set; }
        public string StateCategory { get; set; }
        public string WorkItemState { get; set; }
        public string WorkItemBoardColumn { get; set; }
        public bool WorkItemBoardColumnDone { get; set; }

        public LifecycleState(string stateCategory, string workItemState, string workItemBoardColumn, bool workItemBoardColumnDone, int position)
        {
            StateCategory = stateCategory;
            WorkItemState = workItemState;
            WorkItemBoardColumn = workItemBoardColumn;
            WorkItemBoardColumnDone = workItemBoardColumnDone;
            Position = position;
        }

        public string Key => $"{WorkItemState}-{WorkItemBoardColumn}-{WorkItemBoardColumnDone}";
    }


    public class LifecycleStates
    {
        private List<LifecycleState> states = GetConfigStates();

        public LifecycleState GetCurrentWorkItemStateCategory(WItem witem)
        {
            if (witem == null) return null;

            if (witem.State == "New" && string.IsNullOrWhiteSpace(witem.BoardColumn))
            {
                witem.BoardColumn = "Backlog";
            }

            var state = states.SingleOrDefault(x => x.WorkItemState == witem.State &&
                                          x.WorkItemBoardColumn == witem.BoardColumn &&
                                          x.WorkItemBoardColumnDone == witem.BoardColumnDone);

            if (state == null)
            {
                state = states.FirstOrDefault(x => x.WorkItemState == witem.State);
            }

            if (state == null)
            {
                throw new Exception("Cannot determine state for WItem " + witem.ID + ", revision " + witem.Rev);
            }

            return state;
        }

        public int MinStateIndex
        {
            get { return states.OrderBy(x => x.Position).First().Position; }
        }
        public int MaxStateIndex
        {
            get { return states.OrderBy(x => x.Position).Last().Position; }
        }

        public LifecycleState[] GetStates(int position)
        {
            return states.Where(x => x.Position == position).ToArray();
        }

        public DateTime? GetStartDate(List<WItem> revisions, int position)
        {
            //find the first revision where data matches any of the lifecyclestates
            //if can't find, start looking for position -1, until reach 1

            DateTime? ret = null;

            int lookingForPosition = position;

            while (lookingForPosition >= MinStateIndex)
            {
                List<string> lifecycleKeys = GetStates(position).Select(x=>x.Key).ToList();

                var results = revisions.Where(r => lifecycleKeys.Contains(r.Key));

                if (results.Any())
                {
                    ret = results.OrderBy(x => x.RevisedDate).First().RevisedDate;
                    break;
                }

                lookingForPosition -= 1;
            }

            return ret;
        }

        public DateTime? GetEndDate(List<WItem> revisions, int position)
        {
            //End date for 'position' is the first start date for he next position, unless item is closed, then it's the start date for closed

            DateTime? ret = null;

            int lookingForPosition = position;
            if (position != MaxStateIndex) position = position + 1;

            while (lookingForPosition <= MaxStateIndex)
            {
                List<string> lifecycleKeys = GetStates(position).Select(x => x.Key).ToList();

                var results = revisions.Where(r => lifecycleKeys.Contains(r.Key));

                if (results.Any())
                {
                    ret = results.OrderBy(x => x.RevisedDate).First().RevisedDate;
                    break;
                }

                lookingForPosition += 1;
            }

            return ret;
        }

        private static List<LifecycleState> GetConfigStates()
        {
            //NOTE:  DO NOT CHANGE THIS LIST ... THERE'RE SOME HARDED CODED USES OF THIS AND IT WOULD SCREW UP METRICS.
            //TODO:  TURN THIS INTO A LOADABLE LIST PER PROJECT/PROJECT AREA

            List<LifecycleState> states = new List<LifecycleState>();

            states.Add(new LifecycleState("Backlog","New","New",false,1));
            states.Add(new LifecycleState("Backlog","New","New",true,1));
            states.Add(new LifecycleState("Backlog","New","Backlog",true,1));
            states.Add(new LifecycleState("Backlog","New","Backlog",false,1));
            states.Add(new LifecycleState("Backlog","New","Story Authoring",false,1));
            states.Add(new LifecycleState("Backlog","New","Story Authoring",true, 1));
            states.Add(new LifecycleState("Backlog","New","Ready For Dev",false, 1));
            states.Add(new LifecycleState("Backlog","New","Ready For Dev",true, 1));
            states.Add(new LifecycleState("Development", "Active", "Active", false, 2));
            states.Add(new LifecycleState("Development", "Active", "Active", true, 2));
            states.Add(new LifecycleState("Development", "Active", "Development", false, 2));
            states.Add(new LifecycleState("DevelopmentDone", "Active", "Development", true, 3));
            states.Add(new LifecycleState("AsynchronyQA", "Active", "Asynchrony QA", false, 4));
            states.Add(new LifecycleState("AsynchronyQADone", "Active", "Asynchrony QA", true, 5));
            states.Add(new LifecycleState("WegmansQA", "Active", "Wegmans QA", false, 6));
            states.Add(new LifecycleState("WegmansQADone", "Active", "Wegmans QA", true, 7));
            states.Add(new LifecycleState("POReview", "Resolved", "PO Review", false, 8));
            states.Add(new LifecycleState("POReviewDone", "Resolved", "PO Review", true, 9));
            states.Add(new LifecycleState("Smoketest", "Resolved", "Alpha Channel Smoke Test", false, 10));
            states.Add(new LifecycleState("SmoketestDone", "Resolved", "Alpha Channel Smoke Test", true, 11));
            states.Add(new LifecycleState("Closed", "Closed", "Closed", false, 12));

            return states;
        }

        public bool WasInState(List<WItem> revisions, int state, DateTime startDt, DateTime endDt)
        {
            var ret = false;

            //first, check if a revision exists between dates
            var revs = revisions.Where(x => x.ChangedDate >= startDt && x.ChangedDate <= endDt).ToList();
            if (revs != null && revs.Any())
            {
                foreach (var rev in revs)
                {
                    if (this.GetCurrentWorkItemStateCategory(rev).Position == state)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            //otherwise, take the latest rev BEFORE start date, and that is the state
            else
            {
                var rev = revisions.Where(x => x.ChangedDate <= startDt).OrderBy(y => y.Rev).LastOrDefault();
                if (rev != null)
                {
                    if (this.GetCurrentWorkItemStateCategory(rev).Position == state)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }
    }

}
