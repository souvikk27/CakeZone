using CakeZone.Services.Product.Shared.Products;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetProductByNameQuery : IRequest<ProductsDto>
    {
        public string ProductName { get; set; }

        public GetProductByNameQuery(string productName)
        {
            ProductName = productName;
        }
    }
}
