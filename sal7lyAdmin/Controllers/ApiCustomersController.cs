using BL.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sal7lyAdmin.Services;
using Model;
using BL.Security;
using Model.APIModels;
using System;
using System.Linq;
using System.Collections.Generic;
using static BL.SharedCS.Enumrations;
using BL.Secuirty;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MailKit.Net.Smtp;
using MimeKit;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace sal7lyAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiCustomersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IAuthenticateService _authService;
        private readonly string itemsRoot = AppSession.AppURL + AppSession.ItemsUploads;
        private readonly string customerRoot = AppSession.AppURL + AppSession.CustomerUploads;
        public static IHostingEnvironment _environment;

        public ApiCustomersController(IUnitOfWork uow, ISecurity security, IAuthenticateService authService, IHostingEnvironment environment)
        { _uow = uow; _security = security; _authService = authService; _environment = environment; }

        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public IActionResult Register([FromBody] SignUpModelForAPI model)
        {
            var errors = new List<string>();
            try
            {
                #region Save Customer
                var obj = new Customer();
                if (model == null)
                {
                    errors.Add("Customer Data Can't be null");
                }
                else
                {
                    obj.Code = UIHelper.GeneratCode(EN_Screens.Customer, _uow);
                    obj.UserName = model.UserName;
                    obj.Email = model.Email;
                    obj.Password = EncryptANDDecrypt.EncryptText(model.Password);
                    obj.ArabicName = obj.EnglishName = model.Name;
                    obj.Mobile = model.Mobile;
                    obj.Address = model.Address;
                    obj.ImageName = AppSession.CustomerDefaultImage;
                    obj.CreationDate = DateTime.Now;
                    var validatecusterrors = validatecustomer(obj);
                    if (validatecusterrors == null || validatecusterrors.Count() == 0)
                    {
                        _uow.CustomerRepository.Add(obj);
                        _uow.Save();
                        var data = new
                        {
                            Id = obj.Id,
                            Name = obj.ArabicName,
                            obj.Email,
                            obj.UserName,
                            obj.Mobile,
                            obj.Pocket,
                            obj.Address,
                            Image = AppSession.AppURL + AppSession.CustomerUploads + "/" + obj.ImageName,
                            token = ""
                        };
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Record Saved Successfully",
                            Data = data,
                            Errors = null
                        });
                    }
                    else
                    {
                        errors.AddRange(validatecusterrors);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                errors.Add("Error: " + ex.Message);
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                }
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors.ToArray()
            });
        }
        private string[] validatecustomer(Customer customer)
        {
            List<string> errors = new List<string>();
            //unique Email
            var customers = _uow.CustomerRepository.GetAll();
            bool emailexist = customers.Where(ent => ent.Email.ToLower() == customer.Email.ToLower() && ent.Id != customer.Id).Count() > 0;
            if (emailexist)
                errors.Add("Dublicate Value for Email Field equal " + customer.Email);
            //unique UserName
            bool usernameexist = customers.Where(ent => ent.UserName.ToLower() == customer.UserName.ToLower() && ent.Id != customer.Id).Count() > 0;
            if (usernameexist)
                errors.Add("Dublicate Value for UserName Field equal " + customer.UserName);
            //unique mobile
            bool mobileexist = customers.Where(ent => ent.Mobile == customer.Mobile && ent.Id != customer.Id).Count() > 0;
            if (mobileexist)
                errors.Add("Dublicate Value for Mobile Field equal " + customer.Mobile);
            return errors.ToArray();
        }

        [HttpPost, Route("UpdateCustomerProfile")]
        public IActionResult UpdateCustomerProfile([FromBody] CustomerProfileModel model)
        {
            var errors = new List<string>();
            try
            {
                #region Save Customer

                var obj = _uow.CustomerRepository.Get(ent => ent.Id == model.Id);
                var city = _uow.CityRepository.Get(ent => ent.Id == model.CityId);

                if (obj == null)
                {
                    errors.Add("Customer with Id: " + model.Id + " not found");
                }
                if (city == null && model.CityId != 0)
                {
                    errors.Add("City with Id: " + model.CityId + " not found");
                }
                if (errors.Count == 0)
                {
                    obj.ArabicName = obj.EnglishName = model.Name;
                    obj.Email = model.Email;
                    obj.UserName = model.UserName;
                    obj.Mobile = model.Mobile;
                    obj.Password = EncryptANDDecrypt.EncryptText(model.Password);
                    obj.ModificationDate = DateTime.Now;
                    obj.Address = model.Address;
                    obj.CityId = model.CityId;
                    var validatecusterrors = validatecustomer(obj);
                    if (validatecusterrors == null || validatecusterrors.Count() == 0)
                    {
                        _uow.CustomerRepository.Update(obj);
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
                    else
                    {
                        errors.AddRange(validatecusterrors);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add("Error: " + ex.Message);
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                }
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors.ToArray()
            });
        }

        [HttpPost("ChangeProfileImage")]
        public async Task<IActionResult> ChangeImage([FromForm]IFormFile file, long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.GetAll().Where(ent => ent.Id == CustomerId).FirstOrDefault();
                if (CustomerId == 0 || customer == null)
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
                    var webRoot = _environment.WebRootPath;
                    var uploadsFolder = Path.Combine(webRoot, AppSession.CustomerUploads);
                    var filename = await Images.SaveFile(uploadsFolder, file, lastName: customer.ImageName, defaultName: AppSession.CustomerDefaultImage);
                    if (filename != "" && filename != null)
                    {
                        customer.ImageName = filename;
                        _uow.CustomerRepository.Update(customer);
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Record Saved Successfully",
                            Data = new { image = AppSession.AppURL + AppSession.CustomerUploads + "/" + filename },
                            Errors = null
                        });
                    }
                    else
                    {
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Error Occured while update Profile Image Try to Upload again",
                            Data = null,
                            Errors = new string[] { "Error Occured while update Profile Image Try to Upload again" }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpGet, Route("GetCustomerProfile")]
        public IActionResult GetCustomerProfile(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.GetAll().Where(ent => ent.Id == CustomerId).Include(ent => ent.City).Select(
                    ent => new
                    {
                        Id = ent.Id,
                        Name = ent.ArabicName,
                        Email = ent.Email,
                        Mobile = ent.Mobile,
                        Address = ent.Address,
                        City = ent.City.ArabicName,
                        ProfileImage = AppSession.AppURL + AppSession.CustomerUploads + "/" + ent.ImageName
                    }).FirstOrDefault();
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
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrieved Successfully",
                    Data = customer,
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

        [HttpGet, Route("ItemFavoriteList")]
        public IActionResult ItemFavoriteList(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == CustomerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {
                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                var itemslist = _uow.ItemFavouriteRepository.GetMany(ent => ent.CustomerId == CustomerId)
                                .Include(ent => ent.Item).ThenInclude(ent => ent.Service)
                                .Select(ent => new
                                {
                                    Id = ent.Id,
                                    Item = ent.Item.ArabicName,
                                    Service = ent.Item.Service.ArabicName,
                                    Image = itemsRoot + "/" + ent.Item.ItemImages.FirstOrDefault().ImagePath,
                                    IsFavourite = true
                                }).ToList();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Items Favorite List retrieved Successfully",
                    Data = (itemslist == null || itemslist.Count == 0) ? null : itemslist,
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

        [HttpPost, Route("ItemFavorite")]
        public IActionResult ItemFavorite([FromBody] ApiFavListModel model)
        {
            try
            {

                var customer = _uow.CustomerRepository.Get(ent => ent.Id == model.CustomerId);
                var item = _uow.ItemsRepository.Get(ent => ent.Id == model.ItemId);
                if (item == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Item With Id: " + model.ItemId + " not found",
                        Data = null,
                        Errors = new string[] { "Item With Id: " + model.ItemId + " not found" }
                    });
                }
                else if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + model.CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + model.CustomerId + " not found" }
                    });
                }
                else
                {
                    var dbObj = _uow.ItemFavouriteRepository.Get(ent => ent.CustomerId == model.CustomerId && ent.ItemId == model.ItemId);
                    if (model.IsDelete)
                    {
                        if (dbObj == null)
                        {
                            return Ok(new ApiResponseModel
                            {

                                Status = EN_ResponseStatus.Faild,
                                Message = "Item Not Added to Customer Favourite List",
                                Data = null,
                                Errors = new string[] { "Item Not Added to Customer Favourite List" }
                            });
                        }
                        _uow.ItemFavouriteRepository.Delete(dbObj.Id);
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {

                            Status = EN_ResponseStatus.Success,
                            Message = "Record Deleted Successfully",
                            Data = null,
                            Errors = null
                        });

                    }
                    else
                    {
                        if (dbObj == null)
                        {
                            var entity = new ItemFavourite();
                            entity.CustomerId = model.CustomerId;
                            entity.ItemId = model.ItemId;
                            entity.CreationDate = DateTime.Now;
                            _uow.ItemFavouriteRepository.Add(entity);
                            _uow.Save();
                            return Ok(new ApiResponseModel
                            {

                                Status = EN_ResponseStatus.Success,
                                Message = "Record Saved Successfully",
                                Data = null,
                                Errors = null
                            });

                        }
                        else
                        {
                            return Ok(new ApiResponseModel
                            {

                                Status = EN_ResponseStatus.Faild,
                                Message = "Record is already exsists",
                                Data = null,
                                Errors = new string[] { "Record is already exsists" }
                            });
                        }
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

        [HttpGet, Route("CartList")]
        public IActionResult CartList(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == CustomerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                var customeritemsfav = _uow.ItemFavouriteRepository.GetMany(ent => CustomerId == ent.CustomerId).Select(ent => ent.ItemId).ToList();
                var customeritemscart = _uow.ItemsCartRepository.GetMany(ent => ent.CustomerId == CustomerId)
                                        .Include(ent => ent.Item).ThenInclude(ent => ent.Service)
                                        .Select(ent => new
                                        {
                                            Id = ent.Id,
                                            Item = ent.Item.ArabicName,
                                            Price = ent.Item.Price,
                                            Service = ent.Item.Service.ArabicName,
                                            Image = itemsRoot + "/" + ent.Item.ItemImages.FirstOrDefault().ImagePath,
                                            IsFavourite = (customeritemsfav.Contains(ent.ItemId)) ? true : false,
                                            quantity = ent.Quantity
                                        }).ToList();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Items Cart List retrieved Successfully",
                    Data = (customeritemscart == null || customeritemscart.Count == 0) ? null : customeritemscart,
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

        [HttpPost, Route("ItemCart")]
        public IActionResult ItemCart([FromBody] ApiCartListModel model)
        {
            var errorList = new List<string>();
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == model.CustomerId);
                var item = _uow.ItemsRepository.Get(ent => ent.Id == model.ItemId);
                if (customer == null || item == null)
                {
                    if (item == null)
                    {
                        errorList.Add("Item With Id: " + model.ItemId + " not found");
                    }
                    if (customer == null)
                    {
                        errorList.Add("Customer with Id: " + model.CustomerId + " not found");
                    }
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Error Item Or Customer Data",
                        Data = null,
                        Errors = errorList.ToArray()
                    });
                }

                else
                {
                    var dbObj = _uow.ItemsCartRepository.Get(ent => ent.CustomerId == model.CustomerId && ent.ItemId == model.ItemId);
                    if (model.IsDelete)
                    {
                        if (dbObj == null)
                        {
                            return Ok(new ApiResponseModel
                            {

                                Status = EN_ResponseStatus.Faild,
                                Message = "Item Not Added to Customer Cart List",
                                Data = null,
                                Errors = new string[] { "Item Not Added to Customer Cart List" }
                            });
                        }
                        else
                        {
                            _uow.ItemsCartRepository.Delete(dbObj.Id);
                            _uow.Save();
                            return Ok(new ApiResponseModel
                            {

                                Status = EN_ResponseStatus.Success,
                                Message = "Record Deleted Successfully",
                                Data = null,
                                Errors = null
                            });
                        }
                    }
                    else
                    {
                        var msg = "";
                        if (dbObj == null)
                        {
                            var entity = new ItemsCart();
                            entity.CustomerId = model.CustomerId;
                            entity.ItemId = model.ItemId;
                            entity.CreationDate = DateTime.Now;
                            entity.Quantity = model.Quantity;
                            _uow.ItemsCartRepository.Add(entity);
                            msg = "Record Saved Successfully";
                        }
                        else
                        {
                            dbObj.ModificationDate = DateTime.Now;
                            dbObj.Quantity = model.Quantity;
                            _uow.ItemsCartRepository.Update(dbObj);
                            msg = "Record Updated Successfully";
                        }
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {

                            Status = EN_ResponseStatus.Success,
                            Message = msg,
                            Data = null,
                            Errors = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                errorList.Add("Error: " + ex.Message);
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: " + ex.Message,
                    Data = null,
                    Errors = errorList.ToArray()
                });
            }
        }

        [HttpPost, Route("UpdateItemCart")]
        public IActionResult UpdateItemCart(long ItemCartId, int quantity)
        {
            try
            {
                var itemcart = _uow.ItemsCartRepository.Get(ent => ent.Id == ItemCartId);
                if (itemcart == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Error ItemCart Not Found",
                        Data = null,
                        Errors = new string[] { "Error ItemCart Not Found" }
                    });
                }
                else
                {
                    itemcart.Quantity = quantity;
                    itemcart.ModificationDate = DateTime.Now;
                    _uow.ItemsCartRepository.Update(itemcart);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "ItemCart Updated Successfully",
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

        [HttpPost]
        [AllowAnonymous]
        [HttpPost, Route("ContactUs")]
        public IActionResult ContactUs(ContactUs entity)
        {
            ContactUs ContactUsEntity = new ContactUs();
            try
            {
                #region ContactUs

                ContactUsEntity.CreationDate = DateTime.Now;
                ContactUsEntity.Email = entity.Email;
                ContactUsEntity.Name = entity.Name;
                ContactUsEntity.Message = entity.Message;
                _uow.ContactUsRepository.Add(ContactUsEntity);
                _uow.Save();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Saved Successfully",
                    Data = null,
                    Errors = null
                });

                #endregion
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
        [HttpGet, Route("NotificationsList")]
        public IActionResult NotificationsList(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Id == CustomerId);
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                var model = _uow.NotificationRepository.GetMany(ent => ent.TypeOfUser == (int)EN_TypeUser.Customer && ent.ToUSer == CustomerId)
                            .OrderByDescending(ent => ent.CreationDate)
                            .Select(ent => new
                            {
                                Id = ent.Id,
                                Text = ent.Text,
                                URl = ent.URl,
                                Seen = ent.Seen,
                                IsAlert = ent.IsAlert
                            }).ToList();
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrieved Successfully",
                    Data = (model == null || model.Count == 0) ? null : model,
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
        [HttpPost, Route("UpdateNotificationSeen")]
        public IActionResult UpdateNotificationSeen(long NotificationId)
        {
            try
            {
                var notification = _uow.NotificationRepository.Get(ent => ent.Id == NotificationId);
                if (notification == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "notification With Id: " + NotificationId + " not found",
                        Data = null,
                        Errors = new string[] { "notification With Id: " + NotificationId + " not found" }
                    });
                }
                notification.Seen = true;
                notification.ModificationDate = DateTime.Now;
                _uow.NotificationRepository.Update(notification);
                _uow.Save();
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

        [HttpPost, Route("ChargePocket")]
        public IActionResult ChargePocket(long CustomerId, double value)
        {
            try
            {
                var customer = _uow.CustomerRepository.GetAll().Where(ent => ent.Id == CustomerId).FirstOrDefault();
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                else
                {
                    //Update Customer Pocket Value
                    customer.Pocket += value;
                    customer.ModificationDate = DateTime.Now;
                    _uow.CustomerRepository.Update(customer);

                    //Add to Pocket History
                    PocketHistory pocketHistory = new PocketHistory();
                    pocketHistory.Value = value;
                    pocketHistory.Transaction = (int)EN_Transactions.Charge;
                    pocketHistory.ToUSer = CustomerId;
                    pocketHistory.TypeUser = (int)EN_TypeUser.Customer;
                    pocketHistory.Text = "A new Value : " + value + " Added to Your Pocket ";
                    _uow.PocketHistoryRepository.Add(pocketHistory);

                    //Add notification to customer
                    Notification notification = new Notification();
                    notification.Text = "A new Value : " + value + " Added to Your Pocket Successfully";
                    notification.ToUSer = CustomerId;
                    notification.TypeOfUser = (int)EN_TypeUser.Customer;
                    _uow.NotificationRepository.Add(notification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Pocket Updated Successfully",
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

        [HttpGet, Route("PocketDetails")]
        public IActionResult PocketDetails(long CustomerId)
        {
            try
            {
                var customer = _uow.CustomerRepository.GetAll().Where(ent => ent.Id == CustomerId).FirstOrDefault();
                if (customer == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Customer With Id: " + CustomerId + " not found",
                        Data = null,
                        Errors = new string[] { "Customer with Id: " + CustomerId + " not found" }
                    });
                }
                //customer orders in last 30 days
                var customerorders = _uow.OrderRepository.GetAll().Where(ent => ent.CustomerId == CustomerId && (DateTime.Now - ent.CreationDate).TotalDays <= 30)
                                      .Include(ent => ent.OrderService)
                                      .Include(ent => ent.OrderItems);
                var ordersPrice = customerorders.Where(ent => ent.OrderService != null).Sum(ent => ent.OrderService.Price) + customerorders.Where(ent => ent.OrderItems != null).Select(ent => ent.OrderItems.Select(e => e.Price).Sum()).Sum();
                var ordersDiscountPrice = customerorders.Where(ent => ent.OrderService != null).Sum(ent => ent.OrderService.DiscounttPrice) + customerorders.Where(ent => ent.OrderItems != null).Select(ent => ent.OrderItems.Select(e => e.DiscountPrice).Sum()).Sum();

                var transactions = _uow.PocketHistoryRepository.GetMany(ent => ent.TypeUser == (int)EN_TypeUser.Customer && ent.ToUSer == CustomerId)
                                  .Select(ent => new {
                                      Value = ent.Value,
                                      Date = ent.CreationDate,
                                      Transaction = ((EN_Transactions)ent.Transaction).ToString(),
                                      Text = ent.Text
                                  });
                var customerPocketDetails = new
                {
                    Name = customer.ArabicName,
                    Pocket = customer.Pocket,
                    Last30DaysOrdersPrice = ordersPrice,
                    Last30DaysOrdersDiscounts = ordersPrice - ordersDiscountPrice,
                    Transactions = transactions
                };

                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrived Successfully",
                    Data = customerPocketDetails,
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
    }
}
