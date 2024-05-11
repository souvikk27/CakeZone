using Cakezone.Common.Logging;
using CakeZone.Services.Product.Repository.Product;

namespace CakeZone.Services.Product.Endpoints;

public class ProductEndpoints
{
    private readonly ILoggerManager _logger;
    private readonly IProductRepository _productRepository;

    public ProductEndpoints(ILoggerManager logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }
    
    //Craete .NET 8 Minimal APIS For Products
}