using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IRegionRepositry
    { }
    public class RegionRepository : Repository<Region>, IRegionRepositry
    {
        public RegionRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Region> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
