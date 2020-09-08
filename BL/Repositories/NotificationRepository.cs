using BL.Infrastructure;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace BL.Repositories
{
    public interface INotificationRepository
    {
       
    }

    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private DBContext _ctx;

        public NotificationRepository(DBContext ctx) : base(ctx)
        { _ctx = ctx; }
        public void UpdateNotification(long id)
        {
            var model = _ctx.Notification.FirstOrDefault(ent=>ent.Id==id);
            model.Seen = true;
            _ctx.Notification.Update(model);
            _ctx.SaveChanges();

        }
    }
}
