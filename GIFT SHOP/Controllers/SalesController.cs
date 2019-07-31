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
using System.IO;

namespace GIFT_SHOP.Controllers
{
    public class SalesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Sales
        public async Task<ActionResult> Index()
        {
            var uid = Convert.ToInt32(Session["User_ID"]);
            var sales = db.Sales.OrderByDescending(x => x.S_ID).Where(x => x.U_ID == uid).Include(s => s.Delivery).Include(s => s.User).Include(s => s.Stau);
            return View(await sales.ToListAsync());
        }

        // GET: Sales/Create
        public ActionResult Create(int? price)
        {
            ViewBag.Price = price;
            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name");
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username");
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "S_ID,S_date,U_ID,S_add,D_ID,S_sum,S_slip,Staus_ID,S_packagenumber")] Sale sale, HttpPostedFileBase S_slip)
        {
            if (ModelState.IsValid)
            {

                var ini = S_slip;

                //if (S_slip != null)
                if (S_slip != null)
                {
                    string FileName = Path.GetFileName(S_slip.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/slips"), FileName);
                    S_slip.SaveAs(FolderPath);
                    sale.S_slip = FileName;
                }
                else
                {
                    sale.S_slip = "default-image.jpg";
                }
                if (sale.D_ID == 1)
                {
                    sale.S_sum = sale.S_sum + 30;
                }
                if (sale.D_ID == 2)
                {
                    sale.S_sum = sale.S_sum + 50;
                }
                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                var uid = Convert.ToInt32(Session["User_ID"]);
                var update = await db.SaleDetails.Where(x => x.U_ID == uid && x.Sale_ID == 1).ToListAsync();
                var ProID = db.Sales.OrderByDescending(x => x.S_ID).Select(x => x.S_ID).FirstOrDefault();
                if (update.Count() > 0)
                {
                    update.ForEach(x => { x.Sale_ID = ProID; });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name", sale.D_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", sale.U_ID);
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name", sale.Staus_ID);
            return View(sale);
        }

        // GET: Sales/Edit/5
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

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "S_ID,S_date,U_ID,S_add,D_ID,S_sum,S_slip,Staus_ID,S_packagenumber")] Sale sale, HttpPostedFileBase S_slip)
        {
            if (S_slip.ContentLength > 0)
            {
                string FileName = Path.GetFileName(S_slip.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/img/slips"), FileName);
                S_slip.SaveAs(FolderPath);
                sale.S_slip = FileName;

                db.Entry(sale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.Deliveries, "Delivery_ID", "Delivery_name", sale.D_ID);
            ViewBag.U_ID = new SelectList(db.Users, "U_ID", "U_username", sale.U_ID);
            ViewBag.Staus_ID = new SelectList(db.Staus, "Staus_ID", "Staus_name", sale.Staus_ID);
            return View(sale);
        }

        // GET: Sales/Delete/5
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

        // POST: Sales/Delete/5
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
