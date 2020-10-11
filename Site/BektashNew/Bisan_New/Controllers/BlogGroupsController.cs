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
    public class BlogGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: BlogGroups
        public ActionResult Index()
        {
            return View(db.BlogGroups.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: BlogGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogGroup blogGroup = db.BlogGroups.Find(id);
            if (blogGroup == null)
            {
                return HttpNotFound();
            }
            return View(blogGroup);
        }

        // GET: BlogGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogGroup blogGroup,HttpPostedFileBase fileUpload, HttpPostedFileBase coverUpload)
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

                    blogGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                #region Upload and resize cover if needed
                string newCoverFilenameUrl = string.Empty;
                if (coverUpload != null)
                {
                    string covername = Path.GetFileName(coverUpload.FileName);
                    string newCoverFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(covername);

                    newCoverFilenameUrl = "/Uploads/Blog/" + newCoverFilename;
                    string physicalCovername = Server.MapPath(newCoverFilenameUrl);

                    coverUpload.SaveAs(physicalCovername);

                    blogGroup.CoverImage = newCoverFilenameUrl;
                }
                #endregion
                blogGroup.IsDelete=false;
				blogGroup.SubmitDate= DateTime.Now; 
                blogGroup.Id = Guid.NewGuid();
                db.BlogGroups.Add(blogGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogGroup);
        }

        // GET: BlogGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogGroup blogGroup = db.BlogGroups.Find(id);
            if (blogGroup == null)
            {
                return HttpNotFound();
            }
            return View(blogGroup);
        }

        // POST: BlogGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogGroup blogGroup,HttpPostedFileBase fileUpload, HttpPostedFileBase coverUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = blogGroup.ImageUrl;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blogGroup.ImageUrl = newFilenameUrl;
                }
                #endregion
                #region Upload and resize cover if needed
                string newCoverFilenameUrl = blogGroup.CoverImage;
                if (coverUpload != null)
                {
                    string covername = Path.GetFileName(coverUpload.FileName);
                    string newCoverFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(covername);

                    newCoverFilenameUrl = "/Uploads/Blog/" + newCoverFilename;
                    string physicalCovername = Server.MapPath(newCoverFilenameUrl);

                    coverUpload.SaveAs(physicalCovername);

                    blogGroup.CoverImage = newCoverFilenameUrl;
                }
                #endregion
                blogGroup.IsDelete=false;
                db.Entry(blogGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogGroup);
        }

        // GET: BlogGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogGroup blogGroup = db.BlogGroups.Find(id);
            if (blogGroup == null)
            {
                return HttpNotFound();
            }
            return View(blogGroup);
        }

        // POST: BlogGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BlogGroup blogGroup = db.BlogGroups.Find(id);
			blogGroup.IsDelete=true;
			blogGroup.DeleteDate=DateTime.Now;
 
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
    }
}
