using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CartItemsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: Admin/CartItems
        public async Task<ActionResult> Index()
        {
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock);
            return View(await cartItems.ToListAsync());
        }

        // GET: Admin/CartItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = await db.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: Admin/CartItems/Create
        public ActionResult Create()
        {
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId");
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID");
            return View();
        }

        // POST: Admin/CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cartId,productId,quantity,unitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItems.Add(cartItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
            return View(cartItem);
        }

        // GET: Admin/CartItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = await db.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
            return View(cartItem);
        }

        // POST: Admin/CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cartId,productId,quantity,unitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
            return View(cartItem);
        }

        // GET: Admin/CartItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = await db.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: Admin/CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CartItem cartItem = await db.CartItems.FindAsync(id);
            db.CartItems.Remove(cartItem);
            await db.SaveChangesAsync();
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
