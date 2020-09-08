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
    public class ServicesReportController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly ISecurity _security;

        public ServicesReportController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.View))
            { return Redirect("/Home"); }

            var orders = _uow.OrderRepository.GetMany(ent => ent.OrderService != null)
                                             .Include(ent => ent.Customer)
                                             .Include(ent => ent.OrderTrackAction)
                                             .Include(ent => ent.ResponsibleUser)
                                             .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                             .ToHashSet();
            if (AppSession.CurrentUser != null && AppSession.CurrentUser.JobTitleId == (long)En_JobTitle.egent)
                orders = orders.Where(ent => ent.Customer != null && ent.Customer.CityId == AppSession.CurrentUser.CityId).ToHashSet();
            FillFormDropdowns(new Order());

            return View(orders);
        }

        [HttpPost]
        public IActionResult Search(ReportSearchModel model)
        {
            var result = _uow.OrderRepository.GetMany(ent => ent.OrderService != null);
            if (model.CustomerId != 0)
            {
                result = result.Where(ent => ent.CustomerId == model.CustomerId);
            }
            if (model.UserId != 0)
            {
                result = result.Where(ent => ent.ResponsibleUserId == model.UserId);
            }
            if (model.OrderActionId != 0)
            {
                result = result.Where(ent => ent.OrderTrackActionId == model.OrderActionId);
            }

            if (model.Code != null && model.Code != "")
            {
                result = result.Where(ent => ent.Code == model.Code);
            }

            //search with order customer/supplier city
            if (model.CityId != 0)
            {
                result = result.Where(ent => ent.Customer.CityId == model.CityId || ent.ResponsibleUser.CityId == model.CityId);
            }

            switch (model.DateRangeId)
            {
                case "Today":
                    result = result.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date);
                    break;
                case "yesterday":
                    result = result.Where(ent => ent.CreationDate.Date == DateTime.Now.AddDays(-1).Date);
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
            result = result.Include(ent => ent.Customer)
                           .Include(ent => ent.OrderTrackAction)
                           .Include(ent => ent.ResponsibleUser)
                           .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service);

            //   result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();

            return PartialView("_List", result.ToHashSet());
        }

        public IActionResult OpenForm(long? id)
        {
            var entity = new Order();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.OrderRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            FillFormDropdowns(entity);
            return View(entity);
        }

        public IActionResult OrderTrack(long? id)
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var result = new Order();
            if (id != null)
            {
                result = _uow.OrderRepository.GetAll().Where(ent => ent.Id == id)
                                                     .Include(ent => ent.Customer)
                                                     .Include(ent => ent.ResponsibleUser)
                                                     .Include(ent => ent.OrderTrackAction)
                                                     .Include(ent => ent.OrderTracks).ThenInclude(ent => ent.OrderTrackAction)
                                                     .Include(ent => ent.OrderItems).ThenInclude(ent => ent.Item).ThenInclude(ent => ent.Service)
                                                     .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                     .Include(ent => ent.OrderTechAssign).ThenInclude(ent => ent.TechnicalUser)
                                                     .FirstOrDefault();
                if (result == null)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            return View(result);
        }


        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.RejectOrder))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

            try
            {
                var order = _uow.OrderRepository.GetById(id);
                if (order == null)// || order.OrderTrackActionId==(int)OrderActions.completed)
                {
                    status = "error";
                    errorMessage = "Delete Error message";
                }
                else
                {
                    order.ModifiedBy = AppSession.CurrentUser.Id;
                    order.ModificationDate = DateTime.Now;
                    order.OrderTrackActionId = (int)EN_OrderActions.rejected;
                    _uow.OrderRepository.Update(order);
                    //add order track action
                    OrderTrack action = new OrderTrack();
                    action.CreatedBy = AppSession.CurrentUser.Id;
                    action.CreationDate = DateTime.Now;
                    action.OrderId = order.Id;
                    action.OrderTrackActionId = (int)EN_OrderActions.rejected;
                    _uow.OrderTrackRepository.Add(action);
                    _uow.Save();
                }
            }
            catch (Exception ex)
            {
                status = "error";
                errorMessage = "Delete Error message";
            }
            return Json(new { status = status, ErrorMessage = errorMessage });
        }

        public IActionResult CompleteOrder(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.CompleteOrder))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

            try
            {
                var order = _uow.OrderRepository.GetById(id);
                if (order == null || order.OrderTrackActionId >= (int)EN_OrderActions.completed)
                {
                    status = "error";
                    errorMessage = "Order not found";
                }
                else
                {
                    order.ModifiedBy = AppSession.CurrentUser.Id;
                    order.ModificationDate = DateTime.Now;
                    order.OrderTrackActionId = (int)EN_OrderActions.completed;
                    order.DeliverDate = DateTime.Now;
                    _uow.OrderRepository.Update(order);
                    //add order track action
                    OrderTrack action = new OrderTrack();
                    action.CreatedBy = AppSession.CurrentUser.Id;
                    action.CreationDate = DateTime.Now;
                    action.OrderId = order.Id;
                    action.OrderTrackActionId = (int)EN_OrderActions.completed;
                    _uow.OrderTrackRepository.Add(action);
                    _uow.Save();
                }
            }
            catch (Exception ex)
            {
                status = "error";
                errorMessage = "Complete Order Error message";
            }
            return Json(new { status = status, ErrorMessage = errorMessage });
        }

        private void FillFormDropdowns(Order entity)
        {
            var itemsList = _uow.ItemsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.itemsList = new MultiSelectList(itemsList, "Value", "Text");

            var customersList = _uow.CustomerRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet();
            if (AppSession.CurrentUser?.JobTitleId == (long)En_JobTitle.egent)
                customersList = customersList.Where(ent => ent.CityId == AppSession.CurrentUser.CityId).ToHashSet();
            ViewBag.customersList = new SelectList(customersList.Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }), "Value", "Text", entity.CustomerId);

            var orderActionList = _uow.OrderTrackActionRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.OrderActionList = new SelectList(orderActionList, "Value", "Text");

            var TechnicalsList = _uow.UsersRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.JobTitleId == (int)En_JobTitle.Technical);
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                TechnicalsList = TechnicalsList.Where(ent => ent.CityId == AppSession.CurrentUser.CityId);
            ViewBag.TechnicalsList = new SelectList(TechnicalsList.Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet(), "Value", "Text");


            var CustomersList = _uow.CustomerRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet();
            if (AppSession.CurrentUser?.JobTitleId == (long)En_JobTitle.egent)
                customersList = CustomersList.Where(ent => ent.CityId == AppSession.CurrentUser.CityId).ToHashSet();
            ViewBag.CustomersList = new SelectList(CustomersList.Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }), "Value", "Text", entity.CustomerId);

            var CitiesList = _uow.CityRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.CitiesList = new SelectList(CitiesList, "Value", "Text");

        }

        public IActionResult AssignResponsible(long? id)
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var result = _uow.OrderRepository.GetMany(ent => ent.Id == id).Include(ent => ent.OrderService).FirstOrDefault();
            if (id == null || result == null || result.OrderService == null)
            {
                return NotFound();
            }
            var AssignResponsibleList = _uow.UsersRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.Technical != null && ent.Technical.ServiceId == result.OrderService.ServiceId)
                                        .Include(ent => ent.Technical)
                                        .Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName })
                                        .ToHashSet();
            ViewBag.AssignResponsibleList = new SelectList(AssignResponsibleList, "Value", "Text");
            return View(result);
        }

        [HttpPost]
        public IActionResult SaveAssignResponsible(long orderId, long responsibleUserId)
        {
            string status = "success";
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var order = _uow.OrderRepository.Get(ent => ent.Id == orderId);
            var technical = _uow.UsersRepository.Get(ent => ent.Id == responsibleUserId);
            if (order == null)
            {
                status = "error";
                return Json(new { status = status, ErrorMessage = "Order Not Found" });
            }
            else if (technical == null)
            {
                status = "error";
                return Json(new { status = status, ErrorMessage = "Supplier Not Found" });
            }
            else if (order.ResponsibleUserId != null && order.ResponsibleUserId != 0)
            {
                status = "error";
                return Json(new { status = status, ErrorMessage = "Order Already has Responsible User !" });
            }
            else
            {
                //update order responsible
                order.ResponsibleUserId = responsibleUserId;
                order.ModificationDate = DateTime.Now;
                order.ModifiedBy = AppSession.CurrentUser.Id;

                order.OrderTrackActionId = (int)EN_OrderActions.accpted_by_user;
                _uow.OrderRepository.Update(order);

                //add order track action to order
                OrderTrack action = new OrderTrack();
                action.CreationDate = DateTime.Now;
                action.OrderTrackActionId = (int)EN_OrderActions.accpted_by_user;
                action.OrderId = orderId;
                _uow.OrderTrackRepository.Add(action);

                //send notification to customer that his order accepted by technical
                var customername = _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                var technicalname = _uow.UsersRepository.Get(ent => ent.Id == responsibleUserId).ArabicName;
                Notification notification = new Notification();
                notification.Text = "Order With Code: " + order.Code + " Accepted by Technical: " + technicalname;
                notification.ToUSer = order.CustomerId;
                notification.URl = "/Order/OrderTrack" + orderId;
                notification.TypeOfUser = (int)EN_TypeUser.Customer;
                _uow.NotificationRepository.Add(notification);

                //send notification with order assigned by admin to supplier 
                Notification techNotification = new Notification();
                techNotification.Text = "Order With Code: " + order.Code + " For Customer " + customername + " Added to you By Admin ";
                techNotification.TypeOfUser = (int)EN_TypeUser.Technical;
                notification.ToUSer = responsibleUserId;
                _uow.NotificationRepository.Add(techNotification);

                _uow.Save();
                return Json(new { status = status });
            }
        }
    }
}