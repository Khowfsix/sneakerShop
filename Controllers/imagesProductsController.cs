﻿using System;
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
    public class imagesProductsController : Controller
    {
        private sneakerShopEntities2 db = new sneakerShopEntities2();

        // GET: imagesProducts
        public ActionResult Index()
        {
            var imagesProduct = db.imagesProduct.Include(i => i.Product);
            return View(imagesProduct.ToList());
        }

        // GET: imagesProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = db.imagesProduct.Find(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            return View(imagesProduct);
        }

        // GET: imagesProducts/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Product, "productId", "productName");
            return View();
        }

        // POST: imagesProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productId,images")] imagesProduct imagesProduct)
        {
            if (ModelState.IsValid)
            {
                db.imagesProduct.Add(imagesProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Product, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // GET: imagesProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = db.imagesProduct.Find(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Product, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // POST: imagesProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productId,images")] imagesProduct imagesProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagesProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Product, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // GET: imagesProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = db.imagesProduct.Find(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            return View(imagesProduct);
        }

        // POST: imagesProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            imagesProduct imagesProduct = db.imagesProduct.Find(id);
            db.imagesProduct.Remove(imagesProduct);
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
