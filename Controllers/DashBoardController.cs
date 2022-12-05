using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class DashBoardController : Controller
    {
        private sneakerShopEntities db = new sneakerShopEntities();

        // GET: DashBoard
        [Authorize(Roles = "Admin, Shiper")]
        public ActionResult Index()
        {
            //ViewBag.ID = User.Identity.GetUserId();
            //ViewBag.AdminName = User.Identity.GetUserName();
            //ViewBag.Role = User.IsInRole("Admin");
            ViewBag.countDay = countProduct_inDay();
            ViewBag.countMouth = countProduct_inMonth();
            ViewBag.listMostAmount = theMostAmount_inMonth(10);

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ViewResult thongkeTongDoanhThuTheoHang()
        {
            List<ThongKeTheoHangViewModel> model = new List<ThongKeTheoHangViewModel>();
            
            var listofCategory = db.Categories.ToList();           

            foreach (var cate in listofCategory)
            {
                ThongKeTheoHangViewModel temp = new ThongKeTheoHangViewModel();
                temp.Category = cate;
                temp.DoanhThu = 0;
                foreach (var prod in cate.Products)
                {
                    temp.DoanhThu += (long)(prod.amount * prod.price);
                }
                if (temp.DoanhThu != 0)
                    model.Add(temp);
            }

            List<String> listofBrand = new List<string>();
            foreach (var item in model)
            {
                listofBrand.Add(item.Category.categoryName);
            }

            List<long> listofIncome = new List<long>();
            foreach (var item in model)
            {
                listofIncome.Add(item.DoanhThu);
            }

            ViewBag.Brands = listofBrand;
            ViewBag.Incomes = listofIncome;
            

            return View("tongdoanhthutheohang");
        }
        //public List<ThongKeTheoHangViewModel> thongkeDoanhThuTheoHang(DateTime ngayBatDau, DateTime ngayKetThuc)
        //{
        //    List<ThongKeTheoHangViewModel> model = new List<ThongKeTheoHangViewModel>();

        //    var listofCategory = db.Categories.ToList();
        //    var listofOrder = db.Orders.Where(p => p.NgayHoanThanh >= ngayBatDau && p.NgayHoanThanh <= ngayKetThuc)
        //        .Select(p => p.Cart);

        //    foreach (var cate in listofCategory)
        //    {
        //        ThongKeTheoHangViewModel temp = new ThongKeTheoHangViewModel();
        //        temp.Category = cate;
        //        temp.DoanhThu = 0;
        //        foreach (var cart in listofOrder)
        //        {
        //            foreach (var item in cart.CartItems)
        //            {
        //                temp.DoanhThu += Convert.ToInt64(item.quantity * item.unitPrice);
        //            }
                    
        //        }
        //        if (temp.DoanhThu != 0)
        //            model.Add(temp);
        //    }

        //    return model;
        //}
        public ViewResult thongkeTongDoanhThuTheoGioiTinh()
        {
            List<ThongKeTheoHangViewModel> model = new List<ThongKeTheoHangViewModel>();

            var listofSex = db.Products.GroupBy(p => p.sex).Select(g => g.Key).ToList();

            foreach (var sex in listofSex)
            {
                ThongKeTheoHangViewModel temp = new ThongKeTheoHangViewModel();
                temp.Category.categoryName = sex;
                temp.DoanhThu = 0;
                foreach (var prod in db.Products)
                {
                    if (prod.sex == sex)
                        temp.DoanhThu += (long)(prod.amount * prod.price);
                }
                if (temp.DoanhThu != 0)
                    model.Add(temp);
            }

            List<String> listofBrand = new List<string>();
            foreach (var item in model)
            {
                listofBrand.Add(item.Category.categoryName);
            }

            List<long> listofIncome = new List<long>();
            foreach (var item in model)
            {
                listofIncome.Add(item.DoanhThu);
            }

            ViewBag.Brands = listofBrand;
            ViewBag.Incomes = listofIncome;
    
            

            return View("tongdoanhthutheogioitinh");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DonTrongNgay()
        {
            DateTime today = DateTime.Today.Date;
            ViewBag.count_DonHangChuaXacNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 0).Count();
            ViewBag.count_DonHangDaXacNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 1).Count();
            ViewBag.count_DonHangShiperDaNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 2).Count();
            ViewBag.count_DonHangDaHoanThanh = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate) == today && p.status == 3).Count();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DonTrongThang()
        {
            int thisMonth = DateTime.Today.Month;
            ViewBag.count_DonHangChuaXacNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 0).Count();
            ViewBag.count_DonHangDaXacNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 1).Count();
            ViewBag.count_DonHangShiperDaNhan = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 2).Count();
            ViewBag.count_DonHangDaHoanThanh = db.Orders.Where(p => DbFunctions.TruncateTime(p.orderDate).Value.Month == thisMonth && p.status == 3).Count();
            return View();
        }

        [Authorize(Roles = "Admin, Shiper")]
        public int countProduct_inDay()
        {
            int count = 0;
            //List<Order> orders = db.Orders.Where(o => o.orderDate.Value.Date == DateTime.Today.Date).ToList();

            var orders = db.Orders.ToList();
            

            foreach (var item in orders)
            {
                if (item.orderDate != null)
                {
                    if (item.orderDate.Value.Date == DateTime.Today.Date)
                    {
                        count++;
                    }
                }    
               
            }

            //count = orders.Count;
            //return View(orders.ToList());

            return count;
        }


        [Authorize(Roles = "Admin, Shiper")]
        public int countProduct_inMonth()
        {
            int count = 0;
            var orders = db.Orders.ToList();

            foreach (var item in orders)
            {
                if (item.orderDate != null)
                {
                    if (item.orderDate.Value.Month == DateTime.Today.Month)
                    {
                        count++;
                    }
                }
            }

            return count;
        }


        [Authorize(Roles = "Admin, Shiper")]
        public List<Product> theMostAmount_inMonth(int number)
        {
            //List<Product> list = new List<Product>();
            //var orderDetails = db.OrderDetails.Where(o => o.Order.orderDate.Value.Month == DateTime.Today.Month).Include(o => o.Order).Include(o => o.Stock);


            //return list;

            var product = db.Products.Include(p => p.Category).Include(p => p.Stocks).Include(p => p.imagesProducts);
            //Sắp xếp
            product = product.OrderByDescending(s => s.amount);

            return product.ToList().GetRange(0, number);
        }
    }
}