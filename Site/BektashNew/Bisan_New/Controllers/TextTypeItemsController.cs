using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;

namespace Bisan.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class TextTypeItemsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TextTypeItems
        public ActionResult Index(Guid id)
        {
            var textTypeItems = db.TextTypeItems.Include(t => t.TextType).Where(t=>t.IsDelete==false && t.TextTypeId == id).OrderByDescending(t=>t.SubmitDate);
            return View(textTypeItems.ToList());
        }

      

        // GET: TextTypeItems/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: TextTypeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextTypeItem textTypeItem,Guid id,HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/Text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    textTypeItem.ImageUrl = newFilenameUrl;
                }
                #endregion
                textTypeItem.IsDelete=false;
				textTypeItem.SubmitDate= DateTime.Now;
                textTypeItem.TextTypeId = id;
                textTypeItem.Id = Guid.NewGuid();
                db.TextTypeItems.Add(textTypeItem);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=id});
            }

           
            return View(textTypeItem);
        }

        // GET: TextTypeItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextTypeItem textTypeItem = db.TextTypeItems.Find(id);
            if (textTypeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = textTypeItem.TextTypeId;
            return View(textTypeItem);
        }

        // POST: TextTypeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TextTypeItem textTypeItem,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = textTypeItem.ImageUrl;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    textTypeItem.ImageUrl = newFilenameUrl;
                }
                #endregion
                textTypeItem.IsDelete=false;
                db.Entry(textTypeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=textTypeItem.TextTypeId});
            }
            
            return View(textTypeItem);
        }

        // GET: TextTypeItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextTypeItem textTypeItem = db.TextTypeItems.Find(id);
            if (textTypeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = textTypeItem.TextTypeId;
            return View(textTypeItem);
        }

        // POST: TextTypeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TextTypeItem textTypeItem = db.TextTypeItems.Find(id);
			textTypeItem.IsDelete=true;
			textTypeItem.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id=textTypeItem.TextTypeId});
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
