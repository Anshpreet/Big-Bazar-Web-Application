using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BigBazarWebApplication.Models
{
    public class UserModel
    {
        [Display(Name ="User Id")]
        public int UserId { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }
        public bool Role { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
