using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StreamAdmin.Catalog.Config;
using StreamAdmin.Catalog.Models.Context;
using StreamAdmin.Catalog.Repository;

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
    typeof(CatalogProfile).Assembly
);

//ToDo: Óbservar quando será necessário fazer a implementação das interfaces de repository criadas anteriormente.
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
//builder.Services.AddScoped<IPlanRepository, PlanRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
