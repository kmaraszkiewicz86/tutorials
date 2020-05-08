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
        public string Image { get; set; }

        public string Desc { get; set; }

        public Product(string image, string desc)
        {
            Image = image;
            Desc = desc;
        }

        public Product(string symbol, string name, int itemCount, string warehouse, string image, string desc)
        {
            Symbol = symbol;
            Name = name;
            ItemCount = itemCount;
            Warehouse = warehouse;
            Image = image;
            Desc = desc;
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
                new Product("DZ-10", "długopis żelowy", 4, "Katowice", "Images/dlugopis_zelowy.jpeg", "dlugopis_zelowy"),
                new Product("DZ-11", "długopis normalny", 8, "Poznań", "Images/dlugopis_kulkowy.jpg", "dlugopis_kulkowy"),
                new Product("DZ-13", "ołówek", 1, "Warszawa", "Images/olowek.jpg", "olowek"),
                new Product("DZ-15", "wieczne pioro", 5, "Szczecin", "Images/piorowieczne.jpg", "piorowieczne"),
                new Product("DZ-15", "wieczne pioro", 5, "Szczecin", "Images/piorowieczne.jpg", "piorowieczne"),
                new Product("DZ-15", "wieczne pioro", 5, "Szczecin", "Images/piorowieczne.jpg", "piorowieczne"),
                new Product("DZ-15", "wieczne pioro", 5, "Szczecin", "Images/piorowieczne.jpg", "piorowieczne")
            };
        }
    }
}
