using BL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.APIModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace BL.Repositories
{
    public interface IItemsRepository
    {

    }

    public class ItemsRepository : Repository<Item>, IItemsRepository
    {
        private DBContext _ctx;

        public ItemsRepository(DBContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public override IQueryable<Item> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }

        public List<ItemsVM> GetItemsByCategoryId(long id)
        {
            var model = (from item in _ctx.Item.Where(ent => ent.IsActive && !ent.IsDeleted && ent.ServiceId == id)
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



        public HashSet<ApiItemModel> GetApi(Expression<Func<Item, bool>> where, long? CurrentCustId, string root)
        {
            var items = _ctx.Item.Where(ent => ent.IsActive && !ent.IsDeleted);
            if (where != null)
            {
                items = items.Where(where);
            }
            var result = items.Include(ent => ent.ItemImages)
                 .Include(ent => ent.Service)
                 .Select(ent => new ApiItemModel
                 {
                     Id = ent.Id,
                     Name = ent.ArabicName,
                     Price = ent.Price,
                     Service = ent.Service.ArabicName,
                     Image = root + "/" + ent.ItemImages.FirstOrDefault().ImagePath,
                     IsFavourite = false
                 }).OrderByDescending(ent => ent.Id).ToHashSet();
            if (items != null && CurrentCustId != null && CurrentCustId != 0)
            {
                var customeritemsfav = _ctx.ItemFavourite.Where(ent => ent.CustomerId == CurrentCustId).Select(ent => ent.ItemId).ToList();
                foreach (var r in result)
                    r.IsFavourite = (customeritemsfav.Contains(r.Id)) ? true : false;
            }
            return result;
        }

        public ApiItemDetailsModel GetDetailsApi(Expression<Func<Item, bool>> where, long? CurrentCustId, string root)
        {
            var items = _ctx.Item.Where(ent => ent.IsActive && !ent.IsDeleted);
            if (where != null)
            {
                items = items.Where(where);
            }

            var result = items.Include(ent => ent.ItemImages)
                            .Include(ent => ent.Service)
                            .Select(ent => new ApiItemDetailsModel
                            {
                                Id = ent.Id,
                                Name = ent.ArabicName,
                                Description = ent.DescriptionArabic,
                                Price = ent.Price,
                                Service = ent.Service.ArabicName,
                                Image = root + "/" + ent.ItemImages.FirstOrDefault().ImagePath,
                                Images = ent.ItemImages.Select(img => root + "/" + img.ImagePath),
                                IsFavourite = false,
                                Rate = 0
                            }).OrderByDescending(ent => ent.Id).FirstOrDefault();
            if (items != null && CurrentCustId != null && CurrentCustId != 0)
            {
                var customeritemsfav = _ctx.ItemFavourite.Where(ent => ent.CustomerId == CurrentCustId).Select(ent => ent.ItemId).ToList();
                result.IsFavourite = (customeritemsfav.Contains(result.Id)) ? true : false;
                result.Rate = GetRate(result.Id);
            }
            if (items != null)
                result.Rate = GetRate(result.Id);
            return result;
        }

        private double GetRate(long itemId)
        {
            var x = _ctx.OrderItems.Where(ent => ent.ItemId == itemId).GroupBy(ent => ent.Rate).Select(ent => new { key = ent.Key, count = ent.Count() });
            int star1 = x.Where(ent => ent.key == 1).Select(ent => ent.count).FirstOrDefault();
            int star2 = x.Where(ent => ent.key == 2).Select(ent => ent.count).FirstOrDefault();
            int star3 = x.Where(ent => ent.key == 3).Select(ent => ent.count).FirstOrDefault();
            int star4 = x.Where(ent => ent.key == 4).Select(ent => ent.count).FirstOrDefault();
            int star5 = x.Where(ent => ent.key == 5).Select(ent => ent.count).FirstOrDefault();

            double rating = (double)(5 * star5 + 4 * star4 + 3 * star3 + 2 * star2 + 1 * star1) / (star1 + star2 + star3 + star4 + star5);

            rating = Math.Round(rating, 1);

            return rating.ToString()=="NaN" ?0:rating;
        }
    }
}
