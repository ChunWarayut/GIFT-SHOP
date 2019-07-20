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
    public class SaleDetailsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: SaleDetails
        public async Task<ActionResult> Index()
        {
            var uid = Convert.ToInt32(Session["User_ID"]);
            var pro_0 = db.SaleDetails.Where(x => x.Sale_ID == 1 && x.U_ID == uid).ToList();
            if (pro_0.Count() > 0)
            {
                ViewBag.Prosum = db.SaleDetails.Where(x => x.Sale_ID == 1 && x.U_ID == uid).Select(x => x.Pro_Price).Sum();
            }
            else
            {
                ViewBag.Prosum = 0;
            }
            var orderDetails = db.SaleDetails.OrderBy(x => x.Sale_ID).Include(o => o.Product);
            return View(await orderDetails.Where(x => x.Sale_ID == 1 && x.U_ID == uid).ToListAsync());

            //var saleDetails = db.SaleDetails.Include(s => s.Sale).Include(s => s.User);
            //return View(await saleDetails.ToListAsync());
        }

        // GET: SaleDetails/Create
        public ActionResult Create(decimal? price, int? id)
        {
            ViewBag.Price = price;
            ViewBag.Pro_Id = id;
            ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add");
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "S_ID,P_ID,Sd_number,Sale_ID,Pro_Price,U_ID")] SaleDetail saleDetail, decimal Sd_number, decimal Price)
        {
            if (ModelState.IsValid)
            {
                var uid = Convert.ToInt32(Session["User_ID"]);
                saleDetail.Pro_Price = Convert.ToInt32(Price * Sd_number);
                saleDetail.Sale_ID = 1;
                saleDetail.U_ID = uid;
                var update = db.Products.Where(o => o.P_ID == saleDetail.P_ID).FirstOrDefault();
                if (update != null)
                {
                    update.P_amount = Convert.ToInt32(update.P_amount - Sd_number);
                }
                db.SaleDetails.Add(saleDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add", saleDetail.Sale_ID);
            //ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", saleDetail.U_ID);
            return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDetail saleDetail = await db.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add", saleDetail.Sale_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", saleDetail.U_ID);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "S_ID,P_ID,Sd_number,Sale_ID,Pro_Price,U_ID")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add", saleDetail.Sale_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", saleDetail.U_ID);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDetail saleDetail = await db.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return HttpNotFound();
            }
            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SaleDetail saleDetail = await db.SaleDetails.FindAsync(id);
            var update = db.Products.Where(o => o.P_ID == saleDetail.P_ID).FirstOrDefault();
            if (update != null)
            {
                update.P_amount = Convert.ToInt32(update.P_amount + saleDetail.Sd_number);
            }
            db.SaleDetails.Remove(saleDetail);
            await db.SaveChangesAsync();
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
    }
}
