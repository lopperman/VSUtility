using System;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect
{
    public class WorkItemState
    {
        public int WorkItemId { get; set; }
        public string State { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int Count { get; set; }
    }

    public class KanbanItemState
    {
        public string WorkItemState { get; set; }
        public string KanbanColumn { get; set; }
        public bool KanbanColumnDone { get; set; }
        public int Rank { get; set; }
        public string FriendlyState { get; set; }

        public KanbanItemState()
        {
            
        }

        public KanbanItemState(string friendlyState, string workItemState, string kanbanColumn, bool kanbanColumnDone, int rank)
        {
            this.FriendlyState = friendlyState;
            this.WorkItemState = workItemState;
            this.KanbanColumn = kanbanColumn;
            this.KanbanColumnDone = kanbanColumnDone;
            this.Rank = rank;
        }

        public static KanbanItemState Build(WItem wItem)
        {
            var ret = new KanbanItemState();

            ret.KanbanColumn = wItem.BoardColumn;
            ret.KanbanColumnDone = wItem.BoardColumnDone;
            ret.WorkItemState = wItem.State;

            return ret;
        }
    }
}
