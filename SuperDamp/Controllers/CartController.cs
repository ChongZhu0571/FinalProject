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
    public class CartController : Controller
    {
        Database db = new Database();
        // GET: Cart

        public ActionResult Index()
        {
            modelAdaptor ma = new modelAdaptor();
            string username = Session["username"].ToString();
            List<Cart> carts = db.carts.Where(u => u.username.Equals(username)).ToList();
            List<Product> products = db.Products.ToList();
            var list = (from c in carts
                        join p in products
                        on c.productId equals p.Id
                        select new Cart { quantity = c.quantity, product = p,productId=c.productId }).ToList();
            ma.carts = list;
            return View(ma);
        }


        public ActionResult add(int productId)
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                addToCart(productId);
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult CartOperation(modelAdaptor ma,string operation)
        {
            if (ma.carts!=null)
            {
                string username = Session["username"].ToString();
                if (operation.Equals("delete"))
                {
                    var a = ma.carts.Where(s => s.isSelected == true).ToList();
                    foreach (var item in a)
                    {
                        Cart cart = new Cart();
                        cart = db.carts.Where(i => i.productId.Equals(item.productId)).Where(u => u.username.Equals(username)).FirstOrDefault();
                        db.carts.Remove(cart);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                else if (operation.Equals("checkOut"))
                {
                    modelAdaptor ma2 = new modelAdaptor();
                    var a = ma.carts.Where(s => s.isSelected == true).ToList();
                    List<Cart> cartInfo = new List<Cart>();
                    if (a.Count() > 0)
                    {
                        foreach (var item in a)
                        {
                            Cart cart = new Cart();
                            cart = db.carts.Where(i => i.productId.Equals(item.productId)).Where(u => u.username.Equals(username)).FirstOrDefault();
                            cartInfo.Add(cart);
                        }
                        var checkoutInfo = (from c in cartInfo
                                            join p in db.Products.ToList()
                                            on c.productId equals p.Id
                                            select new Cart { product = p, quantity = c.quantity }).ToList();
                        var shipInfo = db.UserInfos.Where(u => u.username.Equals(username)).ToList();
                        var payInfo = db.PaymentInfos.Where(u => u.username.Equals(username)).ToList();
                        ma2.carts = checkoutInfo;
                        ma2.userInfos = shipInfo;
                        ma2.paymentInfos = payInfo;
                        return View("checkOut", ma2);
                    }
                    else
                    {
                        ViewBag.blankCart = "Empty!";
                        return RedirectToAction("Index");
                    }
                }
                else if(operation.Equals("chooseMore"))
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }


            
        }

        public ActionResult checkOut(modelAdaptor ma)
        {
            return View(ma);
        }


        public ActionResult createShipInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createShipInfo(modelAdaptor ma)
        {
            UserInfo userInfo = new UserInfo();
            userInfo = ma.userInfo;
            userInfo.username = Session["username"].ToString();
            db.UserInfos.Add(userInfo);
            db.SaveChanges();
            return RedirectToAction("CheckOut");
        }
        public ActionResult createPaymentInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createPaymentInfo(modelAdaptor ma,DateTime expireDate)
        {
            paymentInfo pi = new paymentInfo();
            pi = ma.paymentInfo;
            pi.username = Session["username"].ToString();
            pi.expireDate = expireDate;
            db.PaymentInfos.Add(pi);
            db.SaveChanges();
            return RedirectToAction("CheckOut");
        }

        public void addToCart(int productId)
        {
            Product product = db.Products.Where(i => i.Id.Equals(productId)).FirstOrDefault();
            Cart cart = new Cart();       
            cart.username = (string)Session["username"];
            cart.productId = product.Id;
            var cartQuery = db.carts.Where(u => u.username.Equals(cart.username) & u.productId.Equals(product.Id));
            if (cartQuery.Count() > 0)
            {
                cartQuery.FirstOrDefault().quantity += 1;
            }
            else
            {
                cart.quantity = 1;
                db.carts.Add(cart);
            }
            db.SaveChanges();            
        }



        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(modelAdaptor ma,string userInfoSelect,string paymentInfo)
        {
            if (userInfoSelect != null && paymentInfo != null)
            {
                Order order = new Order();
                order.username = Session["username"].ToString();
                order.email = db.UserInfos.Where(e => e.username.Equals(order.username)).FirstOrDefault().email;
                order.shipmentInfo = userInfoSelect;
                order.paymentInfo = paymentInfo;
                order.orderStatus = "Processing";
                int amount = 0;
                string orderItems = "";
                var au = db.carts.Where(u => u.username.Equals(order.username)).ToList();
                var pu = db.Products.ToList();
                var bu = (from a in au
                          join p in pu
                          on a.productId equals p.Id
                          select new
                          {
                              name = p.Name,
                              quantity = a.quantity,
                              price = p.Price
                          }).ToList();
                foreach (var item in bu)
                {
                    orderItems += item.name + "*" + item.quantity + " ,";
                    amount += item.price * item.quantity;
                }
                order.orderItems = orderItems.Substring(0, orderItems.Length - 1) + " Amount: " + amount;
                order.orderTime = DateTime.Now;
                db.orders.Add(order);
                db.carts.RemoveRange(au);
                db.SaveChanges();
                return View();
            }
            else
            {
                ViewBag.checkout = "Please check required filed!";
                return RedirectToAction("CheckOut");
            }
            
        }
    }
}