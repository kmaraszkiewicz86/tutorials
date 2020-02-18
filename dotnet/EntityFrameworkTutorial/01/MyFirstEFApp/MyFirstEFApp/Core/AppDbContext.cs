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
            base.OnModelCreating(modelBuilder);
        }
    }
}