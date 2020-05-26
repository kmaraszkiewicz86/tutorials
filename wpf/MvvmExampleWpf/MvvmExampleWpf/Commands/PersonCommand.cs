using System;
using System.Windows.Input;
using MvvmExampleWpf.Models;

namespace MvvmExampleWpf.Commands
{
    public class GenerateDataCommand: ICommand
    {
        private Action _execute;

        private Func<bool> _canExecute;

        public GenerateDataCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class PersonAddCommand: ICommand
    {
        private Action<PersonModel> _execute;

        private Func<bool> _canExecute;

        public PersonAddCommand(Action<PersonModel> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke((PersonModel) parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class KeyUpCommand : ICommand
    {
        private Action _execute;

        private Action<KeyEventArgs> _executeWithKeyEventArgs;

        public KeyUpCommand(Action<KeyEventArgs> executeWithKeyEventArgs)
        {
            _executeWithKeyEventArgs = executeWithKeyEventArgs;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeWithKeyEventArgs?.Invoke((KeyEventArgs)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}