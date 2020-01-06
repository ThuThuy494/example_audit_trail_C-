using System;
using System.Threading.Tasks;
using WebApp.Command;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.Model.Entity;
using WebApp.ViewModel;

namespace WebApp.Handler
{
    public class PersonHandler : BaseHandler, ICommandHandler<PersonCommand, bool>
    {
        public PersonHandler(IUnitOfWork unitOfWork
           ) : base(unitOfWork)
        {
        }
        public bool Execute(PersonCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExecuteAsync(PersonCommand request)
        {
            var entity = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.FirstName + request.LastName,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };


            unitOfWork.PersonRepository.Add(entity);

            var role = new PersonDetail()
            {
                Id = Guid.NewGuid(),
                PersonId = entity.Id,
                RoleName = "Demo",
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            unitOfWork.PersonRoleRepository.Add(role);
            unitOfWork.SaveChanges();
            return true;
        }
    }
}