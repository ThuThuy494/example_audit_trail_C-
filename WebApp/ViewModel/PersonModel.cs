using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel
{
    public class PersonModel
    {
        Guid Id { get; set; }
        public string FirstName { get; set; } // FirstName (length: 150)
        public string LastName { get; set; } // LastName (length: 150)
    }
}
