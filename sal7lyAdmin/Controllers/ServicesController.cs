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
    public class ServicesController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;
        private readonly IHostingEnvironment _env;

        public ServicesController(IUnitOfWork uow, ISecurity Security, IHostingEnvironment env) { _uow = uow; _security = Security; _env = env; }


        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Services, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.ServicesRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.ServicesRepository.GetAll().ToHashSet();
            //  check advanced search
            #region Filter by active
            if (formData.ContainsKey("Active") && bool.Parse(formData["Active"]))
            {
                result = result.Where(ent => ent.IsActive).ToHashSet();
            }
            #endregion
            #region Filter by Code
            if (!string.IsNullOrEmpty(formData["Code"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.Code) && tt.Code.ToLower().Contains(formData["Code"].ToString().ToLower().Trim())).ToHashSet();
            }
            #endregion
            #region Filter by ArabicName
            if (!string.IsNullOrEmpty(formData["ArabicName"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.ArabicName) && tt.ArabicName.ToLower().Contains(formData["ArabicName"].ToString().ToLower().Trim())).ToHashSet();
            }
            #endregion
            #region Filter by EnglishName
            if (!string.IsNullOrEmpty(formData["EnglishName"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.EnglishName) && tt.EnglishName.ToLower().Contains(formData["EnglishName"].ToString().ToLower().Trim())).ToHashSet();
            }
            #endregion

            //  checkالبحث العادي
            if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                switch (searchKey.ToLower())
                {
                    case "arabicname":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.ArabicName) && ent.ArabicName.ToLower().Contains(searchTerm)).ToHashSet();
                        break;
                    case "englishname":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.EnglishName) && ent.EnglishName.ToLower().Contains(searchTerm)).ToHashSet();
                        break;
                    case "code":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.Code) && ent.Code.ToLower().Contains(searchTerm)).ToHashSet();
                        break;
                    case "active":
                        result = result.Where(ent => (ent.IsActive)).ToHashSet();
                        break;
                    default:
                        break;
                }
            }

            result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return PartialView("_List", result);
        }

        public IActionResult OpenForm(long? id)
        {
            var entity = new Model.Service();
            //assign default image to service
            entity.ImagePath = AppSession.ServiceDefaultImage;
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Services, EN_Permissions.Edit))
                { return Redirect("Home"); }

                entity = _uow.ServicesRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Services, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Model.Service entity,IFormCollection form)
        {
            string strReturnMsg = "error";
            try
            {
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.Services, _uow);
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.ServicesRepository.Add(entity); }
                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                    entity.ModifiedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.ServicesRepository.Update(entity); }
                }
                _uow.Save();
                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Services, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

                try
                {
                    var dbObj = _uow.ServicesRepository.GetById(id);
                    if (dbObj == null )
                    {
                        status = "error";
                        errorMessage = "Delete Error message";
                    }
                    else
                    {
                        dbObj.IsDeleted = true;
                        dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                        dbObj.ModificationDate = DateTime.Now;
                        _uow.ServicesRepository.Update(dbObj);
                        _uow.Save();
                    }
                }
                catch (Exception ex)
                {
                    status = "error";
                    errorMessage = "Delete Error message";
                }
                return Json(new { status, ErrorMessage = errorMessage });
        }
        #region Image

        public async Task< IActionResult> SaveServicesImage(long id)
        {
            bool message = true;
            try
            {
                var service = _uow.ServicesRepository.GetById(id);
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                var fileName = AppSession.ServiceDefaultImage;
                if (file != null && file.Length > 0)
                {
                    var webRoot = _env.WebRootPath;
                    var uploadsFolder = Path.Combine(webRoot, AppSession.ServicesImagesUploads);
                    fileName = await Images.SaveFile(uploadsFolder, file, lastName: service.ImagePath, defaultName: AppSession.ServiceDefaultImage);
                    if (fileName == "" || fileName == null) {
                        fileName = AppSession.ServiceDefaultImage;
                        message = false;
                    }
                    
                }
                service.ImagePath = fileName;
                _uow.ServicesRepository.Update(service);
                _uow.Save();
            }
            catch(Exception ex)
            {
                message = false;
            }
            
            return Json(new { success = message });
        }

        public IActionResult DeleteServicesImage(long id)
        {
            try
            {
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.ServicesImagesUploads);
                var dbObj = _uow.ServicesRepository.GetById(id);
                var filePath = "";
                var fileName = AppSession.ServiceDefaultImage;
                //Delete old file
                if (!string.IsNullOrEmpty(dbObj.ImagePath) && dbObj.ImagePath!= AppSession.ServiceDefaultImage)
                {
                    filePath = Path.Combine(uploadsFolder, dbObj.ImagePath);
                    if (System.IO.File.Exists(filePath))
                    { System.IO.File.Delete(filePath); }
                   
                }
                //Update record with filename
                dbObj.ImagePath = fileName;
                _uow.ServicesRepository.Update(dbObj);
                _uow.Save();
                return Json(new { success = true });
            }
            catch (Exception) { }
            return Json(new { success = false });
        }

        #endregion
    }
}