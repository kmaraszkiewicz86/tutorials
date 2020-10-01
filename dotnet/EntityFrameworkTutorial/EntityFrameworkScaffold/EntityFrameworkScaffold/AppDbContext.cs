using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkScaffold
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DogBreeders> DogBreeders { get; set; }
        public virtual DbSet<DogOwnerDogs> DogOwnerDogs { get; set; }
        public virtual DbSet<DogOwners> DogOwners { get; set; }
        public virtual DbSet<Dogs> Dogs { get; set; }
        public virtual DbSet<Puppies> Puppies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=PLMFUL90017;Database=EntityFrameworkRelationshipsTesting;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogBreeders>(entity =>
            {
                entity.HasKey(e => e.DogBreederId);

                entity.HasIndex(e => e.DogId)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Dog)
                    .WithOne(p => p.DogBreeders)
                    .HasForeignKey<DogBreeders>(d => d.DogId);
            });

            modelBuilder.Entity<DogOwnerDogs>(entity =>
            {
                entity.HasKey(e => e.DogOwnerDogId);

                entity.HasIndex(e => e.DogId);

                entity.HasIndex(e => e.DogOwnerId);

                entity.HasOne(d => d.Dog)
                    .WithMany(p => p.DogOwnerDogs)
                    .HasForeignKey(d => d.DogId);

                entity.HasOne(d => d.DogOwner)
                    .WithMany(p => p.DogOwnerDogs)
                    .HasForeignKey(d => d.DogOwnerId);
            });

            modelBuilder.Entity<DogOwners>(entity =>
            {
                entity.HasKey(e => e.DogOwnerId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dogs>(entity =>
            {
                entity.HasKey(e => e.DogId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Puppies>(entity =>
            {
                entity.HasKey(e => e.PuppyId);

                entity.HasIndex(e => e.DogId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Dog)
                    .WithMany(p => p.Puppies)
                    .HasForeignKey(d => d.DogId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
