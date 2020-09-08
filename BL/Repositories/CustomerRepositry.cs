using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface ICustomerRepositry
    { }
    public class CustomerRepository : Repository<Customer>, ICustomerRepositry
    {
        public CustomerRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Customer> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
