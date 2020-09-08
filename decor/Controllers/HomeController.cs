using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Decor.Model;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Decor.Services;
using System.Net.Mail;
using Model;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BL.Infrastructure;
using BL.Security;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using BL.Secuirty;

namespace Decor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly LocService _sharedLocalizer;
        private readonly ISecurity _Security;
        public HomeController(IUnitOfWork uow, LocService SharedLocalizer, ISecurity Security) { _uow = uow; _sharedLocalizer = SharedLocalizer; _Security = Security; }

        public IActionResult Index()
        {

            
            return View();
        }
       
        #region Contact us

        [AllowAnonymous]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Send(ContactUs entity)
        {
            bool isOk = false;
            string msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var fromAddress = new MailAddress(AppSession.CompanyEmail, string.IsNullOrEmpty(entity.Name) ? AppSession.CompanyEmailDisplayName : entity.Name);
                    MailMessage mail = new MailMessage();
                    mail.From = fromAddress;
                    mail.To.Add(new MailAddress(AppSession.CompanyEmail));
                    mail.Body = $"Customer Email: {entity.Email} <br />" + entity.Message;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = true;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(fromAddress.Address, AppSession.CompanyEmailPassword);
                        smtp.Send(mail);
                    }
                    isOk = true;
                }
                catch (Exception ex)
                { msg = "please enter all fields";
                    return Json(new { isOk, msg });
                }
            }
            return Json(new { isOk, msg });
        }
    

        #endregion


        #region About us

        [AllowAnonymous]
        public IActionResult AboutUs()
        {
           
            return View();
        }

        #endregion

     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Model.ErrorViewModel1 { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
