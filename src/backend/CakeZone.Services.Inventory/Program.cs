using System.Text.Json.Serialization;
using CakeZone.Services.Inventory.Extension;
using CakeZone.Services.Inventory.Services.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(option =>
{
    option.Filters.Add<ValidationFilterAttribute>();
}).AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContexts(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureCors();
builder.Services.ConfigureMassTransit();
builder.Services.HandleInfrastructure();
builder.Services.ConfigureMappings();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAutoMigrationBuilder();
app.UseHttpsRedirection();
app.ConfigureExceptionHandler(logger);
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
