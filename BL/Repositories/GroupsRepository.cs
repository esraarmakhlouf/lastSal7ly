using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IGroupsRepository
    { }

    public class GroupsRepository : Repository<Groups>, IGroupsRepository
    {
        public GroupsRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Groups> GetAll()
        {
            return base.GetAll().Where(ent => !ent.IsDeleted);
        }
    }
}
