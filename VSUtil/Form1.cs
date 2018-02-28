using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public Form1()
        {
            InitializeComponent();

            connect = StaticUtils.connect;

            PopulateProjectAreasAndSetDefault();

            cboProject.SelectedIndexChanged += CboProject_SelectedIndexChanged;

        }

        private void CboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TFSRegistry.SetDefaultAreaId(AreaId);
            }
            catch
            {
                
            }
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


            var proj = connect.GetProject("Marketing Temp");
            var areas = connect.GetTeamAreas(proj);
            cboProject.DataSource = areas;
            cboProject.DisplayMember = "Name";
            cboProject.ValueMember = "Id";

            if (TFSRegistry.GetDefaultAreaId() != 0)
            {
                cboProject.SelectedValue = TFSRegistry.GetDefaultAreaId();
            }
            VSCommon.LastDayOfWeek = TFSRegistry.LastDayOfWeek;

        }

        private int AreaId
        {
            get
            {
                int areaId = 0;

                Int32.TryParse(cboProject.SelectedValue.ToString(), out areaId);

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

                var f = new frmDevMetrics(TFSRegistry.GetTFSMdbPath(), connect, AreaId, cboProject.Text);
                f.Show();
            }
        }
    }
}
