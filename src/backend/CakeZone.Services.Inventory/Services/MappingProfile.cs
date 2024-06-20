using AutoMapper;
using CakeZone.Services.Inventory.Shared.Depot;
using CakeZone.Services.Inventory.Shared.Inventory;

namespace CakeZone.Services.Inventory.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Inventory Mappings
            CreateMap<CreateInventoryDto, Model.Inventory>();

            //Storage Depot Mappings
            CreateMap<CreateStorageDepotDto, Model.StorageDepot>();
        }
    }
}
