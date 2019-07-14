using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GIFT_SHOP.Models;

namespace GIFT_SHOP.Controllers
{
    public class AdminLoginController : Controller
    {
        private Database1Entities db = new Database1Entities();
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(GIFT_SHOP.Models.User userModel)
        {
            var userDetail = db.Users.Where(x => x.U_username == userModel.U_username && x.U_password == userModel.U_password).FirstOrDefault();
            if (userDetail == null)
            {
                userModel.LoginErrorMessage = "Email หรือ เบอร์โทรศัพท์ไม่ถูกต้อง";
                return View("Index", userModel);
            }
            else
            {
                Session["User_Email"] = userDetail.U_mail;
                Session["User_Id"] = userDetail.U_ID;
                Session["User_username"] = userDetail.U_username;
                Session["User_Name"] = userDetail.U_name;
                Session["User_Lastname"] = userDetail.U_lastname;
                Session["User_Tel"] = userDetail.U_tel;
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}