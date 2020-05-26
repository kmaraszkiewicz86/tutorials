using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _products;

        private ObservableCollection<string> _warehouses = new ObservableCollection<string>()
            {
                "Katowice 1", "Katowice 2", "Gliwice 1"
            };

        public MainWindow()
        {
            InitializeComponent();
            CreateDataContext();
        }

        private void CreateDataContext()
        {
            _products = Product.GenerateItemsForProductListView();

            productsGrid.ItemsSource = _products;

            //warehousesDataGridComboBoxColumn.ItemsSource = _warehouses;

            CollectionViewSource.GetDefaultView(productsGrid.ItemsSource).GroupDescriptions.Add(new PropertyGroupDescription("Warehouse"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem != null)
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Wybierz zdjęcie";
                fileDialog.Filter = "Image files (*.jpg,*.png;*.jpeg)|*.jpg;*.png;*.jpeg|All files(*.*) | *.* ";
                var result = fileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    ((Product) productsGrid.SelectedItem).Image = fileDialog.FileName;
                    productsGrid.CommitEdit(DataGridEditingUnit.Cell, true);
                    productsGrid.CommitEdit();
                    CollectionViewSource.GetDefaultView(productsGrid.ItemsSource).Refresh();
                }
            }
        }
    }
}
