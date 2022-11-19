using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ShiperController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();
        // GET: Shiper
        [Authorize]
        public ActionResult DonHangChuaNhan()
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            ViewData["userId"]=userId;
            var orders = db.Orders.ToList();
            return View(orders);
        }
        [Authorize]
        public ActionResult NhanGiao(int? orderId)
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            ViewBag.Id = user.Id;
            var shipment = new Shipment();
            var order = db.Orders.FirstOrDefault(c => c.orderID == orderId);
            order.status = 1;
            shipment.shipperID = user.Id;
            shipment.orderID = order.orderID;
            db.Orders.AddOrUpdate(order);
            db.Shipments.Add(shipment);
            db.SaveChanges();
            return RedirectToAction("DonHangChuaNhan","Shiper", new {userId = userId});
        }
        public ActionResult DaGiao(int? orderId)
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            var order = db.Orders.FirstOrDefault(c => c.orderID == orderId);
            order.shipping = 1;
            db.Orders.AddOrUpdate(order);
            db.SaveChanges();
            return RedirectToAction("DonDaNhan", "Shiper", new { shiperId = userId }); ;
        }
        public ActionResult DonDaNhan(string shiperId)
        {
            var shipments = db.Shipments.Where(c => c.shipperID == shiperId).ToList();
            List<Order> orders = new List<Order>();
            foreach (var shipment in shipments)
            {
                orders.Add(db.Orders.FirstOrDefault(c => c.orderID == shipment.orderID));
            }
            return View(orders);
        }
    }
}