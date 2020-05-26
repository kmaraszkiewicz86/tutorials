using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private MainWindow _mainWindow;

        public ProductWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            if (_mainWindow.productListView.SelectedItem == null)
            {
                productAddOrUpdateButton.Content = "Add";
            }

            productWindowGrid.DataContext = _mainWindow.productListView.SelectedItem ?? new Product();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            if (button.Content.ToString().Equals("Add", StringComparison.OrdinalIgnoreCase))
            {
                ((ObservableCollection<Product>)_mainWindow.productListView.ItemsSource)
                    .Add((Product)productWindowGrid.DataContext);
            }

            Close();
        }
    }
}
