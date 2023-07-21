using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace eShop.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<PriceCurrency> PriceCurrencies { get; set; }

    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddMyEntityConfigs();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        string? dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
        string? dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

        if (string.IsNullOrEmpty(dbHost) ||
            string.IsNullOrEmpty(dbPort) ||
            string.IsNullOrEmpty(dbName) ||
            string.IsNullOrEmpty(dbPassword))
        {
            throw EnvironmentVariableException.NotAvailable();
        }

        //// For EF migrations
        //string connectionString = $"Server=localhost;Port=18001;Database=eshop;Uid=root;Pwd=mysql_pa55w0rd!";

        string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid=root;Pwd={dbPassword}";

        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .UseSnakeCaseNamingConvention();
    }
}
