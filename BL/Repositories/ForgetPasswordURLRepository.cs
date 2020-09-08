using BL.Infrastructure;
using Model;

namespace BL.Repositories
{
    public interface IForgetPasswordURLRepository { }

    public class ForgetPasswordURLRepository : Repository<ForgetPasswordURL>, IForgetPasswordURLRepository
    {
        public ForgetPasswordURLRepository(DBContext ctx) : base(ctx) { }
    }
}
