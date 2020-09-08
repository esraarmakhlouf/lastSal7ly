using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ICustomerReviewRepositry
    { }
    public class CustomerReviewRepository : Repository<CustomerReview>, ICustomerReviewRepositry
    {
        public CustomerReviewRepository(DBContext ctx) : base(ctx)
        { }

    }
}
