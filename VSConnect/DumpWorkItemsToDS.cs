using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using VSConnect.Lifecycle;

namespace VSConnect
{
    public class DumpWorkItemsToDS
    {
        public enum FirstOrLastEnum
        {
            First,
            Last
        }

        public enum FeatureState
        {
            None,
            New,
            Analysis,
            Approved,
            Committed,
            InProgress,
            Verification,
            Done,
            Removed
        }

        Connect connect = null;
        int systemAreadId = 0;
        DumpDataSet ds = null;
        string tmpFile = string.Empty;
        INotify notify = null;
        private string projectname = string.Empty;
        private DayOfWeek lastDayOfWeek;
        //private TFSStateManager lcStates = null;

        private void Notify(string message)
        {
            if (notify != null)
            {
                notify.Notify(message);
            }
        }

        public DumpWorkItemsToDS(Connect conn, int systemAreaId, INotify notify, string projectName, DayOfWeek lastDayOfWeek)
        {
            this.lastDayOfWeek = lastDayOfWeek;
            this.projectname = projectName;
            this.notify = notify;
            connect = conn;
            Notify("connectiong to: " + conn.ProjectCollection.Uri);
            this.systemAreadId = systemAreaId;

            
        }

        public void DumpWorkItems(string saveFilePath)
        {

            //get area path
            string systemAreaPath = connect.GetTeamAreas(connect.GetProject(projectname)).First(x => x.Id == systemAreadId).Name;


            tmpFile = Path.GetTempFileName().Replace(".tmp", ".mdb");
            tmpFile = Path.GetFileName(tmpFile);
            Notify("creating " + tmpFile);

            File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Dump.mdb"), Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tmpFile), true);

            ds = new DumpDataSet();

            //string query1 = string.Format("SELECT * FROM WorkItems WHERE System.AreaId = {0}", systemAreadId);
            string query1 = string.Format("SELECT * FROM WorkItems WHERE (System.TeamProject = '{0}' and System.WorkItemType = 'Epic' and System.State <> 'Removed') or (System.TeamProject = '{0}' and System.WorkItemType = 'Feature' and System.State <> 'Removed') or (System.State <> 'Removed' and System.AreaPath = 'Marketing Temp\\{1}')", projectname,systemAreaPath);
            Notify("fetching WorkItems from TFS");
            List<WorkItem> results = connect.ExecuteWorkItemWIQL(query1);

            Notify("analyzing work items ...");

            int witotal = 0;
            int wiCounter = 0;

            AddProjects();

            List<string> workItems = new List<string>();
            workItems.Add("Epic");
            workItems.Add("Feature");
            workItems.Add("Bug");
            workItems.Add("User Story");


            foreach (WorkItem wi in results)
            {



                if (!workItems.Contains(wi.Type.Name))
                {
                    continue;
                }
                if (wi.State == "Removed")
                {
                    continue;
                }

                wiCounter += 1;
                witotal += 1;

                if (wiCounter >= 100)
                {
                    Notify("analyzing work items ... " + witotal);
                    wiCounter = 0;
                }

                AddWorkItem(wi);

                AddWorkItemRevisions(wi);

                AddAttachments(wi);

                AddWorkItemLinks(wi);

                AddHistoryComments(wi);
            }

            Notify("analyzing WorkItem stack ranks...");

            ReorderStackRank();

            AddConfig();

            Notify("saving results to file ...");

            SaveDataset(tmpFile);

            SaveDumpFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tmpFile), saveFilePath);

            Notify("removing temporary files ...");
            File.Delete(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tmpFile));
        }

        private void AddProjects()
        {
            bool hasUpdates = false;

            foreach (var proj in connect.GetTeamAreas(connect.GetProject(projectname)))
            {
                if (ds.Project.All(x => x.Id != proj.Id))
                {
                    hasUpdates = true;
                    ds.Project.AddProjectRow(proj.Id, proj.Name);
                }
            }

            if (hasUpdates)
            {
                Notify("updating project (areas) list ...");
                string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\{1}",
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tmpFile);

                DumpDataSetTableAdapters.ProjectTableAdapter adap = new DumpDataSetTableAdapters.ProjectTableAdapter();
                adap.Connection.ConnectionString = connString;
                adap.Update(ds.Project);

            }
        }

        private void AddConfig()
        {
            ds.Config.AddConfigRow("DumpDate", DateTime.Now.ToString());
            ds.Config.AddConfigRow("AreaId", systemAreadId.ToString());
        }

        private static void SaveDumpFile(string tmpFilePath, string saveFilePath)
        {
            File.Copy(tmpFilePath, saveFilePath, true);
        }

        private void SaveDataset(string tmpFileName)
        {
            string connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\{1}",
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tmpFile);

            DumpDataSetTableAdapters.WorkItemTableAdapter adap = new DumpDataSetTableAdapters.WorkItemTableAdapter();
            adap.Connection.ConnectionString = connString;
            adap.Update(ds.WorkItem);

            DumpDataSetTableAdapters.WorkItemRevisionTableAdapter wirAdap = new DumpDataSetTableAdapters.WorkItemRevisionTableAdapter();
            wirAdap.Connection.ConnectionString = connString;
            wirAdap.Update(ds.WorkItemRevision);

            DumpDataSetTableAdapters.AttachmentTableAdapter attAdap = new DumpDataSetTableAdapters.AttachmentTableAdapter();
            attAdap.Connection.ConnectionString = connString;
            attAdap.Update(ds.Attachment);

            DumpDataSetTableAdapters.LinksTableAdapter linkAdap = new DumpDataSetTableAdapters.LinksTableAdapter();
            linkAdap.Connection.ConnectionString = connString;
            linkAdap.Update(ds.Links);

            Notify("adding WorkItem metrics ...");
            AddMetrics_CycleTime(ds);

            Notify("saving WorkItem metrics ...");
            DumpDataSetTableAdapters.Metrics_CycleTimeTableAdapter metAdap = new DumpDataSetTableAdapters.Metrics_CycleTimeTableAdapter();
            metAdap.Connection.ConnectionString = connString;
            metAdap.Update(ds.Metrics_CycleTime);

            DumpDataSetTableAdapters.WorkItemLifeTableAdapter lifeAdap = new DumpDataSetTableAdapters.WorkItemLifeTableAdapter();
            lifeAdap.Connection.ConnectionString = connString;
            lifeAdap.Update(ds.WorkItemLife);

            DumpDataSetTableAdapters.WorkItemMetricsTableAdapter wimAdap = new DumpDataSetTableAdapters.WorkItemMetricsTableAdapter();
            wimAdap.Connection.ConnectionString = connString;
            wimAdap.Update(ds.WorkItemMetrics);


            DumpDataSetTableAdapters.ConfigTableAdapter confAdap = new DumpDataSetTableAdapters.ConfigTableAdapter();
            confAdap.Connection.ConnectionString = connString;
            confAdap.Update(ds.Config);

            DumpDataSetTableAdapters.FeatureLifeTableAdapter flAdap = new DumpDataSetTableAdapters.FeatureLifeTableAdapter();
            flAdap.Connection.ConnectionString = connString;
            flAdap.Update(ds.FeatureLife);

            DumpDataSetTableAdapters.HistoryCommentsTableAdapter hcAdap = new DumpDataSetTableAdapters.HistoryCommentsTableAdapter();
            hcAdap.Connection.ConnectionString = connString;
            hcAdap.Update(ds.HistoryComments);

            DumpDataSetTableAdapters.CycleTimeDetailTableAdapter ctAdap = new DumpDataSetTableAdapters.CycleTimeDetailTableAdapter();
            ctAdap.Connection.ConnectionString = connString;
            ctAdap.Update(ds.CycleTimeDetail);

            ds.AcceptChanges();

            Notify("creating user story cumulative data ...");
            UserStoryCumulativeFlow.CreateUserStoryCumulativeData(connect,ds,systemAreadId);
            UserStoryCumulativeFlow.CreateUserStoryCumulativeDataByDay(connect,ds,systemAreadId);
            Notify("saving user story cumulative data ...");
            DumpDataSetTableAdapters.UserStoryFlowTableAdapter usfAdap = new DumpDataSetTableAdapters.UserStoryFlowTableAdapter();
            usfAdap.Connection.ConnectionString = connString;
            usfAdap.Update(ds.UserStoryFlow);

            DumpDataSetTableAdapters.UserStoryFlow_ByDayTableAdapter usfdAdap = new DumpDataSetTableAdapters.UserStoryFlow_ByDayTableAdapter();
            usfdAdap.Connection.ConnectionString = connString;
            usfdAdap.Update(ds.UserStoryFlow_ByDay);

            ds.AcceptChanges();

        }

        private void AddMetrics_CycleTime(DumpDataSet ds)
        {
            DateTime earliestStartDev = GetFirstOrLastDate(new string[] { "User Story", "Bug" }, "Active", ds.WorkItemRevision, FirstOrLastEnum.First);
            DateTime latestStartDev = GetFirstOrLastDate(new string[] { "User Story", "Bug" }, "Active", ds.WorkItemRevision, FirstOrLastEnum.Last);

            if (earliestStartDev == DateTime.MinValue)
            {
                return;
            }

            List<int> workItems = new List<int>();

            foreach (DumpDataSet.WorkItemRevisionRow row in ds.WorkItemRevision.Rows)
            {
                if (!workItems.Contains(row.ID)) workItems.Add(row.ID);
            }

            if (workItems.Count == 0) return;

            List<DateTime> lastDaysOfWeeks = new List<DateTime>();
            DateTime processingDate = earliestStartDev.AddDays(-1);

            while (processingDate <= VSCommon.GetLastDayOfWeek(latestStartDev, VSCommon.LastDayOfWeek))
            {
                processingDate = processingDate.AddDays(1);
                DateTime lastDay = VSCommon.GetLastDayOfWeek(processingDate, lastDayOfWeek);
                if (!lastDaysOfWeeks.Contains(lastDay))
                {
                    lastDaysOfWeeks.Add(lastDay);
                    DumpDataSet.Metrics_CycleTimeRow row = ds.Metrics_CycleTime.NewMetrics_CycleTimeRow();
                    row.LastDayOfWeek = lastDay;
                    ds.Metrics_CycleTime.AddMetrics_CycleTimeRow(row);
                }
            }

            List<WItem> wItems = WorkItemUtil.GetWorkItemRevisions(ds.WorkItemRevision);

            List<WItemLife> wItemsLife = GetWorkItemsLife(wItems);
            SaveWorkItemsLifeToDB(wItemsLife, ds);

            CreateWorkItemMetrics(ds);

        }

        private void CreateWorkItemMetrics(DumpDataSet ds)
        {
            WorkItemMetrics metrics = new WorkItemMetrics();
            foreach (DumpDataSet.WorkItemLifeRow row in ds.WorkItemLife.Rows)
            {
                if (!row.IsSubmittedEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.SubmittedEnd, "Submitted", row.WorkItemType, row.SubmittedDays, 1);
                }
                if (!row.IsActiveEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.ActiveEnd, "Active", row.WorkItemType, row.ActiveDays, 1);
                }
                if (!row.IsGroomingEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.GroomingEnd, "Grooming", row.WorkItemType, row.GroomingDays, 1);
                }
                if (!row.IsGroomedEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.GroomedEnd, "Groomed", row.WorkItemType, row.GroomedDays, 1);
                }
                if (!row.IsReadyQAEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.ReadyQAEnd, "ReadyQA", row.WorkItemType, row.ReadyQADays, 1);
                }
                if (!row.IsQAEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.QAEnd, "QA", row.WorkItemType, row.QADays, 1);
                }
                if (!row.IsReadyUATEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.ReadyUATEnd, "ReadyUAT", row.WorkItemType, row.ReadyUATDays, 1);
                }
                if (!row.IsUATEndNull())
                {
                    metrics.AddSingleWorkItemMetric(row.UATEnd, "UAT", row.WorkItemType, row.UATDays, 1);
                }
            }
            if (metrics.GetMetrics().OrderBy(x => x.WeekEnding).Any())
            {
                metrics.FillInMissingWeeksWithZero(metrics.GetMetrics().OrderBy(x => x.WeekEnding).First().WeekEnding, "Active", "DevStory");
                metrics.FillInMissingWeeksWithZero(metrics.GetMetrics().OrderBy(x => x.WeekEnding).First().WeekEnding, "QA", "DevStory");
                metrics.FillInMissingWeeksWithZero(metrics.GetMetrics().OrderBy(x => x.WeekEnding).First().WeekEnding, "UAT", "DevStory");
                foreach (WorkItemMetric wim in metrics.GetMetrics())
                {
                    DumpDataSet.WorkItemMetricsRow row = ds.WorkItemMetrics.NewWorkItemMetricsRow();
                    row.WeekEnding = wim.WeekEnding;
                    row.AvgDaysInState = wim.AverageDaysInState;
                    row.State = wim.State;
                    row.StoryCount = wim.StoryCount;
                    row.WorkItemType = wim.WorkItemType;
                    ds.WorkItemMetrics.AddWorkItemMetricsRow(row);
                }
            }
        }

        private int GetParentId(DumpDataSet.LinksDataTable links, int childId)
        {
            int ret = 0;

            DataRow[] rows = links.Select("SourceWorkItemId = " + childId + " and LinkTypeEnd = 'Parent'");
            if (rows.Count() == 1)
            {
                ret = Convert.ToInt32(rows[0]["TargetWorkItemId"]);
            }

            return ret;
        }

        private void CreateWorkItemMetricsTask(DumpDataSet ds)
        {
            WorkItemMetricsTask metrics = new WorkItemMetricsTask();
            foreach (DumpDataSet.WorkItemLife_TaskRow row in ds.WorkItemLife_Task.Rows)
            {

                int parentId = GetParentId(ds.Links, row.ID);
                if (parentId <= 0) continue;

                row.ParentID = parentId;
                if (!row.IsNewEndNull())
                {
                    metrics.AddSingleWorkItemMetric(parentId, row.NewEnd, "New", row.WorkItemType, row.NewDays, 1);
                }
                if (!row.IsActiveEndNull())
                {
                    metrics.AddSingleWorkItemMetric(parentId, row.ActiveEnd, "Active", row.WorkItemType, row.ActiveDays, 1);
                }
            }

            foreach (int parentId in metrics.GetMetrics().Select(x => x.ParentID).Distinct().ToList())
            {
                metrics.FillInMissingWeeksWithZero(parentId, metrics.GetMetrics().OrderBy(x => x.WeekEnding).First().WeekEnding, "Active", "Task");
            }

            foreach (WorkItemMetricTask wim in metrics.GetMetrics())
            {
                DumpDataSet.WorkItemMetrics_TaskRow row = ds.WorkItemMetrics_Task.NewWorkItemMetrics_TaskRow();
                row.ParentID = wim.ParentID;
                row.WeekEnding = wim.WeekEnding;
                row.AvgDaysInState = wim.AverageDaysInState;
                row.State = wim.State;
                row.StoryCount = wim.StoryCount;
                row.WorkItemType = wim.WorkItemType;
                row.RemainingTasks = GetRemainingTasks(ds, wim.ParentID, wim.WeekEnding);
                ds.WorkItemMetrics_Task.AddWorkItemMetrics_TaskRow(row);
            }
        }

        private int GetRemainingTasks(DumpDataSet ds, int parentId, DateTime asOfDate)
        {

            int ret = 0;

            List<int> taskIds = new List<int>();

            DataRow[] linkRows = ds.Links.Select("TargetWorkItemId = " + parentId + " and LinkTypeEnd = 'Parent'");

            foreach (DataRow row in linkRows)
            {
                int itemId = Convert.ToInt32(row["SourceWorkItemId"]);
                if (!IsTask(ds, itemId)) continue;

                DateTime? latestClosedDate = GetLatestClosedDate(ds, itemId);
                DateTime earliestStateDate = GetEarliestStateDate(ds, itemId);

                if (earliestStateDate <= asOfDate.Date)
                {
                    if (latestClosedDate.HasValue)
                    {
                        if (latestClosedDate.Value.Date > asOfDate.Date)
                        {
                            ret += 1;
                        }
                    }
                    else
                    {
                        ret += 1;
                    }
                }

            }

            return ret;
        }

        private DateTime? GetLatestClosedDate(DumpDataSet ds, int itemId)
        {
            DateTime? ret = null;

            DataRow[] rows = ds.WorkItemRevision.Select("ID = " + itemId, "Rev desc");
            if (rows[0]["State"].ToString().ToLower() == "closed")
            {
                ret = Convert.ToDateTime(rows[0]["ChangedDate"]).Date;
            }

            return ret;
        }

        private DateTime GetEarliestStateDate(DumpDataSet ds, int itemId)
        {
            DataRow[] rows = ds.WorkItemRevision.Select("ID = " + itemId, "Rev asc");
            return Convert.ToDateTime(rows[0]["ChangedDate"]).Date;
        }


        private bool IsTask(DumpDataSet ds, int id)
        {
            bool ret = false;

            DataRow[] rows = ds.WorkItem.Select("ID = " + id);
            if (rows.Count() == 1)
            {
                string type = rows[0]["Type"].ToString().ToLower();
                if (type == "task") ret = true;
            }

            return ret;
        }

        private void SaveWorkItemsLifeTaskToDB(List<WItemLife_Task> items, DumpDataSet ds)
        {
            foreach (WItemLife_Task item in items)
            {
                DumpDataSet.WorkItemLife_TaskRow row = ds.WorkItemLife_Task.NewWorkItemLife_TaskRow();

                row.ID = item.ID;
                row.Title = item.Title;
                row.WorkItemType = item.WorkItemType;
                row.ParentID = item.ParentID;
                if (item.NewEnd.HasValue) row.NewEnd = item.NewEnd.Value;
                if (item.ActiveEnd.HasValue) row.ActiveEnd = item.ActiveEnd.Value;
                if (item.Closed.HasValue) row.Closed = item.Closed.Value;
                if (item.NewDays.HasValue) row.NewDays = item.NewDays.Value;
                if (item.ActiveDays.HasValue) row.ActiveDays = item.ActiveDays.Value;

                ds.WorkItemLife_Task.AddWorkItemLife_TaskRow(row);
            }

        }

        private void SaveFeaturesLifeToDB(List<FeatureLife> items, List<WItem> wItems, DumpDataSet ds)
        {
            if (items == null || items.Count == 0) return;


            //get earliest create date, then loop through everything for every week ending up until this week
            DateTime earliestCreateDate = items.OrderBy(x => x.CreateDate).First().CreateDate;
            DateTime processingDate = VSCommon.GetLastDayOfWeek(earliestCreateDate, VSCommon.LastDayOfWeek);
            DateTime latestProcessingDate = VSCommon.GetLastDayOfWeek(DateTime.Today, VSCommon.LastDayOfWeek);

            while (processingDate <= latestProcessingDate)
            {
                foreach (FeatureLife fl in items)
                {
                    if (fl.CreateDate > processingDate) continue;

                    DumpDataSet.FeatureLifeRow row = ds.FeatureLife.NewFeatureLifeRow();
                    row.ID = fl.ID;
                    row.Title = fl.Title;
                    row.WeekEnding = processingDate;

                    FeatureState state = GetFeatureState(fl.ID, wItems, processingDate);
                    if (state == FeatureState.Removed)
                    {
                        continue;
                    }
                    row.Backlog = 0;
                    row.Approved = 0;
                    row.Committed = 0;
                    row.InProgress = 0;
                    row.Done = 0;
                    row.NewBacklog = 0;
                    row.Backlog_HighValue = 0;
                    row.Analysis = 0;
                    row.Verification = 0;


                    if (state == FeatureState.New && (fl.BusinessPriority <= 0 || fl.BusinessPriority >= 200))
                    {
                        row.Backlog = 1;
                    }

                    if (state == FeatureState.New && (fl.BusinessPriority > 0 && fl.BusinessPriority < 200))
                    {
                        row.Backlog_HighValue = 1;
                    }

                    if (state == FeatureState.Approved) row.Approved = 1;
                    if (state == FeatureState.Analysis)
                    {
                        row.Analysis = 1;
                    }

                    if (state == FeatureState.Committed) row.Committed = 1;

                    if (state == FeatureState.InProgress) row.InProgress = 1;
                    if (state == FeatureState.Verification)
                    {
                        row.Verification = 1;
                    }


                    if (state == FeatureState.Done) row.Done = 1;


                    if (VSCommon.GetLastDayOfWeek(fl.CreateDate, VSCommon.LastDayOfWeek) == processingDate)
                    {
                        row.NewBacklog = 1;
                    }

                    ds.FeatureLife.AddFeatureLifeRow(row);
                }
                processingDate = processingDate.AddDays(7);
            }
        }

        private FeatureState GetFeatureState(int featureId, List<WItem> items, DateTime procDate)
        {
            //go with 11:59:59 PM on procDate
            DateTime date = procDate.AddDays(1).AddMilliseconds(-1);

            List<WItem> featureRevisions = items.Where(x => x.ID == featureId && x.ChangedDate <= date).OrderByDescending(y => y.ChangedDate).ToList();

            string latestState = string.Empty;
            if (featureRevisions.Count() >= 1)
            {
                latestState = featureRevisions.First().State;
            }
            if (latestState == "New") return FeatureState.New;
            if (latestState == "Approved") return FeatureState.Approved;
            if (latestState == "Committed") return FeatureState.Committed;
            if (latestState == "In Progress") return FeatureState.InProgress;
            if (latestState == "Done") return FeatureState.Done;
            if (latestState == "Removed") return FeatureState.Removed;
            if (latestState == "Analysis") return FeatureState.Analysis;
            if (latestState == "Verification") return FeatureState.Verification;


            return FeatureState.None;

        }

        private void SaveWorkItemsLifeToDB(List<WItemLife> items, DumpDataSet ds)
        {
            foreach (WItemLife item in items)
            {
                DumpDataSet.WorkItemLifeRow row = ds.WorkItemLife.NewWorkItemLifeRow();

                row.ID = item.ID;
                row.Title = item.Title;
                row.WorkItemType = item.WorkItemType;

                if (item.SubmittedEnd.HasValue) row.SubmittedEnd = item.SubmittedEnd.Value;
                if (item.ActiveEnd.HasValue) row.ActiveEnd = item.ActiveEnd.Value;
                if (item.GroomingEnd.HasValue) row.GroomingEnd = item.GroomingEnd.Value;
                if (item.GroomedEnd.HasValue) row.GroomedEnd = item.GroomedEnd.Value;
                if (item.ReadyQAEnd.HasValue) row.ReadyQAEnd = item.ReadyQAEnd.Value;
                if (item.QAEnd.HasValue) row.QAEnd = item.QAEnd.Value;
                if (item.ReadyUATEnd.HasValue) row.ReadyUATEnd = item.ReadyUATEnd.Value;
                if (item.UATEnd.HasValue) row.UATEnd = item.UATEnd.Value;
                if (item.Closed.HasValue) row.Closed = item.Closed.Value;

                if (item.SubmittedDays.HasValue) row.SubmittedDays = item.SubmittedDays.Value;
                if (item.GroomingDays.HasValue) row.GroomingDays = item.GroomingDays.Value;
                if (item.GroomedDays.HasValue) row.GroomedDays = item.GroomedDays.Value;
                if (item.ActiveDays.HasValue) row.ActiveDays = item.ActiveDays.Value;
                if (item.ReadyQADays.HasValue) row.ReadyQADays = item.ReadyQADays.Value;
                if (item.QADays.HasValue) row.QADays = item.QADays.Value;
                if (item.ReadyUATDays.HasValue) row.ReadyUATDays = item.ReadyUATDays.Value;
                if (item.UATDays.HasValue) row.UATDays = item.UATDays.Value;


                ds.WorkItemLife.AddWorkItemLifeRow(row);
            }
        }

        private List<FeatureLife> GetFeatureItemsLife(List<WItem> workItemRevisions)
        {
            List<FeatureLife> ret = new List<FeatureLife>();

            workItemRevisions = workItemRevisions.Where(x => x.WorkItemType == "Feature").ToList();

            List<int> featureIds = workItemRevisions.Select(x => x.ID).Distinct().ToList();

            foreach (WItem item in workItemRevisions)
            {
                if (ret.Any(x => x.ID == item.ID)) continue;


                List<WItem> items = workItemRevisions.Where(x => x.ID == item.ID).OrderBy(x => x.Rev).ToList();
                WItem latestRev = items.Last();

                if (latestRev.State == "Removed") continue;

                FeatureLife featureLife = new FeatureLife();
                featureLife.ID = item.ID;
                featureLife.Title = latestRev.Title;
                featureLife.CreateDate = latestRev.CreateDate;
                featureLife.DoneDate = GetEndStateFeature(items, "Done");
                featureLife.EndNewDate = GetEndStateFeature(items, "New");
                featureLife.EndInProgressDate = GetEndStateFeature(items, "In Progress");
                featureLife.EndApproved = GetEndStateFeature(items, "Approved");
                featureLife.EndCommitted = GetEndStateFeature(items, "Committed");
                featureLife.EndAnalysis = GetEndStateFeature(items, "Analysis");
                featureLife.EndVerification = GetEndStateFeature(items, "Verification");
                featureLife.BusinessPriority = latestRev.BusinessPriority;

                ret.Add(featureLife);
            }


            return ret;
        }

        private List<WItemLife> GetWorkItemsLife(List<WItem> workItemRevisions)
        {

            List<WItemLife> ret = new List<WItemLife>();

            foreach (var wItem in workItemRevisions)
            {
                if (ret.Any(x => x.ID == wItem.ID)) continue;
                if (wItem.WorkItemType != "User Story" && wItem.WorkItemType != "Bug") continue;

                var revisions = workItemRevisions.Where(x => x.ID == wItem.ID).OrderBy(x => x.Rev).ToList();


                //TODO:  TFSSTateManger Fix -- the hardcoded numbers below won't work.

                WItemLife life = new WItemLife();
                life.ID = wItem.ID;
                life.WorkItemType = wItem.WorkItemType;
                life.Title = wItem.Title;
                life.BusinessPriority = wItem.BusinessPriority;
                life.SubmittedEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 1);
                life.ActiveEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 2);
                life.GroomedEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 1);
                life.GroomingEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 1);
                life.ReadyQAEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 3);
                life.QAEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 4);
                life.ReadyUATEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 5);
                life.UATEnd = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, 6);
                life.Closed = StaticUtil.CurrentFuzzFile.GetEndDate(revisions, StaticUtil.CurrentFuzzFile.MaxStateIndex);

                life.SubmittedDays = GetDaysInState(revisions, "New");
                life.GroomingDays = GetDaysInState(revisions, "Grooming");
                life.GroomedDays = GetDaysInState(revisions, "Groomed");
                life.ActiveDays = GetDaysInState(revisions, "Active");
                life.ReadyQADays = GetDaysInState(revisions, "Ready QA");
                life.QADays = GetDaysInState(revisions, "QA");
                life.ReadyUATDays = GetDaysInState(revisions, "Ready UAT");
                life.UATDays = GetDaysInState(revisions, "UAT");

                ret.Add(life);

            }



            return ret;
        }

        private List<WItemLife_Task> GetWorkItemsLifeTask(DumpDataSet ds, List<WItem> workItemRevisions)
        {
            List<WItemLife_Task> ret = new List<WItemLife_Task>();

            foreach (WItem item in workItemRevisions)
            {

                if (item.WorkItemType != "Task") continue;
                if (ret.Any(x => x.ID == item.ID)) continue;

                int parentId = GetParentId(ds.Links, item.ID);
                if (parentId <= 0) continue;

                List<WItem> items = workItemRevisions.Where(x => x.ID == item.ID).OrderBy(x => x.Rev).ToList();

                WItemLife_Task life = new WItemLife_Task();

                life.ID = item.ID;
                life.ParentID = parentId;
                life.WorkItemType = item.WorkItemType;
                life.Title = item.Title;
//                life.NewEnd = GetEndState(items, "New");
//                life.ActiveEnd = GetEndState(items, "Active");
//                life.Closed = GetEndState(items, "Closed");
                life.NewDays = GetDaysInState(items, "New");
                life.ActiveDays = GetDaysInState(items, "Active");

                ret.Add(life);
            }

            return ret;
        }


        public static int? GetDaysInState(List<WItem> items, string state)
        {
            //TODO:  Make this work with WEgmans states

            //find each occurrence of state, record weekdays from state to next state
            //if state is last state, then add days from statechange to today
            //ignore any states before removed
            //exclude weekends
            SortedList<DateTime, DateTime?> dates = new SortedList<DateTime, DateTime?>();
            foreach (WItem item in items.OrderBy(x => x.Rev).ToList())
            {
                if (item.State == state && !dates.ContainsKey(item.ChangedDate))
                {
                    dates.Add(item.ChangedDate, null);
                }
            }

            List<DateTime> stateChangeDates = items.OrderBy(x => x.ChangedDate).Select(x => x.ChangedDate).Distinct().ToList();

            for (int i = 0; i < dates.Count; i++)
            {
                if (stateChangeDates.IndexOf(dates.Keys[i]) < stateChangeDates.Count - 1)
                {
                    dates[dates.Keys[i]] = stateChangeDates[stateChangeDates.IndexOf(dates.Keys[i]) + 1];
                }
                else
                {
                    dates[dates.Keys[i]] = DateTime.Now;
                }

            }

            double days = 0;

            foreach (KeyValuePair<DateTime, DateTime?> kvp in dates)
            {
                if (kvp.Key.Date == kvp.Value.Value.Date)
                {
                    TimeSpan ts = kvp.Value.Value.Date.Subtract(kvp.Key.Date);
                    if (Math.Abs(ts.TotalHours) > 4)
                    {
                        days += 1;
                    }
                }
                else
                {
                    days += VSCommon.GetBusinessDays(kvp.Key.Date, kvp.Value.Value.Date);
                }

            }

            return Convert.ToInt32(Math.Round(days, 0, MidpointRounding.AwayFromZero));
        }

//        public static DateTime? GetEndState(List<WItem> items, string state)
//        {
//            List<KanbanItemState> kanbanStates = new List<KanbanItemState>();
//            kanbanStates.Add(new KanbanItemState("Removed","Removed","",false,0));
//
//            kanbanStates.Add(new KanbanItemState("New","New","",false,1));
//            kanbanStates.Add(new KanbanItemState("New", "New","Design Backlog",false,1));
//            kanbanStates.Add(new KanbanItemState("New", "New","Design Backlog",true,1));
//            kanbanStates.Add(new KanbanItemState("New", "New","Design",false,1));
//            kanbanStates.Add(new KanbanItemState("New", "New","Design",true,1));
//
//            kanbanStates.Add(new KanbanItemState("Grooming", "New","Story Authoring",false,2)); //grooming
//            kanbanStates.Add(new KanbanItemState("Grooming", "New","Story Authoring",true,2)); //grooming
//
//            kanbanStates.Add(new KanbanItemState("Groomed", "New", "Ready For Dev",false,3)); //groomed
//            kanbanStates.Add(new KanbanItemState("Groomed", "New", "Ready For Dev",true,3)); //groomed
//
//            kanbanStates.Add(new KanbanItemState("Active", "Active","",false,4)); //in development
//            kanbanStates.Add(new KanbanItemState("Active", "Active","Development",false,4)); //in development
//
//            kanbanStates.Add(new KanbanItemState("Ready QA", "Active","Development",true,5)); //ready for asynchrony QA
//
//            kanbanStates.Add(new KanbanItemState("QA","Active","Asynchrony QA",false,6)); //Asynchrony QA
//
//            kanbanStates.Add(new KanbanItemState("Ready UAT","Active","Asynchrony QA",true,7)); //Ready UAT
//
//            kanbanStates.Add(new KanbanItemState("UAT","Active","Wegmans QA",false,8)); //UAT
//            kanbanStates.Add(new KanbanItemState("UAT", "Active","Wegmans QA",true,8)); //UAT
//            kanbanStates.Add(new KanbanItemState("UAT", "Resolved","PO Review",false,8)); //UAT
//            kanbanStates.Add(new KanbanItemState("UAT", "Resolved","PO Review",true,8)); //UAT
//            kanbanStates.Add(new KanbanItemState("UAT", "Resolved","Alpha Channel Smoke Test",false,8)); //UAT
//            kanbanStates.Add(new KanbanItemState("UAT", "Resolved","Alpha Channel Smoke Test",true,8)); //UAT
//
//            kanbanStates.Add(new KanbanItemState("Closed","Closed","",false,9)); //UAT
//
//
////            SortedList<string, int> states = new SortedList<string, int>();
////            states.Add("Removed", 0);
////            states.Add("New", 1);
////            states.Add("Submitted", 1);
////            states.Add("Grooming", 2);
////            states.Add("Groomed", 3);
////            states.Add("Active", 4);
////            states.Add("Ready for QA", 5);
////            states.Add("QA", 6);
////            states.Add("Ready for UAT", 7);
////            states.Add("UAT", 8);
////            states.Add("Resolved", 9);
////            states.Add("Closed", 10);
//            
//
//            DateTime? ret = null;
//
//            KanbanItemState currentKanbanState = null;
//
//            string currentState = string.Empty;
//
//            items = items.OrderBy(x => x.Rev).ToList();
//
//            
//
//
//            currentState = items.Last().State;
//            int currentStateValue = states.ContainsKey(currentState) ? states[currentState] : -1;
//
//            //find the last item in state, then next state changed date is end
//            for (int i = items.Count - 1; i >= 0; i--)
//            {
//                if (items[i].State == "Removed") break;
//
//                if (items[i].State == state)
//                {
//                    if (i == items.Count - 1 && (items[i].State == "Closed" || items[i].State == "Done"))
//                    {
//                        ret = items[i].ChangedDate;
//                        break;
//                    }
//                    if (i < items.Count - 1 && states[items[i + 1].State] > states[items[i].State])
//                    {
//                        //make sure current state is not less than states[items[i].State
//                        if (currentStateValue > states[items[i].State])
//                        {
//                            ret = items[i + 1].ChangedDate;
//                            break;
//                        }
//                    }
//                }
//            }
//
//            if (!ret.HasValue && state == "UAT")
//            {
//                ret = GetEndState(items, "Closed");
//            }
//
//
//
//            return ret;
//        }



        private DateTime? GetEndStateFeature(List<WItem> items, string state)
        {
            SortedList<string, int> states = new SortedList<string, int>();
            states.Add("Removed", 0);
            states.Add("New", 1);
            states.Add("Submitted", 1);
            states.Add("Analysis", 2);
            states.Add("Approved", 3);
            states.Add("Committed", 4);
            states.Add("In Progress", 5);
            states.Add("Active", 5);
            states.Add("Verification", 6);
            states.Add("Resolved", 7);
            states.Add("Closed", 8);
            states.Add("Done", 9);

            DateTime? ret = null;

            items = items.OrderBy(x => x.Rev).ToList();
            //find the last item in state, then next state changed date is end
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].State == "Removed") break;

                if (!states.Keys.Contains(items[i].State))
                {
                    throw new Exception(string.Format("DumpWorkItemsToDS.GetEndStateFeature is missing a definition for the {0} state.", items[i].State));
                }

                if (items[i].State == state)
                {
                    if (i == items.Count - 1 && (items[i].State == "Done"))
                    {
                        ret = items[i].ChangedDate;
                        break;
                    }
                    if (i < items.Count - 1 && states[items[i + 1].State] > states[items[i].State])
                    {
                        ret = items[i + 1].ChangedDate;
                        break;
                    }
                }
            }

            if (ret.HasValue) ret = VSCommon.GetLastDayOfWeek(ret.Value, VSCommon.LastDayOfWeek);
            return ret;
        }




        private DateTime GetFirstOrLastDate(string[] workItemTypes, string state, DumpDataSet.WorkItemRevisionDataTable table, FirstOrLastEnum firstOrLast)
        {
            DateTime ret = DateTime.MinValue;

            string filter = string.Join(",", workItemTypes);
            filter = "WorkItemType In ('" + filter.Replace(",", "','") + "')";


            DataView view = new DataView(table, filter, "ChangedDate " + (firstOrLast == FirstOrLastEnum.First ? "ASC" : "DESC"), DataViewRowState.CurrentRows);

            if (view.Count > 0)
            {
                ret = Convert.ToDateTime(view[0]["ChangedDate"]);
            }

            return ret;
        }

        private void ReorderStackRank()
        {
            DataView wiView = new DataView(ds.WorkItem);
            wiView.Sort = "StackRank ASC";

            double counter = 0;

            foreach (DataRowView row in wiView)
            {
                counter += 1;
                row["StackRank"] = counter;
            }

            wiView = new DataView(ds.WorkItemRevision);
            wiView.Sort = "StackRank ASC";

            counter = 0;

            foreach (DataRowView row in wiView)
            {
                counter += 1;
                row["StackRank"] = counter;
            }

        }

        private void AddHistoryComments(WorkItem wi)
        {
            List<Revision> revs = wi.Revisions.Cast<Revision>().ToList();

            foreach (var rev in revs)
            {
                Field historyField = rev.Fields.Cast<Field>().ToList().Where(x => x.Name == "History").FirstOrDefault();
                if (historyField != null)
                {
                    if (historyField.Value != null && !string.IsNullOrEmpty(historyField.Value.ToString()))
                    {
                        string comment = Regex.Replace(historyField.Value.ToString(), "<.*?>", "");
                        comment = comment.Replace("&nbsp", " ");

                        ds.HistoryComments.AddHistoryCommentsRow(wi.Id, Convert.ToDateTime(rev.Fields["Changed Date"].Value.ToString()), comment);
                    }
                }
            }
        }

        private void AddWorkItem(WorkItem wi)
        {

            DumpDataSet.WorkItemRow row = ds.WorkItem.NewWorkItemRow();
            row.ID = wi.Id;
            row.Rev = wi.Rev;
            row.Type = wi.Type.Name;
            row.AreaId = wi.AreaId;
            row.AreaPath = wi.AreaPath;
            row.AttachedFileCount = wi.AttachedFileCount;
            row.AuthorizedDate = wi.AuthorizedDate;
            row.ChangedBy = wi.ChangedBy;
            row.ChangedDate = wi.ChangedDate;
            row.RevisedDate = wi.RevisedDate;
            if (wi.RevisedDate > DateTime.Today.AddYears(1))
            {
                row.RevisedDate = row.ChangedDate;
            }
            row.CreatedBy = wi.CreatedBy;
            row.CreatedDate = wi.CreatedDate;
            row.Description = wi.Description;
            row.Reason = wi.Reason;
            row.State = wi.State;
            row.Tags = wi.Tags;
            row.Title = wi.Title;
            row.Uri = wi.Uri.OriginalString;
            row.Project = wi.Project.Name;
            row.Area = wi.AreaPath;
            SetRowValue(row, "TargetRelease", wi.Fields, "TargetRelease");
            SetRowValue(row, "StackRank", wi.Fields, "Stack Rank");
            SetRowValue(row, "ChangedDate", wi.Fields, "State Change Date");
            SetRowValue(row, "BusinessPriority", wi.Fields, "Business Priority", 0);
            SetRowValue(row, "BoardLane", wi.Fields, "Board Lane");
            SetRowValue(row, "BoardColumnDone", wi.Fields, "Board Column Done");
            SetRowValue(row, "BoardColumn", wi.Fields, "Board Column");
            SetRowValue(row, "Priority", wi.Fields, "Priority");
            SetRowValue(row, "Severity", wi.Fields, "Severity");
            
            ds.WorkItem.AddWorkItemRow(row);
        }


        private void AddWorkItemRevisions(WorkItem wi)
        {

            foreach (Revision rev in wi.Revisions)
            {
                AddWorkItemRevision(rev, wi);
            }
        }

        private void AddWorkItemRevision(Revision rev, WorkItem wi)
        {
            DumpDataSet.WorkItemRevisionRow row = ds.WorkItemRevision.NewWorkItemRevisionRow();

            SetRowValue(row, "ID", rev.Fields, "ID");
            SetRowValue(row, "Rev", rev.Fields, "Rev");

            row.WorkItemType = wi.Type.Name;
//            SetRowValue(row, "WorkItemType", rev.Fields, "Work Item Type");

            SetRowValue(row, "Title", rev.Fields, "Title");
            SetRowValue(row, "State", rev.Fields, "State");
            SetRowValue(row, "TargetRelease", rev.Fields, "Target Release");
            SetRowValue(row, "CreatedBy", rev.Fields, "Created By");
            SetRowValue(row, "CreatedDate", rev.Fields, "Created Date");
            SetRowValue(row, "ChangedBy", rev.Fields, "Changed By");
            SetRowValue(row, "ChangedDate", rev.Fields, "Changed Date");
            SetRowValue(row, "RevisedDate", rev.Fields, "Revised Date");
            SetRowValue(row, "AssignedTo", rev.Fields, "Assigned To");
            SetRowValue(row, "Tags", rev.Fields, "Tags");
            SetRowValue(row, "ClosedBy", rev.Fields, "Closed By");
            SetRowValue(row, "ClosedDate", rev.Fields, "Closed Date");
            SetRowValue(row, "AttachedFileCount", rev.Fields, "Attached File Count");
            SetRowValue(row, "Priority", rev.Fields, "Priority");
            SetRowValue(row, "Severity", rev.Fields, "Severity");
            SetRowValue(row, "BusinessPriority", rev.Fields, "Business Priority", 0);
            SetRowValue(row, "BusinessValue", rev.Fields, "Business Value");
            SetRowValue(row, "StackRank", rev.Fields, "Stack Rank");

            SetRowValue(row, "BoardLane", rev.Fields, "Board Lane");
            SetRowValue(row, "BoardColumnDone", rev.Fields, "Board Column Done");
            SetRowValue(row, "BoardColumn", rev.Fields, "Board Column");

            if (row.RevisedDate > DateTime.Today.AddYears(1))
            {
                row.RevisedDate = row.ChangedDate;
            }

            ds.WorkItemRevision.AddWorkItemRevisionRow(row);
        }




        private void AddAttachments(WorkItem wi)
        {
            foreach (Attachment a in wi.Attachments)
            {
                DumpDataSet.AttachmentRow row = ds.Attachment.NewAttachmentRow();

                row.AttachedTime = a.AttachedTime;
                row.AttachmentName = a.Name;
                row.Comment = a.Comment;
                row.Extension = a.Extension;
                row.Id = a.Id;
                row.Path = a.Uri.OriginalString;
                row.WorkItemID = wi.Id;

                ds.Attachment.AddAttachmentRow(row);
            }
        }

        private void AddWorkItemLinks(WorkItem wi)
        {
            foreach (WorkItemLink link in wi.WorkItemLinks)
            {
                DumpDataSet.LinksRow row = ds.Links.NewLinksRow();

                row.AddedBy = link.AddedBy;
                row.AddedDate = link.AddedDate;
                row.Comment = link.Comment;
                row.LinkTypeEnd = link.LinkTypeEnd.Name;
                row.RemovedBy = link.RemovedBy;
                row.RemovedDate = link.RemovedDate;
                row.SourceWorkItemId = link.SourceId;
                row.TargetWorkItemId = link.TargetId;

                ds.Links.AddLinksRow(row);
            }
        }


        private void SetRowValue(DataRow row, string colName, FieldCollection fields, string fieldName, object defaultValue)
        {
            dynamic value = GetFieldValue(fields, fieldName);
            if (value != null)
            {
                row[colName] = value;
            }
            if (defaultValue != null && value == null)
            {
                row[colName] = defaultValue;
            }
        }

        private void SetRowValue(DataRow row, string colName, FieldCollection fields, string fieldName)
        {
            try
            {
                dynamic value = GetFieldValue(fields, fieldName);

                if (value != null)
                {
                    row[colName] = value;
                }
            }
            catch (Exception ex)
            {
                string x = "";
            }
        }

        private dynamic GetFieldValue(FieldCollection fields, string fieldName)
        {
            if (fields.Contains(fieldName))
            {
                return fields[fieldName].Value;
            }
            return null;
        }
    }
}