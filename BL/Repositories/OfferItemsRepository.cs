using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IOfferItemsRepository { }

    public class OfferItemsRepository : Repository<OfferItem>, IOfferItemsRepository
    {
        public OfferItemsRepository(DBContext ctx) : base(ctx)
        { }
    }
}
