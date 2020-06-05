using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace StackLayoutTutorial
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private int Count = 0;

        private List<string> _links;

        public List<string> Links
        {
            get => _links;
            set
            {
                _links = value;
                OnPropertyChanged(nameof(Links));
            }
        }

        private string _testString = "Empty Label";

        public string TestString
        {
            get => _testString;
            set
            {
                _testString = value;
                OnPropertyChanged(nameof(TestString));
            }
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));

                if (_selectedItem != null)
                {
                    NextPageCommand.Execute(_selectedItem);
                }
            }
        }

        private bool _canExecute;

        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                _canExecute = value;
                OnPropertyChanged(nameof(SayHelloCommand.CanExecute));
            }
        }

        public ICommand SayHelloCommand { get; }

        public ICommand NextPageCommand { get; }

        public ViewModel()
        {
            CanExecute = true;

            SayHelloCommand = new Command(() =>
            {
                TestString = $"Clicked {(++Count)} times";
                if (Count == 10)
                {
                    CanExecute = false;
                }
            },
            () =>
            {
                return CanExecute;

            });

            NextPageCommand = new Command(async () =>
            {
                var selectedItem = SelectedItem ?? Links[0];

                var page = new MySecondPage();
                ((MySecondPageViewModel)page.BindingContext).Title = Links[0];

                await App.Current.MainPage.Navigation.PushAsync(page);
            }, () => true);

            Links = new List<string>
            {
                "Test model"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
