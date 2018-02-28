using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect
{
    public class WorkItemRevisionHistory
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public DateTime ChangedDt { get; set; }
        public DateTime RevisedDt { get; set; }
        public string FieldName { get; set; }
        public object NewValue { get; set; }
        public object OldValue { get; set; }

        public override string ToString()
        {
            return
                $"ID: {Id}, Rev: {Revision}, ChDt: {ChangedDt}, RevDt: {RevisedDt}, Field: '{FieldName}', New Value: {NewValue}, Old Value: {OldValue}";
        }
    }
}
