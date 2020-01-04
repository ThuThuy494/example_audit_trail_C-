using System;
using WebApp.HistoryTracking;

namespace WebApp.Model.Entity
{
    [Logger("PersonDetail")]
    public class PersonDetail : IHistoryTracker
    {
        [Logger(false)]
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
                return Person;
            }
        }

    }
}
