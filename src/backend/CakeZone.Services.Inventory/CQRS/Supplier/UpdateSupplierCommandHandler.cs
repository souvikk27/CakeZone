using AutoMapper;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.Supplier;
using CakeZone.Services.Inventory.Shared.Supplier;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Model.Supplier>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<Model.Supplier> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplierExist = await _supplierRepository.FindAsync(x => x.Name == request.UpdateSupplier.Name);
        if (!supplierExist.Any())
        {
            throw new NotFoundApiException("Supplier not found either validate parameters or contact support");
        }
        var supplier = _mapper.Map(request.UpdateSupplier, supplierExist.FirstOrDefault());
        await _supplierRepository.UpdateAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
        return supplier;
    }
}