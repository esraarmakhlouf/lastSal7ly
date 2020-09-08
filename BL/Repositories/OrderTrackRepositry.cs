using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOrderTrackRepositry
    { }
    public class OrderTrackRepository : Repository<OrderTrack>, IOrderTrackRepositry
    {
        public OrderTrackRepository(DBContext ctx) : base(ctx)
        { }
    }
}
