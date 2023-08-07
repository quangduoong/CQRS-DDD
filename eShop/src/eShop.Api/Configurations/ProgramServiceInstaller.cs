using eShop.Api.Middleware;
using eShop.Domain.Abstractions;
using eShop.Domain.Exceptions;
using eShop.Infrastructure.Repositories;

namespace eShop.Api.Configurations;

public class ProgramServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, string envName)
    {
        if (!envName.Equals("Test"))
        {
            // Cached services 
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(opt =>
            {
                string? host = Environment.GetEnvironmentVariable("REDIS_CACHEDB_HOST");
                string? port = Environment.GetEnvironmentVariable("REDIS_CACHEDB_PORT");

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port))
                    throw EnvironmentVariableException.NotAvailable();

                opt.Configuration = $"{host}:{port},abortConnect=false";
            }); // Redis distributed cached service

            services.AddTransient<ExceptionMiddleware>();
        }
    }
}

