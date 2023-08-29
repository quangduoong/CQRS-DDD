﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eShop.Infrastructure.Database;

#nullable disable

namespace eShop.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230721092820_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("eShop.Domain.Entities.PriceCurrency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_price_currency");

                    b.ToTable("price_currency", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("1491857a-9602-44c4-bebb-80ef5e0ca81e"),
                            Description = "Vietnamese currency.",
                            Name = "VND"
                        },
                        new
                        {
                            Id = new Guid("e347c43a-a547-42be-b134-5874454109a5"),
                            Description = "United State's currency.",
                            Name = "USD"
                        });
                });

            modelBuilder.Entity("eShop.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("name");

                    b.Property<double>("PriceAmount")
                        .HasPrecision(8)
                        .HasColumnType("double")
                        .HasColumnName("price_amount");

                    b.Property<Guid>("PriceCurrencyId")
                        .HasColumnType("char(36)")
                        .HasColumnName("price_currency_id");

                    b.Property<int>("Sku")
                        .HasPrecision(3)
                        .HasColumnType("int")
                        .HasColumnName("sku");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("PriceCurrencyId")
                        .HasDatabaseName("ix_products_price_currency_id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("eShop.Domain.Entities.Product", b =>
                {
                    b.HasOne("eShop.Domain.Entities.PriceCurrency", "PriceCurrency")
                        .WithMany("Products")
                        .HasForeignKey("PriceCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_products_price_currencies_price_currency_id");

                    b.Navigation("PriceCurrency");
                });

            modelBuilder.Entity("eShop.Domain.Entities.PriceCurrency", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
