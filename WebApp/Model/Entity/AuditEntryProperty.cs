using System;

namespace WebApp.Model.Entity
{
    // AuditEntryProperties
    public class AuditEntryProperty
    {
        public int AuditEntryPropertyId { get; set; } // AuditEntryPropertyID (Primary key)
        public int AuditEntryId { get; set; } // AuditEntryID
        public string RelationName { get; set; } // RelationName (length: 255)
        public string PropertyName { get; set; } // PropertyName (length: 255)
        public string OldValue { get; set; } // OldValue
        public string NewValue { get; set; } // NewValue
        public DateTime CreatedDate { get; set; } // CreatedDate
        public DateTime UpdateDate { get; set; } // UpdateDate

        // Foreign keys

        /// <summary>
        /// Parent AuditEntry pointed by [AuditEntryProperties].([AuditEntryId]) (FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID)
        /// </summary>
        public virtual AuditEntry AuditEntry { get; set; } // FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID
    }

}
// </auto-generated>


