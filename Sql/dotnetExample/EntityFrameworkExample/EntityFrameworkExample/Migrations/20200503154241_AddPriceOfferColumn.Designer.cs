﻿// <auto-generated />
using System;
using EntityFrameworkExample.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkExample.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200503154241_AddPriceOfferColumn")]
    partial class AddPriceOfferColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityFrameworkExample.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kevin Costner"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Akshay Kumar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sean Connery"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sanjay Dutt"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sharukh Khan"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Lilo Liloviskow"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Mailo Mailovich"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Szarko Szarkovich"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Izka Mariskovich"
                        });
                });

            modelBuilder.Entity("EntityFrameworkExample.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ParentId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Offer1",
                            Price = 0.11m
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 1,
                            Name = "Offer2",
                            Price = 0.23m
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 2,
                            Name = "Offer3",
                            Price = 0.45m
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 5,
                            Name = "Offer4",
                            Price = 1.11m
                        },
                        new
                        {
                            Id = 5,
                            CustomerId = 6,
                            Name = "Offer4",
                            Price = 2.15m
                        },
                        new
                        {
                            Id = 6,
                            CustomerId = 7,
                            Name = "Offer4",
                            Price = 8.88m
                        },
                        new
                        {
                            Id = 7,
                            CustomerId = 8,
                            Name = "Offer4",
                            Price = 7.11m
                        },
                        new
                        {
                            Id = 8,
                            Name = "Offer1.1",
                            ParentId = 1,
                            Price = 9.01m
                        },
                        new
                        {
                            Id = 9,
                            Name = "Offer2.1",
                            ParentId = 2,
                            Price = 0.01m
                        },
                        new
                        {
                            Id = 10,
                            Name = "Offer3.1",
                            ParentId = 3,
                            Price = 0.06m
                        },
                        new
                        {
                            Id = 11,
                            CustomerId = 8,
                            Name = "Offer4",
                            Price = 0.58m
                        },
                        new
                        {
                            Id = 12,
                            CustomerId = 8,
                            Name = "Offer4",
                            Price = 0.78m
                        },
                        new
                        {
                            Id = 13,
                            CustomerId = 8,
                            Name = "Offer4",
                            Price = 5.8m
                        },
                        new
                        {
                            Id = 14,
                            CustomerId = 8,
                            Name = "Offer4",
                            Price = 0.23m
                        });
                });

            modelBuilder.Entity("EntityFrameworkExample.Models.Offer", b =>
                {
                    b.HasOne("EntityFrameworkExample.Models.Customer", "Customer")
                        .WithMany("Offers")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EntityFrameworkExample.Models.Offer", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}