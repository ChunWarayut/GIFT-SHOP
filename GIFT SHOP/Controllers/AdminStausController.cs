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
    public class AdminStausController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AdminStaus
        public async Task<ActionResult> Index()
        {
            return View(await db.Staus.ToListAsync());
        }

        // GET: AdminStaus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stau stau = await db.Staus.FindAsync(id);
            if (stau == null)
            {
                return HttpNotFound();
            }
            return View(stau);
        }

        // GET: AdminStaus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminStaus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Staus_ID,Staus_name")] Stau stau)
        {
            if (ModelState.IsValid)
            {
                db.Staus.Add(stau);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stau);
        }

        // GET: AdminStaus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stau stau = await db.Staus.FindAsync(id);
            if (stau == null)
            {
                return HttpNotFound();
            }
            return View(stau);
        }

        // POST: AdminStaus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Staus_ID,Staus_name")] Stau stau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stau).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stau);
        }

        // GET: AdminStaus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stau stau = await db.Staus.FindAsync(id);
            if (stau == null)
            {
                return HttpNotFound();
            }
            return View(stau);
        }

        // POST: AdminStaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stau stau = await db.Staus.FindAsync(id);
            db.Staus.Remove(stau);
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
