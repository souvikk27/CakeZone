using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Repository
{
    public interface IRepositoryOptions<out TContext> where TContext : DbContext
    {
        TContext Context { get; }
    }
}
