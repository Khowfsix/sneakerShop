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
    public class CartItemsController : Controller
    {
        private sneakerShopEntities2 db = new sneakerShopEntities2();

        // GET: CartItems
        public ActionResult Index()
        {
            var cartItem = db.CartItem.Include(c => c.Cart).Include(c => c.Product);
            return View(cartItem.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.cartId = new SelectList(db.Cart, "cartId", "cartId");
            ViewBag.productId = new SelectList(db.Product, "productId", "productName");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartId,productId,quantity,unitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItem.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cartId = new SelectList(db.Cart, "cartId", "cartId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Product, "productId", "productName", cartItem.productId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.cartId = new SelectList(db.Cart, "cartId", "cartId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Product, "productId", "productName", cartItem.productId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartId,productId,quantity,unitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cartId = new SelectList(db.Cart, "cartId", "cartId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Product, "productId", "productName", cartItem.productId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItem.Find(id);
            db.CartItem.Remove(cartItem);
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
