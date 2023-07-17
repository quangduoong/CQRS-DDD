using eShop.Application.Abstractions;
using eShop.Application.Middleware;
using eShop.Application.Repositories;

namespace eShop.Application.Configurations
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