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
    public class AdminGalleriesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AdminGalleries
        public async Task<ActionResult> Index()
        {
            var galleries = db.Galleries.Include(g => g.Product);
            return View(await galleries.ToListAsync());
        }

        // GET: AdminGalleries/Create
        public ActionResult Create(int? id, string name)
        {
            ViewBag.P_ID = id;
            ViewBag.P_NAME = name;
            return View();
        }

        // POST: AdminGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Gallery_ID,P_ID,Gallery_Name")] Gallery gallery, HttpPostedFileBase Gallery_Name)
        {
            if (ModelState.IsValid)
            {
                if (Gallery_Name.ContentLength > 0)
                {

                    string FileName = Path.GetFileName(Gallery_Name.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    Gallery_Name.SaveAs(FolderPath);
                    gallery.Gallery_Name = FileName;

                    db.Galleries.Add(gallery);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "AdminProducts");
                }
            }

            ViewBag.P_ID = new SelectList(db.Products, "P_ID", "P_name", gallery.P_ID);
            return View(gallery);
        }

        // GET: AdminGalleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: AdminGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gallery gallery = await db.Galleries.FindAsync(id);
            db.Galleries.Remove(gallery);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "AdminProducts");
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
