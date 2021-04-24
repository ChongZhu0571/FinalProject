using System;
using SuperDamp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database = SuperDamp.Models.Database;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SuperDamp.Controllers
{
    public class ProductController : Controller
    {
        Database db = new Database();
        // GET: Product
        public ActionResult Index()
        {
            modelAdaptor modelAdaptor = new modelAdaptor();
            modelAdaptor.products = db.Products.ToList();
            return View(modelAdaptor);
        }
    }
}