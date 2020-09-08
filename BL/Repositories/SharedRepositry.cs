using BL.Infrastructure;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Model;
using Model.APIModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Numerics;
using System.Security.Cryptography;
using static BL.SharedCS.Enumrations;

namespace BL.Repositories
{
    public interface ISharedRepositry
    { }
    public class SharedRepository
    {
        private DBContext _ctx;
        public SharedRepository(DBContext ctx)
        {
            _ctx = ctx;
        }
        public Dashboard GetDashboard()
        {
            var model = new Dashboard();
            model.NewMembers = _ctx.Customer.Count();
            model.Technicians = _ctx.Technicals.Count();
            model.sales = _ctx.OrderItems.Count();
            var orderItems = _ctx.OrderItems.ToList();
            var orderServices = _ctx.OrderServices.ToList();
            model.Services = orderServices.Count();

            model.TechnicalsVM = (from users in _ctx.Users.Where(ent => ent.IsActive && !ent.IsDeleted && ent.JobTitleId == (int)En_JobTitle.Technical)
                                  select new TechnicalsVM
                                  {
                                      ArabicName = users.ArabicName,
                                      EnglishName = users.EnglishName,
                                      OnWork = _ctx.Orders.FirstOrDefault(ent => ent.ResponsibleUserId == users.Id && ent.DeliverDate == DateTime.Now) != null,
                                      Lat = users.Lat,
                                      Long = users.Long,
                                      Online = users.OnLine
                                  }
                                ).ToList();
            model.ItemsVM = (from item in _ctx.Item.Where(ent => ent.IsActive && !ent.IsDeleted)
                             join images in _ctx.ItemImages.Take(1) on item.Id equals images.ItemId
                             select new ItemsVM
                             {
                                 ArabicName = item.ArabicName,
                                 EnglishName = item.EnglishName,
                                 ImagePath = images.ImagePath,
                                 Price = item.Price,
                                 Sales = orderItems.Where(ent => ent.OrderId == item.Id).Sum(ent => ent.Price)
                             }).ToList();
            model.ServeicesVM = (from item in _ctx.Services.Where(ent => ent.IsActive && !ent.IsDeleted)
                                 select new ServeicesVM
                                 {
                                     ArabicName = item.ArabicName,
                                     EnglishName = item.EnglishName,
                                     Sales = orderServices.Where(ent => ent.ServiceId == item.Id).Sum(ent => ent.Price)
                                 }).ToList();
            model.ItemesChart = GetTotalSales();
            model.ServeicesChart = GetTotalServeices();
            model.CountryVM = new List<CountryVM>();
            var countries = _ctx.City.Where(ent => ent.IsActive && !ent.IsDeleted);
            foreach (var item in countries)
            {
                var city = new CountryVM();
                city.ArabicName = item.ArabicName;
                city.EnglishName = item.EnglishName;
                city.Total = _ctx.Users.Where(ent => ent.CityId == item.Id && ent.JobTitleId != (int)En_JobTitle.Technical).Count();
                model.CountryVM.Add(city);
            }
            return model;
        }

        public ItemesChart GetTotalSales()
        {
            DateTime today = DateTime.Today;
            var dataOfweek = (int)today.DayOfWeek;
            var Orders = _ctx.OrderItems.Where(ent => ent.IsActive && !ent.IsDeleted).ToList();

            var itemsChart = new ItemesChart();
            itemsChart.ThisWeek = new List<double>();
            itemsChart.PastWeek = new List<double>();
            for (int i = 0; i < dataOfweek + 1; i++)
            {
                DateTime start = today.AddDays(dataOfweek - i);
                DateTime end = start.AddDays(1);
                var total = Orders.Where(ent => ent.CreationDate >= start && ent.CreationDate <= end).Sum(ent => ent.Price);
                itemsChart.ThisWeek.Add(total);

            }

            for (int i = 0; i < 7 - (dataOfweek + 1); i++) { itemsChart.ThisWeek.Add(0); }

            for (int i = 0; i < 7; i++)
            {
                DateTime start = today.AddDays(-(7 + dataOfweek + i));
                DateTime end = start.AddDays(1);
                var total = Orders.Where(ent => ent.CreationDate >= start && ent.CreationDate <= end).Sum(ent => ent.Price);
                itemsChart.PastWeek.Add(total);

            }

            return itemsChart;
        }


        public ServeicesChart GetTotalServeices()
        {
            DateTime today = DateTime.Today;
            var dataOfweek = (int)today.DayOfWeek;
            var Orders = _ctx.OrderItems.Where(ent => ent.IsActive && !ent.IsDeleted).ToList();

            var itemsChart = new ServeicesChart();

            itemsChart.ThisWeek = new List<double>();
            itemsChart.PastWeek = new List<double>();
            for (int i = 0; i < dataOfweek + 1; i++)
            {
                DateTime start = today.AddDays(dataOfweek - i);
                DateTime end = start.AddDays(1);
                var total = Orders.Where(ent => ent.CreationDate >= start && ent.CreationDate <= end).Sum(ent => ent.Price);
                itemsChart.ThisWeek.Add(total);

            }

            for (int i = 0; i < 7 - (dataOfweek + 1); i++) { itemsChart.ThisWeek.Add(0); }

            for (int i = 0; i < 7; i++)
            {
                DateTime start = today.AddDays(-(7 + dataOfweek + i));
                DateTime end = start.AddDays(1);
                var total = Orders.Where(ent => ent.CreationDate >= start && ent.CreationDate <= end).Sum(ent => ent.Price);
                itemsChart.PastWeek.Add(total);

            }

            return itemsChart;
        }

        public string TimeIsVaild(ApiServiceOrder order)
        {
            var startWorkDay = _ctx.SystemOption.FirstOrDefault(ent => ent.Id == (int)EN_SystemOptions.startWorkDay).Time;
            var endWorkDay = _ctx.SystemOption.FirstOrDefault(ent => ent.Id == (int)EN_SystemOptions.endWorkDay).Time;
            if (order.DeliverTimeFrom.TimeOfDay < startWorkDay)
                return "times of work start from " + startWorkDay;
            if (order.DeliverTimeTo.TimeOfDay > endWorkDay)
                return "times of work end from " + endWorkDay;

            var numOfTechicals = _ctx.Technicals.Where(ent => ent.ServiceId == order.ServiceId).Count();
            var usersWithServices = _ctx.OrderServices.Where(ent => ent.ServiceId == order.ServiceId && ent.Order.OrderTrackActionId != (int)EN_OrderActions.delivered && ent.Order.OrderTrackActionId != (int)EN_OrderActions.completed);
            var currentUser = usersWithServices.Where(s => (s.DeliverTimeFrom >= order.DeliverTimeFrom && s.DeliverTimeFrom <= order.DeliverTimeTo) ||
                                 (s.DeliverTimeTo <= order.DeliverTimeTo && s.DeliverTimeTo >= order.DeliverTimeFrom));
            if (currentUser.Count() < numOfTechicals)
            { return ""; }

            var start = order.DeliverTimeTo;
            var end = start.AddHours(2);

            var closed = start;
            while (end.TimeOfDay <= endWorkDay && end.Date==order.DeliverTimeFrom.Date)
            {
                currentUser = usersWithServices.Where(s => (s.DeliverTimeFrom >= start && s.DeliverTimeFrom <= end) ||
                            (s.DeliverTimeTo <= end && s.DeliverTimeTo >= start));

                if (currentUser.Count() < numOfTechicals)
                { return start.ToString(); }
                start =end;
                end = start.AddHours(2);
                closed = start;
            }


            return closed.ToString();
        }

    }
}
