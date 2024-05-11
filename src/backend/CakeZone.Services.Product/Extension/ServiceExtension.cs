using Cakezone.Common.Logging;
using CakeZone.Common.Repository;
using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Repository.Product;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Serilog.ILogger;

namespace CakeZone.Services.Product.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureLogging(this IServiceCollection services, ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            services.AddSingleton<ILoggerManager>(new LoggerManager(logger));
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
            services.AddScoped(typeof(IRepositoryOptions<>), typeof(RepositoryOptions<>));
        }
    }
}
