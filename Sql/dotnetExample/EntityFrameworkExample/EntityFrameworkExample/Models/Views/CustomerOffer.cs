using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExample.Models.Views
{
    public class CustomerOffer
    {
        public int? CustomerId { get; set; }

        public string Customer { get; set; }

        public int? OfferId { get; set; }

        public string Offer { get; set; }
    }
}

