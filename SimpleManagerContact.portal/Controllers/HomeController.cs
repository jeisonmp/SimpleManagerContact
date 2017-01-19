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
            ViewBag.Administrator = this.GetAdministrator();
            ViewBag.Cities = this.GetCities();

            return View(new core.Controllers.ClientController().GetList(SiteSession.Current.User));
        }

        private bool GetAdministrator()
        {
            var val = false;
            if (SiteSession.Current.User != null)
            {
                val = SiteSession.Current.User.Name.ToUpper().Contains("ADMIN");
            }

            return val;
        }

        private IEnumerable<SelectListItem> GetCities()
        {
            IEnumerable<SelectListItem> list = null;
            var cities = new core.Controllers.CityController().GetList();
            cities.Insert(0, new City() { CityName = "All" });

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
        public ActionResult CustomerFilter(Client client)
        {
            try
            {
                var ret = new JsonAction
                {
                    success = true,
                    message = "Successfully",
                    data = Url.Action("Index", "Home")
                };

                return Json(ret);
            }
            catch (Exception)
            {
                var ret = new JsonAction { success = false, message = "Something's Wrong" };
                return Json(ret);
            }
        }

        [HttpPost]
        public ActionResult UpdateRegions(Guid? CityId = null)
        {
            try
            {
                var regions = new core.Controllers.RegionController().GetList(CityId);
                regions.Insert(0, new Region() { RegionName = "All" });

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
    }
}