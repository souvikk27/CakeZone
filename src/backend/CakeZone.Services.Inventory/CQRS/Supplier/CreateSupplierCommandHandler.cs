using AutoMapper;
using CakeZone.Services.Inventory.Repository.Supplier;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Model.Supplier>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<Model.Supplier> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = _mapper.Map<Model.Supplier>(request.CreateSupplierDto);
        await _supplierRepository.AddAsync(supplier);
        await _supplierRepository.SaveChangesAsync();
        return supplier;
    }
}