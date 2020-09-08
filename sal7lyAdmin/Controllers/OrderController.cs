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
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly ISecurity _security;

        public OrderController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.View))
            { return Redirect("/Home"); }
            var orders = _uow.OrderRepository.GetMany(ent => ent.OrderItems != null && ent.OrderItems.Count > 0)
                                             .Include(ent => ent.Customer)
                                             .Include(ent => ent.OrderTrackAction)
                                             .Include(ent => ent.ResponsibleUser)
                                             .Include(ent => ent.OrderItems)
                                             .OrderByDescending(ent => ent.CreationDate).ToHashSet();
            FillFormDropdowns(new Order());

            return View(orders);
        }

        [HttpPost]
        public IActionResult Search(ReportSearchModel model)
        {
            var orders = _uow.OrderRepository.GetMany(ent => ent.OrderItems != null && ent.OrderItems.Count > 0);
            //search with code
            if (model.Code != "" && model.Code != null)
            {
                orders = orders.Where(ent => ent.Code == model.Code);
            }
            //search with order track action
            if (model.OrderActionId != 0)
            {
                orders = orders.Where(ent => ent.OrderTrackActionId == model.OrderActionId);
            }
            //search with order supplier
            if (model.UserId != 0)
            {
                orders = orders.Where(ent => ent.ResponsibleUserId == model.UserId);
            }
            //search with order customer
            if (model.CustomerId != 0)
            {
                orders = orders.Where(ent => ent.CustomerId == model.CustomerId);
            }
            //search with order customer/supplier city
            if (model.CityId != 0)
            {
                orders = orders.Where(ent => ent.Customer.CityId == model.CityId || ent.ResponsibleUser.CityId == model.CityId);
            }

            switch (model.DateRangeId)
            {
                case "Today":
                    orders = orders.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date);
                    break;
                case "yesterday":
                    orders = orders.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-1));
                    break;
                case "Last week":
                    orders = orders.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-7));
                    break;
                case "previous x days":
                    orders = orders.Where(ent => ent.CreationDate.Date >= DateTime.Now.Date.AddDays(-model.PreviousDaysNum));
                    break;
                case "Custom":
                    orders = orders.Where(ent => ent.CreationDate.Date >= model.StartDate.Date && ent.CreationDate.Date <= model.EndDate.Date
                    );
                    break;
            }
            var result = orders.Include(ent => ent.Customer)
                              .Include(ent => ent.OrderTrackAction)
                              .Include(ent => ent.ResponsibleUser)
                              .Include(ent => ent.OrderItems)
                              .OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return PartialView("_List", result);
        }

        public IActionResult OrderTrack(long? id)
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.Edit))
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
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult OpenForm(long? id)
        {
            var entity = new Order();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.OrderRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            FillFormDropdowns(entity);
            return View(entity);
        }
        [HttpPost]
        public IActionResult Save(Order entity, IFormCollection form)
        {
            //add order
            string strReturnMsg = "error";
            try
            {
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.Order, _uow);
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    entity.OrderTrackActionId = (int)EN_OrderActions.ordered;

                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.OrderRepository.Add(entity); }

                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                    entity.ModifiedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.OrderRepository.Update(entity); }

                }
                _uow.Save();
                #region Save Items

                if (!string.IsNullOrEmpty(form["Items"]))
                {
                    var selectedItems = form["Items"].ToString().Split(",").Select(ent => long.Parse(ent)).ToHashSet();
                    foreach (var selectedItem in selectedItems)
                    {
                        var dbObj = _uow.OrderItemsRepository.Get(ent => ent.ItemId == selectedItem && ent.OrderId == entity.Id);
                        if (dbObj == null)
                        {
                            var obj = new OrderItems();
                            obj.OrderId = entity.Id;
                            obj.ItemId = selectedItem;
                            obj.Quantity = 1;
                            _uow.OrderItemsRepository.Add(obj);
                        }
                    }

                    var deletedOrderItetms = _uow.OrderItemsRepository.GetMany(ent => ent.OrderId == entity.Id && !selectedItems.Contains(ent.ItemId)).Select(ent => ent.Id).ToHashSet();
                    if (deletedOrderItetms.Count() > 0)
                    {
                        _uow.ExecuteSqlCommand("DELETE FROM OrderItems WHERE Id IN(" + string.Join(",", deletedOrderItetms) + ")");
                    }
                    _uow.Save();
                }
                else
                { _uow.ExecuteSqlCommand("DELETE FROM OrderItems WHERE OrderId=" + entity.Id); }

                #endregion

                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            FillFormDropdowns(entity);
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        //reject order
        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.RejectOrder))
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
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.CompleteOrder))
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



            var SuppliersList = _uow.UsersRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.JobTitleId == (int)En_JobTitle.Supplier).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.SuppliersList = new SelectList(SuppliersList, "Value", "Text");

            var CustomersList = _uow.CustomerRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet();
            if (AppSession.CurrentUser?.JobTitleId == (long)En_JobTitle.egent)
                customersList = customersList.Where(ent => ent.CityId == AppSession.CurrentUser.CityId).ToHashSet();
            ViewBag.CustomersList = new SelectList(CustomersList.Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }), "Value", "Text", entity.CustomerId);


            var CitiesList = _uow.CityRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.CitiesList = new SelectList(CitiesList, "Value", "Text");
        }

        public IActionResult AssignResponsible(long? id)
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.ServicesReport, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var result = _uow.OrderRepository.Get(ent => ent.Id == id);
            if (id == null || result == null)
            {
                return NotFound();
            }

            var AssignResponsibleList = _uow.UsersRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.JobTitleId == (int)En_JobTitle.Supplier)
                                        .Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName })
                                        .ToHashSet();
            ViewBag.AssignResponsibleList = new SelectList(AssignResponsibleList, "Value", "Text");
            return View(result);
        }

        [HttpPost]
        public IActionResult SaveAssignResponsible(long orderId, long responsibleUserId)
        {
            string status = "success";
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Order, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var order = _uow.OrderRepository.Get(ent => ent.Id == orderId);
            var supplier = _uow.UsersRepository.Get(ent => ent.Id == responsibleUserId);
            if (order == null)
            {
                status = "error";
                return Json(new { status = status, ErrorMessage = "Order Not Found" });
            }
            else if (supplier == null)
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

                var customername = _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                var suppliername = _uow.UsersRepository.Get(ent => ent.Id == responsibleUserId).ArabicName;

                //send notification to customer that his order accepted by technical
                Notification notification = new Notification();
                notification.Text = "Order With Code: " + order.Code + " Accepted by supplier: " + suppliername;
                notification.ToUSer = order.CustomerId;
                notification.URl = "/Order/OrderTrack" + orderId;
                notification.TypeOfUser = (int)EN_TypeUser.Customer;
                _uow.NotificationRepository.Add(notification);

                ////send notification with order assigned by admin to supplier 
                //Notification techNotification = new Notification();
                //techNotification.Text = "Order With Code: " + order.Code + " For Customer " + customername + " Added to you By Admin ";
                //techNotification.TypeOfUser = (int)EN_TypeUser.Technical;
                //notification.ToUSer = responsibleUserId;
                //_uow.NotificationRepository.Add(techNotification);

                _uow.Save();
                return Json(new { status = status });
            }
        }
    }
}