using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashBoardController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: DashBoard
        [Authorize(Roles = "Admin, Shiper")]
        public ActionResult Index()
        {
            //ViewBag.ID = User.Identity.GetUserId();
            //ViewBag.AdminName = User.Identity.GetUserName();
            //ViewBag.Role = User.IsInRole("Admin");
            ViewBag.countDay = countProduct_inDay();
            ViewBag.countMouth = countProduct_inMonth();
            ViewBag.listMostAmount = theMostAmount_inMonth(10);

            return View();
        }


        [Authorize(Roles = "Admin, Shiper")]
        public int countProduct_inDay()
        {
            int count = 0;
            //List<Order> orders = db.Orders.Where(o => o.orderDate.Value.Date == DateTime.Today.Date).ToList();

            var orders = db.Orders.ToList();
            
            foreach (var item in orders)
            {
                if (item.orderDate.Value.Date == DateTime.Today.Date)
                {
                    count++;
                }
            }

            //count = orders.Count;
            //return View(orders.ToList());

            return count;
        }


        [Authorize(Roles = "Admin, Shiper")]
        public int countProduct_inMonth()
        {
            int count = 0;
            var orders = db.Orders.ToList();

            foreach (var item in orders)
            {
                if (item.orderDate.Value.Month == DateTime.Today.Month)
                {
                    count++;
                }
            }
            count = orders.Count;

            return count;
        }


        [Authorize(Roles = "Admin, Shiper")]
        public List<Product> theMostAmount_inMonth(int number)
        {
            //List<Product> list = new List<Product>();
            //var orderDetails = db.OrderDetails.Where(o => o.Order.orderDate.Value.Month == DateTime.Today.Month).Include(o => o.Order).Include(o => o.Stock);


            //return list;

            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks).Include(p => p.imagesProducts);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);

            return product.ToList().GetRange(0, number);
        }
    }
}