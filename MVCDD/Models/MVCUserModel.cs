using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDD.Models
{
    public class MVCUserModel
    {
        [Required(ErrorMessage = "Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        public string password { get; set; }

        [Required(ErrorMessage = "Required")]
        public string confirmPassword { get; set; }
        public string usertype { get; set; }
    }
}