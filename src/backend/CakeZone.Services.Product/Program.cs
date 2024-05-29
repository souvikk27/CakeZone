using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Services.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.ConfigureLogging();
builder.Services.AddControllers()
    .AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDbContext(configuration);
builder.Services.HandleInfrastructure();
builder.Services.ConfigureMappings();
builder.Services.ConfigureRepositories();
builder.Services.AddImageService();
builder.Services.ConfigureCors();

var app = builder.Build();

app.UseAutoMigrationBuilder();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILoggerManager>());

app.MapControllers();

app.UseStaticFiles();

await app.RunAsync();
