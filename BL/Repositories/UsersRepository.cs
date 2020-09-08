using BL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IUsersRepository
    {  }

    public class UsersRepository :  Repository<Users>, IUsersRepository
    {
        private DBContext _ctx;
        public UsersRepository(DBContext ctx) : base(ctx)
        { _ctx = ctx;  }

        public override IQueryable<Users> GetAll()
        {  return base.GetAll().Where(ent=>!ent.IsDeleted); }
        public  Users GetByIdCustom(long id)
        { return _ctx.Users.Include(ent => ent.Technical).FirstOrDefault(ent => ent.Id == id); }
        public IQueryable<Users> GetUsers()
        {  return GetAll().Include(ent=>ent.Technical).ThenInclude(ent => ent.Service).Where(ent=> !ent.IsDeleted && ent.IsActive); }
    }
}
