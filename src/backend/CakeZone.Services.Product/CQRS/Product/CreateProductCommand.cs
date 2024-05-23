using CakeZone.Services.Product.Shared.Products;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class CreateProductCommand : IRequest<Model.Product>
    {
        public ProductCreateDto ProductCreateDto { get; }

        public CreateProductCommand(ProductCreateDto productCreateDto)
        {
            ProductCreateDto = productCreateDto;
        }
    }
}
