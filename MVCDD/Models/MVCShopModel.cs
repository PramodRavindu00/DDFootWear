using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDD.Models
{
    public class MVCShopModel
    {

        [Required(ErrorMessage = "Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Required")]
        public string usertype { get; set; }
        [Required(ErrorMessage = "Required")]
        public string shopname { get; set; }
        [Required(ErrorMessage = "Required")]
        public string location { get; set; }
    }
}