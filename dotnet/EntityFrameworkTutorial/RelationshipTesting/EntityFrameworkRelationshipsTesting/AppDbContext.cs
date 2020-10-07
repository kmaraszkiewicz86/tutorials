using EntityFrameworkRelationshipsTesting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkRelationshipsTesting
{
    public class AppDbContextDesign : IDesignTimeDbContextFactory<AppDbContext>
    {
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information).AddConsole();
            });

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging();
            //optionsBuilder.UseSqlServer(
            //    "Server=WINDOWSIZABELAM;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

            //optionsBuilder.UseSqlServer(
            //    @"Server=PLMFUL90017;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

            optionsBuilder.UseSqlServer(
                "Server=127.0.0.1;Database=EntityFrameworkRelationshipsTesting;User Id=SA;Password=Grubson@2020");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DbSet<DogOwnerDog> DogOwnerDogs { get; set; }

        public DbSet<DogOwner> DogOwners { get; set; }
        
        public DbSet<DogBreeder> DogBreeders { get; set; }

        public DbSet<Puppy> Puppies { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogOwnerDog>().HasKey(d => new { d.DogId, d.DogOwnerId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
