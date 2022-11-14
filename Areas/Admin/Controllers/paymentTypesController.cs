using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class paymentTypesController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: Admin/paymentTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.paymentTypes.ToListAsync());
        }

        // GET: Admin/paymentTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentType paymentType = await db.paymentTypes.FindAsync(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // GET: Admin/paymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/paymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "paymentTypeID,paymentTypeName")] paymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.paymentTypes.Add(paymentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paymentType);
        }

        // GET: Admin/paymentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentType paymentType = await db.paymentTypes.FindAsync(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: Admin/paymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "paymentTypeID,paymentTypeName")] paymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: Admin/paymentTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentType paymentType = await db.paymentTypes.FindAsync(id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: Admin/paymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            paymentType paymentType = await db.paymentTypes.FindAsync(id);
            db.paymentTypes.Remove(paymentType);
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
