using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using BL.SharedCS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using Newtonsoft.Json;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly ISecurity _security;
        private readonly IHostingEnvironment _env;

        public UsersController(IUnitOfWork uow, ISecurity security, IHostingEnvironment env) { _uow = uow; _security = security; _env = env; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.UsersRepository.GetAll();
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                result = result.Where(ent => ent.CityId == AppSession.CurrentUser.CityId);
            FillFormDropdowns(new Users());
            return View(result.OrderByDescending(ent => ent.CreationDate).ToHashSet());
        }


        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.UsersRepository.GetAll();
            if (AppSession.CurrentUser.JobTitleId == (int)En_JobTitle.egent)
                result = result.Where(ent => ent.CityId == AppSession.CurrentUser.CityId);
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
            return PartialView("_List", result.OrderByDescending(ent => ent.CreationDate).ToHashSet());
        }

        public IActionResult OpenForm(long? id)
        {
            var entity = new Users();
            entity.Technical = new Technical();
            //assign default image to Users
            entity.ImageName = AppSession.UserDefaultImage;
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.UsersRepository.GetAll().Where(ent => ent.Id == id).Include(ent => ent.Technical).FirstOrDefault();
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }

            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }
            FillFormDropdowns(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Users entity, IFormCollection form)
        {
            string strReturnMsg = "error";
            var technical = entity.Technical;
            entity.Technical = null;

            ///revalidate
            ModelState.Clear();

            if (!TryValidateModel(entity))
                return View();
            else
            {
                try
                {

                    entity.Password = EncryptANDDecrypt.EncryptText(entity.Password);
                    if (entity.Id == default)
                    {
                        entity.Code = UIHelper.GeneratCode(EN_Screens.Users, _uow);
                        entity.CreationDate = DateTime.Now;
                        entity.CreatedBy = AppSession.CurrentUser.Id;
                        _uow.UsersRepository.Add(entity);

                        if (entity.JobTitleId == (int)En_JobTitle.Technical)
                        {
                            technical.UsersId = entity.Id;
                            _uow.TechnicalsRepository.Add(technical);
                        }
                    }
                    else
                    {
                        if (!entity.IsMaster)
                        {
                            entity.ModificationDate = DateTime.Now;
                            entity.ModifiedBy = AppSession.CurrentUser.Id;
                            _uow.UsersRepository.Update(entity);
                            if (entity.JobTitleId == (int)En_JobTitle.Technical)
                            {
                                technical.UsersId = entity.Id;
                                _uow.TechnicalsRepository.Update(technical);
                            }
                        }
                    }
                    _uow.Save();
                    #region Save groups
                    if (!entity.IsMaster)
                    {

                        string g = form["Groups"];
                        if (!string.IsNullOrEmpty(form["Groups"]))
                        {
                            var selectedGroups = form["Groups"].ToString().Split(",").Select(ent => long.Parse(ent)).ToHashSet();
                            foreach (var selectedGroupId in selectedGroups)
                            {
                                var dbObj = _uow.UserGroupsRepository.Get(ent => ent.GroupId == selectedGroupId && ent.UserId == entity.Id);
                                if (dbObj == null)
                                {
                                    var obj = new UserGroups();
                                    obj.UserId = entity.Id;
                                    obj.GroupId = selectedGroupId;
                                    _uow.UserGroupsRepository.Add(obj);
                                }
                            }

                            var deletedUserGroups = _uow.UserGroupsRepository.GetMany(ent => ent.UserId == entity.Id && !selectedGroups.Contains(ent.GroupId)).Select(ent => ent.Id).ToHashSet();
                            if (deletedUserGroups.Count() > 0)
                            {
                                _uow.ExecuteSqlCommand("DELETE FROM UserGroups WHERE Id IN(" + string.Join(",", deletedUserGroups) + ")");
                            }
                            _uow.Save();
                        }
                        else
                        { _uow.ExecuteSqlCommand("DELETE FROM UserGroups WHERE UserId=" + entity.Id); }
                    }
                    #endregion
                    strReturnMsg = "success";

                }
                catch (Exception ex)
                { }
            }

            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public async Task<IActionResult> SaveItemImage(long id)
        {
            bool message = false;
            try
            {
                var user = _uow.UsersRepository.GetById(id);
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.UserUploads);
                var fileName = await Images.SaveFile(uploadsFolder, file, lastName: user.ImageName, defaultName: AppSession.UserDefaultImage);
                if (!string.IsNullOrEmpty(fileName))
                {
                    user.ImageName = fileName;
                    _uow.UsersRepository.Update(user);
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
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

            try
            {
                var dbObj = _uow.UsersRepository.GetById(id);
                if (dbObj == null || dbObj.IsMaster)
                {
                    status = "error";
                    errorMessage = "Delete Error message";
                }
                else
                {
                    dbObj.IsDeleted = true;
                    dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                    dbObj.ModificationDate = DateTime.Now;
                    _uow.UsersRepository.Update(dbObj);
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

        private void FillFormDropdowns(Users entity)
        {
            var groupsList = _uow.GroupsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            var userGroups = _security.GetUserGroups(entity.Id).Select(ent => ent.Id).ToHashSet();
            ViewBag.groups = new MultiSelectList(groupsList, "Value", "Text", userGroups);

            var jobTitlesList = _uow.JobTitlesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.jobTitles = new SelectList(jobTitlesList, "Value", "Text", entity.JobTitleId);

            var services = _uow.ServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.services = new SelectList(services, "Value", "Text", (entity.Technical == null) ? null : entity.Technical.ServiceId);

            var citesList = _uow.CityRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
            ViewBag.cites = new SelectList(citesList, "Value", "Text", entity.CityId);
            if (entity.DistrictId != null)
            {
                var districtsList = _uow.DistrictRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.CityID == entity.CityId).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text = ent.ArabicName }).ToHashSet();
                ViewBag.districts = new SelectList(districtsList, "Value", "Text", entity.DistrictId);
            }
        }
        public IActionResult FillDistricts(long id)
        {
            var result = _uow.DistrictRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.CityID == id).Select(ent => new { id = ent.Id, text = ent.ArabicName }).ToHashSet();
            return Json(result);
        }

        [HttpPost]
        public IActionResult CheckExsistUserName(long Id, string UserName)
        {
            bool result = false;

            if (_uow.UsersRepository.Get(ent => ent.UserName.ToLower() == UserName.ToLower() && ent.Id != Id) == null)
            {
                result = true;
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult CheckExsistEmail(long Id, string Email)
        {
            bool result = false;

            if (_uow.UsersRepository.Get(ent => ent.Email.ToLower() == Email.ToLower() && ent.Id != Id) == null)
            {
                result = true;
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult CheckExsistMobile(long Id, string Mobile)
        {
            bool result = false;

            if (_uow.UsersRepository.Get(ent => ent.Mobile == Mobile && ent.Id != Id) == null)
            {
                result = true;
            }
            return Json(result);
        }
    }
}