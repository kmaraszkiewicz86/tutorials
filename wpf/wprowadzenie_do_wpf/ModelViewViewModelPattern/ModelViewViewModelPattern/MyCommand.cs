using System;
using System.Windows.Input;

namespace ModelViewViewModelPattern
{
    public class MyCommand : ICommand
    {
        Action _execute;
        public MyCommand(Action executeMethod)
        {
            _execute = executeMethod;
        }
        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}
