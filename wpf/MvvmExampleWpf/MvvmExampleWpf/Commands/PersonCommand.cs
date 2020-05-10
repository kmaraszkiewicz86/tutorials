using System;
using System.Windows.Input;
using MvvmExampleWpf.Models;

namespace MvvmExampleWpf.Commands
{
    public class PersonCommand: ICommand
    {
        private Action _execute;

        private Action<PersonModel> _executeWithParams;

        private Action<KeyEventArgs> _executeWithKeyEventArgs;

        private Func<bool> _canExecute;

        public PersonCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public PersonCommand(Action<PersonModel> executeWithParams, Func<bool> canExecute)
        {
            _executeWithParams = executeWithParams;
            _canExecute = canExecute;
        }

        public PersonCommand(Action<KeyEventArgs> executeWithKeyEventArgs, Func<bool> canExecute)
        {
            _executeWithKeyEventArgs = executeWithKeyEventArgs;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
            _executeWithParams?.Invoke((PersonModel) parameter);
            _executeWithKeyEventArgs?.Invoke((KeyEventArgs) parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
