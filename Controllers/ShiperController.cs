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
        public ActionResult DonHangChuaNhan()
        {
            var orders = db.Orders.ToList();
            return View(orders);
        }
        public ActionResult NhanGiao(int? orderId)
        {
            var shipment = new Shipment();
            var order = db.Orders.FirstOrDefault(c => c.orderID == orderId);
            order.status = 1;
            shipment.shipperID = "1";
            shipment.orderID = order.orderID;
            db.Orders.AddOrUpdate(order);
            db.Shipments.Add(shipment);
            db.SaveChanges();
            return Redirect("DonHangChuaNhan");
        }
        public ActionResult DaGiao(int? orderId)
        {
            var order = db.Orders.FirstOrDefault(c => c.orderID == orderId);
            order.shipping = 1;
            db.Orders.AddOrUpdate(order);
            db.SaveChanges();
            return Redirect("DonDaNhan");
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