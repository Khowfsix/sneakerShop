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
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class AllProductController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        public ActionResult Index(FormCollection formCollection)
        {
            FilterViewModel filterViewModel = new FilterViewModel();


            filterViewModel.SexCheck = db.Products.GroupBy(p => p.sex)
                .Select(p => p.Key).ToList();


            filterViewModel.CategoryCheckIds = db.Categories.Select(p => p.categoryId).ToList();


            filterViewModel.SizeCheck = db.Stocks.GroupBy(p => p.size)
                .Select(p => p.Key).ToList();


            filterViewModel.SortBy = 0;
            filterViewModel.MaximumPrice = 0;
            filterViewModel.MinPrice = 0;


            //Get list products by filter
            filterViewModel = getFilteredProducts(filterViewModel.MinPrice,
                filterViewModel.MaximumPrice, filterViewModel.SexCheck, filterViewModel.SortBy,
                filterViewModel.CategoryCheckIds, filterViewModel.SearchString, filterViewModel.SizeCheck);

            //Sort 
            switch (filterViewModel.SortBy)
            {
                case 0:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.categoryId);
                    break;
                case 1:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.amount);
                    break;
                case 2:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderBy(p => p.price);
                    break;
                default:
                    break;
            }

            var tempProdList = filterViewModel.productByPriceList.ToList();
            var productList = new List<Product>();

            foreach (var item in tempProdList)
            {
                if (filterViewModel.productByCategoryList.Contains(item))
                {
                    productList.Add(item);
                }
            }

            int pageSize = 6;


            int page = 1;
            filterViewModel.ProductViewModel.Sexs = db.Products.GroupBy(p => p.sex)
                    .Select(p => p.Key).ToList();
            filterViewModel.ProductViewModel.Sizes = db.Stocks.GroupBy(p => p.size)
                    .Select(p => p.Key).ToList();
            filterViewModel.ProductViewModel.ProductPagedList = productList.ToPagedList(page, pageSize);
            filterViewModel.ProductViewModel.SearchString = "";

            var categories = db.Categories.Select(p => p);
            filterViewModel.ProductViewModel.Categories = categories.ToList();

            return View("Index", filterViewModel);
        }

        public PartialViewResult Pagination(int? page, string modela)
        {
            FilterViewModel filterViewModel = new FilterViewModel();

            filterViewModel = JsonConvert.DeserializeObject<FilterViewModel>(modela);

            //Filters
            filterViewModel = getFilteredProducts(filterViewModel.MinPrice,
                filterViewModel.MaximumPrice, filterViewModel.SexCheck, filterViewModel.SortBy,
                filterViewModel.CategoryCheckIds, filterViewModel.SearchString, filterViewModel.SizeCheck);

            //Get search string           
            if (!String.IsNullOrEmpty(filterViewModel.SearchString))
            {
                filterViewModel.productByPriceList = filterViewModel.productByPriceList.Where(b => b.productName.ToLower().
                Contains(filterViewModel.SearchString.ToLower()));
            }


            switch (filterViewModel.SortBy)
            {
                case 0:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.categoryId);
                    break;
                case 1:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.amount);
                    break;
                case 2:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderBy(p => p.price);
                    break;
                default:
                    break;
            }

            if (filterViewModel.productByPriceList == null || filterViewModel.productByCategoryList == null)
            {
                #region set Page
                if (page == null) page = 1;

                int pageSize2 = 6;

                int pageNumber2 = (page ?? 1);

                filterViewModel.ProductViewModel.Page = (int)page;

                #endregion


                filterViewModel.ProductViewModel.ProductPagedList = filterViewModel.ProductList.ToPagedList(filterViewModel.ProductViewModel.Page, pageSize2);
                filterViewModel.ProductViewModel.Sexs = db.Products.GroupBy(p => p.sex)
                        .Select(p => p.Key).ToList();
                filterViewModel.ProductViewModel.Sizes = db.Stocks.GroupBy(p => p.size)
                    .Select(p => p.Key).ToList();
                filterViewModel.ProductViewModel.Categories = db.Categories.Select(p => p).ToList();

                return PartialView("Index", filterViewModel);
            }

            #region get products
            var tempProdList = filterViewModel.productByPriceList.ToList();
            var productList = new List<Product>();


            foreach (var item in tempProdList)
            {
                if (filterViewModel.productByCategoryList.Contains(item))
                {
                    filterViewModel.ProductList.Add(item);
                }
            }
            #endregion


            #region set Page
            if (page == null) page = 1;

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            filterViewModel.ProductViewModel.Page = (int)page;

            #endregion


            filterViewModel.ProductViewModel.ProductPagedList = filterViewModel.ProductList.ToPagedList(filterViewModel.ProductViewModel.Page, pageSize);
            filterViewModel.ProductViewModel.Sexs = db.Products.GroupBy(p => p.sex)
                    .Select(p => p.Key).ToList();
            filterViewModel.ProductViewModel.Sizes = db.Stocks.GroupBy(p => p.size)
                    .Select(p => p.Key).ToList();
            var categories = db.Categories.Select(p => p);
            filterViewModel.ProductViewModel.Categories = categories.ToList();

            return PartialView("Index", filterViewModel);
        }
        [HttpPost]
        public PartialViewResult FilteredProducts(FormCollection formCollection)

        {
            FilterViewModel filterViewModel = new FilterViewModel();

            #region Get list sexs
            string temp = formCollection["CheckboxSex"];

            if (temp != null)
            {
                string[] sex = temp.Split(',');

                filterViewModel.SexCheck = new List<string>();
                foreach (var item in sex)
                {
                    filterViewModel.SexCheck.Add(item);
                }
            }
            else
                filterViewModel.SexCheck = null;
            #endregion


            #region Get list cates
            temp = formCollection["CheckboxCate"];

            if (temp != null)
            {
                string[] cates = temp.Split(',');

                filterViewModel.CategoryCheckIds = new List<int>();
                foreach (var item in cates)
                {

                    filterViewModel.CategoryCheckIds.Add(Convert.ToInt32(item));
                }
            }
            else
                filterViewModel.CategoryCheckIds = null;
            #endregion

            #region checked size    
            temp = formCollection["CheckboxSize"];
            if (temp != null)
            {
                string[] size = temp.Split(',');

                filterViewModel.SizeCheck = new List<int>();
                foreach (var item in size)
                {
                    filterViewModel.SizeCheck.Add(Convert.ToInt32(item));
                }
            }
            else
            {
                filterViewModel.SizeCheck = null;
            }
            #endregion
            int sortby = (formCollection["sortBy"] == null) ? 0 : Convert.ToInt32(formCollection["sortBy"]);
            double? MaximumPrice = (formCollection["maxprice"] == null || formCollection["maxprice"] == "") ? 0 : Convert.ToDouble(formCollection["maxprice"]);
            double? MinPrice = (formCollection["minprice"] == null || formCollection["minprice"] == "") ? 0 : Convert.ToDouble(formCollection["minprice"]);

            filterViewModel.SortBy = sortby;
            filterViewModel.MaximumPrice = MaximumPrice;
            filterViewModel.MinPrice = MinPrice;
            filterViewModel.SortBy = sortby;

            //Get sizes

            //Filters
            filterViewModel = getFilteredProducts(filterViewModel.MinPrice,
                filterViewModel.MaximumPrice, filterViewModel.SexCheck, filterViewModel.SortBy,
                filterViewModel.CategoryCheckIds, filterViewModel.SearchString, filterViewModel.SizeCheck);

            if (filterViewModel.productByPriceList == null || filterViewModel.productByCategoryList == null)
            {

                filterViewModel.ProductViewModel.ProductPagedList = filterViewModel.ProductList.ToPagedList(1, 6);
                filterViewModel.ProductViewModel.Sexs = db.Products.GroupBy(p => p.sex)
                        .Select(p => p.Key).ToList();
                filterViewModel.ProductViewModel.Sizes = db.Stocks.GroupBy(p => p.size)
                    .Select(p => p.Key).ToList();
                filterViewModel.ProductViewModel.Categories = db.Categories.Select(p => p).ToList();

                return PartialView("Index", filterViewModel);
            }
            //Get search string
            filterViewModel.SearchString = formCollection["SearchString"];
            if (!String.IsNullOrEmpty(filterViewModel.SearchString))
            {
                filterViewModel.productByPriceList = filterViewModel.productByPriceList.Where(b => b.productName.ToLower().
                Contains(filterViewModel.SearchString.ToLower()));
            }


            switch (filterViewModel.SortBy)
            {
                case 0:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.categoryId);
                    break;
                case 1:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderByDescending(p => p.amount);
                    break;
                case 2:
                    filterViewModel.productByPriceList = filterViewModel.productByPriceList.OrderBy(p => p.price);
                    break;
                default:
                    break;
            }


            #region get products
            var tempProdList = filterViewModel.productByPriceList.ToList();
            var productList = new List<Product>();


            foreach (var item in tempProdList)
            {
                if (filterViewModel.productByCategoryList.Contains(item))
                {
                    filterViewModel.ProductList.Add(item);
                }
            }
            #endregion

            filterViewModel.ProductViewModel.ProductPagedList = filterViewModel.ProductList.ToPagedList(1, 6);
            filterViewModel.ProductViewModel.Sexs = db.Products.GroupBy(p => p.sex)
                    .Select(p => p.Key).ToList();
            filterViewModel.ProductViewModel.Sizes = db.Stocks.GroupBy(p => p.size)
                    .Select(p => p.Key).ToList();
            var categories = db.Categories.Select(p => p);
            filterViewModel.ProductViewModel.Categories = categories.ToList();

            return PartialView("Index", filterViewModel);
        }
        public FilterViewModel getFilteredProducts(double? minimumPrice,
            double? maximumPrice, List<string> SexCheck, int? sortBy, List<int> categoryCheckIds,
            string searchString, List<int> SizeCheck)
        {
            var categories = db.Categories.Select(p => p);


            if (maximumPrice == 0 || maximumPrice == null) maximumPrice = db.Products.OrderByDescending(p => p.price).First().price;


            FilterViewModel filterViewModel = new FilterViewModel();
            filterViewModel.MaximumPrice = maximumPrice;
            filterViewModel.MinPrice = minimumPrice;
            filterViewModel.SortBy = sortBy;
            filterViewModel.CategoryCheckIds = categoryCheckIds;
            filterViewModel.SexCheck = SexCheck;
            filterViewModel.SizeCheck = SizeCheck;
            filterViewModel.SearchString = searchString;

            if (SexCheck == null || categoryCheckIds == null || SizeCheck == null)
            {
                filterViewModel.productByPriceList = null;
                filterViewModel.productByCategoryList = null;
                return filterViewModel;
            }

            //set productCateList by cate, sex, size 
            foreach (var id in filterViewModel.CategoryCheckIds)
            {

                foreach (var sex in SexCheck)
                {
                    var temp = db.Products.Where(p => p.categoryId == id).Where(p => p.price >= minimumPrice && p.price <= maximumPrice).Where(p => p.sex.Contains(sex))
                          .Select(p => p).OrderByDescending(p => p.categoryId)
                          .Include(p => p.Category).Include(p => p.Stocks)
                          .Include(p => p.imagesProducts).ToList();
                    foreach (var size in SizeCheck)
                    {
                        foreach (var item in temp)
                        {
                            if (item.Stocks.Select(p => p.size).Contains(size))
                                filterViewModel.productByCategoryList.Add(item);
                        }
                    }
                }
            }
            //Set productPriceList
            filterViewModel.productByPriceList = db.Products.Where(p => p.price >= minimumPrice && p.price <= maximumPrice)
                    .Select(p => p).OrderByDescending(p => p.categoryId)
                    .Include(p => p.Category).Include(p => p.Stocks)
                    .Include(p => p.imagesProducts);
            return filterViewModel;
        }
    }
}