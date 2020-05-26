using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ModelViewViewModelPattern.Annotations;

namespace ModelViewViewModelPattern
{
    public class StudentViewModel: INotifyPropertyChanged
    {
        private Student _student;

        public event PropertyChangedEventHandler PropertyChanged;

        public Student Student
        {
            get => _student;
            set
            {
                _student = value;
                OnPropertyChanged(nameof(Student));
            }
        }

        public MyCommand ClearDataCommand { get; set; }

        public StudentViewModel()
        {
            Student = new Student
            {
                Name = "Jan",
                Lastname = "Kowalski",
                StartYearAtUniversity = 2014
            };
            ClearDataCommand = new MyCommand(ClearData);
        }
        private void ClearData()
        {
            if (MessageBox.Show("Do you really clear student data?", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                Student.Name = String.Empty;
                Student.Lastname = String.Empty;
                Student.StartYearAtUniversity = DateTime.Now.Year;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
