﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.ViewModel;

namespace WebApplication1.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> cartItems { get; set; }
        public CheckoutRequest CheckoutModel { get; set; }
    }
}