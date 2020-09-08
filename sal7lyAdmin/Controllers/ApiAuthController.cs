using BL.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.APIModels;
using BL.Security;
using System;
using Model;
using BL.Secuirty;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BL.SharedCS.Enumrations;
using sal7lyAdmin.Services;

namespace sal7lyAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiAuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthenticateService authService;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public ApiAuthController(IUnitOfWork uow,IAuthenticateService _authService, IRazorViewToStringRenderer razorViewToStringRenderer) 
        { _uow = uow; authService = _authService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        [AllowAnonymous]
        [HttpPost("UserLogIn")]
        public IActionResult UserLogIn([FromBody] ApiUserLoginModel request)
        {
            try
            {
                var user = authService.AuthenticateUser(request, out string token);
                if (user != null && user.Technical != null)
                {
                    user.Password = null;
                    var model = new
                    {
                        user.Id,
                        Name = user.ArabicName,
                        user.Location,
                        ServiceName= user.Technical?.Service?.ArabicName,
                        user.UserName,
                        user.Mobile,
                        user.Technical.Pocket,
                        user.CityId,
                        user.DistrictId,
                        token
                    };
                    return Ok(new ApiResponseModel
                    {
                        Status = EN_ResponseStatus.Success,
                        Message = "Logged in successflly",
                        Data =model,
                        Errors=null
                    });
                }
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = "Invalid mobile or password",
                    Data = null,
                    Errors = new string[] { "Invalid mobile or password" }
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: "+ex.Message,
                    Data = null,
                    Errors = new string[] {"Error: "+ ex.Message }
                });
            }
        }


        [AllowAnonymous]
        [HttpPost("LogIn")]
        public IActionResult LogIn([FromBody] ApiLoginModel request)
        {
            try
            {
                var customer = authService.AuthenticateCustomer(request, out string token);
                if (customer != null)
                {
                    var model = new {
                        customer.Id,
                        Name= customer.ArabicName,
                        customer.Email,
                        customer.UserName,
                        customer.Mobile,
                        customer.Pocket,
                        customer.Address,
                        Image=AppSession.AppURL+AppSession.CustomerUploads+"/"+customer.ImageName,
                        token
                    };
                    return Ok(new ApiResponseModel
                    {
                        Status = EN_ResponseStatus.Success,
                        Message = "Logged in successflly",
                        Data = model,
                        Errors=null
                    });
                }
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = "Invalid email or password",
                    Data = null,
                    Errors=new string[] { "Invalid email or password" }
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponseModel
                {
                    Status = EN_ResponseStatus.Faild,
                    Message = "Error: "+ex.Message,
                    Data = null,
                    Errors = new string[] { "Error: " + ex.Message }
                });
            }
        }

        [HttpPost("UserChangePassword")]
        public IActionResult UserChangePassword([FromBody]ChangePassword password,long CurrentUserId)
        {
                var currentuser = _uow.UsersRepository.Get(ent=>ent.Id==CurrentUserId);
                if (currentuser == null)
                {
                    ModelState.AddModelError("", "User with Id :"+CurrentUserId+" Can't be found");
                }
                else if (password != null && !string.IsNullOrEmpty(password.NewPassword) && password.NewPassword == password.ConfirmNewPassword)
                {
                    if (password.CurrentPassword == password.NewPassword)
                    {
                        ModelState.AddModelError("NewPassword", "New Password Must be Different from Current Password");
                    }
                    //current user password equal entered password
                    if (EncryptANDDecrypt.DecryptText(currentuser.Password) != password.CurrentPassword)
                    {
                        ModelState.AddModelError("CurrentPassword", "InCorrect Password!!");
                    }
                    if (ModelState.IsValid)
                    {
                        currentuser.Password = EncryptANDDecrypt.EncryptText(password.NewPassword);
                        currentuser.ModificationDate = DateTime.Now;
                        _uow.UsersRepository.Update(currentuser);
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Change Password Done",
                            Data = null,
                            Errors = null
                        });
                        
                    }
                }
            
            var errors = ModelState.Select(ent => new { key = ent.Key, value = ent.Value.Errors.Select(err => (err.ErrorMessage == null || err.ErrorMessage == "") ? err.Exception.Message : err.ErrorMessage) });
            var errors_list = new List<string>();
            foreach (var sublist in errors)
            {
                foreach (var item in sublist.value)
                    errors_list.Add(sublist.key + ": " + item);
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors_list.ToArray()
            });
        }

        [HttpPost("CustomerChangePassword")]
        public IActionResult CustomerChangePassword([FromBody]ChangePassword password, long CurrentCustId)
        {
            if (ModelState.IsValid)
            {
                var currentcust = _uow.CustomerRepository.Get(ent => ent.Id == CurrentCustId);
                if (currentcust == null)
                {
                    ModelState.AddModelError("", "Customer with Id :" + CurrentCustId + " Can't be found");
                }
                else if (password != null && !string.IsNullOrEmpty(password.NewPassword) && password.NewPassword == password.ConfirmNewPassword)
                {
                    if (password.CurrentPassword == password.NewPassword)
                    {
                        ModelState.AddModelError("NewPassword", "New Password Must be Different from Current Password");
                    }
                    //current user password equal entered password
                    if (EncryptANDDecrypt.DecryptText(currentcust.Password) != password.CurrentPassword)
                    {
                        ModelState.AddModelError("CurrentPassword", "InCorrect Password!!");
                    }
                    if (ModelState.IsValid)
                    {
                        currentcust.Password = EncryptANDDecrypt.EncryptText(password.NewPassword);
                        currentcust.ModificationDate = DateTime.Now;
                        _uow.CustomerRepository.Update(currentcust);
                        _uow.Save();
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Change Password Done",
                            Data = null,
                            Errors = null
                        });

                    }
                }
            }
            var errors = ModelState.Select(ent => new { key = ent.Key, value = ent.Value.Errors.Select(err => (err.ErrorMessage == null || err.ErrorMessage == "") ? err.Exception.Message : err.ErrorMessage) });
            var errors_list = new List<string>();
            foreach (var sublist in errors)
            {
                foreach (var item in sublist.value)
                    errors_list.Add(sublist.key + ": " + item);
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors_list.ToArray()
            });
        }

        [HttpPost("UserResetPasswordEmail")]
        public async Task<IActionResult> UserResetPasswordEmail(ResetPasswordEmail emailModel)
        {
            if (ModelState.IsValid)
            {
                var user = _uow.UsersRepository.Get(ent => ent.Email == emailModel.Email);
                if (user != null)
                {
                    var log = new ForgetPasswordURL();
                    log.ToId = user.Id;
                    log.ToType = (int)EN_TypeUser.User;
                    log.Token = Guid.NewGuid();
                    log.Expiration = DateTime.Now.AddMinutes(30);


                    _uow.ForgetPasswordURLRepository.Add(log);
                    _uow.Save();

                    var subject = "Reset Password Confirmation Email";
                    string url1 = $"{AppSession.AppURL}Account/ResetPassword?token={ log.Token }";
                    var model = new EmailModelsResetPass();
                    model.Url = url1;
                    string htmlbody = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/_ResetPasswordEmail.cshtml", model);
                    var emailsendstatus = EmailsSettings.EmailSend(user.EnglishName, user.Email, subject, htmlbody);
                    if (!emailsendstatus)
                        ModelState.AddModelError("Email", "Error Occured While Send Email!!");
                    else
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Reset Password Email Sent Check Your Email",
                            Data = null,
                            Errors = null
                        });
                    
                }
                else
                {
                    ModelState.AddModelError("Email", "No Email Found Match Entered Email");
                }
            }
            var errors = ModelState.Select(ent => new { key = ent.Key, value = ent.Value.Errors.Select(err => (err.ErrorMessage == null || err.ErrorMessage == "") ? err.Exception.Message : err.ErrorMessage) });
            var errors_list = new List<string>();
            foreach (var sublist in errors)
            {
                foreach (var item in sublist.value)
                    errors_list.Add(sublist.key + ": " + item);
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors_list.ToArray()
            });
        }

        [HttpPost("CustomerResetPasswordEmail")]
        public async Task<IActionResult> CustomerResetPasswordEmail(ResetPasswordEmail emailModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _uow.CustomerRepository.Get(ent => ent.Email == emailModel.Email);
                if (customer != null)
                {
                    var log = new ForgetPasswordURL();
                    log.ToId = customer.Id;
                    log.ToType = (int)EN_TypeUser.Customer;
                    log.Token = Guid.NewGuid();
                    log.Expiration = DateTime.Now.AddMinutes(30);


                    _uow.ForgetPasswordURLRepository.Add(log);
                    _uow.Save();

                    var subject = "Reset Password Confirmation Email";
                    string url1 = $"{AppSession.AppURL}Account/ResetPassword?token={ log.Token }";
                    var model = new EmailModelsResetPass();
                    model.Url = url1;
                    string htmlbody = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/_ResetPasswordEmail.cshtml", model);
                    var emailsendstatus = EmailsSettings.EmailSend(customer.EnglishName, customer.Email, subject, htmlbody);
                    if (!emailsendstatus)
                        ModelState.AddModelError("Email", "Error Occured While Send Email!!");
                    else
                        return Ok(new ApiResponseModel
                        {
                            Status = EN_ResponseStatus.Success,
                            Message = "Reset Password Email Sent Check Your Email",
                            Data = null,
                            Errors = null
                        });

                }
                else
                {
                    ModelState.AddModelError("Email", "No Email Found Match Entered Email");
                }
            }
            var errors = ModelState.Select(ent => new { key = ent.Key, value = ent.Value.Errors.Select(err => (err.ErrorMessage == null || err.ErrorMessage == "") ? err.Exception.Message : err.ErrorMessage) });
            var errors_list = new List<string>();
            foreach (var sublist in errors)
            {
                foreach (var item in sublist.value)
                    errors_list.Add(sublist.key + ": " + item);
            }
            return Ok(new ApiResponseModel
            {
                Status = EN_ResponseStatus.Faild,
                Message = "Error Occured",
                Data = null,
                Errors = errors_list.ToArray()
            });
        }


    }
}
