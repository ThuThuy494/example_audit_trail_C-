using System;

namespace AuditTrail_Console.Entity
{
    // HistoryTrackingValueAudit
    public class HistoryTrackingValueAudit
    {
        public Guid Id { get; set; } // Id (Primary key)
        public string ColumnName { get; set; } // ColumnName
        public string OldValue { get; set; } // OldValue
        public string NewValue { get; set; } // NewValue
        public string Action { get; set; } // Action
        public bool IsDeleted { get; set; } // IsDeleted
        public Guid InsertedById { get; set; } // InsertedById
        public DateTime InsertedAt { get; set; } // InsertedAt
        public Guid UpdatedById { get; set; } // UpdatedById
        public DateTime UpdatedAt { get; set; } // UpdatedAt
        public Guid HistoryTrackingId { get; set; } // HistoryTrackingId

        // Foreign keys

        /// <summary>
        /// Parent HistoryTrackingAudit pointed by [HistoryTrackingValueAudit].([HistoryTrackingId]) (FK__HistoryTr__Histo__403A8C7D)
        /// </summary>
        public virtual HistoryTrackingAudit HistoryTrackingAudit { get; set; } // FK__HistoryTr__Histo__403A8C7D
    }

}
// </auto-generated>


