using Microsoft.EntityFrameworkCore;

namespace TodoDataModel.DatabaseModel
{
	/// <summary>
	/// You can generate model from database using nuget command ex:
	/// Scaffold-DbContext "Server=STACJONARNY\SQLEXPRESS;Database=Todo;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -Output DatabaseModel -Verbose
	/// You must add to *.csproj file:
	/// <PropertyGroup>
	///		<GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
	///</PropertyGroup>
	/// https://github.com/aspnet/EntityFrameworkCore/issues/10473
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
	public partial class TodoContext : DbContext
    {
        public virtual DbSet<Todo> Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=STACJONARNY\SQLEXPRESS;Database=Todo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.CreateTime)
					.IsRequired()
					.HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

				entity.Property(e => e.Completed)
					.IsRequired();
            });
        }
    }
}
