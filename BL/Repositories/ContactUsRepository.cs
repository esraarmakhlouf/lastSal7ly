using BL.Infrastructure;
using Model;
using System.Linq;
using static BL.SharedCS.Enumrations;

namespace BL.Repositories
{
    public interface IContactUsRepository
    { }

    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<ContactUs> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
