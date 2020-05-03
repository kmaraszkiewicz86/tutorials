using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkExample.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? CustomerId { get; set; }

        public int? ParentId { get; set; }

        public decimal Price { get; set; }

        public Customer Customer { get; set; }

        public Offer Parent { get; set; }

        public HashSet<Offer> Children { get; set; }

        public Offer()
        {

        }

        public Offer(int id, string name, int? customerId, int? parentId, decimal price)
        {
            Id = id;
            Name = name;
            CustomerId = customerId;
            ParentId = parentId;
            Price = price;
        }
    }
}
