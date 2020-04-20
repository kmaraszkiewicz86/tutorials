using System;
using System.Collections.Generic;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Services
{
    public class CustomerTableWriter: BaseTableWriter<Customer>
    {
        public override IEnumerable<Customer> Models
        {
            get => models;
            set => models = value;
        }

        public override void Print()
        {
            PrintLine();
            PrintRow("CustomerId", "Customer", "OfferId", "Offer");
            PrintLine();


            foreach (Customer customer in models)
            {
                foreach (var offer in customer.Offers)
                {
                    PrintRow(customer.Id.ToString(), customer.Name, offer.Id.ToString(), offer.Name);
                }
            }

            PrintLine();
        }
    }
}
