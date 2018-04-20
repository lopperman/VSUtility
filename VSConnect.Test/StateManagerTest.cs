using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
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
    }
}
