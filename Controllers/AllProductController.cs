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

        public async Task<ActionResult> Index(int? page, string searchString, double? minimumPrice,
            double? maximumPrice, List<int> categoryCheckIds, int? sortBy)
        {
            FilterViewModel filterViewModel = getFilteredProducts(minimumPrice, maximumPrice, categoryCheckIds, sortBy);


            if (page == null) page = 1;

            var categories = db.Categories.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                filterViewModel.productList = filterViewModel.productList.Where(b => b.productName.ToLower().Contains(searchString));
            }

            switch (sortBy)
            {
                case 0:
                    filterViewModel.productList = filterViewModel.productList.OrderByDescending(p => p.categoryId);
                    break;
                case 1:
                    filterViewModel.productList = filterViewModel.productList.OrderByDescending(p => p.categoryId);
                    break;
                case 2:
                    filterViewModel.productList = filterViewModel.productList.OrderBy(p => p.price);
                    break;
                case 3:
                    filterViewModel.productList = filterViewModel.productList.OrderBy(p => p.price);
                    break;
                default:
                    break;
            }
            int pageSize = 6;

            int pageNumber = (page ?? 1);


            filterViewModel.ProductViewModel.ProductPagedList = filterViewModel.productList.ToPagedList(pageNumber, pageSize);
            filterViewModel.ProductViewModel.SearchString = searchString;
            filterViewModel.ProductViewModel.Categories = categories.ToList();
            filterViewModel.MaximumPrice = maximumPrice;
            filterViewModel.MinPrice = minimumPrice;



            return View("Index", filterViewModel);

        }
        [HttpPost]
        public async Task<ActionResult> FilteredProducts(int? page, string searchString, double? minimumPrice,
            double? maximumPrice, List<int> categoryCheckIds, int? sortBy)
        {
            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks)
                .Include(p => p.imagesProducts);
            var categories = db.Categories.Select(p => p);

            if (!minimumPrice.HasValue) minimumPrice = 0;

            if (!maximumPrice.HasValue) maximumPrice = product.OrderBy(p => p.price).First().price;

            if (categoryCheckIds.Count == 0) categoryCheckIds = db.Categories.OrderBy(p => p.categoryId)
                    .Select(p => p.categoryId)
                    .ToList();
            if (!sortBy.HasValue) sortBy = 0;

            FilterViewModel filterViewModel = new FilterViewModel();
            filterViewModel.MaximumPrice = maximumPrice;
            filterViewModel.MinPrice = minimumPrice;
            filterViewModel.CategoryCheckIds = categoryCheckIds;

            filterViewModel.productList = db.Products.Where(p => p.price >= minimumPrice && p.price <= maximumPrice)
                    .Select(p => p).OrderByDescending(p => p.categoryId)
                    .Include(p => p.Category).Include(p => p.Stocks)
                    .Include(p => p.imagesProducts);

            return PartialView("FilterProducts", filterViewModel);
        }
        public FilterViewModel getFilteredProducts(double? minimumPrice,
            double? maximumPrice, List<int> categoryCheckIds, int? sortBy)
        {
            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks)
                .Include(p => p.imagesProducts);
            var categories = db.Categories.Select(p => p);

            if (!minimumPrice.HasValue) minimumPrice = 0;

            if (!maximumPrice.HasValue) maximumPrice = product.OrderByDescending(p => p.price).First().price;

            categoryCheckIds = new List<int>();

            if (categoryCheckIds == null)
            {
                categoryCheckIds = db.Categories.Select(p => p.categoryId).ToList();
            }
            if (!sortBy.HasValue) sortBy = 0;

            FilterViewModel filterViewModel = new FilterViewModel();
            filterViewModel.MaximumPrice = maximumPrice;
            filterViewModel.MinPrice = minimumPrice;
            filterViewModel.CategoryCheckIds = categoryCheckIds;

            filterViewModel.productList = db.Products.Where(p => p.price >= minimumPrice && p.price <= maximumPrice)
                    .Select(p => p).OrderByDescending(p => p.categoryId)
                    .Include(p => p.Category).Include(p => p.Stocks)
                    .Include(p => p.imagesProducts);

            return filterViewModel;
        }

    }
}