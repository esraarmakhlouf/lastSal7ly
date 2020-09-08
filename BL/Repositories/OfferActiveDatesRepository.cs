using BL.Infrastructure;
using Model;

namespace BL.Repositories
{
    public interface IOfferActiveDatesRepository { }

    public class OfferActiveDatesRepository : Repository<OfferActiveDate>, IOfferActiveDatesRepository
    {
        public OfferActiveDatesRepository(DBContext ctx) : base(ctx) { }
    }
}
