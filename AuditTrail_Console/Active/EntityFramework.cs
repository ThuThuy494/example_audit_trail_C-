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
                FirstName = "Yuki " + name + number,
                LastName = "Yuki " + name + number,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            var json = new JavaScriptSerializer().Serialize(entity);
            Console.WriteLine(json);
            _uow.PersonRepository.Add(entity);
            _uow.SaveChanges();
        }
    }
}
