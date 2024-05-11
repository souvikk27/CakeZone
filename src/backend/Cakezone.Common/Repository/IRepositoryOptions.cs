using Microsoft.EntityFrameworkCore;

namespace CakeZone.Common.Repository
{
    public interface IRepositoryOptions<out TContext> where TContext : DbContext
    {
        TContext Context { get; }
    }
}
