using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class Product: IDataErrorInfo
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public int Priority { get; set; }

        public string this[string columnName]
        {
            get
            {
                var message = string.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            message = "Nazwa musi być wpisana!";
                        else if (Name.Length < 3)
                            message = "Nazwa musi mieć minimum 3 znaki!";
                        break;
                    case nameof(Price):
                        if ((Price < 0.1) || (Price > 1000))
                            message = "Cena musi być z zakresu od 0,10 do 1000";
                        break;
                };
                return message;
            }
        }

        public string Error => throw new NotImplementedException();
    }
}
