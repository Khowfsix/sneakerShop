using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class FilterViewModel
    {
        public double? MaximumPrice { get; set; }
        public double? MinPrice { get; set; }
        //public Pager Pager { get; set; }  
        public List<String> SexCheckIds { get; set; }
        public List<int> CategoryCheckIds { get; set; }
        public IQueryable<Product> productList { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }
        public FilterViewModel()
        {
            ProductViewModel = new ProductViewModel();
        }
    }
}