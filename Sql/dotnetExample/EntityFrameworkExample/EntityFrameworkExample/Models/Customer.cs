using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public HashSet<Offer> Offers { get; set; }

        public Customer()
        {

        }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
