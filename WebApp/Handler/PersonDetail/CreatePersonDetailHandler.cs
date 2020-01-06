using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.PersonDetail;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace WebApp.Handler.PersonDetail
{
    public class CreatePersonDetailHandler : BaseHandler, ICommandHandler<CreatePersonDetailCommand, PersonDetailViewModel>
    {
        public CreatePersonDetailHandler(IUnitOfWorkHistoryTracking unitOfWork
          ) : base(unitOfWork)
        {
        }

        public PersonDetailViewModel Execute(CreatePersonDetailCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDetailViewModel> ExecuteAsync(CreatePersonDetailCommand request)
        {
            var entity = new Model.Entity.PersonDetail()
            {
                Id = Guid.NewGuid(),
                PersonId = request.PersonId,
                RoleName = request.RoleName,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };


            unitOfWork.PersonRoleRepository.Add(entity);

            unitOfWork.SaveChanges();
            return new PersonDetailViewModel()
            {
                PersonId = entity.PersonId,
                RoleName = entity.RoleName,
                CreatedDate = entity.CreatedDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}