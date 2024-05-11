using AutoMapper;
using CakeZone.Services.Product.Services;

namespace CakeZone.Services.Product.Extension;

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