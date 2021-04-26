using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperDamp.Models;

namespace SuperDamp.Controllers
{
    public class MessageController : Controller
    {
        Database db = new Database();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelAdaptor ma)
        {
            Message message = new Message();
            message = ma.message;
            Debug.WriteLine(ma.message.Name, ma.message.Email);
            db.Messages.Add(message);
            db.SaveChanges();           
            return View("success");
        }

        public ActionResult reply()
        {
            return RedirectToAction("Show_Message", "admin");
        }

        public ActionResult delete(int id)
        {
            Message message = db.Messages.Where(i => i.Id.Equals(id)).FirstOrDefault();
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Show_Message", "admin");
        }
    }
}