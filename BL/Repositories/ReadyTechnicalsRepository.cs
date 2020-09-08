using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IReadyTechnicalsRepository
    {  }

    public class ReadyTechnicalsRepository :  Repository<ReadyTechnicals>, IReadyTechnicalsRepository
    {
        public ReadyTechnicalsRepository(DBContext ctx) : base(ctx)
        {  }

        public override IQueryable<ReadyTechnicals> GetAll()
        {  return base.GetAll().Where(ent=>!ent.Technical.IsDeleted); }
    }
}
