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
            ViewBag.Administrator = false;
            if (SiteSession.Current.User != null)
            {
                ViewBag.Administrator = SiteSession.Current.User.Name.ToUpper().Contains("ADMIN");
            }

            return View(new core.Controllers.ClientController().GetList(SiteSession.Current.User));
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
                    message = "Mensagem enviada com sucesso!",
                    data = Url.Action("Index", "Home")
                };

                return Json(ret);
            }
            catch (Exception)
            {
                var ret = new JsonAction { success = false, message = "Ocorreu um erro ao tentar enviar a mensagem!" };
                return Json(ret);
            }
        }
    }
}