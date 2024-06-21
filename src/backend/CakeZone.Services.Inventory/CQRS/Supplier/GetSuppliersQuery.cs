using CakeZone.Services.Inventory.Services;
using CakeZone.Services.Inventory.Services.Filters;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Supplier;

public class GetSuppliersQuery : IRequest<PagedList<Model.Supplier>>
{
    public SupplierParameter Parameter { get; set; }
    public GetSuppliersQuery(SupplierParameter parameter)
    {
        Parameter = parameter;
    }
}