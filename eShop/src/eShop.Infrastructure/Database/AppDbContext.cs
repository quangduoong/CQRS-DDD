using eShop.Domain.Entities;
using eShop.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eShop.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;

    public IDbConnection Connection { get => Database.GetDbConnection(); }

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
