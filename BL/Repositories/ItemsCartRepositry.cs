using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IItemsCartRepositry
    { }
    public class ItemsCartRepository : Repository<ItemsCart>, IItemsCartRepositry
    {
        public ItemsCartRepository(DBContext ctx) : base(ctx)
        { }

        //public override IQueryable<ItemsCart> GetAll()
        //{ return base.GetAll().Where(ent => !ent.IsDeleted); }
    }
}
