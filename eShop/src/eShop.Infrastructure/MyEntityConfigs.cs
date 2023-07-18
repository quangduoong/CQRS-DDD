using eShop.Domain.Entities;
using eShop.Domain.Shared;
using Microsoft.EntityFrameworkCore;

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
        });

        modelBuilder.Entity<Product>()
            .HasOne(product => product.PriceCurrency)
            .WithMany(priceCurrency => priceCurrency.Products)
            .HasForeignKey(product => product.PriceCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PriceCurrency>(en =>
        {
            en.ToTable("price_currency");
            en.HasKey(prop => prop.Id);
            en.Property(prop => prop.Name).HasMaxLength(3).IsRequired(true);
            en.Property(prop => prop.Description).IsRequired(true);
        });

        List<PriceCurrency> priceCurrencies = new AvailableCurrencies().Values;

        modelBuilder.Entity<PriceCurrency>().HasData(priceCurrencies);
    }
}
