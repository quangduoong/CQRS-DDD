using eShop.Api.Configurations;
using eShop.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Collect configurations from layers.
builder.Services.AddMyDbConfig();

builder.Services.AddMyServiceConfig(builder.Configuration);

builder.Services.AddMyAutoMapperConfig();

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

app.Run();
