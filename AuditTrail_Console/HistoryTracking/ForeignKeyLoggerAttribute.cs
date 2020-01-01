using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.HistoryTracking
{
    public class ForeignKeyLoggerAttribute : Attribute
    {
        private readonly string _entityName;
        private readonly string _foreignPropertyName;


        public string EntityName
        {
            get { return _entityName; }
        }

        public string ForeignPropertyName
        {
            get { return _foreignPropertyName; }
        }


        public ForeignKeyLoggerAttribute(string entityName, string foreignPropertyName = "")
        {
            _entityName = entityName;
            _foreignPropertyName = foreignPropertyName;
        }

    }
}
