using eShop.Domain.Exceptions;
using eShop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eShop.Api.Configurations;

public static class MyDbConfig
{
    public static void AddMyDbConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

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

        string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid=root;Pwd={dbPassword}";

        services.AddDbContext<AppDbContext>(opt =>
           {
               opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
           });
    }
}
