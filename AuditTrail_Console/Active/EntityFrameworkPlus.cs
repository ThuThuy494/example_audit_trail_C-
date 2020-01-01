using AuditTrail_Console.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AuditTrail_Console.Active
{
    public static class EntityFrameworkPlus
    {
        private static readonly UnitOfWorkEFPlus _uow = new UnitOfWorkEFPlus();
        public static void Demo_EntityFrameworkPlus()
        {
            var number = 1;
            var name = "EF Plus";
            var entity = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Nine Tailed " + name + number,
                LastName = "Fox " + name + number,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            var json = new JavaScriptSerializer().Serialize(entity);
            Console.WriteLine(json);
            _uow.PersonRepository.Add(entity);
            _uow.SaveChanges();
        }
    }
}
