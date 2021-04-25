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

        public ActionResult Create()
        {
            if (Session["adminName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Admin_Product", "Admin");
        }


        public ActionResult Delete(int id)
        {
            if (Session["adminName"] != null)
            {
                Product product = db.Products.Where(i => i.Id.Equals(id)).FirstOrDefault();
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Admin_Product", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult Update(int id)
        {
            if (Session["adminName"] != null)
            {
                Product product = db.Products.Where(p => p.Id.Equals(id)).FirstOrDefault();
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }


        [HttpPost]
        public ActionResult Update(Product product)
        {
            Product product1 = db.Products.Where(i => i.Id.Equals(product.Id)).FirstOrDefault();
            product1.Name = product.Name;
            product1.Price = product.Price;
            product1.imageURL = product.imageURL;
            product1.Sex = product.Sex;
            product1.Category = product.Category;
            product1.Description = product.Description;
            db.SaveChanges();
            return RedirectToAction("Admin_Product", "Admin");
        }
    }
}