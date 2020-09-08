using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IPocketHistoryRepositry
    { }
    public class PocketHistoryRepository : Repository<PocketHistory>, IPocketHistoryRepositry
    {
        public PocketHistoryRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<PocketHistory> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
