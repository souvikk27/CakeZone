using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Repository.Attribute;
using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Repository.Image;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Services.Image;
using CakeZone.Services.Product.Services.Logging;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
        }

        public static void AddImageService(this IServiceCollection services)
        {
            services.AddTransient<IImageService, ImageService>();
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

        public static void UseAutoMigrationBuilder(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }

        public static void HandleInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssemblies(typeof(ServiceExtension).Assembly));
        }
    }
}
