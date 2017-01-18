using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleManagerContact.portal.Models
{
    public class JsonAction
    {
        public bool success { get; set; }
        public bool session { get; set; } = true;
        public string message { get; set; }
        public Object data { get; set; }
        public bool NoMoreData { get; set; }

    }
}