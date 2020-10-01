using EntityFrameworkRelationshipsTesting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFrameworkRelationshipsTesting
{
    public class AppDbContextDesign : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(
            //    "Server=WINDOWSIZABELAM;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

            optionsBuilder.UseSqlServer(
                @"Server=PLMFUL90017;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

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

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogOwnerDog>().HasKey(d => new { d.DogId, d.DogOwnerId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
