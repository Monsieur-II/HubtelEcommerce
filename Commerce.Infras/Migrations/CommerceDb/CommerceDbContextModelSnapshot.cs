﻿// <auto-generated />
using System;
using Commerce.Infras.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Commerce.Infras.Migrations.CommerceDb
{
    [DbContext(typeof(CommerceDbContext))]
    partial class CommerceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Commerce.Infras.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Commerce.Infras.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a1ff887-cdbe-4b84-98a4-3ded7e50fcbd"),
                            Description = "Breakfast Cereal",
                            Name = "Kellog's Cornflakes",
                            UnitPrice = 10.50m
                        },
                        new
                        {
                            Id = new Guid("28fa08d6-538c-4e44-a682-77c5cc661ad3"),
                            Description = "Chocolate Malt Drink",
                            Name = "Milo",
                            UnitPrice = 5.50m
                        },
                        new
                        {
                            Id = new Guid("a11346c6-b1ea-4095-8aa7-3e78ebff951d"),
                            Description = "Instant Coffee",
                            Name = "Nescafe",
                            UnitPrice = 7.50m
                        },
                        new
                        {
                            Id = new Guid("96bc5947-6b06-47ea-bfca-526b28d55535"),
                            Description = "Black Tea",
                            Name = "Lipton Tea",
                            UnitPrice = 3.50m
                        },
                        new
                        {
                            Id = new Guid("fb5df4c2-4439-4b65-8de8-cc2e3305643b"),
                            Description = "Soft Drink",
                            Name = "Coca Cola",
                            UnitPrice = 2.50m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
