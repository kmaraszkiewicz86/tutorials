using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyFirstEFApp.Models;

namespace MyFirstEFApp.Core
{
    public class AppDbContext: DbContext
    {
        private const string ConnectionString =
            "Server=127.0.0.1,1433;Database=entityTutorial01;User Id=SA;Password=grubson2020@123";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
                "Server=127.0.0.1,1433;Database=entityTutorial01;User Id=SA;Password=grubson2020@123";

            optionsBuilder.UseSqlServer(connectionString);
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceOffer>().HasData(new List<PriceOffer>()
            {
                new PriceOffer
                {
                    PriceOfferId = 1,
                    NewPrice = 1,
                    PromotionalText = "test1"
                },
                new PriceOffer
                {
                    PriceOfferId = 2,
                    NewPrice = 2,
                    PromotionalText = "test2"
                }
            });
            
            modelBuilder.Entity<Author>().HasData(new List<Author>()
            {
                new Author
                {
                    AuthorId = 1,
                    Name = "Test1",
                    WebUrl = "http://www.test1.pl"
                }, 
                new Author
                {
                    AuthorId = 2,
                    Name = "test2",
                    WebUrl = "http://test2.pl"
                }
            });

            modelBuilder.Entity<Book>().HasData(new List<Book>()
            {
                new Book()
                {
                    BookId = 1,
                    Title = "test1",
                    Description = "test1",
                    Price = 1,
                    Pulblisher = "test1",
                    ImageUrl = "brak",
                    PublishedOn = DateTime.Now,
                    PriceOfferId = 1
                },
                new Book()
                {
                    BookId = 2,
                    Title = "test2",
                    Description = "test2",
                    Price = 2,
                    Pulblisher = "test2",
                    ImageUrl = "brak",
                    PublishedOn = DateTime.Now,
                    PriceOfferId = 2
                }
            });

            modelBuilder.Entity<BookAuthor>().HasData(new List<BookAuthor>
            {
                new BookAuthor
                {
                    BookAuthorId = 1,
                    AuthorId = 1,
                    BookId = 1,
                    Order = Encoding.UTF8.GetBytes("test1").First()
                },
                new BookAuthor
                {
                    BookAuthorId = 2,
                    AuthorId = 2,
                    BookId = 2,
                    Order = Encoding.UTF8.GetBytes("test2").First()
                }
            });

            modelBuilder.Entity<Review>().HasData(new List<Review>()
            {
                new Review
                {
                    ReviewId = 1,
                    Comment = "test1",
                    NumStars = 5,
                    VoterName = "test1",
                    BookId = 1
                },
                new Review
                {
                    ReviewId = 2,
                    Comment = "test2",
                    NumStars = 5,
                    VoterName = "test2",
                    BookId = 2
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}