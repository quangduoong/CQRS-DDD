using eShop.Domain.Abstractions;
using eShop.Domain.Middleware;
using eShop.Domain.Repositories;

namespace eShop.Domain.Configurations
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