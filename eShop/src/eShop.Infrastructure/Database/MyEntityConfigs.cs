using eShop.Domain.Entities;
using eShop.Domain.Shared;
using eShop.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure.Database;

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

        modelBuilder.Entity<OutboxMessage>(en =>
        {
            en.ToTable("outbox_message");
            en.HasKey(prop => prop.Id);
            en.Property(prop => prop.Type).HasMaxLength(255).IsRequired(true);
            en.Property(prop => prop.Content).IsRequired(true);
            en.Property(prop => prop.OccurredOnUtc).IsRequired(false);
            en.Property(prop => prop.ProcessedOnUtc).IsRequired(false);
            en.Property(prop => prop.Error).HasMaxLength(255).IsRequired(false);
        });

        modelBuilder.Entity<PriceCurrency>().HasData(new AvailableCurrencies().GetAll());
    }
}
