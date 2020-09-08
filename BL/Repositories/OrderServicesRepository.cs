using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderServicesRepository
    { }
    public class OrderServicesRepository : Repository<OrderServices>, IOrderServicesRepository
    {
        public OrderServicesRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<OrderServices> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
