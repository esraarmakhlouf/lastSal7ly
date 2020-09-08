using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IDistrictRepositry
    { }
    public class DistrictRepository : Repository<District>, IDistrictRepositry
    {
        public DistrictRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<District> GetAll()
        { return base.GetAll(); }
    }
}
