﻿// <auto-generated />
using System;
using EntityFrameworkRelationshipsTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkRelationshipsTesting.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201006132655_without_lengths")]
    partial class without_lengths
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DogId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.DogBreeder", b =>
                {
                    b.Property<int>("DogBreederId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DogBreederId");

                    b.HasIndex("DogId")
                        .IsUnique();

                    b.ToTable("DogBreeders");
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.DogOwner", b =>
                {
                    b.Property<int>("DogOwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DogOwnerId");

                    b.ToTable("DogOwners");
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.DogOwnerDog", b =>
                {
                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<int>("DogOwnerId")
                        .HasColumnType("int");

                    b.HasKey("DogId", "DogOwnerId");

                    b.HasIndex("DogOwnerId");

                    b.ToTable("DogOwnerDogs");
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.Puppy", b =>
                {
                    b.Property<int>("PuppyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DogId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PuppyId");

                    b.HasIndex("DogId");

                    b.ToTable("Puppies");
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.DogBreeder", b =>
                {
                    b.HasOne("EntityFrameworkRelationshipsTesting.Entities.Dog", null)
                        .WithOne("DogBreeder")
                        .HasForeignKey("EntityFrameworkRelationshipsTesting.Entities.DogBreeder", "DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.DogOwnerDog", b =>
                {
                    b.HasOne("EntityFrameworkRelationshipsTesting.Entities.Dog", "Dog")
                        .WithMany("DogOwnerDogs")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkRelationshipsTesting.Entities.DogOwner", "DogOwner")
                        .WithMany("DogOwnerDogs")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFrameworkRelationshipsTesting.Entities.Puppy", b =>
                {
                    b.HasOne("EntityFrameworkRelationshipsTesting.Entities.Dog", "Dog")
                        .WithMany("Puppies")
                        .HasForeignKey("DogId");
                });
#pragma warning restore 612, 618
        }
    }
}
