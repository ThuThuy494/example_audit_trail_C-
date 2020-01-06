using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core
{
    public partial interface ICommandHandler<in TCommand, TResult>
    {
        TResult Execute(TCommand request);
        Task<TResult> ExecuteAsync(TCommand request);
    }

    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand request);
        Task ExecuteAsync(TCommand request);
    }
}
