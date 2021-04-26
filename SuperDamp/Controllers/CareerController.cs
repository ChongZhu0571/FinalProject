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
    public class CareerController : Controller
    {
        // GET: Career
        Database db = new Database();
        // GET: career
        public ActionResult Index()
        {
            modelAdaptor modelAdaptor = new modelAdaptor();
            modelAdaptor.careers = db.Careers.ToList();
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
        public ActionResult Create(modelAdaptor modelAdaptor)
        {
            db.Careers.Add(modelAdaptor.career);
            db.SaveChanges();

            return RedirectToAction("Admin_Career", "Admin");
        }

        public ActionResult Delete(int id)
        {
            Career career = db.Careers.Where(f => f.Id.Equals(id)).FirstOrDefault();
            db.Careers.Remove(career);
            db.SaveChanges();
            return RedirectToAction("Admin_Career","Admin");
        }

        public ActionResult Update(int id)
        {
            if (Session["adminName"] != null)
            {
                Career career = db.Careers.Where(f => f.Id.Equals(id)).FirstOrDefault();
                modelAdaptor modelAdaptor = new modelAdaptor();
                modelAdaptor.career = career;
                return View(modelAdaptor);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }
        [HttpPost]
        public ActionResult Update(modelAdaptor modelAdaptor)
        {

            var getcareer = db.Careers.FirstOrDefault(F => F.Id.Equals(modelAdaptor.career.Id));
            getcareer.jobTitle = modelAdaptor.career.jobTitle;
            getcareer.Type = modelAdaptor.career.Type;
            getcareer.Reference = modelAdaptor.career.Reference;
            getcareer.Location = modelAdaptor.career.Location;
            getcareer.Hours = modelAdaptor.career.Hours;
            getcareer.roleDescription = modelAdaptor.career.roleDescription;
            db.SaveChanges();
            return RedirectToAction("Admin_Career","Admin");
        }
        public async Task<int> SaveChangesAsync()
        {
            Task<int> task1 = db.SaveChangesAsync();
            try
            {
                await task1;
            }
            catch (Exception ex)
            {
                throw;
            }
            return task1.Result;
        }



    }
}