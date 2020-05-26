using System;

namespace GradeBook.UI.Commands
{
    public class InsertNewCommand: BaseCommand
    {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public InsertNewCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public override void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
