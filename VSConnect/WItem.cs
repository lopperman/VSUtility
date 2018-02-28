using System;

namespace VSConnect
{
    public class WItem
    {
        public int ID { get; set; }
        public int Rev { get; set; }
        public string WorkItemType { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public DateTime ChangedDate { get; set; }
        public DateTime RevisedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int BusinessPriority { get; set; }

        public string BoardColumn { get; set; }
        public string BoardLane { get; set; }
        public bool BoardColumnDone { get; set; }

        public string Key => $"{State}-{BoardColumn}-{BoardColumnDone}";
    }

    public class WItemLife
    {
        public int ID { get; set; }
        public string WorkItemType { get; set; }
        public string Title { get; set; }
        public int BusinessPriority { get; set; }
        public DateTime? SubmittedEnd { get; set; }
        public DateTime? GroomingEnd { get; set; }
        public DateTime? GroomedEnd { get; set; }
        public DateTime? ActiveEnd { get; set; }
        public DateTime? ReadyQAEnd { get; set; }
        public DateTime? QAEnd { get; set; }
        public DateTime? ReadyUATEnd { get; set; }
        public DateTime? UATEnd { get; set; }
        public DateTime? Closed { get; set; }
        public int? SubmittedDays { get; set; }
        public int? GroomingDays { get; set; }
        public int? GroomedDays { get; set; }
        public int? ActiveDays { get; set; }
        public int? ReadyQADays { get; set; }
        public int? QADays { get; set; }
        public int? ReadyUATDays { get; set; }
        public int? UATDays { get; set; }
        public Decimal CycleTime { get; set; }

    }

    public class FeatureLife
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EndNewDate { get; set; }
        public DateTime? EndInProgressDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public DateTime? EndApproved { get; set; }
        public DateTime? EndCommitted { get; set; }
        public DateTime? EndAnalysis { get; set; }
        public DateTime? EndVerification { get; set; }
        public int BusinessPriority { get; set; }
    }

    public class WItemLife_Task
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string WorkItemType { get; set; }
        public string Title { get; set; }
        public DateTime? NewEnd { get; set; }
        public DateTime? ActiveEnd { get; set; }
        public DateTime? Closed { get; set; }
        public int? NewDays { get; set; }
        public int? ActiveDays { get; set; }
        public Decimal CycleTime { get; set; }

    }
}
