using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.Category;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace WebApp.Handler.Category
{
    public class CreateCategoryEntityPlusHandler : BaseHandler, ICommandHandler<CreateCategoryCommand, CategoryViewModel>
    {
        public CreateCategoryEntityPlusHandler(IUnitOfWork unitOfWork
              ) : base(unitOfWork)
        {
        }
        public CategoryViewModel Execute(CreateCategoryCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryViewModel> ExecuteAsync(CreateCategoryCommand request)
        {
            var entity = new Model.Entity.Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                SubName = request.SubName,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };


            unitOfWork.CategoryRepository.Add(entity);

            unitOfWork.SaveChanges();

            return new CategoryViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                SubName = entity.SubName,
                CreatedDate = entity.CreatedDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}