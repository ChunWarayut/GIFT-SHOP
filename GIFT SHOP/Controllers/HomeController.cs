using GIFT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GIFT_SHOP.Controllers
{
    public class HomeController : Controller
    {
        private Database1Entities db = new Database1Entities();
        public ActionResult Index(string keyword)
        {
            //Session["User_ID"] = 1;
            if (keyword == " " || keyword == null)
            {
                return View(db.Products.ToList());
            }
            else
            {
                return View(db.Products.Where(x => x.Category.Ca_name.Contains(keyword)).ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}