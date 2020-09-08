using BL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace BL.Repositories
{
    public interface IServicesRepositry
    { }
    public class ServicesRepository : Repository<Service>, IServicesRepositry
    {
        private DBContext _ctx;
        public ServicesRepository(DBContext ctx) : base(ctx)
        { _ctx = ctx; }

        public override IQueryable<Service> GetAll()
        { return base.GetAll().Where(ent => !ent.IsDeleted); }

        public object GetServices(string url)
        {

            var result = (from c in _ctx.Services
                          from o in _ctx.Technicals
                          where c.Id == o.ServiceId
                          orderby c.Id descending
                          select new
                          {
                              Id = c.Id,
                              Name = c.ArabicName,
                              Image = url + c.ImagePath
                          }).Distinct().ToList();
            return result;

        }


        public object GetTopServices(string url)
        {
            var count = 2;
            var services = _ctx.OrderServices.Where(ent => ent.IsActive && !ent.IsDeleted)
                    .GroupBy(ent => ent.ServiceId)
                    .OrderByDescending(ent => ent.Count()).Take(count)
                    .Select(ent => new {
                        Id = ent.Key,
                        Name = ent.Select(e => e.Service.ArabicName).FirstOrDefault(),
                        Image = url + ent.Select(e => e.Service.ImagePath).FirstOrDefault(),
                    }).ToList();
            if (services.Count < count)
            {
                var servicesids = services.Select(ent => ent.Id).ToList();
                var randomservices = _ctx.Services.Where(ent => ent.IsActive && !ent.IsDeleted && !servicesids.Contains(ent.Id)).Select(
                    ent => new
                    {
                        Id = ent.Id,
                        Name = ent.ArabicName,
                        Image = url + ent.ImagePath
                    }).Take(count - services.Count);
                foreach (var service in randomservices)
                {
                    services.AddRange(randomservices);
                }

            }
            /// var topservices=_ctx.OrderServices.Include(ent=>ent.Service).order

            return services;
        }
    }
}
