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
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private int Count = 0;

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

        private bool _canExecute;

        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                _canExecute = value;
                OnPropertyChanged(nameof(CanExecute));
            }
        }

        public ICommand SayHelloCommand { get; }

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
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
