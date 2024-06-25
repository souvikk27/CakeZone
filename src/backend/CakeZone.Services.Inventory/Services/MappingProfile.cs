using AutoMapper;
using CakeZone.Services.Inventory.Shared.Depot;
using CakeZone.Services.Inventory.Shared.Inventory;
using CakeZone.Services.Inventory.Shared.StockIssues;
using CakeZone.Services.Inventory.Shared.StockReceipt;
using CakeZone.Services.Inventory.Shared.Supplier;
using CreateStockIssueDto = CakeZone.Services.Inventory.Shared.StockIssues.CreateStockIssueDto;

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

            //StockIssues Mapping 
            CreateMap<CreateStockIssueDto, Model.StockIssue>();
            CreateMap<UpdateStockIssueDto, Model.StockIssue>();

            //StockReceipt Mapping 
            CreateMap<Shared.StockReceipt.CreateStockReceiptDto, Model.StockReceipt>();
            CreateMap<UpdateStockReceiptDto, Model.StockReceipt>();
        }
    }
}
