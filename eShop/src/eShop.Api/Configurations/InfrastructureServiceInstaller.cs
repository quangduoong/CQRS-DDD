using eShop.Application.Profiles;
using eShop.Domain.Abstractions;
using eShop.Domain.Exceptions;
using eShop.Domain.Shared;
using eShop.Infrastructure.BackgroundJobs;
using eShop.Infrastructure.Database;
using eShop.Infrastructure.Repositories;
using eShop.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;

namespace eShop.Api.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, string envName)
    {
        if (!envName.Equals("Test"))
            AddDbContext(services, configuration);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        if (!envName.Equals("Test"))
            services.Decorate<IProductRepository, DistributedCacheProductRepository>();

        services.AddAutoMapper(new Assembly[] {
                typeof(ProductProfile).GetTypeInfo().Assembly });

        if (!envName.Equals("Test"))
        {
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
                                schedule.WithIntervalInMinutes(1).RepeatForever();
                            });
                    });
            });
            services.AddQuartzHostedService();
        }
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
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
        // string connectionString = $"Server=localhost;Port=18001;Database=eshop;Uid=root;Pwd=P@55w0rd!";

        string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid=root;Pwd={dbPassword}";

        // Binding configurations
        var configurationRoot = (IConfigurationRoot)configuration;
        configurationRoot.GetSection("ConnectionStrings")["Default"] = connectionString;
        configurationRoot.Reload();

        services.Configure<MyDbConnectionOptions>(configuration.GetSection("ConnectionStrings"));

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );
    }
}

