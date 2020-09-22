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
            optionsBuilder.UseSqlServer(
                "Server=WINDOWSIZABELAM;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DbSet<AdditionalName> AdditionalNames { get; set; }

        public DbSet<Puppy> Puppies { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
