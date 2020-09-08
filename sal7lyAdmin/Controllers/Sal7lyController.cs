using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sal7lyAdmin.Models;
using sal7lyAdmin.Services;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class Sal7lyController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _uow;

        private readonly ISecurity _security;
  
        public Sal7lyController(IUnitOfWork uow, ISecurity security, ILogger<HomeController> logger)
        {
            _uow = uow;
            _security = security;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _uow.SharedRepository.GetDashboard();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
