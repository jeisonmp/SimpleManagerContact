using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.portal.Utils
{
    /// <summary>
    /// Classe responsável por armazenar os dados da sessão do usuário
    /// </summary>
    public class SiteSession
    {
        public static SiteSession Current
        {
            get
            {
                if (HttpContext.Current.Session["__SiteSession__"] == null)
                    HttpContext.Current.Session["__SiteSession__"] = new SiteSession();

                return (SiteSession)HttpContext.Current.Session["__SiteSession__"];
            }
        }

        public SiteSession()
        {
            HttpContext.Current.Session.Timeout = 30;
        }


        public void Clear()
        {
            HttpContext.Current.Session["__SiteSession__"] = null;
        }


        public User User { get; set; }
        public string Login { get; set; }
        public String Password { get; set; }

        public static bool IsValid()
        {
            if (HttpContext.Current.Session["__SiteSession__"] == null)
                return false;
            else
                return true;
        }
    }
}