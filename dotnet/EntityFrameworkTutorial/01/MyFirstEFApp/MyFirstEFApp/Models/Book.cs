using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstEFApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Pulblisher { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        
        public ICollection<BookAuthor> BookAuthors { get; set; }

        public ICollection<Review> Reviews { get; set; }
        
        public int PriceOfferId { get; set; }

        [ForeignKey("PriceOfferId")]
        public PriceOffer PriceOffer { get; set; }
    }
}