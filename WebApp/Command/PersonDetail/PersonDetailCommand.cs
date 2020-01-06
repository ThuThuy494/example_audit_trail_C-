using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebApp.Command.PersonDetail
{
    public class UpdatePersonDetailCommand : ICommand
    {
        public Guid Id { get; set; } // Id (Primary key)
        public Guid PersonId { get; set; } // PersonId (length: 150)
        public string RoleName { get; set; } // RoleName (length: 150)
    }
    public class CreatePersonDetailCommand : ICommand
    {
        public Guid PersonId { get; set; } // PersonId (length: 150)
        public string RoleName { get; set; } // RoleName (length: 150)
    }
}