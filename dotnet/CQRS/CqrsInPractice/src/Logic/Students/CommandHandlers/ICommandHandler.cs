using CSharpFunctionalExtensions;
using Logic.Students.Commands;

namespace Logic.Students.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}
