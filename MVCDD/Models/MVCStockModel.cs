using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDD.Models
{
    public class MVCStockModel
    {
        public int stockID { get; set; }
        public string username { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> quantity { get; set; }
    }
}