using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyFirstEFApp.Core;
using MyFirstEFApp.DTO;
using MyFirstEFApp.Models;

namespace MyFirstEFApp.Core
{
    public class QueryExamples
    {
        public static void Execute()
        {
            using var client = new AppDbContext();
            var book = client.Books.First();

            client.Entry(book).Collection(b => b.BookAuthors).Load();

            foreach (var bookAuthor in book.BookAuthors)
            {
                client.Entry(bookAuthor).Reference(b => b.Author).Load();
            }

            client.Entry(book).Collection(b => b.Reviews).Load();

            client.Entry(book).Reference(b => b.PriceOffer).Load();

            var result = client.Books
                .Select(p => new
                    {
                        p.Title,
                        p.Price,
                        NumReviews
                            = p.Reviews.Count,
                    }
                ).First();

            var book2 = client.Books.First();
            var numberRevs = client.Entry(book2).Collection(c => c.Reviews).Query().Count();
            var starRatings = client.Entry(book2).Collection(c => c.Reviews).Query().Select(x => x.NumStars).ToList();

            var book3 = client.Books.Select(p => new
            {
                p.BookId,
                p.Title,
                AuthorString = string.Join(", ",
                    p.BookAuthors.OrderBy(q => q.Order)
                        .Select(q => q.Author.Name))
            }).First();
        }
    }
}