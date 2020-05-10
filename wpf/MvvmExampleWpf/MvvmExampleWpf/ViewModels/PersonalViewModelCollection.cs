using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using MvvmExampleWpf.Commands;
using MvvmExampleWpf.Models;

namespace MvvmExampleWpf.ViewModels
{
    public class PersonalViewModelCollection: BaseViewModel, IDataErrorInfo
    {

        private PersonModel _personModelItem;

        public ObservableCollection<PersonModel> PersonModels
        {
            get => _personModels;
            set
            {
                _personModels = value;
                OnPropertyChanged(nameof(PersonModels));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    AddCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    AddCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private string _name;

        private int _age;

        private PersonCommand _addCommand;

        private PersonCommand _generateAllCommand;

        private PersonCommand _keyPressCommand;

        private ObservableCollection<PersonModel> _personModels;

        public bool CanExecuteAddCommand() => !string.IsNullOrWhiteSpace(Name) && Age > 0;
        public bool CanExecuteGenerateAllCommand() => PersonModels.Count == 0;

        public PersonCommand AddCommand
        {
            get => _addCommand;
            private set => _addCommand = value;
        }

        public PersonCommand GenerateAllCommand
        {
            get => _generateAllCommand;
            private set => _generateAllCommand = value;
        }

        public PersonCommand KeyPressCommand
        {
            get => _generateAllCommand;
            private set => _generateAllCommand = value;
        }

        public PersonModel PersonModelItem
        {
            get => _personModelItem;
            set
            {
                _personModelItem = value;
                OnPropertyChanged(nameof(PersonModelItem));
            }
        }

        public PersonalViewModelCollection()
        {
            AddCommand = new PersonCommand(AddPerson, CanExecuteAddCommand);
            GenerateAllCommand = new PersonCommand(GenerateItems, CanExecuteGenerateAllCommand);
            KeyPressCommand = new PersonCommand(Window_KeyUp, () => true);

            PersonModels = new ObservableCollection<PersonModel>();
        }

        public void GenerateItems()
        {
            PersonModels = new ObservableCollection<PersonModel>
            {
                new PersonModel("Jarek", 13),
                new PersonModel("Jan", 3),
                new PersonModel("Julia", 15),
                new PersonModel("Patryk", 10),
                new PersonModel("Iwona", 12),
                new PersonModel("Paulina", 3),
                new PersonModel("Radosław", 7),
                new PersonModel("Piotr", 9),
            };

            GenerateAllCommand.RaiseCanExecuteChanged();
        }

        public void AddPerson(PersonModel personModel)
        {
            PersonModels.Add(new PersonModel(Name, Age));

            PersonModelItem = new PersonModel(Name, Age);

            GenerateAllCommand.RaiseCanExecuteChanged();
        }

        public void Window_KeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBox.Show("Pressed escape button.");
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                var message = string.Empty;

                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            message = $"{Name} is required";
                        }
                        break;

                    case nameof(Age):
                        if (Age < 1 || Age > 100)
                        {
                            message = $"{Age} have to in range from 1 to 100";
                        }
                        break;
                }

                return message;
            }
        }
    }
}
