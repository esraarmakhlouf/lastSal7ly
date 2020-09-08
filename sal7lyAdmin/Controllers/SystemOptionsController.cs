using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class SystemOptionsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;

        public SystemOptionsController(IUnitOfWork uow,  ISecurity security) { _uow = uow; _security = security; }

        public ActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.SystemOptions, EN_Permissions.View))
            { return Redirect("/Home/Index"); }


            var result = new HashSet<SystemOption>();

            result = _uow.SystemOptionsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && !ent.IsHidden).ToHashSet();

            var categories = result.Select(ent => new { id = ent.Id, text = ent.CategoryArName  }).Distinct().ToHashSet();
            ViewBag.categories = new SelectList(categories, "id", "text");
            return View(result);
        }

        [HttpPost]
        public ActionResult Search(IFormCollection formData)
        {
            //var result = _uow.SystemOptionsRepository.GetMany(ent => !ent.IsDeleted && !ent.IsHidden).ToHashSet();
            var result = _uow.SystemOptionsRepository.GetMany(ent => !ent.IsDeleted).OrderByDescending(ent => ent.CreationDate).ToHashSet();


            result = result.Where(ent => (string.IsNullOrEmpty(formData["category"]) || ent.CategoryArName == formData["category"]) || ent.CategoryEnName == formData["category"]).ToHashSet();

            #region Filter by ArabicName
            switch (formData["ArabicNameSearchCriteria"])
            {
                case "0":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["ArabicName"]) || tt.ArabicName.Contains(formData["ArabicName"])).ToHashSet();
                    break;
                case "1":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["ArabicName"]) || tt.ArabicName.StartsWith(formData["ArabicName"])).ToHashSet();
                    break;
                case "2":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["ArabicName"]) || tt.ArabicName.EndsWith(formData["ArabicName"])).ToHashSet();
                    break;
                case "3":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["ArabicName"]) || tt.ArabicName == formData["ArabicName"]).ToHashSet();
                    break;
                default:
                    break;
            }
            #endregion

            #region Filter by EnglishName
            switch (formData["EnglishNameSearchCriteria"])
            {
                case "0":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["EnglishName"]) || tt.EnglishName.Contains(formData["EnglishName"])).ToHashSet();
                    break;
                case "1":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["EnglishName"]) || tt.EnglishName.StartsWith(formData["EnglishName"])).ToHashSet();
                    break;
                case "2":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["EnglishName"]) || tt.EnglishName.EndsWith(formData["EnglishName"])).ToHashSet();
                    break;
                case "3":
                    result = result.Where(tt =>
          string.IsNullOrEmpty(formData["EnglishName"]) || tt.EnglishName == formData["EnglishName"]).ToHashSet();
                    break;
                default:
                    break;
            }
            #endregion

            return PartialView("_List", result);
        }

        public ActionResult OpenForm(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.SystemOptions, EN_Permissions.Edit))
            { return Redirect("/Home/Index"); }

            SystemOption entity = _uow.SystemOptionsRepository.GetById(id);
            if (entity == null || entity.IsDeleted || entity.IsHidden)
            { return NotFound(); }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(SystemOption entity, IFormCollection form)
        {
            string strReturnMsg = "error";
            try
            {
                var systemOption = _uow.SystemOptionsRepository.GetById(entity.Id);
                if (systemOption.IsReadOnly != true)
                {
                    if (!string.IsNullOrEmpty(form["TextValue"]))
                    { systemOption.Value = form["TextValue"]; }
                   
                    if (!string.IsNullOrEmpty(form["Time"]))
                    {
                        var date = form["Time"].ToString();
                        systemOption.Time = TimeSpan.Parse(date);
                    }
                    
                    systemOption.ModificationDate = DateTime.Now;
                    systemOption.ModifiedBy = AppSession.CurrentUser.Id;


                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(systemOption);
                    if (ModelState.IsValid)
                    { _uow.SystemOptionsRepository.Update(systemOption); }
                    _uow.Save();
                    strReturnMsg = "success";
                }
            }
            catch (Exception ex) { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }
    }
}