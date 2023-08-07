using eShop.Api.Configurations;
using eShop.Api.Middleware;
using eShop.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Collect configurations from layers.
bool isInTestEnv = builder.Environment.EnvironmentName.Equals("Test");
if (!isInTestEnv)
    builder.Services.AddMyDbConfig();

builder.Services.AddMyServicesConfig(isInTestEnv);

builder.Services.AddMyMediatRConfig();

builder.Services.AddMyValidationConfig();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (!isInTestEnv)
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