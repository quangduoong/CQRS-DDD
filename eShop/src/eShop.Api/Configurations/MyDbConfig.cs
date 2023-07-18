namespace eShop.Domain.Configurations;

public static class MyDbConfig
{
    public static void AddMyDbConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<AppDbContext>();
    }
}
