using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class imagesProductsController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: Admin/imagesProducts
        public async Task<ActionResult> Index()
        {
            var imagesProducts = db.imagesProducts.Include(i => i.Product);
            return View(await imagesProducts.ToListAsync());
        }

        // GET: Admin/imagesProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = await db.imagesProducts.FindAsync(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            return View(imagesProduct);
        }

        // GET: Admin/imagesProducts/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "productId", "productName");
            return View();
        }

        // POST: Admin/imagesProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "productId,images")] imagesProduct imagesProduct)
        {
            if (ModelState.IsValid)
            {
                db.imagesProducts.Add(imagesProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // GET: Admin/imagesProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = await db.imagesProducts.FindAsync(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // POST: Admin/imagesProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "productId,images")] imagesProduct imagesProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagesProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "productName", imagesProduct.productId);
            return View(imagesProduct);
        }

        // GET: Admin/imagesProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            imagesProduct imagesProduct = await db.imagesProducts.FindAsync(id);
            if (imagesProduct == null)
            {
                return HttpNotFound();
            }
            return View(imagesProduct);
        }

        // POST: Admin/imagesProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            imagesProduct imagesProduct = await db.imagesProducts.FindAsync(id);
            db.imagesProducts.Remove(imagesProduct);
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
