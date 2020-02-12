namespace MyFirstEFApp.Models
{
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }

        public decimal NewPrice { get; set; }

        public string PromotionalText { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}