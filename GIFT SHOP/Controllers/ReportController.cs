using GIFT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GIFT_SHOP.Controllers
{
    public class ReportController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        // GET: Report
        public ActionResult Products()
        {
            var products = db.Products;
            return View(products.ToList());
        }
        // GET: Report
        public ActionResult Sales()
        {
            var sales = db.Sales.ToList();
            return View(sales);
        }
        // GET: Report
        public ActionResult Users()
        {
            return View(db.Users.ToList());
        }
    }
}