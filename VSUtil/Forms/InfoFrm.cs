using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TFSUtilities.Forms
{
    public partial class InfoFrm : Form, INotify
    {
        public InfoFrm()
        {
            InitializeComponent();
        }

        public InfoFrm(string info, bool showCloseButton): this()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            lblInfo.Text = info;

            cmdClose.Visible = showCloseButton;
            cmdNewJoke.Visible = showCloseButton;

            lblCN.Text = "...";
            Application.DoEvents();

            if (showCloseButton)
            {
                this.Cursor = Cursors.Default;
                ShowChuck();
            }
        }

        public void Notify(string message)
        {
            lblCN.Text = message;
            Application.DoEvents();
        }

        public void ShowChuck()
        {


        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmdNewJoke_Click(object sender, EventArgs e)
        {
            ShowChuck();
        }
    }


}
