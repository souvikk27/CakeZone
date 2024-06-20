using CakeZone.Services.Inventory.Data;
using CakeZone.Services.Inventory.Event;
using CakeZone.Services.Inventory.Repository.Depot;
using CakeZone.Services.Inventory.Repository.Inv;
using CakeZone.Services.Inventory.Repository.StockIssue;
using CakeZone.Services.Inventory.Repository.StockReceipt;
using CakeZone.Services.Inventory.Repository.Supplier;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInventoryRepository), typeof(InventoryRepository));
            services.AddScoped(typeof(IStockIssuesRepository), typeof(StockIssuesRepository));
            services.AddScoped(typeof(IStockReceiptRepository), typeof(StockReceiptRepository));    
            services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
            services.AddScoped(typeof(IStorageDepotRepository), typeof(StorageDepotRepository));
        }

        public static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(buildConfiguration =>
            {
                buildConfiguration.SetKebabCaseEndpointNameFormatter();
                buildConfiguration.AddConsumer<InventoryCreateConsumer>();

                buildConfiguration.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    config.ConfigureEndpoints(context);
                });
            });
        }

        public static void HandleInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(typeof(ServiceExtension).Assembly));

        }
    }
}
