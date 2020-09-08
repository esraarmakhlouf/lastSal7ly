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
   // [Authorize]
    public class ApiGetAllController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IAuthenticateService _authService;
        private readonly IHostingEnvironment _env;

        public ApiGetAllController(IUnitOfWork uow, ISecurity security, IAuthenticateService authService, IHostingEnvironment env) { _uow = uow; _security = security; _authService = authService; _env = env; }

        [HttpGet, Route("GetAllCountries")]
        public IActionResult GetAllCountries()
        {
            var msg = "";
            try
            {
                var Countries = _uow.CountryRepository.GetCountries().Select(ent => new
                {
                    Id = ent.Id,
                    Name = ent.ArabicName,
                    cities = ent.Cities.Select(c => new
                    {
                        Id = c.Id,
                        Name = c.ArabicName,
                        Districts = c.Districts.Select(d => new
                        {
                            Id = d.Id,
                            Name = d.ArabicName,
                        })
                    })
                }).ToHashSet();
                if (Countries != null && Countries.Count() != 0)
                {
                    msg = "Countries Retrived Successfully";
                }
                else
                {
                    msg = "No Countries Added";
                    Countries = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = Countries,
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

        [HttpGet, Route("OrderActions")]
        public IActionResult OrderActions()
        {
            var msg = "";
            try
            {
                var actions = _uow.OrderTrackActionRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet().Select(
                    ent => new {
                        Id = ent.Id,
                        Name = ent.ArabicName
                    });
                if (actions != null && actions.Count() != 0)
                {
                    msg = "Order Actions Retrived Successfully";
                }
                else
                {
                    msg = "No Order Actions Added";
                    actions = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = actions,
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

        #region services

        [HttpGet, Route("Services")]
        public IActionResult Services()
        {
            var msg = "";
            try
            {
                var url = AppSession.AppURL + AppSession.ServicesImagesUploads + "/";

                var services = _uow.ServicesRepository.GetServices( url);//_uow.ServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet().Select(
                //    ent => new {
                //        Id = ent.Id,
                //        Name = ent.ArabicName,
                //        Image = AppSession.AppURL + AppSession.ServicesImagesUploads + "/" + ent.ImagePath,
                //    });
                if (services != null)
                {
                    msg = "Services Retrived Successfully";
                }
                else
                {
                    msg = "No Services Added";
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


        [HttpGet, Route("MostServices")]
        public IActionResult MostServices()
        {
            var msg = "";
            var url = AppSession.AppURL + AppSession.ServicesImagesUploads + "/";
            try
            {
                var model = _uow.ServicesRepository.GetTopServices(url);

                if (model != null)
                {
                    msg = "Services Retrived Successfully";
                }
                else
                {
                    msg = "No Services Added";
                    model = null;
                }
                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = msg,
                    Data = model,
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
    }
}
