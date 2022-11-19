using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartItemsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();
        // GET: CartItems
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c=>c.Id.Equals(userId)).FirstOrDefault();
            //Lấy giỏ hàng của user đang đăng nhập
            var cart = db.Carts.FirstOrDefault(c => c.userId == user.Id);
            var cartLast = db.Carts.OrderByDescending(p=>p.cartId).FirstOrDefault();
            if (cart == null)
            {
                //Tao cart moi cho user
                var newcart = new Cart();
                newcart.status = 1;
                newcart.AspNetUser = user;
                newcart.userId = user.Id;
                newcart.cartId = cartLast.cartId + 1;
                db.Carts.Add(newcart);
                db.SaveChanges();
            }
            cart = db.Carts.FirstOrDefault(c => c.userId == user.Id);
            //Lấy danh sách produsts
            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks).Include(p => p.imagesProducts);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);
            ViewData["bestsellProduct"] = product.ToList().GetRange(0, 4);
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock).Where(c=>c.cartId==cart.cartId);
            ViewData["cartId"] = cart.cartId;
            return View(cartItems.ToList());
        }

        [Authorize]
        public ActionResult AddItem([Bind(Include = "cartId,productId,quantity,unitPrice")] CartItem cartItem)
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            //Lấy giỏ hàng của user đang đăng nhập
            var cart = db.Carts.FirstOrDefault(c => c.userId == user.Id);
            //Lấy giỏ hàng cuối cùng để lấy Id lớn nhất
            var cartLast = db.Carts.OrderByDescending(p => p.cartId).FirstOrDefault();

            Product product = db.Products.Find(cartItem.productId);
            if (cart != null)
            {
                var cartitems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock).Where(c => c.cartId.Equals(cart.cartId));
                var list = cartitems.ToList();
                if (list.Exists(x => x.productId == cartItem.productId))
                {
                    foreach (var item in list)
                    {
                        if (item.productId == cartItem.productId)
                        {
                            item.quantity += cartItem.quantity;
                            db.Entry(item).State = EntityState.Modified;
                        }

                    }
                    db.SaveChanges();
                }
                else
                {

                    //tao moi doi tuong cart item
                    var item = new CartItem();
                    item.cartId = cart.cartId;
                    item.productId = cartItem.productId;
                    item.quantity = cartItem.quantity;
                    item.unitPrice = product.price;
                    db.CartItems.Add(item);
                    db.SaveChanges();
                }
            }
            else
            {
                //Tao cart moi cho user
                var newcart = new Cart();
                newcart.status = 1;
                newcart.AspNetUser = user;
                newcart.userId = user.Id;
                newcart.cartId = cartLast.cartId + 1;
                db.Carts.Add(newcart);
                db.SaveChanges();

                //tao moi doi tuong cart item
                var item = new CartItem();
                item.cartId = 1;
                item.productId = cartItem.productId;
                item.quantity = cartItem.quantity;
                item.unitPrice = product.price;
                db.CartItems.Add(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCartItem(int? id)
        {
            CartItem cartItem = db.CartItems.SingleOrDefault(c => c.productId == id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }
        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId");
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID");
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
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
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
            ViewBag.cartId = new SelectList(db.Carts, "cartId", "userId", cartItem.cartId);
            ViewBag.productId = new SelectList(db.Stocks, "stockID", "stockID", cartItem.productId);
            return View(cartItem);
        }
        
        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.SingleOrDefault(c => c.productId == id);
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
            CartItem cartItem = db.CartItems.SingleOrDefault(c => c.productId == id);
            db.CartItems.Remove(cartItem);
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
