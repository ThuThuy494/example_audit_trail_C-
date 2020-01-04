using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.ViewModel;

namespace WebApp.Domain
{
    public interface IPersonService
    {
        bool Create(PersonModel model);
        bool Update(PersonModel model);

    }
}