using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class CartsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();
        // GET: Carts
        public ActionResult Index()
        {
            var cart = db.Carts.Include(c => c.AspNetUser);
            return View(cart.ToList());
        }
        //GEt : Cart/Checkout

        public ActionResult Checkout(int? cartId)
        {
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock).Where(c=>c.cartId==cartId);
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.cartItems = cartItems.ToList();
            return View(checkoutViewModel);
        }
        // POST: Carts/Checkout
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(FormCollection formCheckout)
        {
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock);
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.cartItems = cartItems.ToList();
            double total = 0;
            foreach (var cartItem in cartItems)
            {
                total += (double)(cartItem.quantity * cartItem.unitPrice);

            }

            Order order = new Order();
            order.orderDate = DateTime.Now;
            order.address = formCheckout["address"];
            order.userID = "1";
            order.cartID = 1;
            order.status = 0;
            order.shipping = 0;
            order.totalPay = (long)total;
            order.paymentType = int.Parse(formCheckout["optradio"]);
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction(actionName: "OrderComplete", controllerName:"Carts", new {cartId = 1} );
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }
        //GET/Carts/OrderComplete
        public ActionResult OrderComplete(int cartId)
        {
            ClearCart(cartId);
            return View();
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartId,userId,buyDate,status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartId,userId,buyDate,status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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
        
        private void ClearCart(int cartId)
        {
            var cartItems = from cartitems in db.CartItems
                         where cartitems.cartId == cartId
                         select cartitems;
            foreach (var item in cartItems)
            {
                db.CartItems.Remove(item);
            }
            db.SaveChanges();

        }

    }
}
