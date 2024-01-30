using MVCDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCDD.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            IEnumerable<MVCStockModel> stockList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Stock").Result;
            stockList = response.Content.ReadAsAsync<IEnumerable<MVCStockModel>>().Result;
            return View(stockList);
        }
    }
}