using AutoMapper;
using CakeZone.Services.Product.Repository.Product;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Model.Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Model.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _productRepository.FindAsync(p => p.Name == request.ProductCreateDto.Products.Name);

            if (productExist.Any())
            {
                return productExist.FirstOrDefault();
            }

            var product = _mapper.Map<Model.Product>(request.ProductCreateDto.Products);
            await _productRepository.AddProductsWithParametersAsync(product, request.ProductCreateDto.CategoryId, request.ProductCreateDto.AttributeProduct);
            await _productRepository.SaveChangesAsync();

            return null;
        }
    }
}