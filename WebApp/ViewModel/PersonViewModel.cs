using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel
{
    public class PersonViewModel
    {
        Guid Id { get; set; }
        public string FirstName { get; set; } // FirstName (length: 150)
        public string LastName { get; set; } // LastName (length: 150)
        public string FullName { get; set; } // LastName (length: 150)
        public DateTime CreatedDate { get; set; } // CreatedDate
        public DateTime UpdateDate { get; set; } // UpdateDate

    }
}
