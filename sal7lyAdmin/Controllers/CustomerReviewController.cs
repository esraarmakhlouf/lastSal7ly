using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class CustomerReviewController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IHostingEnvironment _env;

        public CustomerReviewController(IUnitOfWork uow, ISecurity Security, IHostingEnvironment env) { _uow = uow; _security = Security; _env = env; }


        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.CustomerReview, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.CustomerReviewRepository.GetAll().OrderByDescending(ent => ent.Id).ToHashSet();
            return View(result);
        }

     

        public IActionResult OpenForm(long? id)
        {
            var entity = new Model.CustomerReview();
            //assign default image to CustomerReview
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.CustomerReview, EN_Permissions.Edit))
                { return Redirect("Home"); }

                entity = _uow.CustomerReviewRepository.GetById(id);
                if (entity == null )
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.CustomerReview, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            return View(entity);
        }

    
        #region Image

        public async Task<IActionResult> SaveCustomerReviewImage(long id)
        {
            bool message = true;
            try
            {
                var CustomerReview = new CustomerReview();
                
                if(id!=0) CustomerReview = _uow.CustomerReviewRepository.GetById(id);

                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                var fileName = "";
                if (file != null && file.Length > 0)
                {
                    var webRoot = _env.WebRootPath;
                    var uploadsFolder = Path.Combine(webRoot, AppSession.CustomerReviewUploads);
                    fileName = await Images.SaveFile(uploadsFolder, file, lastName: CustomerReview.ImagePath, defaultName: "");
                    if (fileName == "" || fileName == null) {
                        message = false;
                        return Json(new { success = message });

                    }

                }
                CustomerReview.ImagePath = fileName;
                if(id!=0)
                _uow.CustomerReviewRepository.Update(CustomerReview);
                else
                    _uow.CustomerReviewRepository.Add(CustomerReview);

                _uow.Save();
            }
            catch(Exception ex)
            {
                message = false;
            }
            
            return Json(new { success = message });
        }

        public IActionResult DeleteCustomerReviewImage(long id)
        {
            try
            {
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.CustomerReviewUploads);
                var dbObj = _uow.CustomerReviewRepository.GetById(id);
                var filePath = "";
                var fileName = "";
                //Delete old file
                if (!string.IsNullOrEmpty(dbObj.ImagePath) && dbObj.ImagePath!= "")
                {
                    filePath = Path.Combine(uploadsFolder, dbObj.ImagePath);
                    if (System.IO.File.Exists(filePath))
                    { System.IO.File.Delete(filePath); }
                   
                }
                //Update record with filename
                dbObj.ImagePath = fileName;
                _uow.CustomerReviewRepository.Update(dbObj);
                _uow.Save();
                return Json(new { success = true });
            }
            catch (Exception) { }
            return Json(new { success = false });
        }

        #endregion

        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.CustomerReview, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

            try
            {
                _uow.ExecuteSqlCommand("Delete From CustomerReview Where id='" + id+ "'");
            
                _uow.Save();
        }
            catch (Exception ex)
            {
                status = "error";
                errorMessage = "Delete Error message";
            }
            return Json(new { status, ErrorMessage = errorMessage });
        }
    }
}