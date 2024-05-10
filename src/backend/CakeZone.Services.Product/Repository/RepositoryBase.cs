using CakeZone.Services.Product.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using CakeZone.Services.Product.Model.Exceptions;

namespace CakeZone.Services.Product.Repository
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
            return await Task.Run(() => ApplySpecificationList(specification).FirstOrDefault());
        }

        public virtual Expression<Func<TContext, DbSet<TEntity>>> DataSet() => null;
        public virtual Expression<Func<TEntity, object>> Key() => null;

        public virtual TEntity GetById(object id)
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


        public virtual IEnumerable<TEntity> GetAll()
        {
            var entity = DataSet().Compile()(Context);
            return entity.ToList();
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }


        public virtual TEntity Add(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = Context.Set<TEntity>().Add(entity);
            return entityEntry.Entity;
        }


        public virtual TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }


        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
