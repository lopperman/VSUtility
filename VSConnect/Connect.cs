using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.ProcessConfiguration.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Proxy;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Field = Microsoft.TeamFoundation.WorkItemTracking.Client.Field;
using ProjectInfo = Microsoft.TeamFoundation.Server.ProjectInfo;
using WindowsCredential = Microsoft.TeamFoundation.Client.WindowsCredential;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;
using WorkItemType = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType;


namespace VSConnect
{
    public class Connect
    {
        Uri tfsUri = null;
        TfsTeamProjectCollection tfs = null;
        private VssConnection vssConn = null;

        public Connect(Uri vsUri, string userName, string password, string domain)
        {
            tfsUri = vsUri;

            this.UserName = userName;
            this.Password = password;
            this.Domain = domain;
            

            NetworkCredential cred = new NetworkCredential(userName, password, domain);
            ProjectCollection.Credentials = cred;
        }

        public Connect(Uri vsUri, string userName, string vstsToken)
        {

            tfsUri = vsUri;
            this.UserName = userName;

            VssClientCredentials clientCredentials = new VssClientCredentials(new VssBasicCredential(userName, vstsToken));
            //ProjectCollection.Credentials = clientCredentials;
            VssConnection vssConnection = new VssConnection(vsUri, clientCredentials);
            vssConn = vssConnection;
            

            //   tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);

//            tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
//            tfs.EnsureAuthenticated();

        }


        public string Domain
        { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

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

        public List<TeamFoundationTeam> GetTeams(Project project)
        {
            TfsTeamService _teamService = tfs.GetService<TfsTeamService>();
            IEnumerable<TeamFoundationTeam> _teams = _teamService.QueryTeams(project.Uri.ToString());

        

            return _teams.ToList();

        }


        public List<TfsArea> GetTeamAreas(TeamFoundationTeam team, Project project)
        {
            List<string> ret = new List<string>();

            var _teamConfig = tfs.GetService<TeamSettingsConfigurationService>();

            var _configs = _teamConfig.GetTeamConfigurations(new Guid[] { team.Identity.TeamFoundationId });

            foreach (var _config in _configs)
            {
                foreach (var _area in _config.TeamSettings.TeamFieldValues)
                {
                    ret.Add(_area.Value);                    
                }
            }

            //get all areas for project, so we can return areas with ids
            List<TfsArea> areas = new List<TfsArea>();

            foreach (var a in this.GetTeamAreas(project))
            {
                if (ret.Contains(string.Format("{0}\\{1}",project.Name,a.Name)))
                {
                    areas.Add(new TfsArea(a.Id,a.Name));
                }
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

        public List<string> QueryForSingleListOfString(string wql, string fieldName)
        {
            var ret = new List<string>();

            var items = ExecuteWorkItemWIQL(wql);


            foreach (var item in items)
            {
                if (item.Fields.Contains(fieldName))
                {
                    if (ret.All(x => x != item.Fields[fieldName].Value))
                    {
                        ret.Add(item.IterationPath);
                    }
                }
            }
            

            return ret;
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
