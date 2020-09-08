using System;
using System.Linq;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class GroupsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;

        public GroupsController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Groups, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.GroupsRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.GroupsRepository.GetAll();
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

        public IActionResult OpenForm(long? id)
        {
            var entity = new Groups();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Groups, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.GroupsRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Groups, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Groups entity)
        {
            string strReturnMsg = "error";
            try
            {
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.Groups, _uow);
                    if (string.IsNullOrEmpty(entity.EnglishName)) { entity.EnglishName = entity.ArabicName; }
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    {
                        _uow.GroupsRepository.Add(entity);
                    }
                    else return View(entity);
                }
                else
                {
                    if (!entity.IsMaster)
                    {
                        entity.ModificationDate = DateTime.Now;
                        entity.ModifiedBy = AppSession.CurrentUser.Id;
                        //Re-ValidateModel
                        ModelState.Clear();
                        TryValidateModel(entity);
                        if (ModelState.IsValid)
                        {
                            _uow.GroupsRepository.Update(entity);
                        }
                        else return View(entity);
                    }
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
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Groups, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

            try
            {
                var dbObj = _uow.GroupsRepository.GetById(id);
                if (dbObj == null || dbObj.IsMaster)
                {
                    status = "error";
                    errorMessage = "Delete Error Msg";

                }
                else
                {
                    dbObj.IsDeleted = true;
                    dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                    dbObj.ModificationDate = DateTime.Now;
                    _uow.GroupsRepository.Update(dbObj);
                    _uow.Save();
                }
            }
            catch (Exception ex)
            {
                status = "error";
                errorMessage = "Delete Error Msg";
            }
            return Json(new { status, ErrorMessage = errorMessage });
        }

        #region Permissions

        public IActionResult Permissions(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Groups, EN_Permissions.Edit))
            { return Redirect("/Home"); }

            var group = new Groups();
            group = _uow.GroupsRepository.Get(ent => ent.Id == id && !ent.IsDeleted);
            if (group == null)
            { return NotFound(); }

            ViewBag.modules = _uow.ModulesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).ToHashSet();
            return View(group);
        }

        public IActionResult Screens(long groupId, long moduleId)
        {
            var group = new Groups();
            group = _uow.GroupsRepository.Get(ent => ent.Id == groupId && !ent.IsDeleted);
            if (group == null)
            { return NotFound(); }

            ViewBag.screens = _uow.ScreensRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.ModuleId == moduleId).ToHashSet();
            return View(group);
        }

        public IActionResult SetPermission(long groupId, long screenId, long permissionId, bool allow)
        {
            bool isOk = true;
            try
            {
                if (allow)
                {
                    var gp = new GroupPermissions();
                    gp.GroupId = groupId;
                    gp.ScreenId = screenId;
                    gp.PermissionId = permissionId;
                    _uow.GroupPermissionsRepository.Add(gp);
                }
                else
                {
                    _uow.ExecuteSqlCommand("Delete From GroupPermissions Where GroupId=" + groupId + " AND ScreenId=" + screenId + " AND PermissionId=" + permissionId);
                }
                _uow.Save();
            }
            catch (Exception ex)
            { isOk = false; }

            return Json(isOk);
        }

        public IActionResult SetAllScreenPermissions(long groupId, long screenId, bool allow)
        {
            bool isOk = true;
            try
            {
                if (allow)
                {
                    var screenPermissions = _uow.ScreenPermissionsRepository.GetMany(ent => ent.ScreenId == screenId).ToHashSet();
                    foreach (var permission in screenPermissions)
                    {
                        var gp = new GroupPermissions();
                        gp.GroupId = groupId;
                        gp.ScreenId = screenId;
                        gp.PermissionId = permission.PermissionId;
                        _uow.GroupPermissionsRepository.Add(gp);
                    }
                }
                else
                {
                    _uow.ExecuteSqlCommand("Delete From GroupPermissions Where GroupId=" + groupId + " AND ScreenId=" + screenId);
                }
                _uow.Save();
            }
            catch (Exception ex)
            { isOk = false; }

            return Json(isOk);
        }

        public IActionResult ResetGroupPermissions(long groupId)
        {
            bool isOk = true;
            try
            {
                _uow.ExecuteSqlCommand("Delete From GroupPermissions Where GroupId=" + groupId);
            }
            catch (Exception ex)
            { isOk = false; }

            return Json(isOk);
        }

        public IActionResult GiveGroupAllPermissions(long groupId)
        {
            bool isOk = true;
            try
            {
                var groupPsermissions = _security.GetGroupPermissions(groupId).ToHashSet();
                var screenPermissions = _uow.ScreenPermissionsRepository.GetMany(ent => !groupPsermissions.Any(gp => gp.ScreenId == ent.ScreenId && gp.PermissionId == ent.PermissionId)).ToHashSet();
                foreach (var item in screenPermissions)
                {
                    var gp = new GroupPermissions();
                    gp.GroupId = groupId;
                    gp.ScreenId = item.ScreenId;
                    gp.PermissionId = item.PermissionId;
                    _uow.GroupPermissionsRepository.Add(gp);
                }
                _uow.Save();
            }
            catch (Exception ex)
            { isOk = false; }

            return Json(isOk);
        }

        public IActionResult ModulePermissions(long groupId, long moduleId, bool allow)
        {
            bool isOk = true;
            var screens = _uow.ScreensRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.ModuleId == moduleId).Select(ent => ent.Id).ToHashSet();
            try
            {
                if (allow && screens.Count() > 0)
                {
                    var screenPermissions = _uow.ScreenPermissionsRepository.GetMany(ent => screens.Contains(ent.ScreenId)).ToHashSet();
                    foreach (var item in screenPermissions)
                    {
                        var gp = new GroupPermissions();
                        gp.GroupId = groupId;
                        gp.ScreenId = item.ScreenId;
                        gp.PermissionId = item.PermissionId;
                        _uow.GroupPermissionsRepository.Add(gp);
                    }
                    _uow.Save();
                }
                else
                {
                    _uow.ExecuteSqlCommand("Delete From GroupPermissions Where GroupId=" + groupId + " AND ScreenId IN(" + string.Join(",", screens) + ")");
                }
            }
            catch (Exception)
            { isOk = false; }
            return Json(isOk);
        }

        #endregion
    }
}