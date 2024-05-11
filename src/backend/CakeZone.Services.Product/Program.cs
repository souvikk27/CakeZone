using Cakezone.Common.Logging;
using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Extension;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CakeZone.Services.Product.Controllers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.ConfigureLogging(Log.Logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(configuration);
builder.Services.ConfigureMappings();
builder.Services.ConfigureRepositories();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILoggerManager>());

app.MapControllers();

await app.RunAsync();
