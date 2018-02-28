using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSConnect;
using VSUtil.Classes.Util;

namespace VSUtil
{
    public partial class frmLogin : Form
    {
        private Connect connect = null;

        public frmLogin()
        {
            InitializeComponent();

            txtUri.Text = TFSRegistry.GetUri();
            txtLogin.Text = TFSRegistry.GetLogin();
            txtPassword.Text = TFSRegistry.GetPassword();
            txtDomain.Text = TFSRegistry.GetDomain();

        }




        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void Accept()
        {
            if (string.IsNullOrEmpty(txtUri.Text) || string.IsNullOrEmpty(txtLogin.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtDomain.Text))
            {
                MessageBox.Show("Missing required information");
                return;
            }

            try
            {
                Uri tfsUri = new Uri(txtUri.Text);
                connect = new Connect(tfsUri, txtLogin.Text, txtPassword.Text, txtDomain.Text);

                if (connect != null)
                {
                    TFSRegistry.SetUri(txtUri.Text);
                    TFSRegistry.SetLogin(txtLogin.Text);
                    TFSRegistry.SetPassword(txtPassword.Text);
                    TFSRegistry.SetDomain(txtDomain.Text);
                    StaticUtils.connect = connect;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
