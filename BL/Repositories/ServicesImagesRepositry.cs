using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IServicesImagesRepositry
    { }
    public class ServicesImagesRepository : Repository<OrderServicesImages>, IServicesImagesRepositry
    {
        public ServicesImagesRepository(DBContext ctx) : base(ctx)
        { }

    }
}
