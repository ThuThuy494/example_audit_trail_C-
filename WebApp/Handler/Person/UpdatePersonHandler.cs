using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.Person;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace WebApp.Handler.Person
{
    public class UpdatePersonHandler : BaseHandler, ICommandHandler<UpdatePersonCommand, PersonViewModel>
    {
        public UpdatePersonHandler(IUnitOfWorkHistoryTracking unitOfWork
          ) : base(unitOfWork)
        {
        }
        public PersonViewModel Execute(UpdatePersonCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonViewModel> ExecuteAsync(UpdatePersonCommand request)
        {
            var person = unitOfWork.PersonRepository.GetById(request.Id);
            if (person == null) return null;

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.FullName = request.FirstName + request.LastName;
            person.UpdateDate = DateTime.Now;

            unitOfWork.PersonRepository.Update(person);

            unitOfWork.SaveChanges();

            return new PersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                FullName = person.FullName,
                CreatedDate = person.CreatedDate,
                UpdateDate = person.UpdateDate
            };
        }
    }
}