using AuditTrail_Console.Entity;
using AuditTrail_Console.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AuditTrail_Console.Active
{
    public class EntityFramework
    {
        private static readonly UnitOfWork _uow = new UnitOfWork();
        public static void Demo_EntityFramework()
        {
            var name = "EF ";
            var number = 1;
            var entity = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "taylor " + name + number,
                LastName = "Yuki " + name + number,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            entity.FullName = entity.FirstName + entity.LastName;

            var jsonPerson = new JavaScriptSerializer().Serialize(entity);
            Console.WriteLine(jsonPerson);
            _uow.PersonRepository.Add(entity);

            var role = new PersonDetail()
            {
                Id = Guid.NewGuid(),
                PersonId = entity.Id,
                RoleName = "Demo",
                CreatedDate = DateTime.Now,
                UpdateDate= DateTime.Now
            };

            var jsonPersonRole = new JavaScriptSerializer().Serialize(role);
            Console.WriteLine(jsonPersonRole);
            _uow.PersonRoleRepository.Add(role);

            _uow.SaveChanges();
        }
    }
}
