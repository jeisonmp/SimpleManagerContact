using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimpleManagerContact.portal.Models
{
    public class Login
    {
        [Display(ResourceType = typeof(Localization.Resource), Name = "Label_UserName")]
        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Error_Username_Required")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Localization.Resource), Name = "Label_Password")]
        [Required(ErrorMessageResourceType = typeof(Localization.Resource), ErrorMessageResourceName = "Error_Password_Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}