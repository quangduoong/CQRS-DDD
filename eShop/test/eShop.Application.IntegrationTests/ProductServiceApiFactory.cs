using DotNet.Testcontainers.Builders;
using eShop.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MySql;

namespace eShop.Application.IntegrationTests;

public class ProductServiceApiFactory<TProgram, TDbContext> :
    WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
    where TDbContext : DbContext
{
    private readonly MySqlContainer _container = new MySqlBuilder()
            .WithEnvironment("MYSQL_ROOT_PASSWORD", "pa55w0rd!")
            .WithName("eshop-product-service-db")
            .WithDatabase("eshop")
            .WithPortBinding(18002, 3306)
            .WithWaitStrategy(
                Wait.ForUnixContainer().UntilPortIsAvailable(3306))
            .Build();

    Task IAsyncLifetime.DisposeAsync() => _container.DisposeAsync().AsTask();

    public Task InitializeAsync() => _container.StartAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureTestServices(services =>
        {
            // Remove DbContext.
            services.RemoveAll(typeof(TDbContext));

            // Add container db.
            services.AddDbContext<TDbContext>(options =>
            {
                string connectionString = _container.GetConnectionString();
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });

            // Ensure db was created.
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        });
    }
}

