﻿// <auto-generated />
using AspNetCoreTutorial1.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AspNetCoreTutorial1.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20180420155422_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspNetCoreTutorial1.Models.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("AspNetCoreTutorial1.Models.CarTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CarTypeModels");
                });

            modelBuilder.Entity("AspNetCoreTutorial1.Models.DriverModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DriverModels");
                });

            modelBuilder.Entity("AspNetCoreTutorial1.Models.DriverModelCarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarModelId");

                    b.Property<int>("DriverModelId");

                    b.HasKey("Id");

                    b.HasIndex("CarModelId");

                    b.HasIndex("DriverModelId");

                    b.ToTable("DriverModelCarModels");
                });

            modelBuilder.Entity("AspNetCoreTutorial1.Models.CarModel", b =>
                {
                    b.HasOne("AspNetCoreTutorial1.Models.CarTypeModel", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspNetCoreTutorial1.Models.DriverModelCarModel", b =>
                {
                    b.HasOne("AspNetCoreTutorial1.Models.CarModel", "CarModel")
                        .WithMany("DriverModels")
                        .HasForeignKey("CarModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspNetCoreTutorial1.Models.DriverModel", "DriverModel")
                        .WithMany()
                        .HasForeignKey("DriverModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}