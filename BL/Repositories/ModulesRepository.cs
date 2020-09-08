using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IModulesRepository
    {  }

    public class ModulesRepository :  Repository<Modules>, IModulesRepository
    {
        public ModulesRepository(DBContext ctx) : base(ctx)
        {  }

        public override IQueryable<Modules> GetAll()
        {  return base.GetAll().Where(ent=>!ent.IsDeleted); }
    }
}
