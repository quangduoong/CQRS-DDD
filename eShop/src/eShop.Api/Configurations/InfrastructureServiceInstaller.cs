﻿using eShop.Application.Profiles;
using eShop.Domain.Abstractions;
using eShop.Domain.Exceptions;
using eShop.Infrastructure;
using eShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eShop.Api.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, string envName)
    {
        if (!envName.Equals("Test"))
        {
            AddDbContext(services);
            services.AddScoped<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, DistributedCacheProductRepository>();
            services.AddAutoMapper(new Assembly[] {
                typeof(ProductProfile).GetTypeInfo().Assembly });
        }
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

        string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid=root;Pwd={dbPassword}";

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }
}
