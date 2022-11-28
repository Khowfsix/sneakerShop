using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Shiper, Admin")]
    public class ShiperController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();
        // GET: Shiper
        public ActionResult DonHangChuaNhan()
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            ViewData["userId"]=userId;
            var orders = db.Orders.Where(p => p.status == 1).ToList();
            return View(orders);
        }
        public ActionResult NhanGiao(int? orderId)
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            ViewBag.Id = user.Id;
            var shipment = new Shipment();
            var order = db.Orders.FirstOrDefault(c => c.orderID == orderId);
            order.status = 2;
            order.shipping = 1;
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
            order.shipping = 2;
            order.status = 3;
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