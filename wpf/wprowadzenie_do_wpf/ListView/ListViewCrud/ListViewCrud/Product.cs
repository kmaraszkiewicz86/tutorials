using System;
using System.Collections.ObjectModel;

namespace WpfApp
{
    public class Product
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int ItemCount { get; set; }
        public string Warehouse { get; set; }

        public Product()
        {
            
        }

        public Product(string symbol, string name, int itemCount, string warehouse)
        {
            Symbol = symbol;
            Name = name;
            ItemCount = itemCount;
            Warehouse = warehouse;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Symbol, Name, ItemCount,
                Warehouse);
        }

        public static ObservableCollection<Product> GenerateItemsForProductListView()
        {
            return new ObservableCollection<Product>
            {
                new Product("DZ-10", "długopis żelowy", 4, "Katowice"),
                new Product("DZ-11", "długopis normalny", 8, "Poznań"),
                new Product("DZ-12", "pisak 1", 4, "Warszawa"),
                new Product("DZ-13", "ołówek", 1, "Warszawa"),
                new Product("DZ-14", "gumka", 2, "Łódź"),
                new Product("DZ-15", "linijka", 5, "Szczecin")
            };
        }
    }
}
