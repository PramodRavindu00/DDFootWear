using MVCDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCDD.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<MVCProductModel> productList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product").Result;
            productList = response.Content.ReadAsAsync < IEnumerable<MVCProductModel>>().Result;
            return View(productList);
        }

        public ActionResult AddOrEdit(int id=0) 
        {
            if (id == 0)
                return View(new MVCProductModel());
            else 
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCProductModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MVCProductModel product)
        {
            if (product.ProductID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Product", product).Result;
                TempData["SuccessMessage"] = "New Product Added Successfully";
            }
            else 
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Product/"+ product.ProductID,product).Result;
                TempData["SuccessMessage"] = "Product details updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Product/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}