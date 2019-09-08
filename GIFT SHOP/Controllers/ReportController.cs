using GIFT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Sales(DateTime? gte, DateTime? lte)
        {
            try
            {
                ViewBag.gte = gte;
                ViewBag.lte = lte;
                var sales = db.Sales.Where(x => x.S_date >= gte && x.S_date <= lte).ToList();
                return View(sales);
            }
            catch
            {
                var sales = db.Sales.ToList();
                return View(sales);
            }
        }
        // GET: AdminSaleDetails
        public async Task<ActionResult> Bill(int? id, int? uid)
        {
            ViewBag.Prosum = db.SaleDetails.Where(x => x.Sale_ID == id && x.U_ID == uid).Select(x => x.Pro_Price).Sum();
            ViewBag.FName = db.SaleDetails.Where(x => x.Sale_ID == id && x.U_ID == uid).Select(x => x.User.U_name).FirstOrDefault();
            ViewBag.LName = db.SaleDetails.Where(x => x.Sale_ID == id && x.U_ID == uid).Select(x => x.User.U_lastname).FirstOrDefault();
            var saleDetails = db.SaleDetails.Include(s => s.Sale).Include(s => s.User);
            var iiiiii = await saleDetails.Where(x => x.U_ID == uid && x.Sale_ID == id).ToListAsync();
            return View(await saleDetails.Where(x => x.U_ID == uid && x.Sale_ID == id).ToListAsync());
        }
        // GET: Report
        public ActionResult Users()
        {
            return View(db.Users.ToList());
        }
    }
}