using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebApp.Command.Person
{
    public class UpdatePersonCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } // FirstName (length: 150)
        public string LastName { get; set; } // LastName (length: 150)
    }
    public class CreatePersonCommand : ICommand
    {
        public string FirstName { get; set; } // FirstName (length: 150)
        public string LastName { get; set; } // LastName (length: 150)
    }
}