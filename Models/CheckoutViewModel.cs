using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> cartItems { get; set; }
        public string imageProduct { get; set; }
    }
}