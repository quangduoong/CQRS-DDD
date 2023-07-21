using eShop.Api.Middleware;
using eShop.Application.Behaviors;
using eShop.Domain.Abstractions;
using eShop.Domain.Exceptions;
using eShop.Infrastructure.Repositories;
using MediatR;

namespace eShop.Api.Configurations
{
    public static class MyServiceConfig
    {
        public static void AddMyServiceConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

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

            // Decorator
            services.Decorate<IProductRepository, DistributedCacheProductRepository>();


            // Exception throwing middleware 
            services.AddTransient<ExceptionMiddleware>();

            // Validation pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}