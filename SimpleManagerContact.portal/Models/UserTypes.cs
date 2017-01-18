using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleManagerContact.portal.Models
{
    public partial class UserTypes
    {
        public enum Role
        {
            Admin = 0,
            Seller = 1
        }
    }
}