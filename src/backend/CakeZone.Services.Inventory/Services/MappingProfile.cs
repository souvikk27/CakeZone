using AutoMapper;
using CakeZone.Services.Inventory.Shared.Inventory;

namespace CakeZone.Services.Inventory.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateInventoryDto, Model.Inventory>();
        }
    }
}
