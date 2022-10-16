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
        private sneakerShopEntities1 db = new sneakerShopEntities1();

        public ActionResult Index()
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
            return View(product.ToList().GetRange(0, 8));
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