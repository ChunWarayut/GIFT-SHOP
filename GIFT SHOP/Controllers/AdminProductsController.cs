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
        public async Task<ActionResult> Index(string keyword)
        {

            if (keyword == " " || keyword == null)
            {
                var products = db.Products.Include(p => p.Category);
                return View(await products.ToListAsync());
            }
            else
            {
                return View(db.Products.Where(x => x.P_name.Contains(keyword)).ToList());
            }
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
        public async Task<ActionResult> Create([Bind(Include = "P_ID,P_name,Ca_ID,P_img,P_img_1,P_img_2,P_img_3,P_price,P_colur,P_size,P_texture,P_length,P_chest_waistline,P_amount")] Product product, HttpPostedFileBase P_img, HttpPostedFileBase P_img_1, HttpPostedFileBase P_img_2, HttpPostedFileBase P_img_3)
        {
            if (ModelState.IsValid)
            {

                    product.P_img = "default-image.jpg";

                    product.P_img_1 = "default-image.jpg";

                    product.P_img_2 = "default-image.jpg";

                    product.P_img_3 = "default-image.jpg";

                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
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
        public async Task<ActionResult> Edit([Bind(Include = "P_ID,P_name,Ca_ID,P_img,P_img_1,P_img_2,P_img_3,P_price,P_colur,P_size,P_texture,P_length,P_chest_waistline,P_amount")] Product product, HttpPostedFileBase P_img)
        {

            Product products = db.Products.Find(product.P_ID);


            //product.P_img = products.P_img;
            //product.P_img_1 = products.P_img_1;
            //product.P_img_2 = products.P_img_2;
            //product.P_img_3 = products.P_img_3;


            if (ModelState.IsValid)
            {
                //string FileName = Path.GetFileName(P_img.FileName);
                //string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                //P_img.SaveAs(FolderPath);
                //product.P_img = FileName;

                //string FileName1 = Path.GetFileName(P_img_1.FileName);
                //string FolderPath1 = Path.Combine(Server.MapPath("~/img/products"), FileName1);
                //P_img_1.SaveAs(FolderPath1);
                //product.P_img_1 = FileName1;

                //string FileName2 = Path.GetFileName(P_img_1.FileName);
                //string FolderPath2 = Path.Combine(Server.MapPath("~/img/products"), FileName2);
                //P_img_2.SaveAs(FolderPath2);
                //product.P_img_2 = FileName2;

                //string FileName3 = Path.GetFileName(P_img_1.FileName);
                //string FolderPath3 = Path.Combine(Server.MapPath("~/img/products"), FileName3);
                //P_img_3.SaveAs(FolderPath3);
                //product.P_img_3 = FileName3;

                var update = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                if (update != null)
                {
                    update.P_name = product.P_name;
                    update.Ca_ID = product.Ca_ID;
                    update.P_price = product.P_price; 
                    update.P_colur = product.P_colur;
                    update.P_size = product.P_size;
                    update.P_texture = product.P_texture;
                    update.P_length = product.P_length;
                    update.P_chest_waistline = product.P_chest_waistline;
                    update.P_amount = product.P_amount;
                    //update.P_img = products.P_img;
                    //update.P_img_1 = products.P_img_1;
                    //update.P_img_2 = products.P_img_2;
                    //update.P_img_3 = products.P_img_3;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewBag.Ca_ID = new SelectList(db.Categories, "Ca_ID", "Ca_name", product.Ca_ID);
            return View(product);
        }


        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit_1([Bind(Include = "P_ID,P_img,P_img_1,P_img_2,P_img_3")] Product product, HttpPostedFileBase P_img)
        {
            if (ModelState.IsValid)
            {
                
                var update = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                if (update != null)
                {
                    string FileName = Path.GetFileName(P_img.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img.SaveAs(FolderPath);
                    update.P_img = FileName;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new
                {
                    id = product.P_ID
                });

            }
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit_2([Bind(Include = "P_ID,P_img,P_img_1,P_img_2,P_img_3")] Product product, HttpPostedFileBase P_img_1)
        {
            if (ModelState.IsValid)
            {
                var update = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                if (update != null)
                {
                    string FileName = Path.GetFileName(P_img_1.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img_1.SaveAs(FolderPath);
                    update.P_img_1 = FileName;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new
                {
                    id = product.P_ID
                });

            }
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit_3([Bind(Include = "P_ID,P_img,P_img_1,P_img_2,P_img_3")] Product product, HttpPostedFileBase P_img_2)
        {
            if (ModelState.IsValid)
            {

                var update = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                if (update != null)
                {
                    string FileName = Path.GetFileName(P_img_2.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img_2.SaveAs(FolderPath);
                    update.P_img_2 = FileName;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new
                {
                    id = product.P_ID
                });

            }
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit_4([Bind(Include = "P_ID,P_img,P_img_1,P_img_2,P_img_3")] Product product, HttpPostedFileBase P_img_3)
        {
            if (ModelState.IsValid)
            {
               var update = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                var updsaste = db.Products.Where(o => o.P_ID == product.P_ID).FirstOrDefault();
                if (update != null)
                {
                    string FileName = Path.GetFileName(P_img_3.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/img/products"), FileName);
                    P_img_3.SaveAs(FolderPath);
                    update.P_img_3 = FileName;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new
                {
                    id = product.P_ID
                });

            }
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
