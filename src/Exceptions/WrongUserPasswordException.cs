using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleManagerContact.portal.Exceptions
{
    public class WrongUserPasswordException : LoggableException
    {
        public static String code = "0001";

        public WrongUserPasswordException()
        {
            this._Message = "The e-mail and/ or password entered is invalid.Please try again.";
        }
    }
}