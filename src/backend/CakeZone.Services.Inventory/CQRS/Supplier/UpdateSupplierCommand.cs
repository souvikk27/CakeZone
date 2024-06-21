using CakeZone.Services.Inventory.Shared.Supplier;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class UpdateSupplierCommand : IRequest<Model.Supplier>
{
    public UpdateSupplierDto UpdateSupplier { get; set; }
    public UpdateSupplierCommand(UpdateSupplierDto updateSupplier)
    {
        UpdateSupplier = updateSupplier;
    }
}