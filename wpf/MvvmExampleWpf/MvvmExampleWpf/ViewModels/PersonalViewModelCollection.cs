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

        private PersonAddCommand _addCommand;

        private GenerateDataCommand _generateAllCommand;

        private KeyUpCommand _keyPressCommand;

        private ObservableCollection<PersonModel> _personModels;

        public bool CanExecuteAddCommand() => !string.IsNullOrWhiteSpace(Name) && (Age > 0 && Age <= 100);
        public bool CanExecuteGenerateAllCommand() => PersonModels.Count == 0;

        public PersonAddCommand AddCommand
        {
            get => _addCommand;
            private set => _addCommand = value;
        }

        public GenerateDataCommand GenerateAllCommand
        {
            get => _generateAllCommand;
            private set => _generateAllCommand = value;
        }

        public KeyUpCommand KeyPressCommand
        {
            get => _keyPressCommand;
            private set => _keyPressCommand = value;
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
            AddCommand = new PersonAddCommand(AddPerson, CanExecuteAddCommand);
            GenerateAllCommand = new GenerateDataCommand(GenerateItems, CanExecuteGenerateAllCommand);
            KeyPressCommand = new KeyUpCommand(Window_KeyUp);

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
