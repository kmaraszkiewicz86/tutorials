using System;
using System.Collections.Generic;
using EntityFrameworkExample.Models;
using EntityFrameworkExample.Models.Views;

namespace EntityFrameworkExample.Services
{
    public class CustomerOfferTableWriter : BaseTableWriter<CustomerOffer>
    {
        public override IEnumerable<CustomerOffer> Models
        {
            get => models;
            set => models = value;
        }

        public override void Print()
        {
            PrintLine();
            PrintRow("CustomerId", "Customer", "OfferId", "Offer");
            PrintLine();


            foreach (CustomerOffer row in models)
            {
                PrintRow(row.CustomerId?.ToString() ?? "null",
                    string.IsNullOrEmpty(row.Customer) ? "null" : row.Customer, 
                    row.OfferId?.ToString() ?? "null", 
                    string.IsNullOrEmpty(row.Offer) ? "null" : row.Offer);
            }

            PrintLine();
        }
    }
}
