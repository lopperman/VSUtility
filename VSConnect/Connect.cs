using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace VSConnect
{
    public class Connect
    {
        Uri tfsUri = null;
        TfsTeamProjectCollection tfs = null;

        public Connect(Uri vsUri, string userName, string password, string domain)
        {
            tfsUri = vsUri;

            

            NetworkCredential cred = new NetworkCredential(userName, password, domain);
            ProjectCollection.Credentials = cred;
        }

        public TfsTeamProjectCollection ProjectCollection
        {
            get
            {
                if (tfs == null)
                {
                    tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
                }

                return tfs;
            }
        }


        private List<Project> Projects()
        {
            return GetService<WorkItemStore>()
                .Projects.Cast<Project>().ToList();
        }


        public Project GetProject(string projectName)
        {
            return Projects()
                .Where(x => x.Name == projectName).FirstOrDefault();
        }


        public List<TfsArea> GetTeamAreas(Project project)
        {
            List<TfsArea> areas = new List<TfsArea>();

            foreach (Node node in project.AreaRootNodes)
            {
                areas.AddRange(GetNodeNamesAndIds(node));
            }

            return areas;
        }

        public List<TfsArea> GetNodeNamesAndIds(Node node)
        {
            List<TfsArea> areas = new List<TfsArea>();

            if (node.IsAreaNode)
            {
                areas.Add(new TfsArea(node.Id, node.Name));
            }

            if (node.HasChildNodes)
            {
                foreach (Node child in node.ChildNodes)
                {
                    areas.AddRange(GetNodeNamesAndIds(child));
                }
            }

            return areas;
        }

        public ProjectInfo[] GetTFSProjectInfo()
        {
            ICommonStructureService svc = ProjectCollection.GetService<ICommonStructureService>();

            return svc.ListAllProjects();
        }

        public List<WorkItemRevisionHistory> GetRevisionHistory(int workItemId)
        {
            List<WorkItemRevisionHistory> ret = new List<WorkItemRevisionHistory>();

            WorkItem wi = GetWorkItem(workItemId);

            foreach (Revision rev in wi.Revisions)
            {
                foreach (Field f in rev.Fields)
                {
                    if (f.IsChangedInRevision)
                    {
                        var wirh = new WorkItemRevisionHistory();
                        wirh.ChangedDt = DateTime.Parse(rev.Fields["Changed Date"].Value.ToString());
                        wirh.RevisedDt = DateTime.Parse(rev.Fields["Revised Date"].Value.ToString());
                        wirh.FieldName = f.Name;
                        wirh.Id = workItemId;
                        wirh.NewValue = f.Value;
                        wirh.Revision = rev.Index + 1;
                        if (rev.Index > 0)
                        {
                            wirh.OldValue = wi.Revisions[rev.Index - 1].Fields[f.Name].Value;
                        }
                        ret.Add(wirh);
                    }
                }
            }

            return ret;
        }

        public List<WorkItemType> GetWorkItemTypes(Project proj)
        {
            return proj.WorkItemTypes.Cast<WorkItemType>().ToList();
        }

        public List<WorkItem> ExecuteWorkItemWIQL(string wiql)
        {

            WorkItemStore store = GetService<WorkItemStore>();

            if (store == null) throw new Exception("Unable to get workitemstore");

            return store.Query(wiql).Cast<WorkItem>().ToList();
        }

        public WorkItem GetWorkItem(int id)
        {
            return GetService<WorkItemStore>().GetWorkItem(id);
        }

        public WorkItemState GetWorkItemStateSummary(WorkItem workItem, string stateValue)
        {
            WorkItemState ret = new WorkItemState();

            ret.State = stateValue;
            ret.WorkItemId = workItem.Id;
            ret.FirstDate = GetFirstDate(workItem, "State", stateValue);
            ret.LastDate = GetLastDate(workItem, "State", stateValue);
            ret.Count = GetStateCount(workItem, "State", stateValue);

            return ret;
        }



        private int GetStateCount(WorkItem wi, string fieldName, string fieldValue)
        {
            int ret = 0;

            List<DateTime> stateChangedDates = new List<DateTime>();

            foreach (Revision rev in wi.Revisions)
            {
                List<Field> fields = rev.Fields.Cast<Field>().ToList();

                if (fields.Any(x => x.Name == fieldName && x.Value != null && x.Value.ToString() == fieldValue))
                {
                    if (fields.Any(x => x.Name == "State Change Date"))
                    {
                        Field field = fields.First(x => x.Name == "State Change Date");
                        if (field.Value != null && !stateChangedDates.Contains(Convert.ToDateTime(field.Value.ToString())))
                        {
                            stateChangedDates.Add(Convert.ToDateTime(field.Value.ToString()));
                            ret += 1;
                        }
                    }
                }
            }

            return ret;
        }

        public List<WorkItem> GetLinkedFeatureStories(int parentId)
        {
            string sql = string.Format(@"SELECT [System.Id], [System.State], [System.WorkItemType] 
FROM WorkItemLinks 
WHERE 
(
    Source.[System.Id] = {0}  
) 
AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' 
AND Target.[System.WorkItemType] <> '' 
ORDER BY [System.Id] mode(Recursive)", parentId);

            List<WorkItemLinkInfo> query1 = new Query(GetWorkItemStore(), sql).RunLinkQuery().ToList();

            List<WorkItem> workItems = new List<WorkItem>();

            foreach (WorkItemLinkInfo wili in query1)
            {
                WorkItem wi = GetWorkItem(wili.TargetId);
                if (wi.Type.Name == "User Story" || wi.Type.Name == "Technical Story" || wi.Type.Name == "Bug")
                {
                    workItems.Add(wi);
                }
            }

            return workItems;
        }

        public List<WorkItem> GetWorkItemTasks(int parentId)
        {
            string sql = string.Format(@"SELECT [System.Id], [System.State], [System.WorkItemType] 
FROM WorkItemLinks 
WHERE 
(
    Source.[System.Id] = {0}  
) 
AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward' 
AND Target.[System.WorkItemType] <> '' 
ORDER BY [System.Id] mode(Recursive)", parentId);

            List<WorkItemLinkInfo> query1 = new Query(GetWorkItemStore(), sql).RunLinkQuery().ToList();

            List<WorkItem> workItems = new List<WorkItem>();

            foreach (WorkItemLinkInfo wili in query1)
            {
                WorkItem wi = GetWorkItem(wili.TargetId);
                if (wi.Type.Name == "Task")
                {
                    workItems.Add(wi);
                }
            }

            return workItems;
        }

        private DateTime? GetFirstDate(WorkItem wi, string fieldName, string fieldValue)
        {
            DateTime? ret = DateTime.MaxValue;

            foreach (Revision rev in wi.Revisions)
            {
                List<Field> fields = rev.Fields.Cast<Field>().ToList();

                if (fields.Any(x => x.Name == fieldName && x.Value != null && x.Value.ToString() == fieldValue))
                {
                    DateTime? revDt = DateTime.MinValue;
                    if (rev.Fields.Contains("State Change Date"))
                    {
                        revDt = rev.Fields["State Change Date"].Value as DateTime?;
                        if (revDt != null && revDt < ret.Value)
                        {
                            ret = revDt;
                        }
                    }

                }
            }

            if (ret.Value == DateTime.MaxValue)
            {
                ret = null;
            }

            return ret;
        }

        private DateTime? GetLastDate(WorkItem wi, string fieldName, string fieldValue)
        {
            DateTime? ret = DateTime.MinValue;

            foreach (Revision rev in wi.Revisions)
            {
                List<Field> fields = rev.Fields.Cast<Field>().ToList();

                if (fields.Any(x => x.Name == fieldName && x.Value != null && x.Value.ToString() == fieldValue))
                {
                    DateTime? revDt = DateTime.MinValue;
                    if (rev.Fields.Contains("State Change Date"))
                    {
                        revDt = rev.Fields["State Change Date"].Value as DateTime?;
                        if (revDt != null && revDt > ret.Value)
                        {
                            ret = revDt;
                        }
                    }

                }
            }

            if (ret.Value == DateTime.MinValue)
            {
                ret = null;
            }

            return ret;
        }

        public List<TfsProject> GetTfsProjects()
        {
            List<TfsProject> projs = new List<TfsProject>();

            foreach (Project p in this.GetWorkItemStore().Projects)
            {
                projs.Add(new TfsProject(p.Id, p.Name));
            }

            return projs.OrderBy(x => x.Name).ToList();
        }

        public WorkItemStore GetWorkItemStore()
        {
            return GetService<WorkItemStore>();
        }

        public T GetService<T>() where T : ITfsTeamProjectCollectionObject
        {
            return ProjectCollection.GetService<T>();
        }


        public void testtest()
        {
            
        }

    }
}
