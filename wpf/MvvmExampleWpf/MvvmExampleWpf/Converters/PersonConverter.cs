using System;
using System.Globalization;
using System.Windows.Data;
using MvvmExampleWpf.Models;

namespace MvvmExampleWpf.Converters
{
    public class PersonConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var person = value as PersonModel;

            if (person == null)
                return "No added item";

            return $"Added item => Name ({person.Name}); Age ({person.Age})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
