using System;
using SuperDamp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database = SuperDamp.Models.Database;
using System.Threading.Tasks;

namespace SuperDamp.Controllers
{
    public class FAQController : Controller
    {
        Database db = new Database();
        // GET: FAQ
        public ActionResult Index()
        {
            modelAdaptor modelAdaptor = new modelAdaptor();
            modelAdaptor.faqs = db.FAQs.ToList();
            return View(modelAdaptor);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(modelAdaptor modelAdaptor)
        {
                db.FAQs.Add(modelAdaptor.faq);
                db.SaveChanges();
            
                return RedirectToAction("Admin");
        }

        public ActionResult Delete(int id)
        {
            FAQ faq = db.FAQs.Where(f => f.Id.Equals(id)).FirstOrDefault();
            db.FAQs.Remove(faq);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        public ActionResult Update(int id)
        {
            FAQ faq = db.FAQs.Where(f => f.Id.Equals(id)).FirstOrDefault();
            modelAdaptor modelAdaptor = new modelAdaptor();
            modelAdaptor.faq = faq;
            return View(modelAdaptor);
        }
        [HttpPost]
        public ActionResult Update(modelAdaptor modelAdaptor)
        {

           var getFaq = db.FAQs.FirstOrDefault(F => F.Id.Equals(modelAdaptor.faq.Id));
           getFaq.Question = modelAdaptor.faq.Question;
           getFaq.Answer = modelAdaptor.faq.Answer;
            db.SaveChanges();    
           return RedirectToAction("Admin");
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


        public ActionResult Admin()
        {
            modelAdaptor modelAdaptor = new modelAdaptor();
            modelAdaptor.faqs = db.FAQs.ToList();
            return View(modelAdaptor);
        }
    }
}