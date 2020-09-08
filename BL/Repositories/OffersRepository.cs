using BL.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace BL.Repositories
{
    public interface IOffersRepository
    { }

    public class OffersRepository : Repository<Offer>, IOffersRepository
    {
        private DBContext _ctx;

        public OffersRepository(DBContext ctx) : base(ctx)
        { }

        public override IQueryable<Offer> GetAll()
        {  return base.GetAll().Where(ent => !ent.IsDeleted); }
        
        public  Offer GetOfferBy(string couponCode)
        {
            var model = GetMany(ent => ent.CouponCode == couponCode).Include(ent => ent.Service).Include(ent => ent.MainItem).Include(ent => ent.FreeItem).FirstOrDefault();
            if (model != null)
            {
                var dates = _ctx.OfferActiveDate.Where(ent => ent.OfferId == model.Id && ent.StartDate <= DateTime.Now && ent.EndDate >= DateTime.Now);
                if (dates.Any())
                    return model;
                else
                    return new Offer { Id = 0 };
            }
            else
                return null;
        }
      

    }
}
