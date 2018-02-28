using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace VSUtil.Classes.Util
{
    public static class PrintHelper
    {
        internal static void SetOrientation(Chart chart, bool landscape)
        {
            chart.Printing.PrintDocument.DefaultPageSettings.Landscape = landscape;
        }

        internal static void SetMargins(Chart chart, int bottom, int top, int left, int right)
        {
            chart.Printing.PrintDocument.DefaultPageSettings.Margins.Bottom = bottom;
            chart.Printing.PrintDocument.DefaultPageSettings.Margins.Top = top;
            chart.Printing.PrintDocument.DefaultPageSettings.Margins.Left = left;
            chart.Printing.PrintDocument.DefaultPageSettings.Margins.Right = right;
        }
    }
}
