using MVCDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVCDD.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            IEnumerable<MVCShopModel> shopList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shop").Result;
            shopList = response.Content.ReadAsAsync<IEnumerable<MVCShopModel>>().Result;
            return View(shopList);

        }

        public ActionResult AddNewShop(MVCShopModel shop)
        {
            if (ModelState.IsValid)
            {

                MVCShopModel shopModel = new MVCShopModel
                {
                    username = shop.username,
                    password = shop.password,
                    usertype = shop.usertype,
                    shopname = shop.shopname,
                    location = shop.location
                };

                if (ShopExists(shop.username))
                {
                    TempData["ErrorMessage"] = "User name already taken. Please try with a different one.";
                    return View(shop);
                }



                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Shop", shopModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Shop Added Successfully";
                    return View(shop);
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to Add New Shop";
                    return View(shop);
                }
            }
            return View();
        }

        public ActionResult EditShop(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                TempData["UserNameToBeUpdate"] = username;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shop/" + username).Result;
                return View(response.Content.ReadAsAsync<MVCShopModel>().Result);

            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditShop(MVCShopModel shop)
        {
            string id = TempData["UserNameToBeUpdate"] as string;

            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Shop/" + id, shop).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Shop Details Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to Update Shop Details. Server returned: " + response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    TempData["ErrorMessage"] = "An error occurred during the update: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid model state. Please check your input.";
            }

            return View(shop);
        }


        private bool ShopExists(string checkingShopName)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shop").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<MVCShopModel> shopList;
                shopList = response.Content.ReadAsAsync<IEnumerable<MVCShopModel>>().Result;
                var shopnames = shopList.Select(shop => shop.username);
                if (shopnames.Contains(checkingShopName)) { return true; }
                else { return false; }
            }
            else
            {
                TempData["Alert"] = "Failed to retrive the data from database";
                return false;
            }
        }
    }
}