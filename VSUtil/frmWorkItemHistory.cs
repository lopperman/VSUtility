using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Converters;
using VSConnect;


namespace VSUtil
{
    public partial class frmWorkItemHistory : Form
    {
        private Connect connect = null;
        DataGridViewCellStyle style1 = new DataGridViewCellStyle();
        DataGridViewCellStyle style2 = new DataGridViewCellStyle();
        

        public frmWorkItemHistory()
        {
            InitializeComponent();
            //dgvData.CellFormatting += DgvData_CellFormatting;
        }

        private void DgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!dgvData.Rows[e.RowIndex].IsNewRow && dgvData.Columns[e.ColumnIndex].Name == "NewValue")
                {
                    if (e.Value != DBNull.Value)
                    {
                        if (IsDateTime(e.Value))
                        {
                            e.CellStyle = style1;
                        }
                        else
                        {
                            e.CellStyle = dgvData.DefaultCellStyle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

        private bool IsDateTime(object eValue)
        {
            bool ret = false;

            try
            {
                DateTime dt;
                if (DateTime.TryParse(eValue.ToString(), out dt))
                {
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }

            return ret;
        }

        public frmWorkItemHistory(Connect connect): this()
        {
            this.connect = connect;
            style1.BackColor = Color.PaleGoldenrod;
            style2.BackColor = Color.Salmon;
        }



        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateGrid()
        {
            var history = connect.GetRevisionHistory(Convert.ToInt32(txtWorkItemId.Text));
            dgvData.DataSource = history;
            dgvData.ReadOnly = true;

            dgvData.Columns["ChangedDt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvData.Columns["RevisedDt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvData.Columns["Revision"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvData.Columns["FieldName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvData.Columns["NewValue"].Width = 160;


            for (int row = 1; row < dgvData.Rows.Count; row++)
            {
                if (IsDateTime(dgvData["NewValue", row].Value))
                {
                    dgvData["NewValue", row].Style = style1;
                }
                string fieldName = dgvData["FieldName", row].Value.ToString();
                if (fieldName == "State" || fieldName == "Board Column" || fieldName == "Board Column Done")
                {
                    dgvData["FieldName", row].Style = style2;
                }
            }

        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            ExcelExport.ExportToExcelWithFormatting(dgvData);
        }
    }
}
