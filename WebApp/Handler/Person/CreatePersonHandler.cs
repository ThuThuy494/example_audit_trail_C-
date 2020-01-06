using System;
using System.Threading.Tasks;
using WebApp.Command.Person;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace WebApp.Handler.Person
{
    public class CreatePersonHandler : BaseHandler, ICommandHandler<CreatePersonCommand, PersonViewModel>
    {
        public CreatePersonHandler(IUnitOfWork unitOfWork
           ) : base(unitOfWork)
        {
        }
        public PersonViewModel Execute(CreatePersonCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonViewModel> ExecuteAsync(CreatePersonCommand request)
        {
            var entity = new Model.Entity.Person()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.FirstName + request.LastName,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };


            unitOfWork.PersonRepository.Add(entity);

            unitOfWork.SaveChanges();

            return new PersonViewModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                FullName = entity.FullName,
                CreatedDate = entity.CreatedDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}