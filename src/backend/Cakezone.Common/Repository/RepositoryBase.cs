using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CakeZone.Common.Models.Exception;
using CakeZone.Common.Specification;

namespace CakeZone.Common.Repository
{
    public class RepositoryBase<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(IRepositoryOptions<TContext> options)
        {
            Context = options.Context;
            _dbSet = Context.Set<TEntity>();
        }

        public  IQueryable<TEntity> ApplySpecificationList(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> spec = null)
        {
            return  await Task.Run(() => ApplySpecificationList(spec));
        }

        public async Task<TEntity> FindAsync(ISpecification<TEntity> specification = null)
        {
            return await Task.Run(() => ApplySpecificationList(specification).FirstOrDefault()) ?? throw new InvalidOperationException();
        }

        public virtual Expression<Func<TContext, DbSet<TEntity>>> DataSet() => null;
        public virtual Expression<Func<TEntity, object>> Key() => null;

        public virtual async Task<TEntity> GetById(object id)
        {
            var keyExpression = Key() ?? throw new InvalidOperationException("Key expression is not defined for this repository");

            // Check if the key is a Guid to avoid unnecessary conversion
            if (keyExpression.Body.Type != typeof(Guid))
            {
                throw new InvalidOperationException("Key expression must return a Guid type");
            }

            var entityId = (Guid)id;

            // Use FindAsync for direct database query instead of SingleOrDefaultAsync with a lambda
            var entity = await Context.Set<TEntity>().FindAsync(entityId);

            if (entity == null)
            {
                var entityTypeName = typeof(TEntity).Name;
                throw new NotFoundApiException($"Entity {entityTypeName} with ID '{id}' not found");
            }

            return entity;
        }


        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var entity = DataSet().Compile()(Context);
            return await entity.ToListAsync();
        }


        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }


        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityEntry = await Context.Set<TEntity>().AddAsync(entity);
            return entityEntry.Entity;
        }


        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Task.Yield();
            return entity;
        }


        public virtual async Task Remove(TEntity entity)
        {
            await Task.Run(() => Context.Set<TEntity>().Remove(entity)); 
        }


        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
