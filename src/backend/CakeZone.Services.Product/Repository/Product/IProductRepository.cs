

namespace CakeZone.Services.Product.Repository.Product;

public interface IProductRepository : IRepository<Model.Product>
{
    Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku);
    Task<bool> AddProductsWithParameters(Model.Product product, Guid categoryId, IEnumerable<Guid> attributeIds);
}