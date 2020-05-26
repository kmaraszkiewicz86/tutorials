using System;
using System.Windows.Input;

namespace GradeBook.UI.Commands
{
    public class LoadedWindowCommand: BaseCommand
    {
        private readonly Action _execute;
        
        public LoadedWindowCommand(Action execute)
        {
            _execute = execute;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
