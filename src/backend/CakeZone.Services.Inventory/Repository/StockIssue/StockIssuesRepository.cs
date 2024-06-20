using System.Linq.Expressions;
using CakeZone.Services.Inventory.Data;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Repository.StockIssue
{
    public class StockIssuesRepository : RepositoryBase<Model.StockIssue, ApplicationDbContext>, IStockIssuesRepository
    {
        public StockIssuesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.StockIssue>>> DataSet() => x => x.StockIssue;
        public override Expression<Func<Model.StockIssue, object>> Key() => x => x.Id;
    }
}
