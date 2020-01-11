using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.Category;
using WebApp.Core;
using WebApp.ViewModel;

namespace WebApp.Domain.Imp
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(ICommandProcessor commandProcessor) : base(commandProcessor)
        {
        }

        public Task<CategoryViewModel> CreateAsync(CreateCategoryCommand model)
        {
            return commandProcessor.ProcessAsync<CreateCategoryCommand, CategoryViewModel>(model);
        }

        public Task<CategoryViewModel> UpdateAsync(UpdateCategoryCommand model)
        {
            return commandProcessor.ProcessAsync<UpdateCategoryCommand, CategoryViewModel>(model);
        }
    }
}