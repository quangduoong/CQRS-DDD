using eShop.Domain.Validators;
using FluentValidation;
using System.Reflection;

namespace eShop.Api.Configurations;

public static class MyValidationConfig
{
    public static void AddMyValidationConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        Assembly[] assemblies =
        {
            typeof(CreateProductCommandValidator).GetTypeInfo().Assembly,
        };

        services.AddValidatorsFromAssemblies(assemblies);
    }
}
