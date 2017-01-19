using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleManagerContact.portal.Exceptions;
using SimpleManagerContact.portal.Models;
using SimpleManagerContact.portal.Utils;
using SimpleManagerContact.model;

namespace SimpleManagerContact.portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authentication(string username, string password)
        {
            Session.Clear();
            password = Utils.Encryption.getHashSha256(password);
            
            try
            {
                User user = new Auth().Authentication(username, password);

                SiteSession.Current.User = user;
                SiteSession.Current.Login = user.Email;
                SiteSession.Current.Password = user.Password;

                JsonAction jsonObject = new JsonAction();
                jsonObject.success = true;
                if (user.Name.ToUpper().Contains("SELLER") ||  /// == (int)UserTypes.Role.Seller)
                    user.Name.ToUpper().Contains("ADMIN"))     /// == (int)UserTypes.Role.Admin)
                {
                    jsonObject.data = Url.Action("Index", "Home");
                }

                return Json(jsonObject);
            }
            catch (Exception ex)
            {
                JsonAction jsonObject = new JsonAction();
                jsonObject.success = false;

                if (ex.GetType() == typeof(WrongUserPasswordException))
                {
                    jsonObject.message = Resources.Resource.ResourceManager.GetString("Error_Wrong_UserPwd");
                }
                else
                {
                    jsonObject.message = "Something's Wrong";
                }

                return Json(jsonObject);
            }
        }
    }
}