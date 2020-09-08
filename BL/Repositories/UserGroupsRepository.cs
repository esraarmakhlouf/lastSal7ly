using BL.Infrastructure;
using Model;

namespace BL.Repositories
{
    public interface IUserGroupsRepository
    {  }

    public class UserGroupsRepository :  Repository<UserGroups>, IUserGroupsRepository
    {
        public UserGroupsRepository(DBContext ctx) : base(ctx)
        {  }
    }
}
