using System.Text.Json.Serialization;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContextPool<DatabaseContext>(
    o => o.UseNpgsql(
            builder.Configuration.GetConnectionString("DbConnectionString"),
            x => x.MigrationsAssembly("Infrastructure"))
        .UseSnakeCaseNamingConvention());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<DatabaseContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}