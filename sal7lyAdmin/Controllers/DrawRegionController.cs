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
using Model.CustomModels;
using sal7lyAdmin.Services;
using static BL.SharedCS.Enumrations;

namespace sal7lyAdmin.Controllers
{
    [AuthorizeLogIn]
    public class DrawRegionController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurity _security;

        public DrawRegionController(IUnitOfWork uow, ISecurity security) { _uow = uow; _security = security; }

        public IActionResult Index()
        {
            if (!UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Regions, EN_Permissions.View))
            { return Redirect("/Home"); }

            var result = _uow.RegionRepository.GetAll().OrderByDescending(ent => ent.CreationDate).ToHashSet();
            return View(result);
        }

        [HttpPost]
        public IActionResult Search(IFormCollection formData, string searchTerm = "", string searchKey = "")
        {
            var result = _uow.RegionRepository.GetAll();

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
                result = result.Where(tt => !string.IsNullOrEmpty(tt.ArabicName) && tt.ArabicName.ToLower().Contains(formData["EnglishName"].ToString().ToLower().Trim()));
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
                        result = result.Where(ent => !string.IsNullOrEmpty(ent.ArabicName) && ent.ArabicName.ToLower().Contains(searchTerm));
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

        public IActionResult OpenForm()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Save(List<RegionViewModel> RegionsData)
        {
            string strReturnMsg = "error";
            try
            {

                foreach (var item in RegionsData)
                {
                    Region entity = new Region();

                    entity.ArabicName =item.ArabicName;
                    entity.ArabicName =item.ArabicName;
                    entity.IsCycle =item.IsCycle;
                    entity.ArabicName =item.ArabicName;
                    entity.CreationDate = DateTime.Now;
                    entity.CreatedBy = AppSession.CurrentUser.Id;
                    _uow.RegionRepository.Add(entity);
                    _uow.Save();
                    if (item.LstOfPoints != null)
                    {
                        for (int i = 0; i < item.LstOfPoints.Count(); i++)
                        {


                            RegionPoints regionPoints = new RegionPoints();
                            regionPoints.RegionId = entity.Id;
                            regionPoints.lat = item.LstOfPoints[i].lat;
                            regionPoints.lng = item.LstOfPoints[i].lng;
                            _uow.RegionPointsRepository.Add(regionPoints);

                        }
                    }

                }
                _uow.Save();
                strReturnMsg = "success";
            }
            catch (Exception ex)
            { }
            return Json(new { status = strReturnMsg });
        }

 
     }
}