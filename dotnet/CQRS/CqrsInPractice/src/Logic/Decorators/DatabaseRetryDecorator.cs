using System;
using CSharpFunctionalExtensions;
using Logic.AppServices.Commands;
using Logic.Utils;

namespace Logic.Decorators
{
    public sealed class DatabaseRetryDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        
        private readonly Config _config;

        public DatabaseRetryDecorator(ICommandHandler<TCommand> commandHandler,
            Config config)
        {
            _commandHandler = commandHandler;
            _config = config;
        }

        public Result Handle(TCommand command)
        {
            for (var index = 0; ; index++)
            {
                try
                {
                    return _commandHandler.Handle(command);
                }
                catch (Exception ex)
                {
                    if (index < _config.NumberOfDatabaseRetires && IsDatabaseException(ex))
                        continue;

                    throw;
                };
            }
        }

        private bool IsDatabaseException(Exception exception)
        {
            string message = exception.InnerException?.Message.ToLower() ?? string.Empty;

            return message.Contains("the connection is broken and recovery is not possible")
                || message.Contains("error occurred while establishing a connection");
        }
    }
}