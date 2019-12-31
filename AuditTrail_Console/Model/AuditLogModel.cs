using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Model
{
    public class AuditLogModel
    {
        public Guid Id { get; set; } // Id (Primary key)
        public string AuditType { get; set; } // AuditType (length: 1)
        public string TableName { get; set; } // TableName (length: 150)
        public string Pk { get; set; } // PK (length: 50)
        public string ColumnName { get; set; } // ColumnName (length: 100)
        public string OldValue { get; set; } // OldValue
        public string NewValue { get; set; } // NewValue
        public DateTime Date { get; set; } // Date
        public Guid UserId { get; set; } // UserIdk
        public DateTime? CreateDate { get; set; } // CreateDate
        public DateTime? UpdateDate { get; set; } // UpdateDate
    }
}
