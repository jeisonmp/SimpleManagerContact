using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SimpleManagerContact.portal.Filters;
using SimpleManagerContact.portal.Exceptions;
using SimpleManagerContact.portal.Models;
using SimpleManagerContact.portal.Utils;
using SimpleManagerContact.model;

namespace SimpleManagerContact.portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SessionExpired()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [Session]
        public ActionResult Index()
        {
            ViewBag.Administrator = SiteSession.Current.Administrator;
            ViewBag.Cities = this.GetCities();
            ViewBag.Classifications = this.GetClassifications();
            ViewBag.Sellers = this.GetSellers();

            return View(new core.Controllers.ClientController().GetList(SiteSession.Current.User));
        }

        private IEnumerable<SelectListItem> GetCities()
        {
            IEnumerable<SelectListItem> list = null;
            var cities = new core.Controllers.CityController().GetList();
            cities.Insert(0, new City() { CityName = "Select" });

            if (cities.Count > 0)
            {
                list = cities
                    .Select(o => new SelectListItem
                    {
                        Value = o.CityId.ToString(),
                        Text = o.CityName,
                        Selected = cities.First().CityId == o.CityId
                    });
            }

            return list;
        }

        private IEnumerable<SelectListItem> GetClassifications()
        {
            IEnumerable<SelectListItem> list = null;
            var items = new core.Controllers.ClassificationController().GetList();
            items.Insert(0, new Classification() { ClassificationName = "Select" });

            if (items.Count > 0)
            {
                list = items
                    .Select(o => new SelectListItem
                    {
                        Value = o.ClassificationId.ToString(),
                        Text = o.ClassificationName
                    });
            }

            return list;
        }

        private IEnumerable<SelectListItem> GetSellers()
        {
            IEnumerable<SelectListItem> list = null;
            var items = new core.Controllers.UserController().GetList();
            items.Insert(0, new User() { Name = "Select" });

            if (items.Count > 0)
            {
                list = items
                    .Select(o => new SelectListItem
                    {
                        Value = o.UserId.ToString(),
                        Text = o.Name
                    });
            }

            return list;
        }

        public ActionResult Logoff()
        {
            if (SiteSession.IsValid())
            {
                SiteSession.Current.Clear();
                Session.Abandon();
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult UpdateRegions(Guid? CityId = null)
        {
            try
            {
                var regions = new core.Controllers.RegionController().GetList(CityId);
                regions.Insert(0, new Region() { RegionName = "Select" });

                IEnumerable<SelectListItem> list = regions
                .Select(o => new SelectListItem
                {
                    Value = o.RegionId.ToString(),
                    Text = o.RegionName
                });

                var ret = new JsonAction
                {
                    success = true,
                    data = list
                };

                ViewBag.Regions = list;

                return Json(ret);
            }
            catch (Exception)
            {
                var ret = new JsonAction { success = false, message = "Something's Wrong" };
                return Json(ret);
            }
        }
        
        [HttpPost]
        public ActionResult CustomerFilter(CustomerFields fields, User user = null)
        {
            ViewBag.Administrator = SiteSession.Current.Administrator;

            var clients = new core.Controllers.ClientController().Search(fields, user == null ? SiteSession.Current.User : user);
            return PartialView("_customerList", clients);
        }
    }
}