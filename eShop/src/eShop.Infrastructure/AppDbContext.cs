using eShop.Domain.Entities;
using eShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        string connectionString = "Server=localhost;Port=3306;Database=eShop;Uid=root;Pwd=pa55w0rd!";
        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .UseSnakeCaseNamingConvention();
    }
}
