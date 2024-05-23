using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Services.FIlters;
using CakeZone.Services.Product.Shared.Products;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetAllProductsQuery : IRequest<PagedList<ProductViewDto>>
    {
        public ProductParameter ProductParameter { get; set; }

        public GetAllProductsQuery(ProductParameter productParameter)
        {
            ProductParameter = productParameter;
        }
    }
}
