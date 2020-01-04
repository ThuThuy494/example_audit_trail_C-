﻿using AuditTrail_Console.HistoryTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrail_Console.Entity
{
    [Logger("PersonDetail")]
    public class PersonDetail
    {
        [Logger("Id")]
        public Guid Id { get; set; } // Id (Primary key)
        [Logger("PersonId", false)]
        [ForeignKeyLogger("Person", "FullName")]
        public Guid PersonId { get; set; } // PersonId (length: 150)
        [Logger("RoleName")]
        public string RoleName { get; set; } // RoleName (length: 150)
        [Logger("CreatedDate")]
        public DateTime CreatedDate { get; set; } // CreatedDate
        [Logger("UpdateDate")]
        public DateTime UpdateDate { get; set; } // UpdateDate

        // Foreign keys

        /// <summary>
        /// Parent HistoryTrackingAudit pointed by [HistoryTrackingValueAudit].([HistoryTrackingId]) (FK__HistoryTr__Histo__403A8C7D)
        /// </summary>
        public virtual Person Person { get; set; } // FK__HistoryTr__Histo__403A8C7D
    }
}
