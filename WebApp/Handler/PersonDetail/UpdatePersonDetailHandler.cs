using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var model = new PersonDetailViewModel();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            model.TimeStart = DateTime.Now;

            //To do
            var personDetail = unitOfWork.PersonRoleRepository.GetById(request.Id);
            if (personDetail == null) return null;

            personDetail.PersonId = request.PersonId;
            personDetail.RoleName = request.RoleName;
            personDetail.UpdateDate = DateTime.Now;

            unitOfWork.PersonRoleRepository.Update(personDetail);

            unitOfWork.SaveChanges();
            //To do
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);

            model.PersonId = personDetail.PersonId;
            model.RoleName = personDetail.RoleName;
            model.CreatedDate = personDetail.CreatedDate;
            model.UpdateDate = personDetail.UpdateDate;
            model.TimeRun = elapsedTime;

            return model;
        }
    }
}