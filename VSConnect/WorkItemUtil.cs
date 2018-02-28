using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect
{
    public class WorkItemUtil
    {
        public static List<WItem> GetWorkItemRevisions(DumpDataSet.WorkItemRevisionDataTable table)
        {
            var ret = new List<WItem>();

            foreach (DumpDataSet.WorkItemRevisionRow row in table.Rows)
            {
                var wi = new WItem();
                wi.ID = row.ID;
                wi.CreateDate = row.CreatedDate;
                wi.Rev = row.Rev;
                wi.State = row.State;
                wi.ChangedDate = row.ChangedDate;
                wi.RevisedDate = row.RevisedDate;
                wi.BoardColumn = row.BoardColumn;
                wi.BoardLane = row.BoardLane;
                wi.BoardColumnDone =
                    (row.IsBoardColumnDoneNull() ? false : row.BoardColumnDone == "True" ? true : false);
                wi.Title = row.Title;
                wi.WorkItemType = row.WorkItemType;
                wi.BusinessPriority = row.BusinessPriority;

                if (row.BusinessPriority > 0)
                {
                    string x = "";
                }


                ret.Add(wi);
            }

            return ret;
        }
    }


}
