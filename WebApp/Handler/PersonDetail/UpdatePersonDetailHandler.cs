using Autofac;
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
    public class UpdatePersonDetailHandler : BaseHandler, ICommandHandler<UpdatePersonDetailCommand, PersonDetailViewModel>
    {
        private readonly IComponentContext _componentContext;
        public UpdatePersonDetailHandler(IUnitOfWorkHistoryTracking unitOfWork, IComponentContext container
          ) : base(unitOfWork)
        {
            _componentContext = container;

        }

        public PersonDetailViewModel Execute(UpdatePersonDetailCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDetailViewModel> ExecuteAsync(UpdatePersonDetailCommand request)
        {
            var personDetail = unitOfWork.PersonRoleRepository.GetById(request.Id);
            if (personDetail == null) return null;

            personDetail.PersonId = request.PersonId;
            personDetail.RoleName = request.RoleName;
            personDetail.UpdateDate = DateTime.Now;

            unitOfWork.PersonRoleRepository.Update(personDetail);

            unitOfWork.SaveChanges();
            return new PersonDetailViewModel()
            {
                PersonId = personDetail.PersonId,
                RoleName = personDetail.RoleName,
                CreatedDate = personDetail.CreatedDate,
                UpdateDate = personDetail.UpdateDate
            };
        }
    }
}