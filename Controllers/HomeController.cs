﻿using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        public ActionResult Index()
        {
            //Lay so item trong gio
            var userId = User.Identity.GetUserId();
            //Lấy giỏ hàng của user đang đăng nhập
            var cart = db.Carts.FirstOrDefault(c => c.userId == userId);
            ViewData["Count_Item"] = DemItemTrongCart(cart.cartId);
            ////Lấy danh sách products (best seller) dựa trên số lượng trong cartItem

            ////Lấy danh sách produsts
            //var product = db.Product.Include(p => p.Category).Include(p => p.Stock).Include(p => p.imagesProduct);
            ////Sắp xếp
            //product = product.OrderByDescending(s => s.CartItem.Sum(p => p.quantity));
            //return View(product.ToList().GetRange(0, 8));


            //Lấy danh sách products (best seller) dựa trên amount của product

            //Lấy danh sách produsts
            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks).Include(p => p.imagesProducts);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            ViewData["Count_Item"] = DemItemTrongCart(cart.cartId);
            return View(product.ToList().GetRange(0, 8));
        }
        public int DemItemTrongCart(int cartId)
        {        
            var cartItems = db.CartItems.Where(c => c.cartId.Equals(cartId)).ToList();
            int dem = 0;
            if(cartItems.Count > 0)
                dem= cartItems.Count;
            return dem;
        }
        public ActionResult allProduct()
        {
            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks).Include(p => p.imagesProducts);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            return View("Index", product.ToList());
        }

        public ActionResult productDetail(int productID)
        {
            Product product = db.Products.Where(p => p.productId == productID).FirstOrDefault();
            return View(product);
        }

        public ActionResult productBrand(string brandName)
        {
            var product = db.Products
                            .Include(p => p.Category)
                            .Include(p => p.imagesProducts)
                            .Where(p => p.Category.categoryName.Equals(brandName));
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            return View("Index", product.ToList());
        }

        public ActionResult Searching(string keyword)
        {
            var product = db.Products
                            .Include(p => p.Category)
                            .Include(p => p.Stocks)
                            .Include(p => p.imagesProducts)
                            .Where(p => p.productName.Contains(keyword));
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            return View("Index", product.ToList());
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