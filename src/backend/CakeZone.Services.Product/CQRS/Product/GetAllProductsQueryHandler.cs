using AutoMapper;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Shared.Products;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedList<ProductViewDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<ProductViewDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll();
            IEnumerable<ProductViewDto> productsView = _mapper.Map<IEnumerable<ProductViewDto>>(products);
            var filteredProduct = productsView.Where(product =>
                                 (request.ProductParameter.AddedOn == DateTime.MinValue || request.ProductParameter.AddedOn == product.CreatedAt) &&
                                 (string.IsNullOrEmpty(request.ProductParameter.ProductName) || request.ProductParameter.ProductName == product.Name))
                                 .ToList();
            var metadata = new MetaData().Initialize(request.ProductParameter.PageNumber, request.ProductParameter.PageSize, filteredProduct.Count());
            var pagedList = PagedList<ProductViewDto>.ToPagedList(filteredProduct, request.ProductParameter.PageNumber, request.ProductParameter.PageSize);
            return pagedList;
        }
    }
}
