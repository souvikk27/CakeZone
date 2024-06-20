using System.Linq.Expressions;
using CakeZone.Services.Inventory.Data;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Repository.StockReceipt
{
    public class StockReceiptRepository : RepositoryBase<Model.StockReceipt, ApplicationDbContext>, IStockReceiptRepository
    {
        public StockReceiptRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.StockReceipt>>> DataSet() =>
            m => m.StockReceipt;

        public override Expression<Func<Model.StockReceipt, object>> Key() => k => k.Id;
    }
}
