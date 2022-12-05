using Microsoft.AspNet.Identity;
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
    public class OrdersController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        const int DS_DonTrongNgay_ChuaXacNhan = 10;
        const int DS_DonTrongNgay_DaXacNhan = 11;
        const int DS_DonTrongNgay_ShiperDaNhan = 12;
        const int DS_DonTrongNgay_HoanThanh = 13;

        const int DS_DonTrongThang_ChuaXacNhan = 20;
        const int DS_DonTrongThang_DaXacNhan = 21;
        const int DS_DonTrongThang_ShiperDaNhan = 22;
        const int DS_DonTrongThang_HoanThanh = 23;

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? flag)
        {
            DateTime today = DateTime.Today.Date;
            int thisMonth = DateTime.Today.Month;
            var orders = db.Orders.Include(o => o.AspNetUser).Include(o => o.Cart).Include(o => o.paymentType1);
            switch (flag)
            {
                case null:
                    break;
                case DS_DonTrongNgay_ChuaXacNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 0).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongNgay_DaXacNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 1).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongNgay_ShiperDaNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 2).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongNgay_HoanThanh:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 3).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongThang_ChuaXacNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 0).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongThang_DaXacNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 1).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongThang_ShiperDaNhan:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 2).OrderByDescending(o => o.orderDate);
                    break;
                case DS_DonTrongThang_HoanThanh:
                    orders = orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 3).OrderByDescending(o => o.orderDate);
                    break;
                default:
                    break;
            }
            ViewBag.flag = flag;
            return View(orders.ToList());
        }

        [Authorize]
        public ActionResult IndexUser()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Include(o => o.AspNetUser).Include(o => o.Cart).Include(o => o.paymentType1).Where(o => o.userID.Equals(userId)).OrderByDescending(o => o.orderDate);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int? flag)
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId");
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName");
            ViewBag.flag = flag;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderID,userID,cartID,orderDate,status,shipping,totalPay,paymentType,address")] Order order, int? flag)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", new { flag = flag });
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", order.userID);
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId", order.cartID);
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName", order.paymentType);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id, int? flag)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", order.userID);
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId", order.cartID);
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName", order.paymentType);
            ViewBag.flag = flag;
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderID,userID,cartID,orderDate,status,shipping,totalPay,paymentType,address")] Order order, int? flag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { flag = flag });
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", order.userID);
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId", order.cartID);
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName", order.paymentType);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id, int? flag)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.flag = flag;
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? flag)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index", new { flag = flag });
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
