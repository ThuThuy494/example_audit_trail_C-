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
    public class PersonDetailPlusService : BaseService, IPersonDetailPlusService
    {
        public PersonDetailPlusService(ICommandProcessor commandProcessor) : base(commandProcessor)
        {
        }

        public Task<PersonDetailViewModel> CreateAsync(CreatePersonPlusDetailCommand model)
        {
            return commandProcessor.ProcessAsync<CreatePersonPlusDetailCommand, PersonDetailViewModel>(model);
        }

        public Task<PersonDetailViewModel> UpdateAsync(UpdatePersonPlusDetailCommand model)
        {
            return commandProcessor.ProcessAsync<UpdatePersonPlusDetailCommand, PersonDetailViewModel>(model);
        }
    }
}