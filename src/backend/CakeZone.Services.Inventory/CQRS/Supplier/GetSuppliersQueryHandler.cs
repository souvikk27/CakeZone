using AutoMapper;
using CakeZone.Services.Inventory.Repository.Supplier;
using CakeZone.Services.Inventory.Services;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PagedList<Model.Supplier>>
{
    private readonly ISupplierRepository _supplierRepository;
    public GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }
    public async Task<PagedList<Model.Supplier>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepository.ListAllAsync();
        
        var filteredSupplier = suppliers
            .Where(x => (request.Parameter.AddedOn == DateTime.MinValue || request.Parameter.AddedOn == x.CreatedAt) &&
                        (string.IsNullOrEmpty(request.Parameter.Name) || request.Parameter.Name == x.Name) &&
                        (string.IsNullOrEmpty(request.Parameter.Email) || request.Parameter.Email == x.Email))
            .ToList();
        var metadata = new MetaData().Initialize(request.Parameter.PageNumber,
            request.Parameter.PageSize,
            filteredSupplier.Count);
        
        var pagedList = PagedList<Model.Supplier>.ToPagedList(filteredSupplier,
            request.Parameter.PageNumber,
            request.Parameter.PageSize);
        
        return pagedList;
       
    }
}