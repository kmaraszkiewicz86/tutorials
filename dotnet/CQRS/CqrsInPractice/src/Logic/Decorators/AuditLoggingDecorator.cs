using System;
using CSharpFunctionalExtensions;
using Logic.Students.CommandHandlers;
using Logic.Students.Commands;
using Newtonsoft.Json;

namespace Logic.Decorators
{
    public sealed class AuditLoggingDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;

        public AuditLoggingDecorator(ICommandHandler<TCommand> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public Result Handle(TCommand command)
        {
            PrintLog(command, "Parameters");

            return _commandHandler.Handle(command);
        }

        private void PrintLog(object objectToSerialize, string logType)
        {
            string jsonString = JsonConvert.SerializeObject(objectToSerialize);

            Console.WriteLine($"[{logType}] Command of type {objectToSerialize.GetType().Name}: {jsonString}");
        }
    }
}