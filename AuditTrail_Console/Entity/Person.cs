using AuditTrail_Console.HistoryTracking;
using System;
using System.Collections.Generic;

namespace AuditTrail_Console.Entity
{
    // Persons
    [Logger("Person")]
    public class Person : IHistoryTracker
    {
        [Logger(false)]
        public Guid Id { get; set; } // Id (Primary key)
        [Logger("FirstName")]
        public string FirstName { get; set; } // FirstName (length: 150)
        [Logger("LastName")]
        public string LastName { get; set; } // LastName (length: 150)
        [Logger("FullName")]
        public string FullName { get; set; } // LastName (length: 150)
        [Logger("CreatedDate")]
        public DateTime CreatedDate { get; set; } // CreatedDate
        [Logger("UpdateDate")]
        public DateTime UpdateDate { get; set; } // UpdateDate

        // Reverse navigation

        /// <summary>
        /// Child HistoryTrackingValueAudits where [HistoryTrackingValueAudit].[HistoryTrackingId] point to this entity (FK__HistoryTr__Histo__403A8C7D)
        /// </summary>
        public virtual ICollection<PersonDetail> PersonDetails { get; set; } // HistoryTrackingValueAudit.FK__HistoryTr__Histo__403A8C7D

        public string ObjectName
        {
            get
            {
                return Id.ToString();
            }
        }

        public IHistoryTracker ParentObject
        {
            get
            {
                return null;
            }
        }

        //public Person()
        //{
        //    PersonDetails = new List<PersonDetails>();
        //}
    }

}
// </auto-generated>


