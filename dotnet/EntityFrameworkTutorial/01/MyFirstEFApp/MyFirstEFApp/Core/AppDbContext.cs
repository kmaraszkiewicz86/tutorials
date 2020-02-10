using Microsoft.EntityFrameworkCore;
using MyFirstEFApp.Models;

namespace MyFirstEFApp.Core
{
    public class AppDbContext: DbContext
    {
        private const string ConnectionString =
            "Server=127.0.0.1,1433;Database=entityTutorial01;User Id=SA;Password=grubson2020@123";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Book> Books { get; set; }
    }
}