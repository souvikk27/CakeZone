using AutoMapper;
using CakeZone.Services.Inventory.Services;

namespace CakeZone.Services.Inventory.Extension
{
    public static class MappingExtension
    {
        public static void ConfigureMappings(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
