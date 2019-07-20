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
    public class AdminProductsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AdminProducts
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(await products.ToListAsync());
        }

        // GET: AdminProducts/Create
        public ActionResult Create()
        {
            ViewBag.Ca_ID = new SelectList(db.Categories, "Ca_ID", "Ca_name");
            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "P_ID,P_name,Ca_ID,P_img,P_price,P_colur,P_size,P_texture,P_length,P_chest_waistline,P_amount")] Product product, HttpPostedFileBase P_img)
        {
            if (ModelState.IsValid)
            {
                if (P_img.ContentLength > 0)
                {

                    string FileName = Path.GetFileName(P_img.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img.SaveAs(FolderPath);
                    product.P_img = FileName;

                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.Ca_ID = new SelectList(db.Categories, "Ca_ID", "Ca_name", product.Ca_ID);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ca_ID = new SelectList(db.Categories, "Ca_ID", "Ca_name", product.Ca_ID);
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "P_ID,P_name,Ca_ID,P_img,P_price,P_colur,P_size,P_texture,P_length,P_chest_waistline,P_amount")] Product product, HttpPostedFileBase P_img)
        {
            if (ModelState.IsValid)
            {
                if (P_img.ContentLength > 0)
                {

                    string FileName = Path.GetFileName(P_img.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img.SaveAs(FolderPath);
                    product.P_img = FileName;

                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
            }
            ViewBag.Ca_ID = new SelectList(db.Categories, "Ca_ID", "Ca_name", product.Ca_ID);
            return View(product);
        }

        // GET: AdminProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
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
