using System;
using System.Collections.Generic;
using System.Globalization;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Services
{
    public class CustomerWithTotalPriceModelWriter: BaseTableWriter<CustomerWithTotalPriceModel>
    {
        public override IEnumerable<CustomerWithTotalPriceModel> Models
        {
            get => models;
            set => models = value;
        }

        public override void Print()
        {
            PrintLine();
            PrintRow("CustomerName", "TotalPrice");
            PrintLine();


            foreach (CustomerWithTotalPriceModel item in models)
            {
                PrintRow(item.Name, item.TotalPrice.ToString(CultureInfo.InvariantCulture));
            }

            PrintLine();
        }
    }
}
