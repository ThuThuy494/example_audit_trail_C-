using System.Threading.Tasks;
using WebApp.Command;
using WebApp.Command.Person;
using WebApp.ViewModel;

namespace WebApp.Domain
{
    public interface IPersonService
    {
        Task<PersonViewModel> CreateAsync(CreatePersonCommand model);
        Task<PersonViewModel> UpdateAsync(UpdatePersonCommand model);

    }
}