﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TFSUtilities.Forms;
using VSConnect;
using VSUtil.Classes.Util;
using VSUtil.Forms;


namespace VSUtil
{
    public partial class Form1 : Form
    {
        private Connect connect = null;
        bool initialized = false;
        string tfsPath = TFSRegistry.GetTFSMdbPath();

        private List<TfsArea> availableTeamAreas = new List<TfsArea>();
        private List<int> selectedTeamAreaIds = new List<int>();

        public Form1()
        {
            InitializeComponent();

            connect = StaticUtils.connect;

            PopulateProjectAreasAndSetDefault();

            cboTeam.SelectedIndexChanged += CboTeamSelectedIndexChanged;
            cboRealProject.SelectedIndexChanged += new System.EventHandler(this.cboRealProject_SelectedIndexChanged);
            lstTeamAreas.CheckOnClick = true;

            lstTeamAreas.ItemCheck += LstTeamAreas_ItemCheck;
            lblInfo.Text = "Properties for: " + connect.ProjectCollection.Uri;

        }

        private void LstTeamAreas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ShowWait();

            if (SelectedTeam != null && SelectedProject != null)
            {
                if (availableTeamAreas.Count == 0)
                {
                    availableTeamAreas.AddRange(connect.GetTeamAreas(SelectedProject));
                }


            }

            ShowDefault();
        }

        private void ShowDefault()
        {
            this.Cursor = Cursors.Default;
        }

        private void ShowWait()
        {
            this.Cursor = Cursors.WaitCursor;
        }

        private void CboTeamSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTeamAreas();

//            try
//            {
//                TFSRegistry.SetDefaultAreaId(AreaId);
//            }
//            catch
//            {
//                
//            }
        }

        private void init()
        {
            CheckForTFSMdbFile();
        }

        private string GetUserSelectedTFSFilePath()
        {
            string path = string.Empty;
            SaveFileDialog dial = new SaveFileDialog();
            dial.Filter = "Access db|*.accdb";
            dial.AddExtension = true;
            dial.FileName = "TFS";
            dial.DefaultExt = "accdb";
            dial.InitialDirectory = @"c:\";
            dial.Title = "Choose Location To Save TFS dump file";
            if (dial.ShowDialog() == DialogResult.OK)
            {
                return dial.FileName;
            }
            return string.Empty;
        }
        private bool CanWorkLocally()
        {
            bool ret = false;

            try
            {
                if (Directory.Exists(@"c:\TFSUtility") == false)
                {
                    Directory.CreateDirectory(@"c:\TFSUtility");
                }
                TFSRegistry.SetTFSMdbPath(@"c:\TFSUtility\TFS.mdb");
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
        private void CheckForTFSMdbFile()
        {
            if (string.IsNullOrWhiteSpace(tfsPath))
            {
                if (CanWorkLocally())
                {
                    AskToUpdateTFSFile(@"c:\TFSUtility\tfs.accdb");
                }
                else
                {
                    tfsPath = GetUserSelectedTFSFilePath();
                    if (!string.IsNullOrWhiteSpace(tfsPath))
                    {
                        TFSRegistry.SetTFSMdbPath(tfsPath);
                    }
                }
            }

            AskToUpdateTFSFile(TFSRegistry.GetTFSMdbPath());
        }
        private void AskToUpdateTFSFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (!initialized)
            {
                init();
                initialized = true;
            }


            string message = string.Format("This application will automatically update the 'TFS dump' file if it is more than 4 hours old?{0}{0}Would you like to update the file anyway?", Environment.NewLine);
            if (MessageBox.Show(message, "TFS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (VSCommon.IsFileLocked(TFSRegistry.GetTFSMdbPath()))
                {
                    MessageBox.Show("Cannot update data from TFS because the TFS.mdb file is open on your machine.  Please close it, then try again.");
                    return ;
                }

                string path = TFSRegistry.GetTFSMdbPath();
                Cursor = Cursors.WaitCursor;

                button1.Enabled = false;
                Application.DoEvents();


                var wait = new InfoFrm("Extracting data, please wait ...", false);
                wait.Show(this);
                Application.DoEvents();

                DumpWorkItemsToDS dump = new DumpWorkItemsToDS(connect, 2884, wait, "Marketing Temp", TFSRegistry.LastDayOfWeek);
                dump.DumpWorkItems(path);

                wait.Close();

                button1.Enabled = true;
                Cursor = Cursors.Default;
                Application.DoEvents();


            }

        }

        private void workItemHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new frmWorkItemHistory(connect);
            f.Show();
        }

        private void PopulateProjectAreasAndSetDefault()
        {

            cboRealProject.DataSource = connect.GetTfsProjects();
            cboRealProject.DisplayMember = "Name";
            cboRealProject.ValueMember = "Id";

            string defaultProjectName = TFSRegistry.GetDefaultProjectName("Marketing Temp");
            var proj = connect.GetProject(defaultProjectName);
            cboRealProject.SelectedValue = proj.Id;

            var teams = connect.GetTeams(proj);
            cboTeam.DataSource = teams;
            cboTeam.ValueMember = "Identity";
            cboTeam.DisplayMember = "Name";

            if (teams.Any(x => x.Name == TFSRegistry.GetDefaultTeamName("Wegmans Mobile App")))
            {
                cboTeam.SelectedValue =
                    teams.First(x => x.Name == TFSRegistry.GetDefaultTeamName("Wegmans Mobile App")).Identity;
            }

            UpdateTeamAreas();

            //            cboTeam.DataSource = areas;
            //            cboTeam.DisplayMember = "Name";
            //            cboTeam.ValueMember = "Id";
            //
            //            if (TFSRegistry.GetDefaultAreaId() != 0)
            //            {
            //                cboTeam.SelectedValue = TFSRegistry.GetDefaultAreaId();
            //            }
            //            VSCommon.LastDayOfWeek = TFSRegistry.LastDayOfWeek;

        }

        private void UpdateTeamAreas()
        {
            if (cboTeam.SelectedIndex >= 0)
            {
                var teamAreas = connect.GetTeamAreas(cboTeam.SelectedItem as TeamFoundationTeam);

                foreach (string area in teamAreas)
                {
                    lstTeamAreas.Items.Add(area, true);
                }
            }
        }

        private Project SelectedProject
        {
            get
            {
                if (cboRealProject.SelectedIndex >= 0)
                {
                    return connect.GetProject(cboRealProject.Text);
                }

                return null;
            }
        }

        private TeamFoundationTeam SelectedTeam
        {
            get
            {
                if (cboTeam.SelectedIndex >= 0)
                {
                    return cboTeam.SelectedItem as TeamFoundationTeam;
                    ;
                }

                return null;
            }
        }

        private int AreaId
        {
            get
            {
                int areaId = 0;

                Int32.TryParse(cboTeam.SelectedValue.ToString(), out areaId);

                return areaId;
            }
        }

        private bool DumpTFS()
        {
            if (VSCommon.IsFileLocked(TFSRegistry.GetTFSMdbPath()))
            {
                MessageBox.Show("Cannot update data from TFS because the TFS.accdb file is open on your machine.  Please close it, then try again.");
                return false;
            }

            string path = TFSRegistry.GetTFSMdbPath();
            button1.Enabled = false;
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            var wait = new InfoFrm("Extracting data from TFS, please wait ...", false);
            wait.Show(this);
            Application.DoEvents();

            DumpWorkItemsToDS dump = new DumpWorkItemsToDS(connect, AreaId, wait, "Marketing Temp", TFSRegistry.LastDayOfWeek);
            dump.DumpWorkItems(path);

            wait.Close();

            button1.Enabled = true;
            Cursor = Cursors.Default;
            Application.DoEvents();

            return true;
        }


        private void developmentMetricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!initialized)
            {
                init();
                initialized = true;
            }

            //get the last update date from the file
            if (File.Exists(TFSRegistry.GetTFSMdbPath()) && VSCommon.GetDumpFileAreaId(TFSRegistry.GetTFSMdbPath()) == AreaId)
            {
                DateTime? dumpDate = VSCommon.GetDumpDate(TFSRegistry.GetTFSMdbPath());
                if (DateTime.Now.Subtract(dumpDate.Value).TotalHours >= 4)
                {
                    if (MessageBox.Show("The data used by this application hasn't been updated since " + dumpDate.Value.ToString() + Environment.NewLine + "Would you like to update now?", "Update Ira Dump Data", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (DumpTFS() == false) return;
                    }
                }
            }
            else
            {
                if (DumpTFS() == false) return;
            }

            
            if (File.Exists(TFSRegistry.GetTFSMdbPath()))
            {
                if (VSCommon.IsFileLocked(TFSRegistry.GetTFSMdbPath()))
                {
                    MessageBox.Show("Cannot update data from TFS because the TFS.accdb file is open on your machine.  Please close it, then try again.");
                    return;
                }

                var f = new frmDevMetrics(TFSRegistry.GetTFSMdbPath(), connect, AreaId, cboTeam.Text);
                f.Show();
            }
        }

        private void dynamicChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!initialized)
            {
                init();
                initialized = true;
            }

            //get the last update date from the file
            if (File.Exists(TFSRegistry.GetTFSMdbPath()) && VSCommon.GetDumpFileAreaId(TFSRegistry.GetTFSMdbPath()) == AreaId)
            {
                DateTime? dumpDate = VSCommon.GetDumpDate(TFSRegistry.GetTFSMdbPath());
                if (DateTime.Now.Subtract(dumpDate.Value).TotalHours >= 4)
                {
                    if (MessageBox.Show("The data used by this application hasn't been updated since " + dumpDate.Value.ToString() + Environment.NewLine + "Would you like to update now?", "Update Ira Dump Data", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (DumpTFS() == false) return;
                    }
                }
            }
            else
            {
                if (DumpTFS() == false) return;
            }


            if (File.Exists(TFSRegistry.GetTFSMdbPath()))
            {
                if (VSCommon.IsFileLocked(TFSRegistry.GetTFSMdbPath()))
                {
                    MessageBox.Show("Cannot update data from TFS because the TFS.accdb file is open on your machine.  Please close it, then try again.");
                    return;
                }

                var f = new frmDynamicChart(TFSRegistry.GetTFSMdbPath(), connect, AreaId, cboTeam.Text);
                f.Show();
            }

        }

        private void cboRealProject_SelectedIndexChanged(object sender, EventArgs e)
        {

//            string defaultProjectName = TFSRegistry.GetDefaultProjectName("Marketing Temp");
//            var proj = connect.GetProject(defaultProjectName);
//            cboRealProject.SelectedValue = proj.Id;
//
//            var teams = connect.GetTeams(proj);
//            cboTeam.DataSource = teams;
//            cboTeam.ValueMember = "Identity";
//            cboTeam.DisplayMember = "Name";
//
//            if (teams.Any(x => x.Name == TFSRegistry.GetDefaultTeamName("Wegmans Mobile App")))
//            {
//                cboTeam.SelectedValue =
//                    teams.First(x => x.Name == TFSRegistry.GetDefaultTeamName("Wegmans Mobile App")).Identity;
//            }





        }
    }
}
