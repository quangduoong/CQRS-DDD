using System.Reflection;

namespace eShop.Api.Configurations;

internal static class DependencyInjectionManager
{
    public static IServiceCollection InstallConfigServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string envName,
        params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> installers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(typeInfo =>
                typeof(IServiceInstaller).IsAssignableFrom(typeInfo)
                && !typeInfo.IsInterface
                && !typeInfo.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in installers)
        {
            installer.Install(services, configuration, envName);
        }
        return services;
    }
}

