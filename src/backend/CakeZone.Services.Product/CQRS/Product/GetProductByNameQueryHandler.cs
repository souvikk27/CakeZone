using AutoMapper;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Shared.Products;
using MediatR;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, ProductsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductsDto> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(p => p.Name == request.ProductName);
            return _mapper.Map<ProductsDto>(product);
        }
    }
}
