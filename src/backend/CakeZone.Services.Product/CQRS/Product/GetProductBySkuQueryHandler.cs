using CakeZone.Services.Product.Repository.Product;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetProductBySkuQueryHandler : IRequestHandler<GetProductsBySkuQuery, Model.Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductBySkuQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Model.Product> Handle(GetProductsBySkuQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductsWithImages(request.Sku);

            if (product.Any())
            {
                return product.FirstOrDefault();
            }
            return null;
        }
    }
}
