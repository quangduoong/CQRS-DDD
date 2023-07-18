using eShop.Infrastructure;

namespace eShop.Api.Configurations;

public static class MyDbConfig
{
    public static void AddMyDbConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<AppDbContext>();
    }
}
