using BL.Infrastructure;
using Model;
using System.Linq;

namespace BL.Repositories
{
    public interface IItemImagesRepository
    { }

    public class ItemImagesRepository : Repository<ItemImage>, IItemImagesRepository
    {
        public ItemImagesRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<ItemImage> GetAll()
        {
            return base.GetAll();
        }
    }
}
