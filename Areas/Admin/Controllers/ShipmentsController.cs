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
    public class ShipmentsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: Admin/Shipments
        public async Task<ActionResult> Index()
        {
            var shipments = db.Shipments.Include(s => s.AspNetUser).Include(s => s.Order);
            return View(await shipments.ToListAsync());
        }

        // GET: Admin/Shipments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Admin/Shipments/Create
        public ActionResult Create()
        {
            ViewBag.shipperID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID");
            return View();
        }

        // POST: Admin/Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "shipmentID,shipperID,orderID")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.shipperID = new SelectList(db.AspNetUsers, "Id", "Email", shipment.shipperID);
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", shipment.orderID);
            return View(shipment);
        }

        // GET: Admin/Shipments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.shipperID = new SelectList(db.AspNetUsers, "Id", "Email", shipment.shipperID);
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", shipment.orderID);
            return View(shipment);
        }

        // POST: Admin/Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "shipmentID,shipperID,orderID")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.shipperID = new SelectList(db.AspNetUsers, "Id", "Email", shipment.shipperID);
            ViewBag.orderID = new SelectList(db.Orders, "orderID", "userID", shipment.orderID);
            return View(shipment);
        }

        // GET: Admin/Shipments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Admin/Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shipment shipment = await db.Shipments.FindAsync(id);
            db.Shipments.Remove(shipment);
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
