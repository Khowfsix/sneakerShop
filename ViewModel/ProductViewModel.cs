using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProductViewModel
    {
        public IPagedList<Product> ProductPagedList { get; set; }
        public int MaximumPrice { get; set; }
        public int MinPrice { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        //public Pager Pager { get; set; }
        public string SearchString { get; set; }
        public int? ShopStyle { get; set; }
        public List<int> CategoryCheckIds { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }
        public List<int> SizeCheckIds { get; set; }
    }
}