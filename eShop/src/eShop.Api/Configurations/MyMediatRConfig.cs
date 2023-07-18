using eShop.Domain.Products.Commands;
using eShop.Domain.Products.Queries;
using System.Reflection;

namespace eShop.Api.Configurations;

public static class MyMediatRConfig
{
    public static void AddMyMediatRConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        Assembly[] assemblies = {
            typeof(CreateProductCommand).GetTypeInfo().Assembly,
            typeof(GetProductByIdQuery).GetTypeInfo().Assembly
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
    }
}
