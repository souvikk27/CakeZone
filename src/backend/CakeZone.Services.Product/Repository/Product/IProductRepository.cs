using CakeZone.Services.Product.Shared.Attributes;
using Chronos.Specification;

namespace CakeZone.Services.Product.Repository.Product;

public interface IProductRepository : IRepositoryBase<Model.Product>
{
    Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku);
    Task<bool> AddProductsWithParametersAsync(Model.Product product,
        Guid categoryId,
        IEnumerable<AttributeProductDto> attributeProducts);

    Task<Model.Product> RemoveProductTransactionalAsync(Guid productId);
}