using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace VSUtil
{
    public static class ExcelExport
    {
        public static void ExportToExcelWithFormatting(DataGridView dataGridView1)
        {
            string fileName;

            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xls files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.Title = "To Excel";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");
                for (var i = 0; i < dataGridView1.Columns.Count; i++)
                    worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].Name;

                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                for (var j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    var val1 = "";
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        val1 = dataGridView1.Rows[i].Cells[j].Value.ToString();

                    worksheet.Cell(i + 2, j + 1).Value = val1;

                    if (worksheet.Cell(i + 2, j + 1).Value.ToString().Length > 0)
                    {
                        XLAlignmentHorizontalValues align;

                        switch (dataGridView1.Rows[i].Cells[j].Style.Alignment)
                        {
                            case DataGridViewContentAlignment.BottomRight:
                                align = XLAlignmentHorizontalValues.Right;
                                break;
                            case DataGridViewContentAlignment.MiddleRight:
                                align = XLAlignmentHorizontalValues.Right;
                                break;
                            case DataGridViewContentAlignment.TopRight:
                                align = XLAlignmentHorizontalValues.Right;
                                break;

                            case DataGridViewContentAlignment.BottomCenter:
                                align = XLAlignmentHorizontalValues.Center;
                                break;
                            case DataGridViewContentAlignment.MiddleCenter:
                                align = XLAlignmentHorizontalValues.Center;
                                break;
                            case DataGridViewContentAlignment.TopCenter:
                                align = XLAlignmentHorizontalValues.Center;
                                break;

                            default:
                                align = XLAlignmentHorizontalValues.Left;
                                break;
                        }

                        worksheet.Cell(i + 2, j + 1).Style.Alignment.Horizontal = align;

//                            XLColor xlColor =
//                                XLColor.FromColor(dataGridView1.Rows[i].Cells[j].Style.BackColor);
//                            worksheet.Cell(i + 2, j + 1).AddConditionalFormat().WhenGreaterThan(1).Fill.SetBackgroundColor(xlColor);
//                            //worksheet.Cell(i + 2, j + 1).AddConditionalFormat().WhenNotBlank().Fill.SetBackgroundColor(xlColor);

                        worksheet.Cell(i + 2, j + 1).Style.Font.FontName = dataGridView1.Font.Name;
                        worksheet.Cell(i + 2, j + 1).Style.Font.FontSize = dataGridView1.Font.Size;

                    }
                }
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(fileName);
                //MessageBox.Show("Done");
            }
        }
    }
}
