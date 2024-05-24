using CakeZone.Services.Product.Shared.Products;
using MediatR;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class UpdateProductCommand : IRequest<Model.Product>
    {
        public ProductsUpdateDto ProductUpdateDto { get; set; }
        public UpdateProductCommand(ProductsUpdateDto productsUpdateDto)
        {
            ProductUpdateDto = productsUpdateDto;
        }
    }
}
