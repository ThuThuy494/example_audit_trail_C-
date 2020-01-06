using Autofac;
using System.Threading.Tasks;
using WebApp.ExceptionCustom;

namespace WebApp.Core
{
    public class ApiCommandProcessor : ICommandProcessor
    {
        private readonly IComponentContext componentContext;

        public ApiCommandProcessor(IComponentContext context)
        {
            this.componentContext = context;
        }

        public void Process<TCommand>(TCommand command)
        {
            var commandHandler = this.componentContext.ResolveOptional<ICommandHandler<TCommand>>();

            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            commandHandler.Execute(command);
        }

        public TResult Process<TCommand, TResult>(TCommand command)
        {
            var commandHandler = this.componentContext.ResolveOptional<ICommandHandler<TCommand, TResult>>();
            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return commandHandler.Execute(command);
        }

        public Task ProcessAsync<TCommand>(TCommand command)
        {
            var commandHandler = this.componentContext.ResolveOptional<ICommandHandler<TCommand>>();
            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            return commandHandler.ExecuteAsync(command);
        }

        public Task<TResult> ProcessAsync<TCommand, TResult>(TCommand command)
        {
            var commandHandler = this.componentContext.ResolveOptional<ICommandHandler<TCommand, TResult>>();
            if (commandHandler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }
            return commandHandler.ExecuteAsync(command);
        }
    }
}