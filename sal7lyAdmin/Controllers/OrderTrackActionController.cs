using System;
using System.Collections.Generic;
using System.Linq;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using BL.SharedCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class OrderTrackActionController : Controller
    {
        private readonly IUnitOfWork _uow;
        
        private readonly ISecurity _security;

        public OrderTrackActionController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.OrderTrackAction, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.OrderTrackActionRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.OrderTrackActionRepository.GetAll().ToHashSet();
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
                    default:
                        break;
                }
            }

            result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return PartialView("_List", result);
        }

        public IActionResult OpenForm(int? id)
        {
            var entity = new OrderTrackAction();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.OrderTrackAction, EN_Permissions.Edit))
                { return Redirect("/Home"); }

                entity = _uow.OrderTrackActionRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.OrderTrackAction, EN_Permissions.Create))
                { return Redirect("/Home"); }
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(OrderTrackAction entity)
        {
            string strReturnMsg = "error";
            try
            {
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.OrderTrackAction, _uow);
                    if (string.IsNullOrEmpty(entity.ArabicName)) { entity.ArabicName = entity.ArabicName; }
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                   
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.OrderTrackActionRepository.Add(entity); }
                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                    entity.ModifiedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.OrderTrackActionRepository.Update(entity); }
                }
                _uow.Save();
                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public IActionResult Delete(int id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.OrderTrackAction, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";

                try
                {
                    var dbObj = _uow.OrderTrackActionRepository.GetById(id);
                    if (dbObj == null)
                    {
                        status = "error";
                        errorMessage = "Delete Error message";
                    }
                    else
                    {
                        dbObj.IsDeleted = true;
                        dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                        dbObj.ModificationDate = DateTime.Now;
                        _uow.OrderTrackActionRepository.Update(dbObj);
                        _uow.Save();
                    }
                }
                catch (Exception ex)
                {
                    status = "error";
                    errorMessage = "Delete Error message";
                }
                return Json(new { status = status, ErrorMessage = errorMessage });
        }

    }
}