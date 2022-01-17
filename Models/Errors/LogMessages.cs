using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project4.Models.Errors
{
    public class LogMessages
    {
        public LogMessages()
        {
            RowNumber = "";
            FieldName = "";
            FieldNameToShow = "";
            Description = "";
            Value = "";
        }

        public string RowNumber { get; set; }
        public string FieldName { get; set; }
        public string FieldNameToShow { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
