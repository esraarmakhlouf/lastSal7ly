using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderRepositry
    { }
    public class OrderRepository : Repository<Order>, IOrderRepositry
    {
        public OrderRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Order> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
