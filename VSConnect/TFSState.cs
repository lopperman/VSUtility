using System;

namespace VSConnect
{
    [Serializable]
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
    }
}