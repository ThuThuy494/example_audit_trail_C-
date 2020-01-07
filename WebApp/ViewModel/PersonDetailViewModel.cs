using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModel
{
    public class PersonDetailViewModel
    {
        public Guid PersonId { get; set; } // PersonId (length: 150)
        public string RoleName { get; set; } // RoleName (length: 150)
        public DateTime CreatedDate { get; set; } // CreatedDate
        public DateTime UpdateDate { get; set; } // UpdateDate
        public DateTime TimeStart { get; set; }
        public string TimeRun { get; set; }
    }
}