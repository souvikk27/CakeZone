using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class DeleteSupplierCommand : IRequest<Model.Supplier>
{
    public Guid SupplierId { get; set; }
    public DeleteSupplierCommand(Guid supplierId)
    {
        SupplierId = supplierId;
    }
}