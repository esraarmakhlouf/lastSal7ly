using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ITechnicalsRepository
    {  }

    public class TechnicalsRepository :  Repository<Technical>, ITechnicalsRepository
    {
        public TechnicalsRepository(DBContext ctx) : base(ctx)
        {  }
    }
}
