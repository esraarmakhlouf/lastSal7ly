using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IJobTitlesRepositry
    { }
    public class JobTitlesRepository : Repository<JobTitle>, IJobTitlesRepositry
    {
        public JobTitlesRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<JobTitle> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
