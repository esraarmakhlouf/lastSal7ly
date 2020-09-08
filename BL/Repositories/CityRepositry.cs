using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ICityRepositry
    { }
    public class CityRepository : Repository<City>, ICityRepositry
    {
        public CityRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<City> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
