using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Command.Category;
using WebApp.Core;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace WebApp.Handler.Category
{
    public class UpdateCategoryEntityPlusHandler : BaseHandler, ICommandHandler<UpdateCategoryCommand, CategoryViewModel>
    {
        private readonly IComponentContext _componentContext;
        public UpdateCategoryEntityPlusHandler(IUnitOfWork unitOfWork, IComponentContext container
          ) : base(unitOfWork)
        {
            _componentContext = container;

        }

        public CategoryViewModel Execute(UpdateCategoryCommand request)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryViewModel> ExecuteAsync(UpdateCategoryCommand request)
        {
            var model = new CategoryViewModel();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            model.TimeStart = DateTime.Now;

            //To do
            var category = unitOfWork.CategoryRepository.GetById(request.Id);
            if (category == null) return null;

            category.Name = request.Name;
            category.SubName = request.SubName;
            category.UpdateDate = DateTime.Now;

            unitOfWork.CategoryRepository.Update(category);

            unitOfWork.SaveChanges();
            //To do

            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);

            model.Name = category.Name;
            model.SubName = category.SubName;
            model.CreatedDate = category.CreatedDate;
            model.UpdateDate = category.UpdateDate;
            model.TimeRun = elapsedTime;

            return model;
        }
    }
}