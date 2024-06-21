using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.Supplier;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Model.Supplier>
{
    private readonly ISupplierRepository _supplierRepository;

    public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Model.Supplier> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId);

        if (supplier == null)
        {
            throw new NotFoundApiException("Supplier not found either validate parameters or contact support"); 
        }
        await _supplierRepository.DeleteAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
        return supplier;
    }
}