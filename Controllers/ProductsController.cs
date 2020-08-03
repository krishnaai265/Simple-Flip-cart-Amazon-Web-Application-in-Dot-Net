using MVCDatabaseFirstApproach.Models;
using MVCShoppingCard12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCDatabaseFirstApproach.Controllers
{
    public class ProductsController : Controller
    {
        SopraDbContext db = new SopraDbContext();
        // GET: Products
        
        public ActionResult Index()
        {
            ViewBag.login = "<button type='button' class='btn btn-sm btn - outline - secondary'>@Html.ActionLink('Registration', 'Registration')</button>< button type = 'button' class='btn btn-sm btn-outline-secondary'>@Html.ActionLink('Login', 'Login')</button>";
            var products = db.Products.ToList();
            return View(products);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Cart(int id)
        {
            ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return View();
        }

        public ActionResult Remove(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)

                if (cart[i].Product.ProductId.Equals(id))
                    return i;

            return -1;
        }

        public ActionResult Registration() {
            
            return View();
        }
        [HttpPost]
        public ActionResult Registration(user user) {
            db.users.Add(user);
            db.SaveChanges();
      //      Session["username"] = user.id;
            ViewBag.UserName = user.name;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }
      
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            user u = (user)db.users.Where(i => i.username.Equals(username)).SingleOrDefault();
            //          user u = (user)db.users.Find(username);
            Console.WriteLine(username +"  " +u+"  "+password);
            if (u != null)
            {
                if (u.username == username && u.password == password) {
                    Session["username"] = username;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else {
                return RedirectToAction("Login");
            }         
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Remove("username");
            return RedirectToAction("Index");
        }

        public ActionResult Pay() {
            if (Session["username"] == null) {
                return RedirectToAction("login1");
            }          
                return View();
            
            
        }
        
        public ActionResult login1() {
            return View();
        }

        [HttpPost]
        public ActionResult Login1(string username, string password)
        {
            user u = (user)db.users.Where(i => i.username.Equals(username)).SingleOrDefault();

            if (u != null)
            {
                if (u.username == username && u.password == password)
                {
                    Session["username"] = u.username;
                    return RedirectToAction("Pay");
                }
                else
                {
                    return RedirectToAction("Login1");
                }
            }
            else
            {
                return RedirectToAction("Login1");
            }
        }
        

    }
}
