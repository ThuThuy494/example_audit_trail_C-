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
    public class EntityFramework : IEntityFramework
    {
        private readonly IUnitOfWork _uow;
        public EntityFramework(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void Demo_EntityFramework()
        {
            Update();
        }

        private void Add()
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
                UpdateDate = DateTime.Now
            };

            var jsonPersonRole = new JavaScriptSerializer().Serialize(role);
            Console.WriteLine(jsonPersonRole);
            _uow.PersonRoleRepository.Add(role);
            _uow.SaveChanges();
        }

        private void Update()
        {
            var id = Guid.Parse("43CFD131-F923-4117-810E-A7BEBA5822C6");
            var entity = _uow.PersonRepository.GetById(id);
            entity.FullName = "Thu Thuy";
            _uow.PersonRepository.Update(entity);
            _uow.SaveChanges();
        }
    }
}
