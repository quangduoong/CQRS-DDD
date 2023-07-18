using eShop.Domain.Abstractions;
using eShop.Api.Middleware;
using eShop.Infrastructure.Repositories;

namespace eShop.Api.Configurations
{
    public static class MyServiceConfig
    {
        public static void AddMyServiceConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<ExceptionMiddleware>();
        }
    }
}