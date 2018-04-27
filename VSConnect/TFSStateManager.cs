using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace VSConnect
{
    public class TFSStateManager 
    {
        public List<TFSState> _states = null;
        public string ProjectUri { get; set; }
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public int AreaId { get; set; }

        public List<TFSState> States
        {
            get
            {
                if (_states == null) _states = new List<TFSState>();
                return _states.OrderBy(x=>x.CategoryIndex).ThenBy(y=>y.Index).ToList();
            }
            set { _states = value; }
        }

        public void AddState(string category, string kanbanColumn, string systemState, bool kanbanColumnDoing,
            bool kanbanColumnDone, bool devDone = false, bool qaDone = false, bool uatDone = false)
        {
            if (kanbanColumnDoing == kanbanColumnDone)
            {
                throw new Exception("kanbanColumnDoing and kanbanColumnDone cannot have the same value in TFSStateManager.AddState");
            }
            var bools = new bool[] {devDone, qaDone, uatDone};
            if (bools.ExceedsThreshold<bool>(1))
            {
                throw new Exception("Only one of the following can be true:  devDone, qaDone, uatDone.  TFSStateManager.AddState");
            }

            if (States.Any(x => x.Category == category && x.KanbanColumn == kanbanColumn &&
                                       x.SystemState == systemState && x.KanbanColumnDoing == kanbanColumnDoing &&
                                       x.KanbanColumnDone == kanbanColumnDone))
            {
                //don't add
            }
            else
            {
                var state = new TFSState();
                state.Category = category;
                state.KanbanColumn = kanbanColumn;
                state.SystemState = systemState;
                state.KanbanColumnDoing = kanbanColumnDoing;
                state.KanbanColumnDone = kanbanColumnDone;
                state.DevDone = devDone;
                state.QADone = qaDone;
                state.UATDone = uatDone;
                state.Index = States.Count;

                state.CategoryIndex = GetCategoryIndex(category);

                _states.Add(state);

            }

        }

        public TFSState[] GetDevDoneStates
        {
            get
            {
                if (_states.Any(x => x.DevDone))
                {
                    return _states.Where(x => x.DevDone).ToArray();
                }
                return null;
            }
        }

        public TFSState[] GetQADoneStates
        {
            get
            {
                if (_states.Any(x => x.QADone))
                {
                    return _states.Where(x => x.QADone).ToArray();
                }
                return null;
            }
        }

        public TFSState[] GetUATDoneStates
        {
            get
            {
                if (_states.Any(x => x.UATDone))
                {
                    return _states.Where(x => x.UATDone).ToArray();
                }
                return null;
            }
        }

        public TFSState GetCurrentWorkItemState(WItem witem)
        {
            if (witem == null) return null;

            if (witem.State == "New" && string.IsNullOrWhiteSpace(witem.BoardColumn))
            {
                witem.BoardColumn = "Backlog";
            }

            if (witem.State == "Removed")
            {
                //we have a work item that was removed, but later re-instated
                witem.State = "Backlog";
                witem.BoardColumn = "Backlog";
            }

            var state = _states.SingleOrDefault(x => x.SystemState == witem.State &&
                                                    x.KanbanColumn == witem.BoardColumn &&
                                                    x.KanbanColumnDone == witem.BoardColumnDone);

            if (state == null)
            {
                state = _states.FirstOrDefault(x => x.SystemState == witem.State);
            }

            if (state == null)
            {
                throw new Exception("Cannot determine state for WItem " + witem.ID + ", revision " + witem.Rev);
            }

            return state;

        }

        public int MinStateIndex
        {
            get { return _states.OrderBy(x => x.CategoryIndex).First().CategoryIndex; }
        }
        public int MaxStateIndex
        {
            get { return _states.OrderBy(x => x.CategoryIndex).Last().CategoryIndex; }
        }

        public TFSState[] GetStates(int position)
        {
            return _states.Where(x => x.CategoryIndex == position).ToArray();
        }

        public DateTime? GetStartDate(List<WItem> revisions, int position)
        {
            //find the first revision where data matches any of the lifecyclestates
            //if can't find, start looking for position -1, until reach 1

            DateTime? ret = null;

            int lookingForPosition = position;

            while (lookingForPosition >= MinStateIndex)
            {
                List<string> lifecycleKeys = GetStates(position).Select(x => x.Key).ToList();

                var results = revisions.Where(r => lifecycleKeys.Contains(r.Key));

                if (results.Any())
                {
                    ret = results.OrderBy(x => x.ChangedDate).First().ChangedDate;
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
                    ret = results.OrderBy(x => x.ChangedDate).First().ChangedDate;
                    break;
                }

                lookingForPosition += 1;
            }

            return ret;
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
                    if (this.GetCurrentWorkItemState(rev).CategoryIndex == state)
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
                    if (this.GetCurrentWorkItemState(rev).CategoryIndex == state)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public bool EnteredState(List<WItem> revisions, int state, DateTime startDt, DateTime endDt)
        {
            var ret = false;
            var revisionsInDateRange = revisions.Where(x => x.ChangedDate >= startDt && x.ChangedDate <= endDt)
                .OrderBy(y => y.Rev).ToList();

            int startState = 0;

            if (revisions.Any(x => x.ChangedDate < startDt))
            {
                startState = revisions.Where(x => x.ChangedDate < startDt).OrderBy(x => x.Rev).Last().LifecycleState;
            }

            if (startState != state)
            {
                foreach (var rev in revisionsInDateRange)
                {
                    if (rev.LifecycleState == state) return true;
                }
            }
            else
            {
                //if is state we're looking for, then did state change, then change back to state, then true

                var firstRevOfDifferentState = revisionsInDateRange.Where(x => x.LifecycleState != state).FirstOrDefault();
                if (firstRevOfDifferentState == null) return false;

                int firstDifferentRev = firstRevOfDifferentState.Rev;

                return revisionsInDateRange.Where(x => x.Rev > firstDifferentRev && x.LifecycleState == state).Any();
            }

            return ret;
        }

        public int GetCategoryIndex(string category)
        {
            if (States.Any(x => x.Category == category))
            {
                return States.First(x => x.Category == category).CategoryIndex;
            }
            if (States.Count == 0) return 1;

            int highestCatIndex = States.Max(x => x.CategoryIndex);

            return highestCatIndex + 1;
        }

        public static SortedDictionary<string,TFSStateManager> LoadFuzzFiles(string directoryName)
        {
            SortedDictionary<string,TFSStateManager> ret = new SortedDictionary<string, TFSStateManager>();

            foreach (var file in Directory.GetFiles(directoryName))
            {
                if (file.ToLower().EndsWith(".fuzz"))
                {
                    string output = File.ReadAllText(file);
                    TFSStateManager deserializedStateManager = JsonConvert.DeserializeObject<TFSStateManager>(output);
                    ret.Add(Path.GetFileName(file),deserializedStateManager);
                }
            }

            return ret;
        }

        public static void SaveFuzzFile(string directoryName, TFSStateManager sm)
        {
            string output = JsonConvert.SerializeObject(sm);

            var fuzzFiles = LoadFuzzFiles(directoryName);

            foreach (KeyValuePair<string, TFSStateManager> kvp in fuzzFiles)
            {
                if (kvp.Value.ProjectName == sm.ProjectName && kvp.Value.TeamName == sm.TeamName &&
                    kvp.Value.AreaId == sm.AreaId)
                {
                    File.Delete(Path.Combine(@"{0}\{1}",directoryName,kvp.Key));                    
                }
            }

            string path = string.Format(@"{0}\{1}.fuzz", directoryName, Guid.NewGuid());

            // This text is added only once to the file.
                // Create a file to write to.
            
            File.WriteAllText(path, output);

        }
    }


}