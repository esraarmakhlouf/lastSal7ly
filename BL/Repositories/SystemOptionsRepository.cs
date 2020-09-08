using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ISystemOptionsRepository { }

    public class SystemOptionsRepository :  Repository<SystemOption>, ISystemOptionsRepository
    {
        public SystemOptionsRepository(DBContext ctx) : base(ctx)
        {  }

        public override IQueryable<SystemOption> GetAll()
        {  return base.GetAll().Where(ent=>!ent.IsDeleted); }
    }
}
