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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace sal7lyAdmin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiTechnicalsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IAuthenticateService _authService;
        public static IHostingEnvironment _environment;
        public ApiTechnicalsController(IUnitOfWork uow, ISecurity security, IAuthenticateService authService, IHostingEnvironment environment)
        { _uow = uow; _security = security; _authService = authService; _environment = environment; }


        [HttpPost, Route("ChargePocket")]
        public IActionResult ChargePocket(long TechnicalId, double value)
        {
            try
            {

                var technical = _uow.UsersRepository.GetAll().Where(ent => ent.Id == TechnicalId).Include(ent => ent.Technical).FirstOrDefault();
                if (technical == null || technical.Technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Technical with Id: " + TechnicalId + " not found",
                        Data = null,
                        Errors = new string[] { "Technical with Id: " + TechnicalId + " not found" }
                    });
                }
                else
                {
                    //Update Technical Pocket Value
                    technical.Technical.Pocket += value;
                    technical.ModificationDate = DateTime.Now;
                    _uow.UsersRepository.Update(technical);

                    //Add to Pocket History
                    PocketHistory pocketHistory = new PocketHistory();
                    pocketHistory.Value = value;
                    pocketHistory.Transaction = (int)EN_Transactions.Charge;
                    pocketHistory.ToUSer = TechnicalId;
                    pocketHistory.TypeUser = (int)EN_TypeUser.Technical;
                    pocketHistory.Text = "A new Value : " + value + " Added to Your Pocket ";
                    _uow.PocketHistoryRepository.Add(pocketHistory);

                    //Add notification to technical
                    Notification notification = new Notification();
                    notification.Text = "A new Value : " + value + " Added to Your Pocket Successfully";
                    notification.ToUSer = TechnicalId;
                    notification.TypeOfUser = (int)EN_TypeUser.Technical;
                    _uow.NotificationRepository.Add(notification);
                    _uow.Save();
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Record Updated Successfully",
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
        public IActionResult PocketDetails(long TechnicalId)
        {
            try
            {
                var technical = _uow.UsersRepository.GetAll().Where(ent => ent.Id == TechnicalId).Include(ent => ent.Technical).FirstOrDefault();
                if (technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Technical With Id: " + TechnicalId + " not found",
                        Data = null,
                        Errors = new string[] { "Technical with Id: " + TechnicalId + " not found" }
                    });
                }
                //technical orders in last 30 days
                var technicalordersPrice = _uow.OrderRepository.GetAll().Where(ent => ent.ResponsibleUserId == TechnicalId && (DateTime.Now - ent.CreationDate).TotalDays <= 30 && ent.OrderTrackActionId >= (int)EN_OrderActions.completed).Sum(ent => ent.OrderService.Price);
                var transactions = _uow.PocketHistoryRepository.GetMany(ent => ent.TypeUser == (int)EN_TypeUser.Technical && ent.ToUSer == TechnicalId)
                                  .Select(ent => new {
                                      Value = ent.Value,
                                      Date = ent.CreationDate,
                                      Transaction = ((EN_Transactions)ent.Transaction).ToString(),
                                      Text = ent.Text
                                  });
                var technicalPocketDetails = new
                {
                    Name = technical.ArabicName,
                    Commision = technical.Technical.Commission,
                    technical.Technical.Pocket,
                    Last30DaysOrdersPrice = technicalordersPrice,
                    Last30DaysDeliverPrice = 0.0,
                    Transactions = transactions
                };

                return Ok(new ApiResponseModel
                {

                    Status = EN_ResponseStatus.Success,
                    Message = "Record Retrived Successfully",
                    Data = technicalPocketDetails,
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

        [HttpGet, Route("GetTechnicalProfile")]
        public IActionResult GetTechnicalProfile(long TechnicalId)
        {
            try
            {
                var technical = _uow.UsersRepository.GetMany(ent => ent.Id == TechnicalId)
                                                    .Include(ent => ent.Technical).ThenInclude(ent => ent.Service).FirstOrDefault();
                if (technical == null || technical.Technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Technical with Id: " + TechnicalId + " not found",
                        Data = null,
                        Errors = new string[] { "Technical with Id: " + TechnicalId + " not found" }
                    });
                }
                else
                {
                    var technicaldata = new
                    {
                        Name = technical.ArabicName,
                        UserName = technical.UserName,
                        Mobile = technical.Mobile,
                        Location = technical.Location,
                        Service = technical.Technical.Service.ArabicName,
                        Commission = technical.Technical.Commission
                    };
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Success,
                        Message = "Record Retrieved Successfully",
                        Data = technicaldata,
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


        [HttpPost, Route("UpdateTechnicalProfile")]
        public IActionResult UpdateTechnicalProfile([FromBody] TechnicalProfileModel model)
        {
            var errors = new List<string>();
            try
            {
                #region Save Technical

                var technical = _uow.UsersRepository.GetMany(ent => ent.Id == model.Id).Include(ent => ent.Technical).FirstOrDefault();
                if (technical == null || technical.Technical == null)
                {
                    errors.Add("Technical with Id: " + model.Id + " not found");
                }
                if (errors.Count == 0)
                {
                    technical.ArabicName = technical.EnglishName = model.Name;
                    technical.UserName = model.UserName;
                    technical.Mobile = model.Mobile;
                    technical.Password = EncryptANDDecrypt.EncryptText(model.Password);
                    technical.ModificationDate = DateTime.Now;
                    technical.Location = model.Location;
                    var validatetechnerrors = validatetechnical(technical);
                    if (validatetechnerrors == null || validatetechnerrors.Count() == 0)
                    {
                        _uow.UsersRepository.Update(technical);
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
                        errors.AddRange(validatetechnerrors);
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

        private string[] validatetechnical(Users user)
        {
            List<string> errors = new List<string>();

            var users = _uow.UsersRepository.GetAll();
            //unique Email
            bool emailexist = users.Where(ent => ent.Email.ToLower() == user.Email.ToLower() && ent.Id != user.Id).Count() > 0;
            if (emailexist)
                errors.Add("Dublicate Value for Email Field equal " + user.Email);
            //unique UserName
            bool usernameexist = users.Where(ent => ent.UserName.ToLower() == user.UserName.ToLower() && ent.Id != user.Id).Count() > 0;
            if (usernameexist)
                errors.Add("Dublicate Value for UserName Field equal " + user.UserName);
            //unique mobile
            bool mobileexist = users.Where(ent => ent.Mobile == user.Mobile && ent.Id != user.Id).Count() > 0;
            if (mobileexist)
                errors.Add("Dublicate Value for Mobile Field equal " + user.Mobile);
            return errors.ToArray();
        }

        [HttpPost("ChangeProfileImage")]
        public async Task<IActionResult> ChangeImage([FromForm]IFormFile file, long TechnicalUserId)
        {
            try
            {
                var technical = _uow.TechnicalsRepository.GetAll().Where(ent => ent.UsersId == TechnicalUserId).Include(ent => ent.User).FirstOrDefault();
                if (TechnicalUserId == 0)
                {
                    return Ok(new ApiResponseModel
                    {
                        Status = EN_ResponseStatus.Faild,
                        Message = "Technical with UserId: " + TechnicalUserId + " not found",
                        Data = null,
                        Errors = new string[] { "Technical with UserId: " + TechnicalUserId + " not found" }
                    });
                }
                else
                {
                    var webRoot = _environment.WebRootPath;
                    var uploadsFolder = Path.Combine(webRoot, AppSession.UserUploads);
                    var technicaluser = technical.User;
                    var filename = await Images.SaveFile(uploadsFolder, file, lastName: technicaluser.ImageName, defaultName: AppSession.UserDefaultImage);
                    if (filename != "" && filename != null)
                    {

                        technicaluser.ImageName = filename;
                        _uow.UsersRepository.Update(technicaluser);
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Record Saved Successfully",
                            Data = new { image = AppSession.AppURL + AppSession.UserUploads + "/" + filename },
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

        [HttpGet, Route("NotificationsList")]
        public IActionResult NotificationsList(long TechnicalUserId)
        {
            try
            {
                var technical = _uow.TechnicalsRepository.Get(ent => ent.UsersId == TechnicalUserId);
                if (technical == null)
                {
                    return Ok(new ApiResponseModel
                    {

                        Status = EN_ResponseStatus.Faild,
                        Message = "Technical With UsersId: " + TechnicalUserId + " not found",
                        Data = null,
                        Errors = new string[] { "Technical With UsersId: " + TechnicalUserId + " not found" }
                    });
                }
                var model = _uow.NotificationRepository.GetMany(ent => ent.TypeOfUser == (int)EN_TypeUser.Technical && ent.ToUSer == TechnicalUserId)
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
    }
}
