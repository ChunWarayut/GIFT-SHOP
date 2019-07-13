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
    public class AdminSaleDetailsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AdminSaleDetails
        public async Task<ActionResult> Index()
        {
            var saleDetails = db.SaleDetails.Include(s => s.Sale).Include(s => s.User);
            return View(await saleDetails.ToListAsync());
        }

        // GET: AdminSaleDetails/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: AdminSaleDetails/Create
        public ActionResult Create()
        {
            ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add");
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username");
            return View();
        }

        // POST: AdminSaleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "S_ID,P_ID,Sd_number,Sale_ID,Pro_Price,U_ID")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                db.SaleDetails.Add(saleDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Sale_ID = new SelectList(db.Sales, "S_ID", "S_add", saleDetail.Sale_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", saleDetail.U_ID);
            return View(saleDetail);
        }

        // GET: AdminSaleDetails/Edit/5
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

        // POST: AdminSaleDetails/Edit/5
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

        // GET: AdminSaleDetails/Delete/5
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

        // POST: AdminSaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SaleDetail saleDetail = await db.SaleDetails.FindAsync(id);
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
