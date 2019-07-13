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
    public class AdminSalesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AdminSales
        public async Task<ActionResult> Index()
        {
            var sales = db.Sales.Include(s => s.Delivery).Include(s => s.User).Include(s => s.Stau);
            return View(await sales.ToListAsync());
        }

        // GET: AdminSales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: AdminSales/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name");
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username");
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name");
            return View();
        }

        // POST: AdminSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "S_ID,S_date,U_ID,S_add,D_ID,S_sum,S_slip,Staus_ID,S_packagenumber")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name", sale.D_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", sale.U_ID);
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name", sale.Staus_ID);
            return View(sale);
        }

        // GET: AdminSales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name", sale.D_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", sale.U_ID);
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name", sale.Staus_ID);
            return View(sale);
        }

        // POST: AdminSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "S_ID,S_date,U_ID,S_add,D_ID,S_sum,S_slip,Staus_ID,S_packagenumber")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name", sale.D_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", sale.U_ID);
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name", sale.Staus_ID);
            return View(sale);
        }

        // GET: AdminSales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: AdminSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sale sale = await db.Sales.FindAsync(id);
            db.Sales.Remove(sale);
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
