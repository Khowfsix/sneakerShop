using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class StocksController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: Stocks
        [Authorize(Roles = ("Admin"))]
        public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.Product);
            return View(stocks.ToList());
        }

        public ActionResult productDetail_Stock(int productID, int? size)
        {
            Stock stock = new Stock();
            List<int> sizeList = new List<int>();

            stock = db.Stocks.Include(s => s.Product)
                                .Where(s => s.productId == productID)
                                .FirstOrDefault();
            sizeList = db.Stocks.Where(s => s.productId == productID).Select(s => s.size).ToList();

            if (size != null)
            {
                stock = db.Stocks.Include(s => s.Product)
                                .Where(s => s.productId == productID)
                                .Where(s => s.size == size)
                                .FirstOrDefault();
            }

            ViewBag.sizeList = sizeList;
            return View(stock);
        }

        // GET: Stocks/Details/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        [Authorize(Roles = ("Admin"))]
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "productId", "productName");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public ActionResult Create([Bind(Include = "stockID,productId,size,inStock,lastUpdate")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "productId", "productName", stock.productId);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "productName", stock.productId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public ActionResult Edit([Bind(Include = "stockID,productId,size,inStock,lastUpdate")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "productName", stock.productId);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
