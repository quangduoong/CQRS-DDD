using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(en =>
        {
            en.ToTable("products");
            en.HasKey(prop => prop.Id);
            en.Property(prop => prop.Name).HasColumnType("varchar(40)").IsRequired(true);
            en.Property(prop => prop.Sku).HasPrecision(3).IsRequired(true);
            en.Property(prop => prop.PriceAmount).HasPrecision(8).IsRequired(true);
            en.Property(prop => prop.PriceAmount).IsRequired(true);
        }
        );
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
