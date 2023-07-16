using eShop.Infrastructure.Abstractions;
using eShop.Infrastructure.Middleware;
using eShop.Infrastructure.Repositories;

namespace eShop.Infrastructure.Configurations
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