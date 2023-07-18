using eShop.Domain.Abstractions;
using eShop.Api.Middleware;
using eShop.Infrastructure.Repositories;
using MediatR;
using eShop.Application.Behaviors;

namespace eShop.Api.Configurations
{
    public static class MyServiceConfig
    {
        public static void AddMyServiceConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Exception class throwing middleware 
            services.AddTransient<ExceptionMiddleware>();

            // Validation pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}