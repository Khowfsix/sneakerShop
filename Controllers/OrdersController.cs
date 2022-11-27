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

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.AspNetUser).Include(o => o.Cart).Include(o => o.paymentType1).OrderByDescending(o => o.orderDate);
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
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId");
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderID,userID,cartID,orderDate,status,shipping,totalPay,paymentType,address")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", order.userID);
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId", order.cartID);
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName", order.paymentType);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
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
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderID,userID,cartID,orderDate,status,shipping,totalPay,paymentType,address")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", order.userID);
            ViewBag.cartID = new SelectList(db.Carts, "cartId", "userId", order.cartID);
            ViewBag.paymentType = new SelectList(db.paymentTypes, "paymentTypeID", "paymentTypeName", order.paymentType);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
