using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core
{
    public interface ICommandProcessor
    {
        void Process<TCommand>(TCommand command);
        TResult Process<TCommand, TResult>(TCommand command);
        Task ProcessAsync<TCommand>(TCommand command);
        Task<TResult> ProcessAsync<TCommand, TResult>(TCommand command);
    }
}
