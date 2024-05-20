

namespace CakeZone.Services.Product.Repository.Product;

public interface IProductRepository : IRepository<Model.Product>
{
    Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku);
}