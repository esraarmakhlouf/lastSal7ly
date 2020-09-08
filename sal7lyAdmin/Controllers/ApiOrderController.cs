using BL.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sal7lyAdmin.Services;
using Model;
using BL.Security;
using Model.APIModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using BL.Secuirty;
using Microsoft.EntityFrameworkCore;
using static BL.SharedCS.Enumrations;
using BL.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace sal7lyAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///[Authorize]
    public class ApiOrderController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IAuthenticateService _authService;
        private readonly IHostingEnvironment _env;

        public ApiOrderController(IUnitOfWork uow, ISecurity security, IAuthenticateService authService, IHostingEnvironment env) { _uow = uow; _security = security; _authService = authService; _env = env; }
        const String folderName = "files";
        readonly String folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        private bool AddOrderAction(int orderActionID, long orderId)
        {
            try
            {
                OrderTrack action = new OrderTrack();
                action.CreationDate = DateTime.Now;
                action.OrderTrackActionId = orderActionID;
                action.OrderId = orderId;
                _uow.OrderTrackRepository.Add(action);
                _uow.Save();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        #region Customer Orders

        [HttpPost, Route("AddItemsOrder")]
        public IActionResult AddItemsOrder(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == CustomerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                else
                {
                    var cartList = _uow.ItemsCartRepository.GetMany(ent => ent.CustomerId == CustomerId).ToList();
                    if (cartList == null || cartList.Count() == 0)
                    {
                        return Ok(new ApiResponseModel
                        {

                            Status = EN_ResponseStatus.Faild,
                            Message = "Empty Cart List Found for Customer with Id: " + CustomerId,
                            Data = null,
                            Errors = new string[] { "Empty Cart List Found for Customer with Id: " + CustomerId }
                        });
                    }
                    else
                    {
                        //add order
                        var order_obj = new Order();
                        order_obj.Code = UIHelper.GeneratCode(EN_Screens.Order, _uow);
                        order_obj.CreationDate = DateTime.Now;
                        order_obj.OrderTrackActionId = (int)EN_OrderActions.ordered;
                        order_obj.CustomerId = CustomerId;
                        _uow.OrderRepository.Add(order_obj);
                        _uow.Save();
                        //order track action
                        AddOrderAction((int)EN_OrderActions.ordered, order_obj.Id);
                        //save cart list items to orderitems
                        foreach (var cartitem in cartList)
                        {
                            //check item exist
                            var item = _uow.ItemsRepository.GetById(cartitem.ItemId);
                            if (item != null)
                            {
                                //order item not added
                                var orderItemObj = _uow.OrderItemsRepository.Get(ent => ent.ItemId == cartitem.ItemId && ent.OrderId == order_obj.Id);
                                if (orderItemObj == null)
                                {
                                    orderItemObj = new OrderItems();
                                    orderItemObj.OrderId = order_obj.Id;
                                    orderItemObj.ItemId = cartitem.ItemId;
                                    orderItemObj.Quantity = cartitem.Quantity;
                                    orderItemObj.Price = item.Price;
                                    //calculate discount value on item
                                    //var discount = 5.5;
                                    //orderItemObj.DiscountPrice = item.Price - discount;
                                    orderItemObj.DiscountPrice = item.Price;
                                    _uow.OrderItemsRepository.Add(orderItemObj);

                                    //delete cart list item
                                    _uow.ItemsCartRepository.Delete(cartitem.Id);
                                }
                            }

                        }
                        _uow.Save();
                        //add notifications
                        AddOrderNotifications(order_obj.Code, order_obj.Id, CustomerId, true);

                        return Ok(new ApiResponseModel
                        {

                            Status = EN_ResponseStatus.Success,
                            Message = "Record Saved Successfully",
                            Data = new { ID = order_obj.Id },
                            Errors = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        private TimeSpan stringtotime(string strtime)
        {
            List<string> time = strtime.Split(new char[] { ':' }).ToList();
            while (time.Count < 3) time.Insert(0, "00");
            return new TimeSpan(Int32.Parse(time[0]), Int32.Parse(time[1]), Int32.Parse(time[2]));
        }
        [HttpPost, Route("AddServiceOrder")]
        public async Task<IActionResult> AddServiceOrder([FromBody] ApiServiceOrder order)
        {
            try
            {
                //check Service exist
                var service = _uow.ServicesRepository.GetById(order.ServiceId);
                if (service == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Service with Id: " + order.ServiceId + " not found",
                        Data = null,
                        Errors = new string[] { "Service with Id: " + order.ServiceId + " not found" }
                    });
                }
                #region Save Order

                //var DeliverTimeFrom = stringtotime(order.DeliverTimeFrom);
                var vaildTime = _uow.SharedRepository.TimeIsVaild(order);
                if (vaildTime != "")
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Error: " + vaildTime,
                        Data = null,
                        Errors = new string[] { "Error: " + vaildTime }
                    });
                var order_obj = new Order();
                order_obj.Code = UIHelper.GeneratCode(EN_Screens.Order, _uow);
                order_obj.CreationDate = DateTime.Now;
                order_obj.OrderTrackActionId = (int)EN_OrderActions.ordered;
                order_obj.CustomerId = order.CustomerId;
                _uow.OrderRepository.Add(order_obj);

                _uow.Save();
                //order track action
                AddOrderAction((int)EN_OrderActions.ordered, order_obj.Id);

                #region Save Service

                var orderservice = new OrderServices();
                orderservice.OrderId = order_obj.Id;
                orderservice.ServiceId = order.ServiceId;
                orderservice.ServiceComment = order.ServiceComment;
                orderservice.IsPriority = order.IsPriority;
                orderservice.DeliverTimeFrom = DateTime.Now;//order.DeliverTimeFrom;
                orderservice.DeliverTimeTo = order.DeliverTimeTo;
                orderservice.PromoCode = order.PromoCode;
                _uow.OrderServicesRepository.Add(orderservice);
                _uow.Save();

                #endregion

                AddOrderNotifications(order_obj.Code, order_obj.Id, order_obj.CustomerId, false);

                #endregion
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Saved Successfully",
                    Data = new { ID = order_obj.Id },
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("AddServiceOrderImages")]
        public async Task<IActionResult> AddServiceOrderImages([FromForm] List<IFormFile> files, long orderId)
        {
            try
            {
                var order = _uow.OrderRepository.GetMany(ent => ent.Id == orderId && ent.OrderService != null).Include(ent => ent.OrderService).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Service Order with Id: " + orderId + " not found",
                        Data = null,
                        Errors = new string[] { "Service Order with Id: " + orderId + " not found" }
                    });
                }


                #region Order Service images
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.OrderServiceUploads);
                if (files != null && files.Count > 0)
                {
                    var orderserviceimage = new OrderServicesImages();
                    foreach (var file in files)
                    {
                        var filename = await Images.SaveFile(uploadsFolder, file, lastName: "");
                        if (filename != "" && filename != null)
                        {
                            orderserviceimage = new OrderServicesImages();
                            orderserviceimage.OrderId = orderId;
                            orderserviceimage.ImagePath = filename;
                            _uow.ServicesImagesRepository.Update(orderserviceimage);
                        }
                    }
                    _uow.Save();
                }


                #endregion

                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Saved Successfully",
                    Data = null,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("AddServiceOrderImage")]
        public async Task<IActionResult> AddServiceOrderImage([FromForm] IFormFile file, long orderId)
        {
            try
            {
                var order = _uow.OrderRepository.GetMany(ent => ent.Id == orderId && ent.OrderService != null).Include(ent => ent.OrderService).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Service Order with Id: " + orderId + " not found",
                        Data = null,
                        Errors = new string[] { "Service Order with Id: " + orderId + " not found" }
                    });
                }


                #region Order Service images
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.OrderServiceUploads);
                var orderserviceimage = new OrderServicesImages();
                var filename = await Images.SaveFile(uploadsFolder, file, lastName: "");
                if (filename != "" && filename != null)
                {
                    orderserviceimage = new OrderServicesImages();
                    orderserviceimage.OrderId = orderId;
                    orderserviceimage.ImagePath = filename;
                    _uow.ServicesImagesRepository.Update(orderserviceimage);
                }
                _uow.Save();
                #endregion

                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Saved Successfully",
                    Data = null,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        private void AddOrderNotifications(string Code, long OrderId, long CustomerId, bool isOrder)
        {
            //send notification with order added to customer
            Notification notification = new Notification();
            notification.Text = "Order Added Successfully With Code: " + Code;
            notification.ToUSer = CustomerId;
            notification.TypeOfUser = (int)EN_TypeUser.Customer;
            _uow.NotificationRepository.Add(notification);

            //send notification with order added to admin
            Notification adminNotification = new Notification();
            adminNotification.Text = "New Order Added With Code: " + Code + " By Customer " + _uow.CustomerRepository.Get(ent => ent.Id == CustomerId).ArabicName;
            adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
            if (isOrder)
            {
                adminNotification.URl = AppSession.AppURL + "Order/OrderTrack/" + OrderId;
            }
            else { adminNotification.URl = AppSession.AppURL + "ServicesReport/OrderTrack/" + OrderId; }
            _uow.NotificationRepository.Add(adminNotification);
            _uow.Save();
        }
        [HttpGet, Route("GetCustOrders")]
        public IActionResult GetCustOrders(long customerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == customerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + customerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + customerId + " not found" }
                    });
                }
                var orders = _uow.OrderRepository.GetMany(ent => ent.CustomerId == customerId).Include(ent => ent.OrderTrackAction)
                                                 .Select(ent => new
                                                 {
                                                     Id = ent.Id,
                                                     code = ent.Code,
                                                     CreationDate = ent.CreationDate,
                                                     Status = ent.OrderTrackAction.ArabicName
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustFinishServOrders")]
        public IActionResult GetCustFinishServOrders(long customerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == customerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + customerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + customerId + " not found" }
                    });
                }
                var orders = _uow.OrderRepository.GetMany(ent => ent.IsActive && ent.CustomerId == customerId && ent.OrderTrackActionId >= (int)EN_OrderActions.completed && ent.OrderService != null)
                                                 .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                 .Select(ent => new
                                                 {
                                                     code = ent.Code,
                                                     Name = ent.OrderService.Service.ArabicName,
                                                     Image = ent.OrderService.ServiceImage,
                                                     deliverDate = ent.DeliverDate,
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustNotFinishServOrders")]
        public IActionResult GetCustNotFinishServOrders(long customerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == customerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + customerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + customerId + " not found" }
                    });
                }
                var url = AppSession.AppURL + AppSession.OrderServiceUploads + "/";

                var orders = _uow.OrderRepository.GetMany(ent => ent.OrderTrackActionId >= (int)EN_OrderActions.ordered && ent.OrderTrackActionId < (int)EN_OrderActions.completed && ent.CustomerId == customerId && ent.OrderService != null)
                                                 .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                 .Include(ent => ent.OrderTrackAction)
                                                 .Include(ent => ent.ServicesImages)
                                                 .Include(ent => ent.ResponsibleUser)
                                                 .Select(ent => new
                                                 {
                                                     Id = ent.Id,
                                                     code = ent.Code,
                                                     CreationDate = ent.CreationDate,
                                                     Name = ent.OrderService.Service.ArabicName,
                                                     Image = ent.ServicesImages.Select(i => new { ImagePath = url + i.ImagePath }),

                                                     Comment = ent.OrderService.ServiceComment,
                                                     StatusId = ent.OrderTrackActionId,
                                                     Status = ent.OrderTrackAction.ArabicName,
                                                     ent.OrderService.DeliverTimeFrom,
                                                     ent.OrderService.DeliverTimeTo,
                                                     ResponsibleName = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.ArabicName,
                                                     ResponsiblePhone = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.Mobile,
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustFinishItemsOrders")]
        public IActionResult GetCustFinishItemsOrders(long customerId)
        {
            try
            {
                Request.Headers.TryGetValue("token", out var token);

                var customer = _authService.GetCustomerByToken(token);
                //var customer = _uow.CustomerRepository.Get(ent => ent.Id == customerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + customerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + customerId + " not found" }
                    });
                }
                var url = AppSession.AppURL + AppSession.ItemsUploads + "/";

                var orders = _uow.OrderRepository.GetMany(ent => ent.IsActive && ent.CustomerId == customerId && ent.OrderTrackActionId >= (int)EN_OrderActions.completed && ent.OrderItems != null && ent.OrderItems.Count() > 0)
                                                 .Include(ent => ent.OrderItems).ThenInclude(ent => ent.Item).ThenInclude(ent => ent.ItemImages)
                                                 .Select(ent => new
                                                 {
                                                     code = ent.Code,
                                                     items = ent.OrderItems.Select(item => new
                                                     {
                                                         Name = item.Item.EnglishName,
                                                         Image = url + item.Item.ItemImages.FirstOrDefault().ImagePath,
                                                         Price = item.Price,
                                                         DiscountPrice = item.DiscountPrice,
                                                     }),
                                                     deliverDate = ent.DeliverDate
                                                 }).ToHashSet();

                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustNotFinishItemsOrders")]
        public IActionResult GetCustNotFinishItemsOrders(long customerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == customerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer with Id: " + customerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + customerId + " not found" }
                    });
                }
                var url = AppSession.AppURL + AppSession.ItemsUploads + "/";

                var orders = _uow.OrderRepository.GetMany(ent => ent.OrderTrackActionId >= (int)EN_OrderActions.ordered && ent.OrderTrackActionId < (int)EN_OrderActions.completed && ent.CustomerId == customerId && ent.OrderItems != null && ent.OrderItems.Count() > 0)
                                                 .Include(ent => ent.ResponsibleUser)
                                                 .Include(ent => ent.OrderItems).ThenInclude(ent => ent.Item).ThenInclude(ent => ent.ItemImages)
                                                .Include(ent => ent.OrderTrackAction)
                                                 .Select(ent => new
                                                 {
                                                     Id = ent.Id,
                                                     code = ent.Code,
                                                     CreationDate = ent.CreationDate,
                                                     items = ent.OrderItems.Select(item => new
                                                     {
                                                         Name = item.Item.EnglishName,
                                                         Image = (item.Item.ItemImages != null && item.Item.ItemImages.Count > 0) ?
                                                         url + item.Item.ItemImages.FirstOrDefault().ImagePath : null,
                                                         Price = item.Item.Price,
                                                         DiscountPrice = item.Price,
                                                         Quantity = item.Quantity,

                                                     }),

                                                     StatusId = ent.OrderTrackActionId,
                                                     Status = ent.OrderTrackAction.ArabicName,
                                                     ResponsibleName = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.ArabicName,
                                                     ResponsiblePhone = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.Mobile,
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustOrderTrack")]
        public IActionResult GetCustOrderTrack(long orderId)
        {
            try
            {
                var order = _uow.OrderRepository.GetMany(ent => ent.IsActive && ent.Id == orderId && ent.OrderService != null)
                                                .Include(ent => ent.ResponsibleUser)
                                                .Include(ent => ent.OrderTrackAction)
                                                .Include(ent => ent.OrderService)
                                                .Select(ent => new
                                                {
                                                    code = ent.Code,
                                                    TrackAction = ent.OrderTrackAction.ArabicName,
                                                    ent.OrderService.DeliverTimeFrom,
                                                    ent.OrderService.DeliverTimeTo,
                                                    ResponsibleName = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.ArabicName,
                                                    ResponsiblePhone = (ent.ResponsibleUser == null) ? null : ent.ResponsibleUser.Mobile,
                                                }).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Order with Id: " + orderId + " not found",
                        Data = null,
                        Errors = new string[] { "Order with Id: " + orderId + " not found" }
                    });
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrieved Successfully",
                    Data = order,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("CancelCustOrder")]
        public IActionResult CancelCustOrder(long OrderId)
        {
            try
            {
                var order = _uow.OrderRepository.Get(ent => ent.Id == OrderId);
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                else if (order.OrderTrackActionId == (int)EN_OrderActions.canceled)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order already Canceled !!",
                        Data = null,
                        Errors = new string[] { "This Order already Canceled !!" }
                    });
                }
                else if (order.OrderTrackActionId >= (int)EN_OrderActions.completed || order.OrderTrackActionId == (int)EN_OrderActions.rejected)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Can't Be Canceled !!",
                        Data = null,
                        Errors = new string[] { "This Order Can't Be Canceled !!" }
                    });
                }
                else
                {
                    //cancel order
                    order.ModificationDate = DateTime.Now;
                    order.OrderTrackActionId = (int)EN_OrderActions.canceled;
                    //add cancel order track action
                    AddOrderAction((int)EN_OrderActions.canceled, order.Id);
                    _uow.OrderRepository.Update(order);
                    //technical assig
                    var last_tech_assigned = _uow.OrderTechnicalAssignmentRepository.GetMany(ent => ent.OrderId == order.Id).OrderByDescending(ent => ent.CreationDate).FirstOrDefault();
                    if (last_tech_assigned != null && last_tech_assigned.status != (int)EN_OrderTechnicalAssignmentStatus.rejected)
                    {
                        last_tech_assigned.ModificationDate = DateTime.Now;
                        last_tech_assigned.status = (int)EN_OrderTechnicalAssignmentStatus.notavailable;
                        _uow.OrderTechnicalAssignmentRepository.Update(last_tech_assigned);
                        //send notification order not available
                        Notification notification = new Notification();
                        notification.Text = "Order With Code: " + order.Code + " Not Available now to Serve";
                        notification.ToUSer = last_tech_assigned.TechnicalUserId ?? 0;
                        notification.TypeOfUser = (int)EN_TypeUser.Technical;
                        _uow.NotificationRepository.Add(notification);
                    }

                    //send notification with order canceled to admin
                    Notification adminNotification = new Notification();
                    adminNotification.Text = "Order With Code: " + order.Code + " Canceled By Customer " + _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                    adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                    adminNotification.URl = AppSession.AppURL + "Order/OrderTrack/" + order.Id;
                    _uow.NotificationRepository.Add(adminNotification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Record Canceled Successfully",
                        Data = null,
                        Errors = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetMostOrders")]
        public IActionResult GetMostOrders()
        {
            try
            {
                var services = _uow.OrderServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted)
                    .GroupBy(ent => ent.ServiceId)
                                   .OrderByDescending(ent => ent.Count()).Take(10)
                                   .Select(ent => new {
                                       id = ent.Key,
                                       name = ent.Select(e => e.Service.ArabicName).FirstOrDefault(),
                                       count = ent.Count()
                                   });

                var msg = "";
                if (services != null && services.Count() > 0)
                {
                    msg = "Most Orders Retrived Successfully";
                }
                else
                {
                    msg = "No Orders Found";
                    services = null;
                }
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = services,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }
        #endregion

        #region Technical Orders

        [HttpGet, Route("GetUserFinishOrders")]
        public IActionResult GetUserFinishOrders(long userId)
        {
            try
            {
                var user = _uow.UsersRepository.Get(ent => ent.Id == userId);
                if (user == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This User Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This User Is  Not Found !!" }
                    });
                }
                var orders = _uow.OrderRepository.GetMany(ent => ent.IsActive && ent.ResponsibleUserId == userId && ent.OrderTrackActionId >= (int)EN_OrderActions.completed && ent.OrderService != null)
                                                 .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                 .Include(ent => ent.Customer)
                                                 .Select(ent => new
                                                 {
                                                     code = ent.Code,
                                                     CustomerName = ent.Customer.ArabicName,
                                                     Rate = ent.OrderService.Rate,
                                                     Price = ent.OrderService.Price
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetUserNotFinishOrders")]
        public IActionResult GetUserNotFinishOrders(long userId)
        {
            try
            {
                var user = _uow.UsersRepository.Get(ent => ent.Id == userId);
                if (user == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This User Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This User Is  Not Found !!" }
                    });
                }
                var orders = _uow.OrderRepository.GetMany(ent => ent.IsActive && ent.OrderTrackActionId < (int)EN_OrderActions.completed && ent.OrderTrackActionId >= (int)EN_OrderActions.ordered && ent.ResponsibleUserId == userId && ent.OrderService != null)
                                                 .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                 .Include(ent => ent.Customer)
                                                 .Select(ent => new
                                                 {
                                                     Id = ent.Id,
                                                     code = ent.Code,
                                                     CreationDate = ent.CreationDate,
                                                     Name = ent.OrderService.Service.ArabicName,
                                                     Image = ent.OrderService.ServiceImage,
                                                     CustomerName = ent.Customer.ArabicName,
                                                     CustomerAddress = ent.Customer.Address,
                                                     IsPriority = ent.OrderService.IsPriority
                                                 }).ToHashSet();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Records Retrieved Successfully",
                    Data = (orders == null || orders.Count == 0) ? null : orders,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetTechOrderDetails")]
        public IActionResult GetTechOrderDetails(long orderId)
        {
            try
            {
                var url = AppSession.AppURL + AppSession.OrderServiceUploads + "/";
                var order = _uow.OrderRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.Id == orderId && ent.OrderService != null)
                                                .Include(ent => ent.Customer)
                                                .Include(ent => ent.OrderService).ThenInclude(ent => ent.Service)
                                                .Include(ent => ent.ServicesImages)
                                                .Select(ent => new
                                                {
                                                    Service = ent.OrderService.Service.ArabicName,
                                                    Image = ent.ServicesImages.Select(i => new { ImagePath = url + i.ImagePath }),
                                                    Comment = ent.OrderService.ServiceComment,
                                                    CustomerName = ent.Customer.ArabicName,
                                                    CustomerAddress = ent.Customer.Address,
                                                    ent.OrderService.DeliverTimeFrom,
                                                    ent.OrderService.DeliverTimeTo,
                                                }).ToHashSet();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrieved Successfully",
                    Data = order,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("AcceptOrder")]
        public IActionResult AcceptOrder(long OrderId, long technicalId)
        {
            long technicalUserId = technicalId;
            try
            {

                var order = _uow.OrderRepository.GetById(OrderId);
                var technicaluser = _uow.UsersRepository.GetById(technicalUserId);
                var last_tech_assigned = _uow.OrderTechnicalAssignmentRepository.GetMany(ent => ent.OrderId == order.Id).OrderByDescending(ent => ent.CreationDate).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                else if (technicaluser == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Technical Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Technical Is  Not Found !!" }
                    });
                }
                else if (order.OrderTrackActionId != (int)EN_OrderActions.ordered)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Not Available Yet !!",
                        Data = null,
                        Errors = new string[] { "This Order Not Available Yet !!" }
                    });
                }
                else if (last_tech_assigned == null || last_tech_assigned.status != (int)EN_OrderTechnicalAssignmentStatus.waitToAnswer)
                {

                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Not available yet",
                        Data = null,
                        Errors = new string[] { "This Order Not available yet" }
                    });
                }
                else if (last_tech_assigned.TechnicalUserId != technicalUserId)
                {

                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Assigned To Other Technical",
                        Data = null,
                        Errors = new string[] { "This Order Assigned To Other Technical" }
                    });
                }

                else
                {
                    //add order to technical
                    order.ModificationDate = DateTime.Now;
                    order.OrderTrackActionId = (int)EN_OrderActions.accpted_by_user;
                    order.ResponsibleUserId = technicalUserId;
                    _uow.OrderRepository.Update(order);
                    //add accepted by user order track action
                    AddOrderAction((int)EN_OrderActions.accpted_by_user, order.Id);
                    //order assign to technical 
                    last_tech_assigned.ModificationDate = DateTime.Now;
                    last_tech_assigned.status = (int)EN_OrderTechnicalAssignmentStatus.accepted;
                    _uow.OrderTechnicalAssignmentRepository.Update(last_tech_assigned);
                    //send notification to customer that his order accepted by technical
                    Notification notification = new Notification();
                    notification.Text = "Order With Code: " + order.Code + " Accepted by technical";
                    notification.ToUSer = order.CustomerId;
                    notification.TypeOfUser = (int)EN_TypeUser.Customer;
                    _uow.NotificationRepository.Add(notification);

                    //send notification with order accepted by technical to admin
                    Notification adminNotification = new Notification();
                    var customername = _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                    var technicalname = _uow.UsersRepository.Get(ent => ent.Id == technicalUserId).ArabicName;
                    adminNotification.Text = "Order With Code: " + order.Code + " For Customer " + customername + " By Technical " + technicalname;
                    adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                    adminNotification.URl = AppSession.AppURL + "Order/OrderTrack/" + order.Id;
                    _uow.NotificationRepository.Add(adminNotification);

                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Order Accepted Successfully",
                        Data = null,
                        Errors = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("TechnicalRejectOrder")]
        public IActionResult TechnicalRejectOrder(long OrderId, long technicalId)
        {
            long technicalUserId = technicalId;
            try
            {
                var order = _uow.OrderRepository.GetById(OrderId);
                var technical = _uow.UsersRepository.GetById(technicalUserId);
                var last_tech_assigned = _uow.OrderTechnicalAssignmentRepository.GetMany(ent => ent.OrderId == order.Id).OrderByDescending(ent => ent.CreationDate).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                else if (technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Technical Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Technical Is  Not Found !!" }
                    });
                }
                else if (last_tech_assigned.TechnicalUserId != technicalUserId)
                {

                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Assigned To Other Technical",
                        Data = null,
                        Errors = new string[] { "This Order Assigned To Other Technical" }
                    });
                }
                else if (order.OrderTrackActionId != (int)EN_OrderActions.ordered || last_tech_assigned.status != (int)EN_OrderTechnicalAssignmentStatus.waitToAnswer)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Not Available Yet !!",
                        Data = null,
                        Errors = new string[] { "This Order Not Available Yet !!" }
                    });
                }
                else
                {
                    //order rejected assign by technical 
                    last_tech_assigned.ModificationDate = DateTime.Now;
                    last_tech_assigned.status = (int)EN_OrderTechnicalAssignmentStatus.rejected;
                    _uow.OrderTechnicalAssignmentRepository.Update(last_tech_assigned);
                    _uow.Save();
                    //send notification with order rejected by technical to admin
                    Notification adminNotification = new Notification();
                    adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                    adminNotification.URl = AppSession.AppURL + "ServicesReport/OrderTrack/" + order.Id;
                    //times the order rejected
                    var count = _uow.OrderTechnicalAssignmentRepository.GetMany(ent => ent.OrderId == order.Id && ent.status == (int)EN_OrderTechnicalAssignmentStatus.rejected).Count();

                    //Add alert notification to admin
                    if (count >= 2)
                    {
                        adminNotification.IsAlert = true;
                        adminNotification.Text = "Order With Code: " + order.Code + " Rejected by technicals more than once";
                    }
                    else
                    {
                        adminNotification.Text = "Order With Code: " + order.Code + " Rejected by technical " + _uow.UsersRepository.Get(ent => ent.Id == technicalUserId).ArabicName;
                    }
                    _uow.NotificationRepository.Add(adminNotification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Order Rejected Successfully",
                        Data = null,
                        Errors = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("CompleteOrder")]
        public IActionResult CompleteOrder(long OrderId, long technicalId)
        {
            long technicalUserId = technicalId;
            try
            {
                var order = _uow.OrderRepository.GetById(OrderId);
                var technical = _uow.UsersRepository.GetByIdCustom(technicalUserId);
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                else if (technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Technical Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Technical Is  Not Found !!" }
                    });
                }
                else if (order.ResponsibleUserId != technicalUserId)
                {

                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Not Available For The Technical",
                        Data = null,
                        Errors = new string[] { "This Order Not Available For The Technical" }
                    });
                }
                else if ((int)order.OrderTrackActionId >= (int)EN_OrderActions.completed)
                {

                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Already Completed",
                        Data = null,
                        Errors = new string[] { "This Order Already Completed" }
                    });
                }
                else
                {
                    //update order to completed
                    order.ModificationDate = DateTime.Now;
                    order.OrderTrackActionId = (int)EN_OrderActions.completed;
                    order.DeliverDate = DateTime.Now;
                    _uow.OrderRepository.Update(order);

                    //add completed order track action
                    AddOrderAction((int)EN_OrderActions.completed, order.Id);
                    _uow.Save();

                    //send notification to technical to enter price for completed order
                    Notification notification = new Notification();
                    notification.Text = "Order With Code: " + order.Code + " Completed Enter Service Price";
                    notification.ToUSer = technicalUserId;
                    notification.TypeOfUser = (int)EN_TypeUser.Technical;
                    _uow.NotificationRepository.Add(notification);

                    //send notification with order completed by technical to admin
                    Notification adminNotification = new Notification();
                    var customername = _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                    var technicalname = _uow.UsersRepository.Get(ent => ent.Id == technicalUserId).ArabicName;
                    adminNotification.Text = "Order With Code: " + order.Code + " For Customer " + customername + " Completed By Technical " + technicalname;
                    adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                    adminNotification.URl = AppSession.AppURL + "ServicesReport/OrderTrack/" + order.Id;
                    _uow.NotificationRepository.Add(adminNotification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Order Completed Successfully",
                        Data = null,
                        Errors = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("CalculateCommision")]
        public IActionResult CalculateCommision(long orderId, long cost)
        {
            try
            {
                var order = _uow.OrderRepository.GetAll().Include(ent => ent.OrderService).Where(ent => ent.Id == orderId).FirstOrDefault();
                if (order == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Is  Not Found !!" }
                    });
                }
                else if (order.OrderTrackActionId < (int)EN_OrderActions.completed)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Not Completed",
                        Data = null,
                        Errors = new string[] { "This Order Not Completed" }
                    });
                }
                else if (order.OrderService.Price > 0)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Already has Commision",
                        Data = null,
                        Errors = new string[] { "This Order Already has Commision" }
                    });
                }
                else
                {
                    var techcommision = _uow.TechnicalsRepository.Get(ent => ent.UsersId == order.ResponsibleUserId);
                    order.TechCommission = (techcommision != null) ? techcommision.Commission : 0;
                    _uow.OrderRepository.Update(order);
                    _uow.Save();
                    _uow.ExecuteSqlCommand("UPDATE OrderServices " +
                           "SET ModificationDate = '" + DateTime.Now + "',Price= " + cost + " " +
                           "Where OrderId =" + orderId.ToString());
                    //update technical poket
                    var user = _uow.UsersRepository.GetAll().Include(ent => ent.Technical).Where(ent => ent.Id == order.ResponsibleUserId).FirstOrDefault();
                    if (user.Technical != null)
                    {
                        //Update Technical Pocket Value
                        var tech = user.Technical;
                        var technicalProfit = cost * tech.Commission;
                        var companyProfit = cost - technicalProfit;
                        tech.Pocket -= companyProfit;
                        _uow.TechnicalsRepository.Update(tech);

                        //Add to Pocket History
                        PocketHistory pocketHistory = new PocketHistory();
                        pocketHistory.Value = companyProfit;
                        pocketHistory.Transaction = (int)EN_Transactions.ServiceCompanyProfit;
                        pocketHistory.ToUSer = user.Id;
                        pocketHistory.TypeUser = (int)EN_TypeUser.Technical;
                        pocketHistory.Text = "A new Value : " + companyProfit + " Subtracted From Your Pocket For Order: " + order.Code + " as Company Profit";
                        _uow.PocketHistoryRepository.Add(pocketHistory);

                        //Add notification to Technical
                        Notification technotification = new Notification();
                        technotification.Text = "A new Value : " + companyProfit + " Subtracted From Your Pocket For Order: " + order.Code + " as Company Profit";
                        technotification.ToUSer = user.Id;
                        technotification.TypeOfUser = (int)EN_TypeUser.Technical;
                        _uow.NotificationRepository.Add(technotification);
                    }

                    //send notification to customer with price
                    Notification notification = new Notification();
                    notification.Text = "Order With Code: " + order.Code + " Completed with Cost  " + cost;
                    notification.ToUSer = order.CustomerId;
                    notification.TypeOfUser = (int)EN_TypeUser.Customer;
                    _uow.NotificationRepository.Add(notification);

                    //send notification with order cost to admin
                    Notification adminNotification = new Notification();
                    var customername = _uow.CustomerRepository.Get(ent => ent.Id == order.CustomerId).ArabicName;
                    var technicalname = _uow.UsersRepository.Get(ent => ent.Id == order.ResponsibleUserId).ArabicName;
                    adminNotification.Text = "Order With Code: " + order.Code + " For Customer " + customername + "Completed By Technical " + technicalname + " With cost " + cost;
                    adminNotification.TypeOfUser = (int)EN_TypeUser.Admin;
                    adminNotification.URl = AppSession.AppURL + "Order/OrderTrack/" + order.Id;
                    _uow.NotificationRepository.Add(adminNotification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Commision Added Successfully",
                        Data = null,
                        Errors = null
                    });

                }

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }
        #endregion

        [HttpPost, Route("ReviewItem")]
        public IActionResult ReviewItem(ApiReviewItemModel review)
        {
            try
            {
                var orderItem = _uow.OrderItemsRepository.GetAll().Where(ent => ent.OrderId == review.OrderId && ent.ItemId == review.ItemId).FirstOrDefault();
                if (orderItem != null)
                {
                    orderItem.Rate = review.Rate;
                    orderItem.Review = review.Review;
                    orderItem.ModificationDate = DateTime.Now;
                    _uow.OrderItemsRepository.Update(orderItem);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "review Added Successfully",
                        Data = null,
                        Errors = null
                    });
                }
                else
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Item Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Item Is  Not Found !!" }
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost, Route("ReviewService")]
        public IActionResult ReviewService(ApiReviewServiceModel review)
        {
            try
            {
                var orderService = _uow.OrderServicesRepository.GetAll().Where(ent => ent.OrderId == review.OrderId && ent.ServiceId == review.ServiceId).FirstOrDefault();
                if (orderService != null)
                {
                    orderService.Rate = review.Rate;
                    orderService.Review = review.Review;
                    orderService.ModificationDate = DateTime.Now;
                    _uow.OrderServicesRepository.Update(orderService);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "review Added Successfully",
                        Data = null,
                        Errors = null
                    });
                }
                else
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "This Order Service Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Order Service Is  Not Found !!" }
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }
    }
}
