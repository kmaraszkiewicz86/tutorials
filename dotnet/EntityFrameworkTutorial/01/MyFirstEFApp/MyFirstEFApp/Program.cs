using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyFirstEFApp.Core;
using MyFirstEFApp.Models;

namespace MyFirstEFApp
{
    class Program
    {
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}