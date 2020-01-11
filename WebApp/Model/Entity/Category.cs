using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z.EntityFramework.Plus;

namespace WebApp.Model.Entity
{
    [AuditInclude]
    //[AuditDisplay("CategoryEntityNameTest")]
    public class Category
    {
        [AuditExclude]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SubName { get; set; }

        [AuditExclude]
        public DateTime CreatedDate { get; set; }

        [AuditExclude]
        public DateTime UpdateDate { get; set; }
    }
}