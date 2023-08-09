using eShop.Application.Profiles;
using eShop.Domain.Abstractions;
using eShop.Domain.Exceptions;
using eShop.Infrastructure;
using eShop.Infrastructure.BackgroundJobs;
using eShop.Infrastructure.Interceptors;
using eShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;

namespace eShop.Api.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, string envName)
    {
        services.AddScoped<ConvertFromDomainEventToOutboxMessageInterceptor>();

        if (!envName.Equals("Test"))
            AddDbContext(services);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.Decorate<IProductRepository, DistributedCacheProductRepository>();
        services.AddAutoMapper(new Assembly[] {
                typeof(ProductProfile).GetTypeInfo().Assembly });

        services.AddQuartz(config =>
        {
            JobKey jobKey = new(nameof(ProcessOutboxMessageJob));

            config
                .AddJob<ProcessOutboxMessageJob>(jobKey)
                .AddTrigger(trigger =>
                {
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                        {
                            schedule.WithIntervalInMinutes(5).RepeatForever();
                        });
                });
        });
        services.AddQuartzHostedService();
    }

    private static void AddDbContext(IServiceCollection services)
    {
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

        //// For when adding migration
        // string connectionString = $"Server=localhost;Port=18001;Database=eshop;Uid=root;Pwd=mysql_pa55w0rd!";

        string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid=root;Pwd={dbPassword}";

        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            var interceptor = sp.GetService<ConvertFromDomainEventToOutboxMessageInterceptor>();

            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .AddInterceptors(interceptor!);
        });
    }
}

