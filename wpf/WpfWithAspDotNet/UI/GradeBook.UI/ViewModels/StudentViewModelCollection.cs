using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using GradeBook.Core.Models;
using GradeBook.UI.Commands;
using GradeBook.UI.Services.Interfaces;

namespace GradeBook.UI.ViewModels
{
    public class StudentViewModelCollection: BaseViewModel, IDataErrorInfo
    {
        #region services

        private readonly IStudentService _studentService;

        #endregion

        #region commands

        public InsertNewCommand InsertNewCommand
        {
            get => _insertNewCommand;
            set => _insertNewCommand = value;
        }

        private InsertNewCommand _insertNewCommand;

        public LoadedWindowCommand LoadedWindowCommand
        {
            get => _loadedWindowCommand;
            set => _loadedWindowCommand = value;
        }

        private LoadedWindowCommand _loadedWindowCommand;

        private bool CanExecute =>
            IsValidByColumnName(nameof(Name), 100)
            && IsValidByColumnName(nameof(Lastname), 150);

        #endregion

        #region bindings

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    InsertNewCommand?.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _name;

        public string Lastname
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    InsertNewCommand?.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        private string _lastname;

        private ObservableCollection<StudentModel> _studentModels;

        public ObservableCollection<StudentModel> StudentModels
        {
            get => _studentModels;
            set
            {
                _studentModels = value;
                OnPropertyChanged(nameof(StudentModels));
            }
        }

        #endregion

        public StudentViewModelCollection(IStudentService studentService)
        {
            _studentService = studentService;
            LoadedWindowCommand = new LoadedWindowCommand(FetchAllDataFromService);

            InsertNewCommand = new InsertNewCommand(InsertNewData, () => CanExecute);
        }

        #region service actions

        private void FetchAllDataFromService()
        {
            StudentModels = new ObservableCollection<StudentModel>(_studentService.GetAll());
        }

        private void InsertNewData()
        {
            StudentModels.Add(new StudentModel
            {
                Name = Name,
                Lastname = Lastname,
            });
        }

        #endregion

        #region IDataErrorInfo implemntations

        public string this[string columnName]
        {
            get
            {
                var maxLength = 0;

                switch (columnName)
                {
                    case nameof(Name):
                        maxLength = 100;
                        break;

                    case nameof(Lastname):
                        maxLength = 150;
                        break;
                }

                return GetErrorByColumnName(columnName, maxLength);
            }
        }

        private bool IsValidByColumnName(string columnName, int maxLength)
        {
            var field = GetType().GetProperty(columnName).GetValue(this)?.ToString();

            return !string.IsNullOrWhiteSpace(field) && field.Length <= maxLength;
        }

        private string GetErrorByColumnName(string columnName, int maxLength)
        {
            var field = GetType().GetProperty(columnName).GetValue(this)?.ToString();

            if (string.IsNullOrWhiteSpace(field))
            {
                return $"{columnName} is required";
            }
            else if (field.Length > maxLength)
            {
                return $"Maximum length of {columnName} is 100";
            }

            return string.Empty;
        }

        public string Error
        {
            get
            {
                var stringBuilder = new StringBuilder();

                if (IsValidByColumnName(nameof(Name), 100))
                    stringBuilder.AppendLine(GetErrorByColumnName(nameof(Name), 100));

                if (IsValidByColumnName(nameof(Lastname), 150))
                    stringBuilder.AppendLine(GetErrorByColumnName(nameof(Lastname), 150));

                return stringBuilder.ToString();
            }
        }

        #endregion
    }
}
