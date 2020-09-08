using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BL.Infrastructure;
using BL.Security;
using BL.Secuirty;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;
using System.Threading.Tasks;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class OffersController : Controller
    {
        private readonly IUnitOfWork _uow;
       
        private readonly ISecurity _Security;
        private readonly IHostingEnvironment _env;

        public OffersController(IUnitOfWork uow,  ISecurity Security, IHostingEnvironment env) { _uow = uow;  _Security = Security; _env = env; }

        public IActionResult Index()
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Offers, EN_Permissions.View))
            { return Redirect("/Home/Index"); }
            var result = new HashSet<Offer>();
           
                result = _uow.OffersRepository.GetMany(ent => !ent.IsDeleted).ToHashSet();
            
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            string source = !string.IsNullOrEmpty(formData["source"]) ? formData["source"].ToString() : "";
            if (!string.IsNullOrEmpty(searchTerm)) { searchTerm = searchTerm.Trim().ToLower(); }
            //var result = _uow.OffersRepository.GetMany(ent => !ent.IsDeleted).ToHashSet();
            var result = _uow.OffersRepository.GetMany(ent => !ent.IsDeleted)
                .OrderByDescending(ent => ent.CreationDate).ToHashSet();

            if (searchKey == "Active")
            { result = result.Where(ent => ent.IsActive).ToHashSet(); }
            if (formData.ContainsKey("Active"))
            {
                if (bool.Parse(formData["Active"]))
                { result = result.Where(ent => ent.IsActive).ToHashSet(); }
            }
            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrEmpty(searchKey))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchTerm))
                    {
                        switch (searchKey.ToLower())
                        {
                            case "arabicname":
                                result = result.Where(ent => !string.IsNullOrEmpty(ent.ArabicName) && ent.ArabicName.ToLower().Contains(searchTerm)).ToHashSet();
                                break;
                            case "englishname":
                                result = result.Where(ent => !string.IsNullOrEmpty(ent.ArabicName) && ent.ArabicName.ToLower().Contains(searchTerm)).ToHashSet();
                                break;
                            case "code":
                                result = result.Where(ent => !string.IsNullOrEmpty(ent.Code) && ent.Code.ToLower().Contains(searchTerm.Trim())).ToHashSet();
                                break;
                            case "active":
                                result = result.Where(tt =>
             (tt.ArabicName.ToLower().Contains(searchTerm.ToLower().Trim()))

            || (string.IsNullOrEmpty(tt.ArabicName) ? false : tt.ArabicName.ToLower().Contains(searchTerm.ToLower().Trim()))

             || (string.IsNullOrEmpty(tt.Code) ? false : tt.Code.ToLower().Contains(searchTerm.ToLower().Trim()))
            ).ToHashSet();
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    result = result.Where(tt =>
             (tt.ArabicName.ToLower().Contains(searchTerm.ToLower()))

             || (tt.ArabicName == null ? false : (tt.ArabicName.ToLower().Contains(searchTerm.ToLower()) || tt.ArabicName.ToLower().Contains(searchTerm.ToLower())))).ToHashSet();
                }
            }
            else if (result != null && result.Count() > 0)
            {
                #region Filter by ArabicName
                if (!string.IsNullOrEmpty(formData["ArabicName"]))
                {
                    result = result.Where(tt => !string.IsNullOrEmpty(tt.ArabicName) && ((string.IsNullOrEmpty(formData["ArabicName"])) || tt.ArabicName.ToLower().Contains(formData["ArabicName"].ToString().ToLower().Trim()))).ToHashSet();
                }
                #endregion

                #region Filter by EnglishName
                if (!string.IsNullOrEmpty(formData["EnglishName"]))
                {
                    result = result.Where(tt => !string.IsNullOrEmpty(tt.ArabicName) && ((string.IsNullOrEmpty(formData["EnglishName"])) || tt.ArabicName.ToLower().Contains(formData["EnglishName"].ToString().ToLower().Trim()))).ToHashSet();
                }
                #endregion

                #region Filter by Code
                if (!string.IsNullOrEmpty(formData["Code"]))
                {
                    result = result.Where(tt => !string.IsNullOrEmpty(tt.Code) && ((string.IsNullOrEmpty(formData["Code"])) || tt.Code.ToLower().Contains(formData["Code"].ToString().ToLower().Trim()))).ToHashSet();
                }
                #endregion
            }
            result = result.OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return PartialView(source == "OffersStock" ? "_OffersStockList" : "_List", result);
        }

        public IActionResult OpenForm(long? id)
        {
            var entity = new Offer();
            //entity.From = DateTime.Now;
            //entity.To = DateTime.Now.AddDays(1);
            entity.ImagePath = AppSession.OfferDefaultImage;
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Offers, EN_Permissions.Edit))
                { return Redirect("/Home/Index"); }

                entity = _uow.OffersRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
                var offersNum = _uow.OffersRepository.GetMany(ent=>ent.IsActive && !ent.IsDeleted && ent.Id==id).Count();

                ViewBag.offersNum = offersNum;
            }
            else
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Offers, EN_Permissions.Create))
                { return Redirect("/Home/Index"); }
                var offersNum = _uow.OffersRepository.GetAll().Count();

                if (offersNum >= 4)
                    entity.IsActive = false;
                ViewBag.offersNum = offersNum;
            }
            FillFormDropdowns(entity);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Offer entity, HashSet<OfferActiveDate> activeDates, IFormCollection form)
        {
            string strReturnMsg = "error";
            try
            {
                ModelState.Clear();
                TryValidateModel(entity);
                if (ModelState.IsValid)
                {
                    if (entity.Id == default)
                    {
                        entity.Code = UIHelper.GeneratCode(EN_Screens.Offers, _uow);
                        if (string.IsNullOrEmpty(entity.ArabicName)) { entity.ArabicName = entity.ArabicName; }
                        entity.CreationDate = DateTime.Now;
                        entity.CreatedBy = AppSession.CurrentUser.Id;

                        //Re-ValidateModel
                        _uow.OffersRepository.Add(entity);

                    }
                    else
                    {
                        entity.ModificationDate = DateTime.Now;
                        entity.ModifiedBy = AppSession.CurrentUser.Id;

                        //Re-ValidateModel
                        _uow.OffersRepository.Update(entity);
                    }
                    _uow.Save();

                    #region Save Active Dates
                    {
                        var selectedActiveDatesIds = activeDates.Select(ent => ent.Id).ToHashSet();
                        var deletedItems = _uow.OfferActiveDatesRepository.GetMany(ent => ent.OfferId == entity.Id && !selectedActiveDatesIds.Contains(ent.Id)).Select(ent => ent.Id).ToHashSet();
                        if (deletedItems.Count() > 0)
                        {
                            _uow.ExecuteSqlCommand("DELETE FROM OfferActiveDates WHERE Id IN(" + string.Join(",", deletedItems) + ")");
                        }

                        foreach (var activeDate in activeDates)
                        {
                            if (!string.IsNullOrEmpty(activeDate.StrStartDate))
                            { activeDate.StartDate = DateTime.ParseExact(activeDate.StrStartDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture); }
                            if (!string.IsNullOrEmpty(activeDate.StrEndDate))
                            { activeDate.EndDate = DateTime.ParseExact(activeDate.StrEndDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture); }

                            if (activeDate.Id == default)
                            {
                                activeDate.OfferId = entity.Id;
                                _uow.OfferActiveDatesRepository.Add(activeDate);
                            }
                            else
                            {
                                _uow.OfferActiveDatesRepository.Update(activeDate);
                            }
                        }
                        _uow.Save();
                    }
                    #endregion


                    #region Save Offer Items

                    if (!string.IsNullOrEmpty(form["entity[OfferItems]"]))
                    {
                        var selectedItems = form["entity[OfferItems]"].ToString().Split(",").Select(ent => long.Parse(ent)).ToHashSet();
                        foreach (var selectedItemId in selectedItems)
                        {
                            var dbObj = _uow.OfferItemsRepository.Get(ent => ent.ItemId == selectedItemId && ent.OfferId == entity.Id);
                            if (dbObj == null)
                            {
                                var obj = new OfferItem();
                                obj.OfferId = entity.Id;
                                obj.ItemId = selectedItemId;
                                _uow.OfferItemsRepository.Add(obj);
                            }
                        }

                        var deletedOfferItems = _uow.OfferItemsRepository.GetMany(ent => ent.OfferId == entity.Id && !selectedItems.Contains(ent.ItemId)).Select(ent => ent.Id).ToHashSet();
                        if (deletedOfferItems.Count() > 0)
                        {
                            _uow.ExecuteSqlCommand("DELETE FROM OfferItems WHERE Id IN(" + string.Join(",", deletedOfferItems) + ")");
                        }
                        _uow.Save();
                    }
                    else
                    { _uow.ExecuteSqlCommand("DELETE FROM OfferItems WHERE OfferId=" + entity.Id); }

                    #endregion


                    strReturnMsg = "success";
                }

            }
            catch (Exception) { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public IActionResult Delete(long id)
        {
            //Check User Permission For this Page
            if (!UserAccountMannager.HasPermission(_uow, _Security, AppSession.CurrentUser.Id, EN_Screens.Offers, EN_Permissions.Delete))
            { return Redirect("/Home/Index"); }
            string status = "success";
            string errorMessage = "";

            try
            {
                var dbObj = _uow.OffersRepository.GetById(id);
                var invoices = 1;
                //invoices = _uow.InvoicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Count();
                if (dbObj == null)
                {
                    status = "error";
                    errorMessage = "Delete Error Msg";
                }
                else if (invoices > 0)
                {
                    status = "error";
                    errorMessage = "This Offer is related to invoices";
                }
                else
                {
                    dbObj.IsDeleted = true;
                    dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                    dbObj.ModificationDate = DateTime.Now;
                    _uow.OffersRepository.Update(dbObj);
                    _uow.Save();
                }
            }
            catch (Exception)
            {
                status = "error";
                errorMessage = "Delete Error Msg";
            }
            return Json(new { status, errorMessage });
        }

        public void FillFormDropdowns(Offer entity)
        {
            var cats = _uow.ServicesRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text =  ent.ArabicName }).ToHashSet();
            ViewBag.cats = new SelectList(cats, "Value", "Text", entity.MainCategoryId);

            var items = _uow.ItemsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted).Select(ent => new SelectListItem { Value = ent.Id.ToString(), Text =  ent.ArabicName }).ToHashSet();

          
            var OfferItemsIds = _uow.OfferItemsRepository.GetMany(ent => ent.OfferId == entity.Id).Select(ent => ent.ItemId).ToHashSet();
            ViewBag.items = new MultiSelectList(items, "Value", "Text", OfferItemsIds);
        }

        public IActionResult FillItems(long id)
        {
            //var subCatsIds = _uow.CategoriesSubCategoryRepository.GetMany(ent => ent.CategoryId == id).Select(ent => ent.SubCategoryId).ToHashSet();
            var result = _uow.ItemsRepository.GetMany(ent => ent.IsActive && !ent.IsDeleted && ent.ServiceId == id).Select(ent => new { id = ent.Id, text =  ent.ArabicName }).ToHashSet();
            return Json(result);
        }

        #region Image

        public async Task<IActionResult> SaveOfferImage(long id)
        {
            bool message = true;
            try
            {
                var offer = _uow.OffersRepository.GetById(id);
                var file = HttpContext.Request.Form.Files.FirstOrDefault();
                var fileName = AppSession.OfferDefaultImage;
                if (file != null && file.Length > 0)
                {
                    var webRoot = _env.WebRootPath;
                    var uploadsFolder = Path.Combine(webRoot, AppSession.OfferImagesUploads);
                    fileName = await Images.SaveFile(uploadsFolder, file, lastName: offer.ImagePath, defaultName: AppSession.OfferDefaultImage);
                    if (fileName == "" || fileName == null)
                    {
                        fileName = AppSession.OfferDefaultImage;
                        message = false;
                    }

                }
                offer.ImagePath = fileName;
                _uow.OffersRepository.Update(offer);
                _uow.Save();
            }
            catch (Exception ex)
            {
                message = false;
            }
            return Json(new { success = message });
        }

        public IActionResult DeleteOfferImage(long id)
        {
            try
            {
                var webRoot = _env.WebRootPath;
                var uploadsFolder = Path.Combine(webRoot, AppSession.OfferImagesUploads);
                var dbObj = _uow.OffersRepository.GetById(id);
                var filePath = "";
                var fileName = AppSession.OfferDefaultImage;
                //Delete old file
                if (!string.IsNullOrEmpty(dbObj.ImagePath) && dbObj.ImagePath != AppSession.OfferDefaultImage)
                {
                    filePath = Path.Combine(uploadsFolder, dbObj.ImagePath);
                    if (System.IO.File.Exists(filePath))
                    { System.IO.File.Delete(filePath); }

                }
                //Update record with filename
                dbObj.ImagePath = fileName;
                _uow.OffersRepository.Update(dbObj);
                _uow.Save();
                return Json(new { success = true });
            }
            catch (Exception) { }
            return Json(new { success = false });
            
        }

        #endregion
    }
}