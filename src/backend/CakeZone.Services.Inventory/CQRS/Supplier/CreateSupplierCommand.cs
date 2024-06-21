using CakeZone.Services.Inventory.Shared.Supplier;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class CreateSupplierCommand : IRequest<Model.Supplier>
{
    public CreateSupplierDto CreateSupplierDto { get; set; }
    
    public CreateSupplierCommand(CreateSupplierDto createSupplierDto)
    {
        CreateSupplierDto = createSupplierDto;
    }
}