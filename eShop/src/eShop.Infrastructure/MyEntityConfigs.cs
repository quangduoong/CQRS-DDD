using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShop.Infrastructure;

public static class MyEntityConfigs
{
    public static void AddMyEntityConfigs(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(en =>
        {
            en.ToTable("products");
            en.HasKey(prop => prop.Id);
            en.Property(prop => prop.Name).HasColumnType("varchar(40)").IsRequired(true);
            en.Property(prop => prop.Sku).HasPrecision(3).IsRequired(true);
            en.Property(prop => prop.PriceAmount).HasPrecision(8).IsRequired(true);
        }
        );

        modelBuilder.Entity<Product>()
            .HasOne(product => product.PriceCurrency)
            .WithMany()
            .HasForeignKey(product => product.PriceCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PriceCurrency>(en =>
        {
            en.ToTable("price_currency");
            en.HasKey(prop => prop.Id);
            en.Property(prop => prop.Name).HasMaxLength(3).IsRequired(true);
            en.Property(prop => prop.Description).IsRequired(true);
        });

        StreamReader streamReader = new("../../currencies.json");
        string json = streamReader.ReadToEnd();
        List<PriceCurrency> priceCurrencies = JsonSerializer.Deserialize<List<PriceCurrency>>(json)!;

        modelBuilder.Entity<PriceCurrency>().HasData(priceCurrencies);
    }

}
