using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using WebApplication1.ViewModel;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AllProductController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        public ActionResult Index(int? page, string searchString, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            
            if (page == null) page = 1;

            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks)
                .Include(p => p.imagesProducts);
            var categories = db.Categories.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                product = product.Where(b => b.productName.ToLower().Contains(searchString));
            }

            product = product.OrderBy(p => p.productId);

            int pageSize = 6;
         
            int pageNumber = (page ?? 1);

            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.productPagedList = product.ToPagedList(pageNumber, pageSize);
            productViewModel.searchString = searchString;
            productViewModel.categories = categories.ToList();
           
            return View("Index", productViewModel);

        }


        //[HttpPost]
        //public async Task<ActionResult> Filter(int? page, string searchString)
        //{
        //    // 1. Tham số int? dùng để thể hiện null và kiểu int
        //    // page có thể có giá trị là null và kiểu int.

        //    // 2. Nếu page = null thì đặt lại là 1.
        //    if (page == null) page = 1;
        //    // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
        //    // theo BookID mới có thể phân trang.
        //    var product = db.Products.Include(p => p.Category).Include(p => p.Stocks)
        //        .Include(p => p.imagesProducts);
        //    var categories = db.Categories.Select(p => p);

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        searchString = searchString.ToLower();
        //        product = product.Where(b => b.productName.ToLower().Contains(searchString));
        //    }

        //    product = product.OrderBy(p => p.productId);

        //    // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
        //    int pageSize = 6;

        //    // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
        //    // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
        //    int pageNumber = (page ?? 1);

        //    ProductViewModel productViewModel = new ProductViewModel();

        //    productViewModel.productPagedList = product.ToPagedList(pageNumber, pageSize);
        //    productViewModel.searchString = searchString;
        //    productViewModel.categories = categories.ToList();

        //    // 5. Trả về các Link được phân trang theo kích thước và số trang.
        //    return View("Index", productViewModel);

        //}

    }
}