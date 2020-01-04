using System;
using System.Collections.Generic;

namespace AuditTrail_Console.Entity
{
    // AuditEntries
    public class AuditEntry
    {
        public int AuditEntryId { get; set; } // AuditEntryID (Primary key)
        public string EntitySetName { get; set; } // EntitySetName (length: 255)
        public string EntityTypeName { get; set; } // EntityTypeName (length: 255)
        public int State { get; set; } // State
        public string StateName { get; set; } // StateName (length: 255)
        public string CreatedBy { get; set; } // CreatedBy (length: 255)
        public DateTime CreatedDate { get; set; } // CreatedDate
        public DateTime UpdateDate { get; set; } // UpdateDate

        // Reverse navigation

        /// <summary>
        /// Child AuditEntryProperties where [AuditEntryProperties].[AuditEntryID] point to this entity (FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID)
        /// </summary>
        public virtual ICollection<AuditEntryProperty> AuditEntryProperties { get; set; } // AuditEntryProperties.FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID

        public AuditEntry()
        {
            AuditEntryProperties = new List<AuditEntryProperty>();
        }
    }

}
// </auto-generated>


