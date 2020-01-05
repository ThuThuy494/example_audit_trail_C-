using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Core;

namespace WebApp.Domain.Imp
{
    //Move Base Service to Infr
    public abstract class BaseService
    {
        protected readonly ICommandProcessor commandProcessor;

        protected BaseService(ICommandProcessor commandProcessor)
        {
            this.commandProcessor = commandProcessor;
        }
    }
}