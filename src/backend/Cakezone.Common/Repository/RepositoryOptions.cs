using Microsoft.EntityFrameworkCore;

namespace CakeZone.Common.Repository
{
    public class RepositoryOptions<TContext> : IRepositoryOptions<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public RepositoryOptions(TContext context)
        {
            _context = context;
        }

        public TContext Context => _context;
    }
}
