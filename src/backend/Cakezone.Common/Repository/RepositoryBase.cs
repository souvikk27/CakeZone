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
            var keyExpression = Key() ?? throw new InvalidOperationException("Key expression is not defined for this repository.");
            // Create a parameter expression for the entity
            var entityParameter = Expression.Parameter(typeof(TEntity), "entity");
            // Access the key property using the Key expression
            var keyAccessExpression = Expression.Invoke(keyExpression, entityParameter);
            var castedKey = Expression.Convert(keyAccessExpression, typeof(Guid));
            // Create a lambda expression for the predicate
            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(castedKey, Expression.Constant(id)),
                entityParameter
            );
            // Use the lambda expression to retrieve the entity by ID
            var entity = Context.Set<TEntity>().SingleOrDefault(lambda);
            if (entity == null)
            {
                var entityTypeName = typeof(TEntity).Name;
                throw new NotFoundApiException($"{entityTypeName} with ID '{id}'");
            }
            return entity;
        }


        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var entity = DataSet().Compile()(Context);
            return await entity.AsNoTracking().ToListAsync();
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
