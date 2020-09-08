using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IPermissionsRepository
    {  }

    public class PermissionsRepository :  Repository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(DBContext ctx) : base(ctx)
        {  }

        public override IQueryable<Permissions> GetAll()
        {  return base.GetAll().Where(ent=>!ent.IsDeleted); }
    }
}
