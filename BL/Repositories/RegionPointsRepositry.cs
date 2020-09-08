using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IRegionPointsRepositry
    { }
    public class RegionPointsRepository : Repository<RegionPoints>, IRegionPointsRepositry
    {
        public RegionPointsRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<RegionPoints> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
