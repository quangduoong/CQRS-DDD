using eShop.Domain.Profiles;

namespace eShop.Domain.Configurations;

public static class MyAutoMapperConfig
{
    public static void AddMyAutoMapperConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(ProductProfile));
    }
}
