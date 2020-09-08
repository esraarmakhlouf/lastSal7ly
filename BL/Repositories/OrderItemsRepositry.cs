using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderItemsRepositry
    { }
    public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepositry
    {
        public OrderItemsRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<OrderItems> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
