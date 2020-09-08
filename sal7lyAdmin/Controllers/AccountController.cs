using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Model;
using Model.APIModels;
using sal7lyAdmin.Services;
using RazorEngine;
using RazorEngine.Templating;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;
        private readonly ISecurity _Security;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public AccountController(IUnitOfWork uow, IHostingEnvironment env, ISecurity Security, IRazorViewToStringRenderer razorViewToStringRenderer)
        { _uow = uow; _env = env; _Security = Security;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }


        public IActionResult Index()
        {
            //var txt = emailtext();
            return View();
        }

        public ActionResult CheckIfLogin()
        {
            var _user = WebHelpers.HttpContext.Session.Get<Users>("CurrentCustomer");
            if (_user != null)
            {
                return Ok(new ResponseModel
                {
                    ModelState = EN_ModelState.Success,
                    Status = EN_ResponseStatus.Success,
                    Message = "user  logged in",
                    Data = null
                });
            }
            else
            {
                return Ok(new ResponseModel
                {
                    ModelState = EN_ModelState.Success,
                    Status = EN_ResponseStatus.Faild,
                    Message = "you must  logged  in first",
                    Data = null
                });
            }

        }

        #region Log in

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            LoginViewModel user = new LoginViewModel();
            user.RememberMe = false;
            if (Request.Cookies[EncryptANDDecrypt.EncryptText("ECommerceUserName")] != null)
            {
                user.UserName = EncryptANDDecrypt.DecryptText(Request.Cookies[EncryptANDDecrypt.EncryptText("ECommerceUserName")]);
                user.Password = EncryptANDDecrypt.DecryptText(Request.Cookies[EncryptANDDecrypt.EncryptText("ECommercePassword")]);

                user.RememberMe = true;

                CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                var currentUser = _uow.UsersRepository.GetMany(ent => ent.UserName == user.UserName && !ent.IsDeleted).FirstOrDefault();
                if (currentUser == null)
                {
                    return View("Index");
                }
                AppSession.CurrentUser = currentUser;
                var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                if (model.RememberMe) { option.Expires = DateTime.Now.AddDays(30); }
                Response.Cookies.Append(EncryptANDDecrypt.EncryptText("ECommerceUserName"), EncryptANDDecrypt.EncryptText(model.UserName), option);
                Response.Cookies.Append(EncryptANDDecrypt.EncryptText("ECommercePassword"), EncryptANDDecrypt.EncryptText(model.Password), option);
                var user = _uow.UsersRepository.GetMany(ent => ent.UserName == model.UserName && !ent.IsDeleted).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "UserNotFoundErrMsg");
                    ViewBag.ErrorMessage = "User Not Found ";
                    return View(model);
                }
                else if (!user.IsActive)
                {
                    ModelState.AddModelError("", "UserIsInActiveErrMsg");
                    ViewBag.ErrorMessage = "User Is InActive";
                    return View(model);
                }
                else if (!UserAccountMannager.AuthenticateUser(model, _Security, _uow))
                {
                    ModelState.AddModelError("", "UserOrPasswordIsWrongErrMsg");
                    ViewBag.ErrorMessage = "User Or Password Is WrongErrMsg";
                    return View(model);
                }
                else
                {
                    AppSession.CurrentUser = user;
                    var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        #endregion


        [AuthorizeLogIn]
        public ActionResult LogOut()
        {
            AppSession.CurrentUser = null;
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("LogIn", "Account");
        }

        [AuthorizeLogIn]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [AuthorizeLogIn]
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword password)
        {
            if (ModelState.IsValid)
            {
                var currentuser = AppSession.CurrentUser;
                if (currentuser == null)
                {
                    return RedirectToAction("LogIn", "Account");
                }
                if (password != null && !string.IsNullOrEmpty(password.NewPassword) && password.NewPassword == password.ConfirmNewPassword)
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
                        AppSession.CurrentUser = null;
                        return RedirectToAction("ChangePasswordDone", "Account");
                    }
                }
            }
            return View(password);
        }

        [HttpGet]
        public IActionResult ChangePasswordDone()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidatePassword(string CurrentPassword)
        {
            bool result;
            var currentuser = AppSession.CurrentUser;
            if (EncryptANDDecrypt.DecryptText(currentuser.Password) == CurrentPassword)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result);
        }

        [HttpGet]
        public IActionResult ResetPasswordEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> ResetPasswordEmail(ResetPasswordEmail emailModel)
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
                    var emailsendstatus=EmailsSettings.EmailSend(user.EnglishName, user.Email,subject,htmlbody);
                    if (!emailsendstatus)
                        ModelState.AddModelError("Email", "Error Occured While Send Email!!");
                    else 
                    return RedirectToAction("ResetPasswordDone", "Account");
                }
                else
                {
                    ModelState.AddModelError("Email", "No Email Found Match Entered Email");
                }
            }
            return View(emailModel);
        }

        public IActionResult ResetPasswordDone()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(Guid token)
        {
            var resetpass = new ResetPassword();
            var log = _uow.ForgetPasswordURLRepository.Get(ent => ent.Token == token );
            if (log != null && (DateTime.Now - log.Expiration).TotalMinutes < 30)
            {
                resetpass.token = token;
            }
            else resetpass.ActiveToken = false;
            return View(resetpass);
        }
        [HttpPost]
        public IActionResult ResetPassword( ResetPassword passwordModel)
        {
            if (ModelState.IsValid)
            {
                var log = _uow.ForgetPasswordURLRepository.Get(ent => ent.Token == passwordModel.token);
                if (log != null)
                {
                    Users user = null;
                    Customer cust = null;
                    if (log.ToType == (int)EN_TypeUser.Customer)
                    {
                        cust = _uow.CustomerRepository.Get(ent => ent.Id == log.ToId);
                    }
                    else
                    {
                        user=_uow.UsersRepository.Get(ent => ent.Id == log.ToId);
                    }
                      
                    //token still active
                    if ((DateTime.Now - log.Expiration).TotalMinutes < 30)
                    {
                        if (user != null)
                        {
                            user.Password = EncryptANDDecrypt.DecryptText(passwordModel.Password);
                            user.ModificationDate = DateTime.Now;
                            _uow.UsersRepository.Update(user);
                            

                        }
                        else if(cust != null)
                        {
                            cust.Password = EncryptANDDecrypt.DecryptText(passwordModel.Password);
                            cust.ModificationDate = DateTime.Now;
                            _uow.CustomerRepository.Update(cust);
                            
                        }
                        else
                        {
                            ModelState.AddModelError("token", "Invalid token information");
                            
                        }
                        if (ModelState.IsValid)
                        {
                            _uow.Save();
                            ViewBag.Type = log.ToType;
                            return RedirectToAction("ResetPasswordComplete", "Account");
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("token", "Expired token");
                    }
                }
                else
                {
                    ModelState.AddModelError("token", "Invalid token");
                }
            }
            return View(passwordModel);
        }

        [HttpGet]
        public IActionResult ResetPasswordComplete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidateEmail(string Email)
        {
            bool result;
            result = false;
            var user = _uow.UsersRepository.Get(ent => ent.Email == Email);
            if (user != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result);
        }

        

    }
}