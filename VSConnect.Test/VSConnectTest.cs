﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Proxy;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common.Internal;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using NUnit.Framework;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem;

namespace VSConnect.Test
{
    [TestFixture]
    public class VSConnectTest
    {
        Uri tfsUri = new Uri("https://wegmans.visualstudio.com/");
        Connect connect = null;

        [OneTimeSetUp]
        public void setup()
        {
            connect = new Connect(tfsUri, "", "","");
        }

        [OneTimeTearDown]
        public void teardown()
        {
            connect = null;
        }

        [Test]
        public void testConnect()
        {
        }

        [Test]
        public void checkWorkItem()
        {
            var item = connect.GetWorkItem(117385);

            var project = connect.GetProject("Marketing Temp");

            var areas = connect.GetTeamAreas(project);

            var info = connect.GetTFSProjectInfo();

            string x = "";
        }

        [Test]
        public void GetRevisionHistory()
        {
            var ret = connect.GetRevisionHistory(117530);

            string x = "";
        }

        [Test]
        public void GetTemporalData()
        {
            //            string date = "3/13/2018";
            //            string query1 = string.Format("SELECT * FROM WorkItems WHERE (System.CreatedDate >= '{0}') or (System.ChangedDate >= '{0}')", date);

            string query1 = string.Format("SELECT * FROM WorkItems WHERE (System.AreaPath Under 'Marketing Temp\\Wegmans Mobile App') AND (System.IterationPath Under 'Marketing Temp\\Asynchrony')");

            var results = connect.ExecuteWorkItemWIQL(query1);

            bool tryit = results.Any(x => x.Id == 120431);

            int count = results.Count;
        }

        [Test]
        public void TestGetListOfIterationPaths()
        {
            ICommonStructureService css = connect.ProjectCollection.GetService<ICommonStructureService>();

            ProjectInfo projectInfo = css.GetProjectFromName("MARKETING TEMP");

            NodeInfo[] nodes = css.ListStructures(projectInfo.Uri);



            //GetNodes can use with:

            //Area = 1

            //Iteration = 0

            XmlElement AreaTree = css.GetNodesXml(new string[] { nodes[1].Uri }, true);

            XmlElement IterationsTree = css.GetNodesXml(new string[] { nodes[0].Uri }, true);

            XmlNode AreaNodes = AreaTree.ChildNodes[0];

            XmlNode IterationsNodes = IterationsTree.ChildNodes[0];

            int myNodeCount = IterationsNodes.FirstChild.ChildNodes.Count;

            List<string> list = new List<string>();

            for (int i = 0; i < myNodeCount; i++)
            {
                XmlNode Node = IterationsNodes.ChildNodes[0].ChildNodes[i];


                var x = AddNodeItem(Node);

                list.Add(Node.Attributes["Name"].Value);

                if (Node.HasChildNodes)
                {
                    foreach (XmlNodeList childNode in Node.ChildNodes)
                    {
                        //list.AddRange(AddNodeItem(Node, childNode.Item(0)));


//                        foreach (XmlNode n in childNode)
//                        {
//
//                        }

                    }
                }
            }
        }

        private List<string> AddNodeItem(XmlNode node)
        {
            var ret = new List<string>();

            if (node.Attributes["Path"] != null)
            {
                ret.Add(node.Attributes["Path"].Value);
            }

            if (node.FirstChild != null)
            {
                AddNodeItem(node.FirstChild);
            }

            return ret;
        }



        [Test]
        public void GetParents()
        {
            string q =
                "select [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] from WorkItemLinks where ([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') and (Target.[System.Id] = 119412) order by [System.Id] mode (Recursive, ReturnMatchingChildren)";

            var results = connect.ExecuteWorkItemWIQL(q);

            int count = results.Count;
        }

        [Test]
        public void GetTeamKanbanBoards()
        {
            var project = connect.GetProject("Marketing Temp");

            var areas = project.AreaRootNodes;

            var x = "";


        }

        [Test]
        public void TestGetTeams()
        {
            var project = connect.GetProject("Marketing Temp");
            var test = connect.GetTeams(project);
        }

        [Test]
        public void GetTeamAreas()
        {
            var teams = connect.GetTeams(connect.GetProject("Marketing Temp")).ToList();

            foreach (var team in teams)
            {
                var areas = connect.GetTeamAreas(team, connect.GetProject("Marketing Temp"));

                if (areas.Count > 1)
                {
                    string x = "";
                }
            }

        }

        [Test]
        public void testSpecialQuery()
        {
            var Query = "Select System.IterationPath From WorkItems Where [System.TeamProject] = '" +
                        connect.GetProject("Marketing Temp").Name + "'";

            var restuls = connect.QueryForSingleListOfString(Query, "Iteration Path");
        }

        [Test]
        public void DisectTheShitOutOfAWorkItem()
        {
            List<string> includeFields = new List<string>();
            includeFields.Add("State Change Date");
            includeFields.Add("Board Lane");
            includeFields.Add("Board Column");
            includeFields.Add("Board Column Done");
            includeFields.Add("State");
            includeFields.Add("Changed Date");
            includeFields.Add("Revised Date");


            WorkItem wi = connect.GetWorkItem(125197);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} - {1}", wi.Id, wi.Title));

            sb.AppendLine("Current Revision");

            foreach (Field field in wi.Fields)
            {
                if (!includeFields.Contains(field.Name)) continue;

                sb.AppendLine(string.Format("Field: {0}, Value: {1}", field.Name,field.Value));
            }
            sb.AppendLine("---");
            foreach (Revision rev in wi.Revisions)
            {
                

                sb.AppendLine(string.Format("Revision {0}",rev.Index));
                foreach (Field field in rev.Fields)
                {
                    if (!includeFields.Contains(field.Name)) continue;
                    sb.AppendLine(string.Format("Field: {0}, Value: {1}", field.Name, field.Value));
                }
                sb.AppendLine("---");
            }

            string result = sb.ToString();

            string x = "";

        }

    }
}
