using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ListViewCrudSingleWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Product _newOrExistsProduct;

        private ObservableCollection<Product> _products;

        private CollectionView _productCollectionView;

        public MainWindow()
        {
            InitializeComponent();
            CreateDataContext();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            _newOrExistsProduct = new Product("DA-000", "", 0, "");
            productsListView.DataContext = _newOrExistsProduct;
            _products.Add(_newOrExistsProduct);
        }

        private void productsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _newOrExistsProduct = productsListView.SelectedItem as Product ?? new Product("DA-000", "", 0, "");
            productFormGrid.DataContext = _newOrExistsProduct;
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
                _productCollectionView.Refresh();
            }
        }

        private void CreateDataContext()
        {
            _products = Product.GenerateItemsForProductListView();
            _newOrExistsProduct = new Product("DA-000", "", 0, "");

            productsListView.ItemsSource = _products;
            productFormGrid.DataContext = _newOrExistsProduct;

            CreateSorting();
        }

        private void CreateSorting()
        {
            _productCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(productsListView.ItemsSource);
            _productCollectionView.SortDescriptions.Add(new SortDescription("Warehouse", ListSortDirection.Ascending));
            _productCollectionView.SortDescriptions.Add(new SortDescription("ItemCount", ListSortDirection.Ascending));
        }

        private Product FindProductItemFromButtonTag(object buttonInObject)
        {
            var button = (Button)buttonInObject;

            return _products.FirstOrDefault(
                p => p.Symbol.Equals(button.Tag.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
