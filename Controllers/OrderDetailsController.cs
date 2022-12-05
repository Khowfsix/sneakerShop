using PayPal.Api;
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
    public class OrderDetailsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: OrderDetails
        [Authorize]
        public ActionResult Index(int orderID)
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Where(o => o.orderID == orderID).Include(o => o.Stock);
            var order = db.Orders.Where(o => o.orderID == orderID).FirstOrDefault();
            ViewBag.OrderDetail = orderDetails;
            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult listAdmin(int orderID)
        {
            var orderDetails = db.OrderDetails.Include(o => o.Order).Where(o => o.orderID == orderID).Include(o => o.Stock);
            var order = db.Orders.Where(o => o.orderID == orderID).FirstOrDefault();
            ViewBag.OrderDetail = orderDetails;
            return View(order);
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID");
            ViewBag.stockID = new SelectList(db.Stocks, "stockID", "stockID");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderDetailId,orderID,stockID,quantity,unitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", orderDetail.orderID);
            ViewBag.stockID = new SelectList(db.Stocks, "stockID", "stockID", orderDetail.stockID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", orderDetail.orderID);
            ViewBag.stockID = new SelectList(db.Stocks, "stockID", "stockID", orderDetail.stockID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderDetailId,orderID,stockID,quantity,unitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", orderDetail.orderID);
            ViewBag.stockID = new SelectList(db.Stocks, "stockID", "stockID", orderDetail.stockID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
