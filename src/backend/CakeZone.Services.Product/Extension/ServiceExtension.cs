using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Repository.Attribute;
using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Repository.Product;
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
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped(typeof(Repository.IRepositoryOptions<>), typeof(Repository.RepositoryOptions<>));
        }
    }
}
