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

        public Customer Customer { get; set; }

        public Offer Parent { get; set; }

        public HashSet<Offer> Children { get; set; }

        public Offer()
        {

        }

        public Offer(int id, string name, int? customerId, int? parentId)
        {
            Id = id;
            Name = name;
            CustomerId = customerId;
            ParentId = parentId;
        }
    }
}
