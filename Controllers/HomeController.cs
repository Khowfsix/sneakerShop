using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private sneakerShopEntities2 db = new sneakerShopEntities2();
       
        public ActionResult Index(int bestSeller)
        {
            ////Lấy danh sách products (best seller) dựa trên số lượng trong cartItem

            ////Lấy danh sách produsts
            //var product = db.Product.Include(p => p.Category).Include(p => p.Stock).Include(p => p.imagesProduct);
            ////Sắp xếp
            //product = product.OrderByDescending(s => s.CartItem.Sum(p => p.quantity));
            //return View(product.ToList().GetRange(0, 8));


            //Lấy danh sách products (best seller) dựa trên amount của product

            //Lấy danh sách produsts
            var product = db.Product.Include(p => p.Category).Include(p => p.Stock).Include(p => p.imagesProduct);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            return View(product.ToList().GetRange(0, bestSeller));
        }

            public ActionResult allProduct()
        {
            var product = db.Product.Include(p => p.Category).Include(p => p.Stock).Include(p => p.imagesProduct);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            return View("Index", product.ToList());
        }

        public ActionResult productDetail(int productID)
        {
            Product product = db.Product.Where(p => p.productId == productID).FirstOrDefault();
            return View(product);
        }

        public ActionResult cartUser(int userID)
        {
            Cart cart = db.Cart.Where(c => c.userId == userID).Where(c => c.status == 0).FirstOrDefault();
            var cartItem = db.CartItem.Where(ci => ci.cartId == cart.cartId);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}