using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<PriceCurrency> PriceCurrencies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddMyEntityConfigs();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
