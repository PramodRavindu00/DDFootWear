using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVCDD.Models;

namespace MVCDD.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Admin() {
        
            return View();
        }
        public ActionResult Shop()
        {
            
            return View();
        }
        public ActionResult Customer()
        {
        
            return View();
        }

        public ActionResult ShowProducts() 
        {
            IEnumerable<MVCProductModel> productList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product").Result;
            productList = response.Content.ReadAsAsync<IEnumerable<MVCProductModel>>().Result;
            return View(productList);
        }
        public ActionResult ShowShops()
        {
            IEnumerable<MVCShopModel> shopList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shop").Result;
            shopList = response.Content.ReadAsAsync<IEnumerable<MVCShopModel>>().Result;
            return View(shopList);
        }
    }
}