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

            //optionsBuilder.UseSqlServer(
            //    "Server=127.0.0.1;Database=EntityFrameworkRelationshipsTesting;User Id=SA;Password=Grubson@2020");

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-RSB5ESB;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

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

        public DbSet<GetPupiesWithParentDogName> GetPupiesWithParentDogs { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogOwnerDog>().HasKey(d => new { d.DogId, d.DogOwnerId });
            modelBuilder.Entity<GetPupiesWithParentDogName>().HasNoKey().ToView("GetPupiesWithParentDogName");

            modelBuilder.Entity<Dog>().HasData(new[]
            {
                new Dog { DogId = 1, Name = "Mailo" },
                new Dog { DogId = 2, Name = "Lilo" },
                new Dog { DogId = 3, Name = "Szarlo" },
                new Dog { DogId = 4, Name = "Izka" }
            });

            modelBuilder.Entity<Puppy>().HasData(new[]
            {
                new Puppy { PuppyId = 1, Name = "Mailo1", DogId = 1 },
                new Puppy { PuppyId = 2, Name = "Mailo2", DogId = 1 },
                new Puppy { PuppyId = 3, Name = "Lilo1", DogId = 2 },
                new Puppy { PuppyId = 4, Name = "Lilo2", DogId = 2 },
                new Puppy { PuppyId = 5, Name = "Lilo3", DogId = 2 },
                new Puppy { PuppyId = 6, Name = "Szarlo1", DogId = 3 },
                new Puppy { PuppyId = 7, Name = "Szarlo2", DogId = 3 },
                new Puppy { PuppyId = 8, Name = "Szarlo3", DogId = 3 }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
