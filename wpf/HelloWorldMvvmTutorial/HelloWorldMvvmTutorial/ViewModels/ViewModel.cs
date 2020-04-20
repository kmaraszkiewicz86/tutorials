using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HelloWorldMvvmTutorial.Annotations;

namespace HelloWorldMvvmTutorial.ViewModels
{
    public class ViewModel: INotifyPropertyChanged
    {
        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                Output = _input;
                OnPropertyChanged("Input");
            }
        }

        private string _output;

        public string Output
        {
            get { return _output; }
            set
            {
                _output = value + " MVVM World";
                OnPropertyChanged("Output");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
