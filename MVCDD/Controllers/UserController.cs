using MVCDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCDD.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<MVCUserModel> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<MVCUserModel>>().Result;
            return View(userList);
        }

        public ActionResult UserRegister(MVCUserModel user)
        {
            if (ModelState.IsValid)
            {

                MVCUserModel userModel = new MVCUserModel
                {
                    username = user.username,
                    password = user.password,
                    confirmPassword=user.confirmPassword,
                    usertype = "user"
                };

                if (UserExists(user.username))
                {
                    TempData["WarningMessage"] = "User name already taken. Please try with a different one.";
                    return View( user); 
                }

                if (user.password != user.confirmPassword) 
                {
                    TempData["ErrorMessage"] = "Password and confirm password shoud be same";
                    return View(user);
                }
               
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("User", userModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Registered Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to register the user";
                }
            }
            return View(user);
        }

      
        public ActionResult UserLogin(MVCUserModel user) {
            if (HttpContext.Request.HttpMethod == "POST")
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User").Result;

                if (response.IsSuccessStatusCode)
                {
                    var userList = response.Content.ReadAsAsync<List<MVCUserModel>>().Result;
                    var existingUser = userList.FirstOrDefault(u => u.username == user.username);

                    if (existingUser == null)
                    {
                        TempData["ErrorMessage"] = "Invalid login. User Name not Found";
                        TempData.Keep("ErrorMessage");
                    }
                    else
                    {
                        if (existingUser.password == user.password)
                        {

                            if (existingUser.usertype == "Admin") {
                                TempData["SuccessMessage"] = "Valid login as the Admin";
                                return RedirectToAction("Admin","Page");
                                

                            }
                            else if (existingUser.usertype == "user") {
                                TempData["SuccessMessage"] = "Valid login as a customer";
                                return RedirectToAction("Customer", "Page");
                               
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid login. Incorrect Password";
                        }
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Error retrieving data";
                }
            }
            return View();
          
        }

        public ActionResult Logout() {
          
           // Session.Abandon();
           
            TempData["SuccessMessage"] = "Logout Successful"; 
            
            
            return RedirectToAction("UserLogin", "User");
           
        }
        public ActionResult Delete(string id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("User/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "User Account Deactivated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to deativate the the user account";
            }
            
            return RedirectToAction("Index");
        }
        private bool UserExists(string checkingUserName)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<MVCUserModel> userList;
                userList = response.Content.ReadAsAsync<IEnumerable<MVCUserModel>>().Result;
                var usernames = userList.Select(user => user.username);
                if (usernames.Contains(checkingUserName)) { return true; }
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