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

namespace sal7lyAdmin.Controllers
{
    public class Emails : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;
        private readonly ISecurity _Security;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public Emails(IUnitOfWork uow, IHostingEnvironment env, ISecurity Security, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _uow = uow; _env = env; _Security = Security;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }


        public IActionResult Index()
        {
            //var txt = emailtext();
            return View();
        }

        public IActionResult _ResetPasswordEmail()
        {
            var emails = new EmailModelsResetPass();
            emails.Url = "https://www.google.com";
            return View(emails);
        }
    }
}