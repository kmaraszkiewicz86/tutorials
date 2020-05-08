using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _products;

        private CollectionView _productListCollectionView;

        public MainWindow()
        {
            InitializeComponent();

            InitDataBinding();
        }

        private void InitDataBinding()
        {
            _products = Product.GenerateItemsForProductListView();
            productListView.ItemsSource = _products;

            _productListCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(
                productListView.ItemsSource);

            GenerateSortingConfigurationForProductListView();


            //Add filter options
            _productListCollectionView.Filter = UserFilter;
        }

        /// <summary>
        /// Add some sorting configuration
        /// </summary>
        private void GenerateSortingConfigurationForProductListView()
        {
            _productListCollectionView.SortDescriptions
                .Add(new SortDescription("Warehouse", ListSortDirection.Ascending));
            _productListCollectionView.SortDescriptions
                .Add(new SortDescription("ItemCount", ListSortDirection.Ascending));
        }
        
        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(filterQueryTextBox.Text))
            {
                return true;
            }

            return ((Product) item).Warehouse.IndexOf(filterQueryTextBox.Text,
                StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Refresh the <see cref="productListView"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterQueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(
                productListView.ItemsSource).Refresh();
        }

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {
            productListView.SelectedItem = FindProductItemFromButtonTag(sender);

            RedirectToProductWindow();
        }

        private void productListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RedirectToProductWindow();
        }

        private void RedirectToProductWindow()
        {
            var productWindow = new ProductWindow(this);
            productWindow.ShowDialog();
        }

        private void deleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var itemToRemove = FindProductItemFromButtonTag(sender);

            if (itemToRemove == null)
                return;

            var result = MessageBox.Show($"Do really want to remove item {itemToRemove.Name}", "",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _products.Remove(itemToRemove);
                _productListCollectionView.Refresh();
            }
        }

        private Product FindProductItemFromButtonTag(object buttonInObject)
        {
            var button = (Button)buttonInObject;

            return _products.FirstOrDefault(
                p => p.Symbol.Equals(button.Tag.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var productWindow = new ProductWindow(this);
            productWindow.ShowDialog();
        }
    }
}
