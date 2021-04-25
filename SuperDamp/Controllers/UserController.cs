﻿using SuperDamp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperDamp.Controllers
{
    public class UserController : Controller
    {
        Database db = new Database();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            if (ModelState.IsValid)
            {
                var u = db.users.Where(s => s.username.Equals(user.username));
                var e = db.users.Where(s => s.email.Equals(user.username));
                if (u.Count() > 0 || e.Count()>0)
                {
                    var ps1 = u.Where(p => p.password.Equals(user.password));
                    var ps2 = e.Where(p => p.password.Equals(user.password));

                    if (ps1.Count() > 0)
                    {
                        Session["username"] = u.FirstOrDefault().username;                       
                        return RedirectToAction("Index","Home");
                    }
                    else if (ps2.Count() > 0)
                    {
                        Session["username"] = e.FirstOrDefault().username;
                    }
                    else
                    {
 
                        ViewBag.error = "Password is not correct!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Username or Email does not exists!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user,string password2)
        {

            if (ModelState.IsValid)
            {
                if (user.password == password2)
                {
                    var checkUsername = db.users.FirstOrDefault(s => s.username == user.username);
                    var checkEmail= db.users.FirstOrDefault(s => s.email == user.email);
                    if (checkUsername == null)
                    {
                        if(checkEmail == null)
                        {
                            db.users.Add(user);
                            db.SaveChanges();
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ViewBag.error = "Email already exists";
                            return View();
                        }
                        
                    }
                    else
                    {
                        ViewBag.error = "User already exists";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "retyped password does not match!";
                    return View();
                }



            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }


        public new ActionResult Profile()
        {
            if (Session["adminName"] != null)
            {
                modelAdaptor ma = new modelAdaptor();
                string username = Session["username"].ToString();
                var userInfo = db.UserInfos.Where(u => u.username.Equals(username)).FirstOrDefault();
                ma.userInfo = userInfo;
                return View(ma);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
           
        }

        public ActionResult Order()
        {
            if (Session["username"] != null)
            {
                modelAdaptor ma = new modelAdaptor();
                string username = Session["username"].ToString();
                var orderInfo = db.orders.Where(o => o.username.Equals(username)).ToList();
                ma.orders = orderInfo;
                return View(ma);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }

        public ActionResult Cancel(int id)
        {
            Order order = db.orders.Where(o => o.orderNo.Equals(id)).FirstOrDefault();
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Order", "User");
        }
        public ActionResult Cancel2(int id)
        {
            Order order = db.orders.Where(o => o.orderNo.Equals(id)).FirstOrDefault();
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Admin_Order", "Admin");
        }

        public ActionResult Ship(int id)
        {
            if (Session["adminName"] != null)
            {
                
                modelAdaptor ma = new modelAdaptor();
                Order order = new Order();
                order.orderNo = id;
                ma.order = order;
                return View(ma);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpPost]
        public ActionResult Ship(modelAdaptor ma)
        {
            int orderNo = ma.order.orderNo;
            Order order = db.orders.Where(o => o.orderNo.Equals(orderNo)).FirstOrDefault();
            order.orderStatus = "Shipped";
            order.trackingNumber = ma.order.trackingNumber;
            db.SaveChanges();
            return RedirectToAction("Admin_Order","Admin");
        }

        public ActionResult Track(int id)
        {
            string trackingNo = db.orders.Where(i => i.orderNo.Equals(id)).FirstOrDefault().trackingNumber;
            Session["tracked"] = trackingNo;
            return RedirectToAction("Order", "User");
        }
    }
}