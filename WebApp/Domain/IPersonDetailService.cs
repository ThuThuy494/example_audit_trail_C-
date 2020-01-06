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
    public interface IPersonDetailService
    {
        Task<PersonDetailViewModel> CreateAsync(CreatePersonDetailCommand model);
        Task<PersonDetailViewModel> UpdateAsync(UpdatePersonDetailCommand model);
    }
}
