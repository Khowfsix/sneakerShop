using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Helper;
using Order = WebApplication1.Models.Order;
using System.Data.Entity.Migrations;

namespace WebApplication1.Controllers
{
    public class CartsController : Controller
    {
        private HomeController homeController;
        private PayPal.Api.Payment payment;
        private sneakerShopEntities db = new sneakerShopEntities();
        private double TyGiaUSD = 24815;
        // GET: Carts
        public ActionResult Index()
        {
            var cart = db.Carts.Include(c => c.AspNetUser);
            return View(cart.ToList());

        }
        //GEt : Cart/Checkout

        [Authorize]
        public ActionResult Checkout(int? cartId)
        {
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            //Lấy giỏ hàng của user đang đăng nhập
            var cart = db.Carts.FirstOrDefault(c => c.userId == user.Id);
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock).Where(c => c.cartId == cartId);
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            checkoutViewModel.cartItems = cartItems.ToList();
            checkoutViewModel.username = user.UserName;
            return View(checkoutViewModel);
        }
        // POST: Carts/Checkout
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(FormCollection formCheckout)
        {
            var cartId = Convert.ToInt32(formCheckout["cartId"]);
            var cart = db.Carts.Where(c => c.cartId == cartId).FirstOrDefault();
            var userId = User.Identity.GetUserId();
            //Lấy user đang đăng nhập
            var user = db.AspNetUsers.Where(c => c.Id.Equals(userId)).FirstOrDefault();
            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Stock).Where(c => c.cartId == cart.cartId).ToList();
            double totalItem = 0;
            foreach (var cartItem in cartItems)
            {
                totalItem += (double)(cartItem.quantity * cartItem.unitPrice);
            }
            Order order = new Order();
            order.orderDate = DateTime.Now;
            order.address = formCheckout["address"];
            order.userID = user.Id;
            order.cartID = cartId;
            order.status = 0;
            order.shipping = 0;
            order.totalPay = (long)(totalItem + totalItem * 8 / 100);
            order.paymentType = int.Parse(formCheckout["optradio"]);

            order.customerName = formCheckout["customerName"];
            order.numberPhone = formCheckout["numberPhone"];
            order.Email = formCheckout["email"];

            db.Orders.Add(order);
            db.SaveChanges();
            CreateOrderDetail(cartItems, order.orderID);
            /*
            if (order.paymentType == 3)
            {
                return RedirectToAction("PaymentWithPaypal", "Carts", new { order = order, cartItems = cartItems });
            }*/

            return RedirectToAction(actionName: "OrderComplete", controllerName: "Carts", new { cartId = cartId });
        }

        public void CreateOrderDetail(List<CartItem> cartItems, int orderID)
        {
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail();
                orderDetail.orderID = orderID;
                orderDetail.stockID = cartItem.Stock.stockID;
                orderDetail.quantity = cartItem.quantity;
                orderDetail.unitPrice = cartItem.unitPrice;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }
        //GET/Carts/OrderComplete
        public ActionResult OrderComplete(int cartId)
        {
            ClearCart(cartId);
            return View();
        }
        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartId,userId,buyDate,status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartId,userId,buyDate,status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "userId", "username", cart.userId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
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

        private void ClearCart(int cartId)
        {
            var cartItems = from cartitems in db.CartItems
                            where cartitems.cartId == cartId
                            select cartitems;
            foreach (var item in cartItems)
            {
                db.CartItems.Remove(item);
            }
            db.SaveChanges();

        }
        /*
        public ActionResult PaymentWithPaypal(Order order, List<CartItem> cartItems)
        {
            APIContext apiContext = Configuration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Carts/PaymentWithPayPal?";
                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, order, cartItems);
                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        ViewBag.KQ = "That Bai";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                PayPalLogger.Log("Error" + ex.Message);
                ViewBag.KQ = "That Bai";
                return View();
            }
            ViewBag.KQ = "Thanh Cong";
            return View();
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, Order order, List<CartItem> cartItems)
        {
            var itemList = new ItemList() { items = new List<Item>() };
            //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
            //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
            double tax = 1;
            double shiping = 1;
            foreach (var item in cartItems)
            {
                var product = db.Products.Find(item.productId);
                itemList.items.Add(new Item()
                {
                    //Thông tin đơn hàng
                    name = product.productName,
                    currency = "USD",
                    price = product.price.ToString(),
                    quantity = item.quantity.ToString(),
                    sku = "sku"
                });

            }
            //Hình thức thanh toán qua paypal
            var payer = new Payer() { payment_method = "paypal" };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            //các thông tin trong đơn hàng
            var details = new Details()
            {
                tax = tax.ToString(),
                shipping = shiping.ToString(),
                subtotal = (order.totalPay / TyGiaUSD).ToString()
            };
            //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
            var amount = new Amount()
            {
                currency = "USD",
                total = (tax + shiping + order.totalPay / TyGiaUSD).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };
            var transactionList = new List<Transaction>();
            //Tất cả thông tin thanh toán cần đưa vào transaction
            transactionList.Add(new Transaction()
            {
                description = "Thanh toan paypal",
                invoice_number = order.cartID.ToString() + Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            var temp = this.payment.Create(apiContext);
            return temp;
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }**/
    }
}