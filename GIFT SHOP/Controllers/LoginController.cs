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
    public class LoginController : Controller
    {
        private Database1Entities db = new Database1Entities();
        // GET: Login
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
                userModel.LoginErrorMessage = "Email หรือ Password";
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
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "U_ID,U_username,U_password,U_name,U_lastname,U_tel,U_mail,U_add")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }


                Session["User_Email"] = user.U_mail;
                Session["User_Id"] = user.U_ID;
                Session["User_username"] = user.U_username;
                Session["User_Name"] = user.U_name;
                Session["User_Lastname"] = user.U_lastname;
                Session["User_Tel"] = user.U_tel;
                return RedirectToAction("Index", "Home");
            //return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}