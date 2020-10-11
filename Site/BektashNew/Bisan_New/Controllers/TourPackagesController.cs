using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Bisan.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class TourPackagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TourPackages
        public ActionResult Index(Guid id)
        {

            List< TourPackage> TourPackages = db.TourPackages.Include(t => t.Hotel).Where(t => t.IsDelete == false && t.TourId == id).OrderByDescending(t => t.SubmitDate).ToList();
            Tour tour = db.Tours.Find(id);
            ViewBag.tourCategoryId = tour.TourCategoryId;
            return View(TourPackages);
        }



        // GET: TourPackages/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.HotelId = new SelectList(db.Hotels.Where(current=>current.IsDelete==false).OrderBy(current => current.Title), "Id", "Title");

            ViewBag.id = id;
            return View();
        }

        // POST: TourPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TourPackage TourPackage, Guid id)
        {
            if (ModelState.IsValid)
            {
                TourPackage.IsDelete = false;
                TourPackage.SubmitDate = DateTime.Now;
                TourPackage.Id = Guid.NewGuid();
                TourPackage.TourId = id;
                db.TourPackages.Add(TourPackage);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.HotelId = new SelectList(db.Hotels.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");

            return View(TourPackage);
        }

        // GET: TourPackages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourPackage TourPackage = db.TourPackages.Find(id);
            if (TourPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title",TourPackage.HotelId);

            ViewBag.id = TourPackage.TourId;
            return View(TourPackage);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourPackage tourPackage)
        {
            if (ModelState.IsValid)
            {
               tourPackage.IsDelete = false;
                db.Entry(tourPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tourPackage.TourId });
            }
            ViewBag.id =tourPackage.TourId;
            ViewBag.HotelId = new SelectList(db.Hotels.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title", tourPackage.HotelId);

            return View(tourPackage);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourPackage TourPackage = db.TourPackages.Find(id);
            if (TourPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = TourPackage.TourId;
            return View(TourPackage);
        }

        // POST: TourPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TourPackage TourPackage = db.TourPackages.Find(id);
            TourPackage.IsDelete = true;
            TourPackage.DeleteDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = TourPackage.TourId });
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
