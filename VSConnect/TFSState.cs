using System;
using System.Xml.Serialization;

namespace VSConnect
{
    public class TFSState
    {

        public TFSState()
        {
        }

        public int Index { get; set; }
        public string Category { get; set; }
        public string KanbanColumn { get; set; }
        public string SystemState { get; set; }
        public bool KanbanColumnDoing { get; set; }
        public bool KanbanColumnDone { get; set; }
        public int CategoryIndex { get; set; }
        public bool DevDone { get; set; }
        public bool QADone { get; set; }
        public bool UATDone { get; set; }

        public string Key => $"{SystemState}-{KanbanColumn}-{KanbanColumnDone}";


        public override string ToString()
        {
            return string.Format("Idx: {0}, Category: {1}, CatIdx: {6}, KanbanColumn {2}, SysState: {3}, Doing: {4}, Done:{5}",
                Index, Category, KanbanColumn, SystemState, KanbanColumnDoing, KanbanColumnDone,CategoryIndex);
        }
    }
}