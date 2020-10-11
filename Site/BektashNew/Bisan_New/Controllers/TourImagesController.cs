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
    public class TourImagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TourImages
        public ActionResult Index(Guid id)
        {
            Tour tour = db.Tours.Find(id);
            List<TourImage> tourImages = db.TourImages.Include(t => t.Tour).Where(t=>t.IsDelete==false && t.TourId==id).OrderByDescending(t=>t.SubmitDate).ToList();
            ViewBag.id = tour.TourCategoryId;
            return View(tourImages);
        }


        // GET: TourImages/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: TourImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TourImage tourImage,Guid id,HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    tourImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                tourImage.IsDelete=false;
				tourImage.SubmitDate= DateTime.Now; 
                tourImage.Id = Guid.NewGuid();
                tourImage.TourId = id;
                db.TourImages.Add(tourImage);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=id});
            }

            
            return View(tourImage);
        }

        // GET: TourImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourImage tourImage = db.TourImages.Find(id);
            if (tourImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = tourImage.TourId;
            return View(tourImage);
        }

        // POST: TourImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourImage tourImage,HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = tourImage.ImageUrl;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    tourImage.ImageUrl = newFilenameUrl;
                }
                #endregion
                tourImage.IsDelete=false;
                db.Entry(tourImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=tourImage.TourId});
            }
          return View(tourImage);
        }

        // GET: TourImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourImage tourImage = db.TourImages.Find(id);
            if (tourImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = tourImage.TourId;
            return View(tourImage);
        }

        // POST: TourImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TourImage tourImage = db.TourImages.Find(id);
			tourImage.IsDelete=true;
			tourImage.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id=tourImage.TourId});
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
