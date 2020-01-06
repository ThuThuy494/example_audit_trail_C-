using System.Threading.Tasks;
using WebApp.Command;
using WebApp.Core;

namespace WebApp.Domain.Imp
{
    public class PersonService : BaseService, IPersonService
    {
        //public readonly IUnitOfWork _unitOfWork;
        //public PersonService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public PersonService(ICommandProcessor commandProcessor) : base(commandProcessor)
        {
        }

        public Task<bool> CreateAsync(PersonCommand request)
        {
            return commandProcessor.ProcessAsync<PersonCommand, bool>(request);
        }

        public Task<bool> UpdateAsync(PersonCommand request)
        {
            return commandProcessor.ProcessAsync<PersonCommand, bool>(request);
        }

        //public bool Create(PersonModel model)
        //{
        //    var entity = new Person()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        FullName = model.FirstName + model.LastName,
        //        CreatedDate = DateTime.Now,
        //        UpdateDate = DateTime.Now
        //    };


        //    _unitOfWork.PersonRepository.Add(entity);

        //    var role = new PersonDetail()
        //    {
        //        Id = Guid.NewGuid(),
        //        PersonId = entity.Id,
        //        RoleName = "Demo",
        //        CreatedDate = DateTime.Now,
        //        UpdateDate = DateTime.Now
        //    };

        //    try
        //    {
        //        _unitOfWork.PersonRoleRepository.Add(role);
        //        _unitOfWork.SaveChanges();
        //        return true;

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool Update(PersonModel model)
        //{
        //    var id = Guid.Parse("43CFD131-F923-4117-810E-A7BEBA5822C6");
        //    var entity = _unitOfWork.PersonRepository.GetById(id);
        //    entity.FullName = "Thu Thuy";

        //    try
        //    {
        //        _unitOfWork.PersonRepository.Update(entity);
        //        _unitOfWork.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}