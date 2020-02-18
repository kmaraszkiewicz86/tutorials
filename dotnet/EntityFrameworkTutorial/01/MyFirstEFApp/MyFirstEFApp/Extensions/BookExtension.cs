using System.Linq;
using MyFirstEFApp.DTO;
using MyFirstEFApp.Models;

namespace MyFirstEFApp.Extensions
{
    public static class BookExtension
    {
        public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books)
        {
            return books.Select(p => new BookListDto
            {
                BookId = p.BookId,
                Title = p.Title,
                PublishedOn = p.PublishedOn,
                ActualPrice = p.PriceOffer == null
                              ? p.Price
                              : p.PriceOffer.NewPrice,
                PromotionPromotionalText = p.PriceOffer == null
                                           ? null
                                           : p.PriceOffer.PromotionalText,
                AuthorsOrdered = string.Join(", ",
                    p.BookAuthors.OrderBy(q => q.Order)
                        .Select(q => q.Author.Name)),
                ReviewsCount =  p.Reviews.Count,
                ReviewsAverageVotes = p.Reviews.Select(y => (double?)y.NumStars).Average()
            });
        }
    }
}