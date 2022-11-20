using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            ViewBag.ID = User.Identity.GetUserId();
            ViewBag.AdminName = User.Identity.GetUserName();
            ViewBag.Role = User.IsInRole("Admin");
            return View();
        }
    }
}