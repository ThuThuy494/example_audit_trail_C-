using System;
using System.Collections.Generic;

namespace WebApp.Model.Entity
{
    // HistoryTrackingAudit
    public class HistoryTrackingAudit
    {
        public Guid Id { get; set; } // Id (Primary key)
        public Guid RecordId { get; set; } // RecordId
        public string RecordType { get; set; } // RecordType
        public string ObjectName { get; set; } // ObjectName
        public string SubObjectName { get; set; } // SubObjectName
        public bool IsDeleted { get; set; } // IsDeleted
        public Guid InsertedById { get; set; } // InsertedById
        public DateTime InsertedAt { get; set; } // InsertedAt
        public Guid UpdatedById { get; set; } // UpdatedById
        public DateTime UpdatedAt { get; set; } // UpdatedAt

        // Reverse navigation

        /// <summary>
        /// Child HistoryTrackingValueAudits where [HistoryTrackingValueAudit].[HistoryTrackingId] point to this entity (FK__HistoryTr__Histo__403A8C7D)
        /// </summary>
        public virtual ICollection<HistoryTrackingValueAudit> HistoryTrackingValueAudits { get; set; } // HistoryTrackingValueAudit.FK__HistoryTr__Histo__403A8C7D

        //public HistoryTrackingAudit()
        //{
        //    HistoryTrackingValueAudits = new List<HistoryTrackingValueAudit>();
        //}
    }

}
// </auto-generated>


