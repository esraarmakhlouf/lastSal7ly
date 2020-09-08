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
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _uow;
        
        private readonly ISecurity _security;

        public CountryController(IUnitOfWork uow, ISecurity security)
        { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Country, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.CountryRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.CountryRepository.GetAll();
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
            return PartialView("_List", result.OrderByDescending(ent => ent.CreationDate).ToHashSet());
        }

        public IActionResult OpenForm(long? id)
        {
            var entity = new Country();
            if (id != null)
            {
                //Check User Permission For this Page
                if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Country, EN_Permissions.Edit))
                { return Redirect("/Country/Index"); }

                entity = _uow.CountryRepository.GetById(id);
                if (entity == null || entity.IsDeleted)
                { return NotFound(); }
            }
           

            return View(entity);
        }

        [HttpPost]
        public IActionResult Save(Country entity, HashSet<City> cities)
        {
            string strReturnMsg = "error";
            try
            {
                if (entity.Id == default)
                {
                    entity.Code = UIHelper.GeneratCode(EN_Screens.Country, _uow);
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.CountryRepository.Add(entity); }
                }
                else
                {
                    entity.ModificationDate = DateTime.Now;
                    entity.ModifiedBy = AppSession.CurrentUser.Id;
                    //Re-ValidateModel
                    ModelState.Clear();
                    TryValidateModel(entity);
                    if (ModelState.IsValid)
                    { _uow.CountryRepository.Update(entity); }
                }
                _uow.Save();
                #region Save Cities
                if (cities != null && cities.Count() > 0)
                {
                    var addedUpdatedCities = cities.Where(ent => !ent.IsDeleted).ToHashSet();
                    foreach (var city in addedUpdatedCities)
                    {
                        var districts = city.Districts;
                        if (city.Id == default)
                        {
                            if (string.IsNullOrEmpty(city.EnglishName))
                            {
                                city.EnglishName = city.ArabicName;
                            }
                            city.CountryID = entity.Id;
                            city.CreatedBy = AppSession.CurrentUser.Id;
                            city.CreationDate = DateTime.Now;
                            city.Districts = null;
                            _uow.CityRepository.Add(city);
                        }
                        else
                        {
                            city.ModifiedBy = AppSession.CurrentUser.Id;
                            city.ModificationDate = DateTime.Now;
                            city.Districts = null;
                            _uow.CityRepository.Update(city);
                        }
                        _uow.Save();
                        if (districts != null && districts.Count() > 0)
                        {
                            #region Save Districts
                            var addedUpdatedDistricts = districts.Where(ent => !ent.IsDeleted).ToHashSet();
                            foreach (var district in addedUpdatedDistricts)
                            {
                                if (district.Id == default)
                                {
                                    if (string.IsNullOrEmpty(district.EnglishName))
                                    {
                                        district.EnglishName = district.ArabicName;
                                    }
                                    district.CreatedBy = AppSession.CurrentUser.Id;
                                    district.CreationDate = DateTime.Now;
                                    district.CityID = city.Id;
                                    _uow.DistrictRepository.Add(district);
                                }
                                else
                                {
                                    district.ModifiedBy = AppSession.CurrentUser.Id;
                                    district.ModificationDate = DateTime.Now;
                                    _uow.DistrictRepository.Update(district);
                                }
                            }
                            var deletedDistrictsIds = districts.Where(ent => ent.IsDeleted).Select(ent => ent.Id).ToHashSet();
                            if (deletedDistrictsIds.Count() > 0)
                            {
                                _uow.ExecuteSqlCommand("UPDATE District " +
                                    "SET IsDeleted = 1 ,ModifiedBy = " + AppSession.CurrentUser.Id + " ,ModificationDate = '" + DateTime.Now + "' " +
                                    "Where (IsDeleted = 0 ) AND (Id IN(" + string.Join(",", deletedDistrictsIds) + "))");
                            }
                            _uow.Save();
                            #endregion
                        }
                    }
                    var deletedCitiesIds = cities.Where(ent => ent.IsDeleted).Select(ent => ent.Id).ToHashSet();
                    if (deletedCitiesIds.Count() > 0)
                    {
                        _uow.ExecuteSqlCommand("UPDATE City " +
                            "SET IsDeleted = 1 ,ModifiedBy = " + AppSession.CurrentUser.Id + " ,ModificationDate = '" + DateTime.Now + "' " +
                            "Where (IsDeleted = 0 ) AND (Id IN(" + string.Join(",", deletedCitiesIds) + "))");

                        _uow.ExecuteSqlCommand("UPDATE District " +
                            "SET IsDeleted = 1  ,ModifiedBy = " + AppSession.CurrentUser.Id + " ,ModificationDate = '" + DateTime.Now + "' " +
                            "Where (IsDeleted = 0 ) AND (CityId IN(" + string.Join(",", deletedCitiesIds) + "))");
                    }
                    _uow.Save();

                }
                #endregion
                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            return Json(new { id = entity.Id, status = strReturnMsg });
        }

        public IActionResult Delete(long id)
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Country, EN_Permissions.Delete))
            { return Redirect("/Home"); }

            string status = "success";
            string errorMessage = "";
            try
            {
                var dbObj = _uow.CountryRepository.GetById(id);
                if (dbObj == null)
                {
                    status = "error";
                    errorMessage = "Delete Error message";
                }
                else
                {
                    _uow.ExecuteSqlCommand("UPDATE district " +
                        "SET IsDeleted = 1 ,ModifiedBy = " + AppSession.CurrentUser.Id + " ,ModificationDate = '" + DateTime.Now + "' " +
                        "From district inner join City on City.Id = district.CityId Where (district.IsDeleted = 0 ) AND (City.CountryId =" + id.ToString() + ")");
                    _uow.ExecuteSqlCommand("UPDATE City " +
                        "SET IsDeleted = 1 ,ModifiedBy = " + AppSession.CurrentUser.Id + " ,ModificationDate = '" + DateTime.Now + "' " +
                        "Where (IsDeleted = 0 ) AND (CountryId =" + id.ToString() + ")");
                    dbObj.IsDeleted = true;
                    dbObj.ModifiedBy = AppSession.CurrentUser.Id;
                    dbObj.ModificationDate = DateTime.Now;
                    _uow.CountryRepository.Update(dbObj);
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