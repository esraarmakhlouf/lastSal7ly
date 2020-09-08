using BL.Infrastructure;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace BL.Repositories
{
    public interface IItemFavouriteRepository
    {
       
    }

    public class ItemFavouriteRepository : Repository<ItemFavourite>, IItemFavouriteRepository
    {
        private DBContext _ctx;

        public ItemFavouriteRepository(DBContext ctx) : base(ctx)
        { _ctx = ctx; }

        
        public List<ItemsVM> GetItemsByCustomerId(long id)
        {
            var itemFavourite = _ctx.ItemFavourite.Where(ent =>  ent.CustomerId == id).Select(ent=>ent.ItemId);
            var model = (from item in _ctx.Item.Where(ent => ent.IsActive && !ent.IsDeleted &&itemFavourite.Contains( ent.Id ))
                         join images in _ctx.ItemImages.Take(1) on item.Id equals images.ItemId

                         select new ItemsVM
                         {
                             ArabicName = item.ArabicName,
                             EnglishName = item.EnglishName,
                             ImagePath = images.ImagePath,
                             Price = item.Price

                         }).ToList();
            return model;
        }
    }
}
