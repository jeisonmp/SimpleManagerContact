using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleManagerContact.portal.Exceptions
{
    public class LoggableException : Exception
    {
        public override string Message
        {
            get { return _Message; }
        }

        public override string ToString()
        {
            return this.Message;
        }

        protected string _Message;


        public LoggableException()
        {

        }
    }
}