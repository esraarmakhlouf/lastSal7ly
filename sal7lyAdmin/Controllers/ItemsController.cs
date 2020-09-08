using BL.Infrastructure;
using BL.Secuirty;
using BL.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using OfficeOpenXml;
using sal7lyAdmin.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using static BL.SharedCS.Enumrations;





namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class ItemsController : Controller
    {
        private readonly IUnitOfWork _uow;
       
        private readonly ISecurity _Security;
        private readonly IHostingEnvironment _env;

        public ItemsController(IUnitOfWork uow,  ISecurity Security, IHostingEnvironment env) { _uow = uow;  _Security = Security; _env = env; }

        #region Items

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Items, EN_Permissions.View))
            { return Redirect("/Home/Index"); }

            var result = _uow.ItemsRepository.GetAll().Include(ent => ent.Service).OrderByDescending(ent => ent.CreationDate).ToHashSet();
            FillFormDropdowns(new Item());
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result= _uow.ItemsRepository.GetMany(ent => !ent.IsDeleted).Include(ent=>ent.Service).ToHashSet();
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
            #region Filter by service
            if (!string.IsNullOrEmpty(formData["ServiceId"]))
            {
                result = result.Where(tt => tt.ServiceId==Int64.Parse(formData["ServiceId"].ToString())).ToHashSet();
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

        public IActionResult OpenForm(long? id)
        {
            var entity = new Item();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Items, EN_Permissions.Edit))
                { return Redirect("/Home/Index"); }

                entity = _uow.ItemsRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Items, EN_Permissions.Create))
                { return Redirect("/Home/Index"); }
            }
            FillFormDropdowns(entity);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Item entity, IFormCollection form)
        {
            string strReturnMsg = "error";
           
            try
            {
                //Re-ValidateModel
                ModelState.Clear();
                TryValidateModel(entity);
                if (ModelState.IsValid)
                {
                    long taxId = 0;

                    #region item
                    if (entity.Id == default)
                    {
                        entity.Code = UIHelper.GeneratCode(EN_Screens.Items, _uow);
                        if (string.IsNullOrEmpty(entity.ArabicName)) { entity.ArabicName = entity.ArabicName; }

                        entity.CreationDate = DateTime.Now;
                        entity.CreatedBy = AppSession.CurrentUser.Id;
                        _uow.ItemsRepository.Add(entity);
                        _uow.Save();
                    }
                    else
                    {
                        entity.ModificationDate = DateTime.Now;
                        entity.ModifiedBy = AppSession.CurrentUser.Id;

                        _uow.ItemsRepository.Update(entity);
                        _uow.Save();
                    }
                    #endregion

                    strReturnMsg = "success";
                }
            }
            catch (Exception ex) { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Items, EN_Permissions.Delete))
            { return Redirect("/Home/Index"); }
            string status = "success";
            string errorMessage = "";

            try
            {
                var dbObj = _uow.ItemsRepository.GetById(id);
                if (dbObj == null)
                {
                    status = "error";
                    errorMessage = "Delete Error Msg";
                }
                else
                {
                  
                    dbObj.IsDeleted = true;
                    dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                    dbObj.ModificationDate = DateTime.Now;
                    _uow.ItemsRepository.Update(dbObj);
                    _uow.Save();
                }
            }
            catch (Exception)
            {
                status = "error";
                errorMessage = "Delete Error Msg";
            }
            return Json(new { status, ErrorMessage = errorMessage });
        }

        #endregion


        #region Copy Item

        [HttpPost]
        public IActionResult SaveDuplicateItem(long itemId, string itemName)
        {
            string strReturnMsg = "error";
            var itemEntity = _uow.ItemsRepository.Get(ent => ent.Id == itemId && !ent.IsDeleted && ent.IsActive);
            try
            {
                if (itemEntity != null)
                {
                    #region item
                    itemEntity.Id = default;
                    itemEntity.Code = UIHelper.GeneratCode(EN_Screens.Items, _uow);
                    itemEntity.ArabicName = itemName;
                    itemEntity.EnglishName = itemName;
                    itemEntity.CreationDate = DateTime.Now;
                    itemEntity.CreatedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(itemEntity);
                    if (ModelState.IsValid)
                    { _uow.ItemsRepository.Add(itemEntity); }
                    else
                    {
                        return Json(new { status = strReturnMsg });
                    }
                    _uow.Save();
                    #endregion


                    strReturnMsg = "success";
                }

            }
            catch (Exception ex) { }
            return Json(new { status = strReturnMsg });
        }

        #endregion

        #region Item Images

        public IActionResult SaveItemImage(long id, string deletedids)
        {
            bool message = false;
            var webRoot = _env.WebRootPath;
            var uploadsFolder = Path.Combine(webRoot, AppSession.ItemsUploads);
            try
            {
                if (!string.IsNullOrEmpty(deletedids))
                {
                    var ids = deletedids.Split(',').Select(ent => long.Parse(ent)).ToHashSet();
                    var imagesObj = _uow.ItemImagesRepository.GetMany(ent => ent.IsActive && ids.Contains(ent.Id)).ToHashSet();
                    foreach (var image in imagesObj)
                    {
                        var imagesObjPath = "";
                        var filePath = "";
                        imagesObjPath = image.ImagePath;
                        //Delete old file
                        if (!string.IsNullOrEmpty(imagesObjPath))
                        {
                            filePath = Path.Combine(uploadsFolder, imagesObjPath);
                            if (System.IO.File.Exists(filePath))
                            { System.IO.File.Delete(filePath); }
                        }
                        _uow.ItemImagesRepository.Delete(image.Id);
                    }
                    _uow.Save();
                }
                var files = HttpContext.Request.Form.Files;
                if (files != null && files.Count > 0)
                {
                    if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var filePath = "";
                            //Save new file
                            var fileName = Guid.NewGuid() + file.FileName;
                            filePath = Path.Combine(uploadsFolder, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            { file.CopyTo(fileStream); }
                            //Update record with filename
                            var ItemImage = new ItemImage();
                            ItemImage.ItemId = id;
                            ItemImage.ImagePath = fileName;
                            _uow.ItemImagesRepository.Add(ItemImage);

                        }
                    }
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

        #endregion


        #region Shared Methods

        private void FillFormDropdowns(Item entity)
        {
            var cats = _uow.ServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text =  ent.ArabicName }).ToHashSet();
            ViewBag.ServiceId = new SelectList(cats, "Value", "Text", entity.ServiceId);
        }
        #endregion
    }
}