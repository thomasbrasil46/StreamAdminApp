using Microsoft.EntityFrameworkCore;
using StreamAdmin.Subscription.Config;
using StreamAdmin.Subscription.Models.Context;
using StreamAdmin.Subscription.Repository;
using StreamAdmin.Subscription.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'MySqlConnection' nao foi configurada.");

builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(SubscriptionProfile).Assembly
);

builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
builder.Services.AddScoped<IUserAccessRepository, UserAccessRepository>();
builder.Services.AddHttpClient<IPlatformCatalogClient, PlatformCatalogClient>(client =>
{
    string catalogUrl = builder.Configuration["ServiceUrls:PlatformCatalog"]
        ?? throw new InvalidOperationException("ServiceUrls:PlatformCatalog nao foi configurada.");
    client.BaseAddress = new Uri(catalogUrl);
    client.Timeout = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
