using SuperDamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperDamp.Controllers
{
    public class HomeController : Controller
    {
        public static List<Product> products = new List<Product>();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

       

        public ActionResult Checkout(Product product)
        { 
          

            return View("Checkout", products);
        }



    }
}