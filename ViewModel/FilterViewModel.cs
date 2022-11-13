using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class FilterViewModel
    {
        public int? MaximumPrice { get; set; }
        public int? MinPrice { get; set; }
        public List<string> SexList { get; set; }
        public List<int> CategoryIdList { get; set; }
        public int? SortBy { get; set; }
        public string SearchString { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }
        //public List<int> SizeCheckIds { get; set; }
        //public int? PageSize { get; set; }
        //public int InitialMaximumPrice { get; set; }
    }
}