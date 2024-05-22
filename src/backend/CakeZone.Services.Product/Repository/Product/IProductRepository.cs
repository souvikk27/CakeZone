

using CakeZone.Services.Product.Shared.Attributes;

namespace CakeZone.Services.Product.Repository.Product;

public interface IProductRepository : IRepository<Model.Product>
{
    Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku);
    Task<bool> AddProductsWithParametersAsync(Model.Product product, Guid categoryId, IEnumerable<AttributeProductDto> attributeProducts);
}