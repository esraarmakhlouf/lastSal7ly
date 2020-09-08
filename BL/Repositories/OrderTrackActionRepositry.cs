using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderTrackActionRepositry
    { }
    public class OrderTrackActionRepository : Repository<OrderTrackAction>, IOrderTrackActionRepositry
    {
        public OrderTrackActionRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<OrderTrackAction> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
