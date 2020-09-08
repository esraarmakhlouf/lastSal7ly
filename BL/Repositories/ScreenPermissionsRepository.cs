using BL.Infrastructure;
using Model;

namespace BL.Repositories
{
    public interface IScreenPermissionsRepository
    {  }

    public class ScreenPermissionsRepository :  Repository<ScreenPermissions>, IScreenPermissionsRepository
    {
        public ScreenPermissionsRepository(DBContext ctx) : base(ctx)
        {  }
    }
}
