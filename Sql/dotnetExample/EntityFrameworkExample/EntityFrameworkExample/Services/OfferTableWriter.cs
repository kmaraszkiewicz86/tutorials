using System;
using System.Collections.Generic;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Services
{
    public class OfferTableWriter : BaseTableWriter<Offer>
    {
        public override IEnumerable<Offer> Models
        {
            get => models;
            set => models = value;
        }

        public override void Print()
        {
            Console.Clear();
            PrintLine();
            PrintRow("ParentOfferId", "ParentOffer", "CustomerId", "Customer", "ChildOfferId", "ChildOffer");
            PrintLine();


            foreach (Offer offer in models)
            {
                foreach(Offer child in offer.Children)
                {
                    PrintRow(offer.Id.ToString(),
                        offer.Name,
                        offer.Customer?.Id.ToString() ?? "null",
                        offer.Customer?.Name.ToString() ?? "null",
                        child.Id.ToString(),
                        child.Name);
                }
            }

            PrintLine();
        }
    }
}
