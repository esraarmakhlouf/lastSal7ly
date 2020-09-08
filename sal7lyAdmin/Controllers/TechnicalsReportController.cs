using System;
using System.Collections.Generic;
using System.Linq;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using BL.SharedCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class TechnicalsReportController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly ISecurity _security;

        public TechnicalsReportController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.TechnicalsReport, EN_Permissions.View))
            { return Redirect("/Home"); }

            var Technicals = _uow.TechnicalsRepository.GetAll();
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                Technicals = Technicals.Include(ent => ent.User)
                                       .Where(ent => ent.User.CityId == AppSession.CurrentUser.CityId);

            Technicals = Technicals.Include(ent => ent.User).ThenInclude(ent => ent.Orders).ThenInclude(ent => ent.OrderService)
                                   .Include(ent => ent.Service);
            FillFormDropdowns(new Users());
            return View(Technicals.ToHashSet());
        }

        [HttpPost]
        public IActionResult Search(ReportSearchModel model)
        {
            var result = _uow.TechnicalsRepository.GetAll();
            //serach technical
            if (model.UserId != 0)
            {
                result = result.Where(ent => ent.UsersId == model.UserId);
            }
            //serach city
            if (model.CityId != 0)
            {
                result = result.Where(ent => ent.User.CityId == model.CityId);
            }
            //serach service
            if (model.ServiceId != 0)
            {
                result = result.Where(ent => ent.ServiceId == model.ServiceId);
            }

            result = result.Include(ent => ent.User).ThenInclude(ent => ent.Orders).ThenInclude(ent => ent.OrderService)
                           .Include(ent => ent.Service);
            //   result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();

            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                result = result.Where(ent => ent.User.CityId == AppSession.CurrentUser.CityId);
            return PartialView("_List", result.ToHashSet());
        }

        [HttpPost]
        public IActionResult SearchOrders(ReportSearchModel model)
        {
            var result = _uow.OrderRepository.GetAll().Include(ent => ent.Customer)
                                                     .Include(ent => ent.OrderTrackAction)
                                                     .Include(ent => ent.ResponsibleUser)
                                                     //.Include(ent => ent.OrderTrack)
                                                     //orders which user responsible for
                                                     //orders which has last user assignment with wait state to the current user
                                                     .Where(ent => (ent.ResponsibleUserId != null && ent.ResponsibleUserId == model.UserId));
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                result = result.Where(ent => ent.ResponsibleUser != null && ent.ResponsibleUser.CityId == AppSession.CurrentUser.CityId);
            switch (model.DateRangeId)
            {
                case "Today":
                    result = result.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date);
                    break;
                case "yesterday":
                    result = result.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-1));
                    break;
                case "Last week":
                    result = result.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-7));
                    break;
                case "previous x days":
                    result = result.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-model.PreviousDaysNum));
                    break;
                case "Custom":
                    result = result.Where(ent => ent.CreationDate.Date >= model.StartDate.Date && ent.CreationDate.Date <= model.EndDate.Date
                    );
                    break;
            }
            //   result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();

            return PartialView("_TechOrdersList", result.ToHashSet());
        }


        public IActionResult Orders(long id)
        {
            //var json = new OrderController(_uow, _security).UserOrders(UserId);
            //Check User Permission For this Page
            ViewBag.UserId = id;
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.View))
            { return Redirect("/Home"); }

            var orders = _uow.OrderRepository.GetAll().Include(ent => ent.Customer)
                                                     .Include(ent => ent.OrderTrackAction)
                                                     .Include(ent => ent.ResponsibleUser)
                                                     //.Include(ent => ent.OrderTrack)
                                                     //orders which user responsible for
                                                     //orders which has last user assignment with wait state to the current user
                                                     .Where(ent => (ent.ResponsibleUserId != null && ent.ResponsibleUserId == id))// || (ent.OrderTrackActionId == (int)OrderActions.ordered))
                                                     .ToHashSet();
            return View(orders);
        }

        private void FillFormDropdowns(Users entity)
        {
            var itemsList = _uow.ItemsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.itemsList = new MultiSelectList(itemsList, "Value", "Text");
            var orderActionList = _uow.OrderTrackActionRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.OrderActionList = new SelectList(orderActionList, "Value", "Text");

            var TechnicalsList = _uow.UsersRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.JobTitleId == (int)En_JobTitle.Technical);
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                TechnicalsList = TechnicalsList.Where(ent => ent.CityId == AppSession.CurrentUser.CityId);
            ViewBag.TechnicalsList = new SelectList(TechnicalsList.Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet(), "Value", "Text");

            var CitiesList = _uow.CityRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.CitiesList = new SelectList(CitiesList, "Value", "Text");

            var ServicesList = _uow.ServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.ServicesList = new SelectList(ServicesList, "Value", "Text");
        }
    }
}