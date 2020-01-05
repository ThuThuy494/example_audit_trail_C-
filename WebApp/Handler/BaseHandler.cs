using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Infrastructure;

namespace WebApp.Handler
{
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork unitOfWork;

        protected BaseHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}