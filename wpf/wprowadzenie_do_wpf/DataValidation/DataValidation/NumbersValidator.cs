using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp
{
    class NumbersValidator : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            double numberWasValidated = 0;
            try
            {
                if (value.ToString().Length > 0)
                    numberWasValidated = Double.Parse(value.ToString());
            }
            catch (FormatException e)
            {
                return new ValidationResult(false, "Not allowed characters - " +
                                                   e.Message);
            }

            if (numberWasValidated < Min || numberWasValidated > Max)
            {
                return new ValidationResult(false, "Provide number in range: " +
                                                   Min + " - " + Max);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
