using MVCDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCDD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Product()
        {
            IEnumerable<MVCProductModel> productList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product").Result;
            productList = response.Content.ReadAsAsync<IEnumerable<MVCProductModel>>().Result;
            return View(productList);
        }
        public ActionResult Shop()
        {
            IEnumerable<MVCShopModel> shopList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shop").Result;
            shopList = response.Content.ReadAsAsync<IEnumerable<MVCShopModel>>().Result;
            return View(shopList);
        }
    }
}