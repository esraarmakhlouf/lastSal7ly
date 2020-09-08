using System;
using System.IO;
using System.Linq;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;
using System.Threading.Tasks;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _uow;
        
        private readonly ISecurity _security;
        private readonly IHostingEnvironment _env;

        public CustomerController(IUnitOfWork uow, ISecurity security, IHostingEnvironment env) 
        { 
            _uow = uow; 
            _security = security;
            _env = env; 
        }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Customer, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.CustomerRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            FillFormDropdowns(new Customer());
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.CustomerRepository.GetAll();
            //  check advanced search
            #region Filter by active
            if (formData.ContainsKey("Active") && bool.Parse(formData["Active"]))
            {
                result = result.Where(ent => ent.IsActive);
            }
            #endregion
            #region Filter by Code
            if (!string.IsNullOrEmpty(formData["Code"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.Code) && tt.Code.ToLower().Contains(formData["Code"].ToString().ToLower().Trim()));
            }
            #endregion
            #region Filter by ArabicName
            if (!string.IsNullOrEmpty(formData["ArabicName"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.ArabicName) && tt.ArabicName.ToLower().Contains(formData["ArabicName"].ToString().ToLower().Trim()));
            }
            #endregion
            #region Filter by EnglishName
            if (!string.IsNullOrEmpty(formData["EnglishName"]))
            {
                result = result.Where(tt => !string.IsNullOrEmpty(tt.EnglishName) && tt.EnglishName.ToLower().Contains(formData["EnglishName"].ToString().ToLower().Trim()));
            }
            #endregion
            #region Filter by City
            long city = 0;
            if (formData.ContainsKey("City") && long.TryParse(formData["City"].ToString(), out city))
            {
                result = result.Where(tt => tt.CityId == city);
            }
            #endregion
            //  checkالبحث العادي
            if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                switch (searchKey.ToLower())
                {
                    case "arabicname":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.ArabicName) && ent.ArabicName.ToLower().Contains(searchTerm));
                        break;
                    case "englishname":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.EnglishName) && ent.EnglishName.ToLower().Contains(searchTerm));
                        break;
                    case "code":
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.Code) && ent.Code.ToLower().Contains(searchTerm));
                        break;

                    default:
                        break;
                }
            }
            
            result = result.OrderByDescending(ent => ent.CreationDate);
            return PartialView("_List", result.ToHashSet());
        }
        public JsonResult EmailExists(string Email)
        {
            var user = _uow.CustomerRepository.Get(ent=>ent.Email==Email.Trim());
            return user == null ?
                Json(true) :
                Json(
                    string.Format("an account for address {0} already exists.",
                    Email));
        }
        public IActionResult OpenForm(long? id)
        {
            var entity = new Customer();
            //assign default image to customer
            entity.ImageName = AppSession.CustomerDefaultImage;
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Customer, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.CustomerRepository.GetById(id);
                
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
                
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Customer, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
           
            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Customer entity)
        {
            string strReturnMsg = "error";
            
            try
            {
                entity.Password = EncryptANDDecrypt.EncryptText(entity.Password);
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.Customer, _uow);
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    //UploadedFile(entity, form);
                    if (ModelState.IsValid)
                    { 
                        _uow.CustomerRepository.Add(entity); 
                    }
                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                    entity.ModifiedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.CustomerRepository.Update(entity); }
                }
                _uow.Save();
                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public async Task< IActionResult> SaveCustomerImage(long id)
        {
            bool message = false;
            try
            {
                var customer = _uow.CustomerRepository.GetById(id);
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                var fileName = await Images.SaveFile(AppSession.CustomerUploads, file,lastName:customer.ImageName, defaultName: AppSession.CustomerDefaultImage);
                if (fileName != "" && fileName != null)
                {
                    customer.ImageName = fileName;
                    _uow.CustomerRepository.Update(customer);
                    _uow.Save();
                    message = true;
                }
            }
            catch (Exception)
            {
                message = false;
            }
            return Json(new { success = message });
        }

        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Customer, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

                try
                {
                    var dbObj = _uow.CustomerRepository.GetById(id);
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
                        _uow.CustomerRepository.Update(dbObj);
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

        private void FillFormDropdowns(Customer entity)
        {
            
            var CitiesList = _uow.CityRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.CitiesList = new SelectList(CitiesList, "Value", "Text");
        }
    }
}