using EntityFrameworkExample.Helpers;
using EntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Core
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        private Customer[] CustomerData => new Customer[]
        {
            new Customer(1, "Kevin Costner"),
            new Customer(2, "Akshay Kumar"),
            new Customer(3, "Sean Connery"),
            new Customer(4, "Sanjay Dutt"),
            new Customer(5, "Sharukh Khan"),
            new Customer(6, "Lilo Liloviskow"),
            new Customer(7, "Mailo Mailovich"),
            new Customer(8, "Szarko Szarkovich"),
            new Customer(9, "Izka Mariskovich")
        };

        private Offer[] OfferData => new Offer[]
        {
            new Offer(1, "Offer1", null, null),
            new Offer(2, "Offer2", 1, null),
            new Offer(3, "Offer3", 2, null),
            new Offer(4, "Offer4", 5, null),
            new Offer(5, "Offer4", 6, null),
            new Offer(6, "Offer4", 7, null),
            new Offer(7, "Offer4", 8, null),
            new Offer(8, "Offer1.1", null, 1),
            new Offer(9, "Offer2.1", null, 2),
            new Offer(10, "Offer3.1", null, 3)
        };

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationBuilderHelper.GetDefaultConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(CustomerData);
            modelBuilder.Entity<Offer>().HasData(OfferData);
        }
    }
}
