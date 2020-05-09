using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelViewViewModelPattern.Annotations;

namespace ModelViewViewModelPattern
{
    public class Student: INotifyPropertyChanged
    {
        private string name;

        private string lastname;

        private int _startYearAtUniversity;



        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Lastname
        {
            get => lastname;
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        public int StartYearAtUniversity
        {
            get => _startYearAtUniversity;
            set
            {
                if (_startYearAtUniversity != value)
                {
                    _startYearAtUniversity = value;
                    OnPropertyChanged(nameof(StartYearAtUniversity));
                }
            }
        }

        public string TimeInUniversity =>
            (DateTime.Now.Year - StartYearAtUniversity).ToString();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
