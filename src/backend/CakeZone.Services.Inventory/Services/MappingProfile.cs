using AutoMapper;
using CakeZone.Services.Inventory.Shared.Depot;
using CakeZone.Services.Inventory.Shared.Inventory;
using CakeZone.Services.Inventory.Shared.Supplier;

namespace CakeZone.Services.Inventory.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Inventory Mappings
            CreateMap<CreateInventoryDto, Model.Inventory>();
            CreateMap<UpdateInventoryDto, Model.Inventory>();

            //Storage Depot Mappings
            CreateMap<CreateStorageDepotDto, Model.StorageDepot>();
            CreateMap<UpdateStorageDepotDto, Model.StorageDepot>();
            
            //Supplier Mappings
            CreateMap<CreateSupplierDto, Model.Supplier>();
            CreateMap<UpdateSupplierDto, Model.Supplier>();
        }
    }
}
