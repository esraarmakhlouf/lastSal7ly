using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IScreensRepository
    {  }

    public class ScreensRepository :  Repository<Screens>, IScreensRepository
    {
        public ScreensRepository(DBContext ctx) : base(ctx)
        {  }

        public override IQueryable<Screens> GetAll()
        {  return base.GetAll().Where(ent=>!ent.IsDeleted); }
    }
}
