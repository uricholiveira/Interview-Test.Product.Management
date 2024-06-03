using System.Text.Json.Serialization;
using Application.Models;
using Business.Extensions;
using Infrastructure.Database;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContextPool<DatabaseContext>(
    o => o.UseNpgsql(
            builder.Configuration.GetConnectionString("DbConnectionString"),
            x => x.MigrationsAssembly("Infrastructure"))
        .UseSnakeCaseNamingConvention());

builder.Services.AddAutoMapper(x => x.AddProfile<MappingProfile>());
builder.Services.RegisterAppServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<DatabaseContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();