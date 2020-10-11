using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using ViewModels;

namespace Bisan_New.Controllers
{
    public class ProductGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();


        public ActionResult Index()
        {
            var productGroups = db.ProductGroups.Include(p => p.Parent).Where(p => p.IsDelete == false).OrderByDescending(p => p.SubmitDate);
            return View(productGroups.ToList());
        }

        // GET: ProductGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.ProductGroups, "Id", "Title");
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductGroup productGroup, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    productGroup.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                productGroup.IsDelete = false;
                productGroup.SubmitDate = DateTime.Now;
                productGroup.Id = Guid.NewGuid();
                db.ProductGroups.Add(productGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.ProductGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.ProductGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductGroup productGroup, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    productGroup.HeaderImageUrl = newFilenameUrl;
                }
                #endregion
                productGroup.IsDelete = false;
                db.Entry(productGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.ProductGroups, "Id", "Title", productGroup.ParentId);
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            productGroup.IsDelete = true;
            productGroup.DeleteDate = DateTime.Now;

            db.SaveChanges();
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

  

        //public List<ProductGroupWithProduct> GetProductGroup(Guid parentId)
        //{
        //    List<ProductGroup> productGroups = db.ProductGroups.Where(c => c.ParentId == parentId).ToList();

        //    List<ProductGroupWithProduct> productGroup = new List<ProductGroupWithProduct>();

        //    foreach (ProductGroup group in productGroups)
        //    {
        //        productGroup.Add(new ProductGroupWithProduct()
        //        {
        //            ProductGroup = group,
        //            Products = db.Products.Where(c=>c.ProductGroupId==group.Id).ToList()
        //        });
        //    }

        //    return productGroup;
        //}
    }
}
