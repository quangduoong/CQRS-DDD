using eShop.Api.Configurations;
using eShop.Api.Middleware;
using eShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallConfigServices(
    builder.Configuration,
    builder.Environment.EnvironmentName,
    typeof(IServiceInstaller).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

// Migrations
if (!builder.Environment.EnvironmentName.Equals("Test"))
    using (IServiceScope scope = app.Services.CreateScope())
    {
        IServiceProvider services = scope.ServiceProvider;
        AppDbContext context = services.GetRequiredService<AppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

app.Run();

public partial class Program { }