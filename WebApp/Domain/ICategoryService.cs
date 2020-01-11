using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Command.Category;
using WebApp.ViewModel;

namespace WebApp.Domain
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateAsync(CreateCategoryCommand model);
        Task<CategoryViewModel> UpdateAsync(UpdateCategoryCommand model);
    }
}
