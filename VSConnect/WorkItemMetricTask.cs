using System;
using System.Collections.Generic;
using System.Linq;

namespace VSConnect
{
    public class WorkItemMetricsTask
    {
        private List<WorkItemMetricTask> _metrics = new List<WorkItemMetricTask>();

        public List<WorkItemMetricTask> GetMetrics()
        {
            return _metrics;
        }

        public void FillInMissingWeeksWithZero(int parentId, DateTime startDate, string state, string workItemType)
        {
            DateTime lastDayOfWeek = startDate.Date;
            if (startDate.DayOfWeek != VSCommon.LastDayOfWeek)
            {
                int daysUntilLastDayOfWeek = ((int)VSCommon.LastDayOfWeek - (int)lastDayOfWeek.DayOfWeek + 7) % 7;
                lastDayOfWeek = lastDayOfWeek.AddDays(daysUntilLastDayOfWeek);
            }

            DateTime nextEndOfWeekDate = DateTime.Today;
            if (nextEndOfWeekDate.DayOfWeek != VSCommon.LastDayOfWeek)
            {
                int daysUntilEndOfWeek = ((int)VSCommon.LastDayOfWeek - (int)nextEndOfWeekDate.DayOfWeek + 7) % 7;
                nextEndOfWeekDate = nextEndOfWeekDate.AddDays(daysUntilEndOfWeek);
            }

            while (lastDayOfWeek <= nextEndOfWeekDate)
            {
                AddSingleWorkItemMetric(parentId, lastDayOfWeek, state, workItemType, 0, 0);
                lastDayOfWeek = lastDayOfWeek.AddDays(7);
            }

        }

        public WorkItemMetricsTask AddSingleWorkItemMetric(int parentId, DateTime endDate, string state, string workItemType, decimal avgDaysInState, int storyCount)
        {
            DateTime lastDayOfWeek = endDate.Date;
            if (endDate.DayOfWeek != VSCommon.LastDayOfWeek)
            {
                int daysUntilLastDayOfWeek = ((int)VSCommon.LastDayOfWeek - (int)lastDayOfWeek.DayOfWeek + 7) % 7;
                lastDayOfWeek = lastDayOfWeek.AddDays(daysUntilLastDayOfWeek);
            }

            if (workItemType != "Task") return this;

            WorkItemMetricTask metric = _metrics.Where(x => x.ParentID == parentId && x.WeekEnding == lastDayOfWeek && x.State == state && x.WorkItemType == workItemType).SingleOrDefault();
            bool add = false;
            if (metric == null)
            {
                metric = new WorkItemMetricTask();
                add = true;
                metric.ParentID = parentId;
                metric.WeekEnding = lastDayOfWeek;
                metric.State = state;
                metric.WorkItemType = workItemType;
            }
            metric.AddMetric(avgDaysInState, storyCount);

            if (add) _metrics.Add(metric);

            return this;
        }
    }

    public class WorkItemMetricTask
    {
        private List<decimal> _avgDaysInState = new List<decimal>();
        private List<int> _storyCount = new List<int>();

        public int ParentID { get; set; }
        public DateTime WeekEnding { get; set; }
        public string WorkItemType { get; set; }
        public string State { get; set; }

        public void AddMetric(decimal avgDaysInState, int storyCount)
        {
            _avgDaysInState.Add(avgDaysInState);
            _storyCount.Add(storyCount);
        }

        public decimal AverageDaysInState
        {
            get
            {
                return StoryCount == 0 ? 0 : _avgDaysInState.Sum() / StoryCount;
            }
        }

        public int StoryCount
        {
            get
            {
                return _storyCount.Count == 0 ? 0 : _storyCount.Sum();
            }
        }
    }
}
