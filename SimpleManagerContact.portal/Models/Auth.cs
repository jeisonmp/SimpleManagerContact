using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.portal.Exceptions;
using SimpleManagerContact.model;

namespace SimpleManagerContact.portal.Models
{
    public class Auth
    {
        public Auth() { }

        public User Authentication(string username, string password)
        {
            User logged = new core.Controllers.UserController().Logged(username, password);

            if (logged == null)
            {
                throw new WrongUserPasswordException();
            }
            
            return logged;
        }
    }
}