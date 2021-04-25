using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperDamp.Models;

namespace SuperDamp.Controllers
{
    public class AdminController : Controller
    {
        Database db = new Database();
        // GET: Admin
        public ActionResult Logined()
        {
            if (Session["adminName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admins.Where(s => s.adminName.Equals(admin.adminName));
                if (a.Count() > 0)
                {
                    var ps = a.Where(p => p.password.Equals(admin.password));

                    if (ps.Count() > 0)
                    {
                        Session["adminName"] = a.FirstOrDefault().adminName;
                        return RedirectToAction("Logined");
                    }
                    else
                    {

                        ViewBag.adminLogin = "Password is not correct!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.adminLogin = "Username does not exists!";
                    return View();
                }
            }
            return View();
        }


        public ActionResult Admin_Product()
        {
            if (Session["adminName"] != null)
            {

                List<Product> products = db.Products.ToList();
             
                return View(products);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }


        public ActionResult Admin_Career()
        {
            if (Session["adminName"] != null)
            {
                modelAdaptor modelAdaptor = new modelAdaptor
                {
                    careers = db.Careers.ToList()
                };
                return View(modelAdaptor);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult Admin_Faq()
        {
            if (Session["adminName"] != null)
            {
                modelAdaptor modelAdaptor = new modelAdaptor();
                modelAdaptor.faqs = db.FAQs.ToList();
                return View(modelAdaptor);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult Show_Message()
        {
            if (Session["adminName"] != null)
            {
                return View(db.Messages.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
           
        }

        public ActionResult Admin_order()
        {
            if (Session["adminName"] != null)
            {
                modelAdaptor ma = new modelAdaptor();
                ma.orders = db.orders.ToList();
                return View(ma);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}