using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class Product
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int ItemCount { get; set; }
        public string Warehouse { get; set; }

        public string Image { get; set; }

        public Product(string image)
        {
            Image = image;
        }

        public Product(string symbol, string name, int itemCount, string warehouse, string image)
        {
            Symbol = symbol;
            Name = name;
            ItemCount = itemCount;
            Warehouse = warehouse;
            Image = image;
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
                new Product("DZ-10", "długopis żelowy", 4, "Katowice", "Images/dlugopis_zelowy.jpeg"),
                new Product("DZ-11", "długopis normalny", 8, "Poznań", "Images/dlugopis_kulkowy.jpg"),
                new Product("DZ-13", "ołówek", 1, "Warszawa", "Images/olowek.jpg"),
                new Product("DZ-15", "wieczne pioro", 5, "Szczecin", "Images/piorowieczne.jpg")
            };
        }
    }
}
