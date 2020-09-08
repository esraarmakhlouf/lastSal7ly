using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model;
using sal7lyAdmin.Hubs;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    public class SendDataController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IHubContext<NotificationHub> _hubcontext;
        IUnitOfWork unit = sal7lyAdmin.Startup.un;

        public SendDataController(IUnitOfWork uow,  ISecurity security, IHubContext<NotificationHub> hubcontext)
        {
            _uow = uow;
            _security = security;
            _hubcontext = hubcontext;

        }
        public IActionResult Index()
        {
            if (!sal7lyAdmin.Startup.SendDataIsStarted)
            {
                Thread thread = new Thread(GetNotfications);
                thread.Start();
                sal7lyAdmin.Startup.SendDataIsStarted = true;
            }

            return View();
        }
        [NonAction]
        public void GetNotfications()
        {
            try
            {
                while (true)
                {
                    var model = unit.NotificationRepository.GetMany(ent => !ent.Seen && ent.TypeOfUser==(int)EN_TypeUser.Admin).ToList();
                   
                    sendDataData(model);
                    //t.Start();
                    Thread.Sleep(1*60*1000); // 1 Minutes


                }
            }
            catch (Exception ex)
            {
            }
        }


        public void sendDataData(List<Notification> model )
        {

            _hubcontext.Clients.All.SendAsync("UpdateNotf", model);

        }

        public void updateNotf(long Id)
        {
          _uow.NotificationRepository.UpdateNotification(Id);
        }

    }
}