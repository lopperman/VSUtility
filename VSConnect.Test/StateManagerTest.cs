using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace VSConnect.Test
{
    [TestFixture]
    public class StateManagerTest
    {
        [Test]
        public void StateTest()
        {
            var state = new TFSState();
            state.Index = 0;
            Assert.AreEqual(0,state.Index);

            state.Category = "New";
            Assert.AreEqual("New",state.Category);

            state.KanbanColumn = "Kanban Test";
            Assert.AreEqual("Kanban Test",state.KanbanColumn);

            state.SystemState = "In Progress";
            Assert.AreEqual("In Progress",state.SystemState);

            state.KanbanColumnDoing = false;
            Assert.AreEqual(false,state.KanbanColumnDoing);

            state.KanbanColumnDone = false;
            Assert.AreEqual(false,state.KanbanColumnDone);

            state.CategoryIndex = 0;
            Assert.AreEqual(0,state.CategoryIndex);

            state.DevDone = false;
            Assert.AreEqual(false,state.DevDone);

            state.QADone = false;
            Assert.AreEqual(false,state.QADone);

            state.UATDone = false;
            Assert.AreEqual(false,state.UATDone);

        }


        [Test]
        public void testCannotAddWithBothKanbanColumnsSameValuasdfe()
        {
            var sm = new TFSStateManager();
            
            Assert.That(() => sm.AddState("", "", "", false, false),Throws.TypeOf<Exception>());
            Assert.That(() => sm.AddState("", "", "", true, true),Throws.TypeOf<Exception>());

            Assert.DoesNotThrow(() => sm.AddState("", "", "", false, true));
            Assert.DoesNotThrow(() => sm.AddState("", "", "", true, false));
        }

        [Test]
        public void testCannotHaveMultipleTruesforDevQaUAtDone()
        {
            var sm = new TFSStateManager();

            Assert.That(() => sm.AddState("", "", "", true, false,true,true,true), Throws.TypeOf<Exception>());
            Assert.That(() => sm.AddState("", "", "", true, false,false,true,true), Throws.TypeOf<Exception>());
            Assert.That(() => sm.AddState("", "", "", true, false,true,false,true), Throws.TypeOf<Exception>());
            Assert.That(() => sm.AddState("", "", "", true, false,true,true,false), Throws.TypeOf<Exception>());

            Assert.DoesNotThrow(() => sm.AddState("", "", "", false, true,false,false,false));
            Assert.DoesNotThrow(() => sm.AddState("", "", "", false, true,true,false,false));
            Assert.DoesNotThrow(() => sm.AddState("", "", "", false, true,false,true,false));
            Assert.DoesNotThrow(() => sm.AddState("", "", "", false, true,false,false,true));

        }

        [Test]
        public void testAddStates()
        {
            var sm = new TFSStateManager();

            sm.ProjectName = "Serialize Test";

            sm.AddState("Cat1","Backlog","New",true,false);
            sm.AddState("Cat1","Backlog","New",false,true);
            sm.AddState("Cat1","Story Authoring","New",true,false);
            sm.AddState("Cat1","Story Authoring","New",false,true);

            sm.AddState("Cat2","Ready for Dev","New",true,false);
            sm.AddState("Cat2","Ready for Dev","New",false,true);

            sm.AddState("Development","Development","Active",true,false);

            sm.AddState("DevelopmentDone","Development","Active",false,true,true);

            var states = sm.States;

            string testFile = @"C:\Work\tmp\text.xml";

            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }

            string output = JsonConvert.SerializeObject(sm);

            TFSStateManager deserializedStateManager = JsonConvert.DeserializeObject<TFSStateManager>(output);

        }

        [Test]
        public void canSerializeDeserialzeTFSStateManagerByProjNameTeamNameAreaId()
        {
            var sm = new TFSStateManager();

            sm.ProjectName = "Serialize Test";
            sm.TeamName = "Team Test";
            sm.AreaId = 67;

            sm.AddState("Cat1", "Backlog", "New", true, false);
            sm.AddState("Cat1", "Backlog", "New", false, true);
            sm.AddState("Cat1", "Story Authoring", "New", true, false);
            sm.AddState("Cat1", "Story Authoring", "New", false, true);
            sm.AddState("Cat2", "Ready for Dev", "New", true, false);
            sm.AddState("Cat2", "Ready for Dev", "New", false, true);
            sm.AddState("Development", "Development", "Active", true, false);
            sm.AddState("DevelopmentDone", "Development", "Active", false, true, true);


            TFSStateManager.SaveFuzzFile(Path.GetDirectoryName(TFSRegistry.GetTFSMdbPath()),sm);


            var FuzzFiles = TFSStateManager.LoadFuzzFiles(Path.GetDirectoryName(TFSRegistry.GetTFSMdbPath()));
        }

        [Test]
        public void testCreateManualFileForWMA()
        {
            var sm = new TFSStateManager();

            sm.ProjectUri = "https://wegmans.visualstudio.com/";
            sm.ProjectName = "MARKETING TEMP";
            sm.TeamName = "Wegmans Mobile App";
            sm.AreaId = 2884;


            //cat, kanban col, sys state
            sm.AddState("Backlog", "New", "New", false, true);
            sm.AddState("Backlog", "New", "New", true, false);
            sm.AddState("Backlog", "Backlog", "New", false, true);
            sm.AddState("Backlog", "Backlog", "New", true, false);
            sm.AddState("Backlog", "Story Authoring", "New", false, true);
            sm.AddState("Backlog", "Story Authoring", "New", true, false);
            sm.AddState("Backlog", "Ready For Dev", "New", false, true);
            sm.AddState("Backlog", "Ready For Dev", "New", true, false); 

            sm.AddState("Development", "Active", "Active", true, false);
            sm.AddState("Development", "Active", "Active", false, true);
            sm.AddState("Development", "Development", "Active", true, false);
            sm.AddState("DevelopmentDone", "Development", "Active", false, true, true); //dev done
    
            sm.AddState("AsynchronyQA","Asynchrony QA","Active",true,false);
            sm.AddState("AsynchronyQADone","Asynchrony QA","Active",false,true,false,true); //qa done

            sm.AddState("WegmansQA","Wegmans QA","Active",true,false);
            sm.AddState("WegmansQADone","Wegmans QA","Active",false,true,false,false,true); //uat done

            sm.AddState("POReview","PO Review","Resolved",true,false);
            sm.AddState("POReviewDone","PO Review","Resolved",false,true); 

            sm.AddState("Smoketest","Alpha Channel Smoke Test","Resolved",true,false);
            sm.AddState("SmoketestDone","Alpha Channel Smoke Test","Resolved",false,true);

            sm.AddState("Closed","Closed","Closed",false,true); 
            sm.AddState("Closed","Closed","Closed",true,false); 

            TFSStateManager.SaveFuzzFile(Path.GetDirectoryName(TFSRegistry.GetTFSMdbPath()),sm);

        }
    }
}
