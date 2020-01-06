using System.Threading.Tasks;
using WebApp.Command;

namespace WebApp.Domain
{
    public interface IPersonService
    {
        Task<bool> CreateAsync(PersonCommand model);
        Task<bool> UpdateAsync(PersonCommand model);

    }
}