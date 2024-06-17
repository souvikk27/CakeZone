using CakeZone.Services.Product.Repository.Product;
using MediatR;
using AutoMapper;
using CakeZone.Services.Product.Model.Exception;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Model.Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Model.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductUpdateDto.ProductId);
            if (product == null)
            {
                throw new NotFoundApiException($"Product with ID {request.ProductUpdateDto.ProductId} not found");
            }
            var productUpdate = _mapper.Map<Model.Product>(request.ProductUpdateDto);
            await _productRepository.UpdateAsync(productUpdate);
            await _productRepository.SaveChangesAsync();
            return productUpdate;
        }
    }
}
