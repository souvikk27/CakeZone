using CakeZone.Services.Inventory.Data;
using CakeZone.Services.Inventory.Repository;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInventoryRepository), typeof(InventoryRepository));
        }

        public static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        }
    }
}
