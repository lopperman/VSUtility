using System;
using System.Collections.Generic;
using System.Linq;

namespace VSConnect
{
    [Serializable]
    public class TFSStateManager
    {
        private SortedDictionary<int, TFSState> _states = null;
        public string ProjectUri { get; set; }
        public string ProjectName { get; set; }
        public string TeamName { get; set; }
        public int AreaId { get; set; }

        public SortedDictionary<int, TFSState> States
        {
            get
            {
                if (_states == null) _states = new SortedDictionary<int,TFSState>();
                return _states;
            }
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

            if (States.Values.Any(x => x.Category == category && x.KanbanColumn == kanbanColumn &&
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

                //add to bottom of categories.

            }

        }

        private int GetCategoryIndex(string category)
        {
            if (States.Values.Any(x => x.Category == category))
            {
                return States.Values.First(x => x.Category == category).CategoryIndex;
            }
            if (States.Count == 0) return 0;

            int highestCatIndex = States.Values.Max(x => x.CategoryIndex);

            return highestCatIndex + 1;
        }
    }
}