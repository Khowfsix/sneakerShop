using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class CheckoutRequest
    {
        public string Name { get; set; }   
        public string Phone { get; set; }
        public int paymentType { get; set; }
        public string address { get; set; } 
        public List<OrderVM> orderDetail { get; set; }
    }
}