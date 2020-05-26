using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreditRatingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Result
        {
            public string Test { get; set; } = "Brak danych";
        }

        public MainWindow()
        {
            InitializeComponent();
            DoWork();
        }

        private void DoWork()
        {
            var testInstance = new Result()
            {
                Test = "TEstowow"
            };

            DataContext = testInstance;
        }
    }
}
