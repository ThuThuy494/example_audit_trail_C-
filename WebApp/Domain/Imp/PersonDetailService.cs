using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.PersonDetail;
using WebApp.Core;
using WebApp.ViewModel;

namespace WebApp.Domain.Imp
{
    public class PersonDetailService : BaseService, IPersonDetailService
    {
        public PersonDetailService(ICommandProcessor commandProcessor) : base(commandProcessor)
        {
        }

        public Task<PersonDetailViewModel> CreateAsync(CreatePersonDetailCommand model)
        {
            return commandProcessor.ProcessAsync<CreatePersonDetailCommand, PersonDetailViewModel>(model);
        }

        public Task<PersonDetailViewModel> UpdateAsync(UpdatePersonDetailCommand model)
        {
            return commandProcessor.ProcessAsync<UpdatePersonDetailCommand, PersonDetailViewModel>(model);
        }
    }
}