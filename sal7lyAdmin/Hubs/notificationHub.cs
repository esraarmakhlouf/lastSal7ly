using BL.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sal7lyAdmin.Hubs
{
    public class NotificationHub: Hub
    {

        public async Task SendNotification()
        {
            await Clients.All.SendAsync("UpdateNotf");
        }

     
    }
}
