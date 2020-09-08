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

namespace sal7lyAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiItemsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IAuthenticateService _authService;
        private readonly IHostingEnvironment _env;
        private readonly string itemsRoot = AppSession.AppURL + AppSession.ItemsUploads;

        public ApiItemsController(IUnitOfWork uow, ISecurity security, IAuthenticateService authService, IHostingEnvironment env) { _uow = uow; _security = security; _authService = authService; _env = env; }

        [AllowAnonymous]
        [HttpPost, Route("GetItems")]
        public IActionResult GetItems(long? CurrentCustId)
        {
            var msg = "";
            try
            {
                var items = _uow.ItemsRepository.GetApi(null, CurrentCustId, itemsRoot).ToList();
                if (items != null && items.Count() != 0)
                {
                    msg = "Items Retrived Successfully";
                }
                else
                {
                    msg = "No Items Added";
                    items = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = items,
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

        [AllowAnonymous]
        [HttpPost, Route("GetItemsByServiceId")]
        public IActionResult GetItemsByServiceId(long ServiceId, long? CurrentCustId)
        {
            var msg = "";
            try
            {
                var items = _uow.ItemsRepository.GetApi(ent => ent.ServiceId == ServiceId, CurrentCustId, itemsRoot).ToList();
                if (items != null && items.Count() != 0)
                {
                    msg = "Items Retrived Successfully";
                }
                else
                {
                    msg = "No Items Added";
                    items = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = items,
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

        [AllowAnonymous]
        [HttpPost, Route("GetItemDetails")]
        public IActionResult GetItemDetails(long ItemId, long? CurrentCustId)
        {
            try
            {
                var item = _uow.ItemsRepository.GetDetailsApi(ent => ent.Id == ItemId, CurrentCustId, itemsRoot);
                if (item == null)
                {
                    return Ok(new ApiResponseModel
                    {
                         
                        Status = EN_ResponseStatus.Faild,
                        Message = "This Item Is  Not Found !!",
                        Data = null,
                        Errors = new string[] { "This Item Is  Not Found !!" }
                    });
                }
                else
                {
                    return Ok(new ApiResponseModel
                    {
                         
                        Status = EN_ResponseStatus.Success,
                        Message = "Record Retrived Successfully",
                        Data = item,
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

        [AllowAnonymous]
        [HttpGet, Route("GetNewItems")]
        public IActionResult GetNewItems(long? CurrentCustId)
        {
            var msg = "";
            try
            {
                //get last added 10 items
                var items = _uow.ItemsRepository.GetApi(null, CurrentCustId, itemsRoot).Take(10).ToList();
                if (items != null && items.Count() != 0)
                {
                    msg = "Items Retrived Successfully";
                }
                else
                {
                    msg = "No Items Added";
                    items = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = items,
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

        #region Offers

        [HttpGet, Route("GetAllOffers")]
        public IActionResult GetAllOffers()
        {
            try
            {
                var offers = _uow.OffersRepository.GetAll().Where(ent => ent.IsActive && !ent.IsDeleted)
                    .Include(ent => ent.Service)
                    .Select(ent => new {
                        Id = ent.Id,
                        Name = ent.ArabicName,
                        ent.CouponCode,
                        ent.OfferValue,
                        Service = ent.Service.ArabicName,
                        ent.MainItemId,
                        ent.FreeItemId,
                        ent.MainQty,
                        ent.FreeQty,
                        Image = AppSession.AppURL + AppSession.OfferImagesUploads + "/" + ent.ImagePath

                    }).ToHashSet();
                if (offers != null && offers.Count() != 0)
                {
                    return Ok(new ApiResponseModel
                    {
                         
                        Status = EN_ResponseStatus.Success,
                        Message = "offers Retrived Successfully",
                        Data = offers,
                        Errors = null
                    });
                }
                else
                {
                    return Ok(new ApiResponseModel
                    {
                         
                        Status = EN_ResponseStatus.Success,
                        Message = "No offers Added",
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
        //[HttpGet, Route("GetAllOfferByCouponCode")]
        //public IActionResult GetAllOfferByCouponCode(string couponCode)
        //{
        //    try
        //    {
        //        var offer = _uow.OffersRepository.GetOfferBy(couponCode);
                
               
        //        if (offer == null  )
        //        {
        //            return Ok(new ApiResponseModel
        //            {
                         
        //                Status = EN_ResponseStatus.Success,
        //                Message = "No offers Added",
        //                Data = null,
        //                Errors = null
        //            });
                   
        //        }
        //        else if(offer.Id==0)
        //        {

        //            return Ok(new ApiResponseModel
        //            {
                         
        //                Status = EN_ResponseStatus.Success,
        //                Message = "this offer  is over",
        //                Data = null,
        //                Errors = null
        //            });
        //        }
        //        else
        //        {
        //            var model = new
        //            {
        //                offer.Id,
        //                name = offer.ArabicName,
        //                offer.CouponCode,
        //                offer.OfferValue,
        //                serviceId = offer.MainCategoryId,
        //                offer.MainItemId,
        //                offer.FreeItemId,
        //                offer.FreeQty,
        //                offer.MainQty,
        //                service = offer.Service?.ArabicName,
        //                mainItem = offer.MainItem?.ArabicName,
        //                freeItem = offer.FreeItem?.ArabicName
        //            };
        //            return Ok(new ApiResponseModel
        //            {
                         
        //                Status = EN_ResponseStatus.Success,
        //                Message = "offer Retrived Successfully",
        //                Data = model,
        //                Errors = null
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ApiResponseModel
        //        {
                     
        //            Status = EN_ResponseStatus.Faild,
        //            Message = "Error: " + ex.Message,
        //            Data = null,
        //            Errors = new string[] { "Error: " + ex.Message }
        //        });
        //    }
        //}
        #endregion

    }
}
