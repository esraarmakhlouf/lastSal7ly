using BL.Infrastructure;
using Model;

namespace BL.Repositories
{
    public interface IGroupPermissionsRepository
    {  }

    public class GroupPermissionsRepository :  Repository<GroupPermissions>, IGroupPermissionsRepository
    {
        public GroupPermissionsRepository(DBContext ctx) : base(ctx)
        {  }
    }
}
