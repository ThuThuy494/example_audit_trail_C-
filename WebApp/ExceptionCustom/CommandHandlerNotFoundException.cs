using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ExceptionCustom
{
    public class CommandHandlerNotFoundException : System.Exception
    {
        public CommandHandlerNotFoundException(Type type)
           : base(string.Format("Command handler not found for command type: {0}", type))
        {
        }

        public CommandHandlerNotFoundException(Type commandType, Type commandResult)
            : base(string.Format("Command handler not found for command type: {0}, and command result type: {1}", commandType, commandResult))
        {
        }
    }
}