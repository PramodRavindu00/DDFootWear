using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDD.Models
{
    public class MVCProductModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Required")]
        public Nullable<decimal> price { get; set; }
    }
}