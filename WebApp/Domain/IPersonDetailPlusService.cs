using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Command.Person;
using WebApp.Command.PersonDetail;
using WebApp.ViewModel;

namespace WebApp.Domain
{
    public interface IPersonDetailPlusService
    {
        Task<PersonDetailViewModel> CreateAsync(CreatePersonPlusDetailCommand model);
        Task<PersonDetailViewModel> UpdateAsync(UpdatePersonPlusDetailCommand model);
    }
}
