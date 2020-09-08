using BL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ICountryRepositry
    { }
    public class CountryRepository : Repository<Country>, ICountryRepositry
    {
        public CountryRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Country> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
          public  IQueryable<Country> GetCountries()
        { return GetAll().Where(ent => ent.IsActive &&!ent.IsDeleted).Include(ent=>ent.Cities).ThenInclude(ent=>ent.Districts); }

    }

}
