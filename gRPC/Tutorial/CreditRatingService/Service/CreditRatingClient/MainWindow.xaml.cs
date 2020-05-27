using System.Windows;
using CreditRatingClient.ViewModels;

namespace CreditRatingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreditViewModel _creditViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _creditViewModel = new CreditViewModel();
            DoWork();
        }

        private void DoWork()
        {
            DataContext = _creditViewModel;

            _creditViewModel.ChangeValue();
        }
    }
}
