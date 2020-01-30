using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.ConfigurationModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspNetCoreTutorial1.Models.EntityFramework
{
    public class TestDbContext : IdentityDbContext<AppUser>
    {
	    /// <summary>
	    ///     <para>
	    ///         Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> class using the specified options.
	    ///         The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
	    ///         configuration of the options.
	    ///     </para>
	    /// </summary>
	    /// <param name="options">The options for this context.</param>
	    public TestDbContext(DbContextOptions options) : base(options)
	    {
			
	    }

	    public DbSet<LogEntry> LogEntries { get; set; }
	    public DbSet<CarModel> CarModels { get; set; }
		public DbSet<CarTypeModel> CarTypeModels { get; set; }
	    public DbSet<DriverModel> DriverModels { get; set; }
	    public DbSet<DriverModelCarModel> DriverModelCarModels { get; set; }
    }
}
