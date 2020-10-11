using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Bisan_New.Controllers
{
    public class TourDetailsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid id)
        {
            var tourDetails = db.TourDetails.Include(t => t.Tour).Where(t=>t.TourId==id&& t.IsDelete==false).OrderByDescending(t=>t.SubmitDate);
            return View(tourDetails.ToList());
        }

       
        public ActionResult Create(Guid id)
        {
            ViewBag.TourId = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TourDetail tourDetail,Guid id)
        {
            if (ModelState.IsValid)
            {
                tourDetail.TourId = id;
				tourDetail.IsDelete=false;
				tourDetail.SubmitDate= DateTime.Now; 
                tourDetail.Id = Guid.NewGuid();
                db.TourDetails.Add(tourDetail);
                db.SaveChanges();
                return RedirectToAction("Index",new{id=id});
            }

            ViewBag.TourId = id;
            return View(tourDetail);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourDetail tourDetail = db.TourDetails.Find(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = tourDetail.TourId;
            return View(tourDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourDetail tourDetail)
        {
            if (ModelState.IsValid)
            {
				tourDetail.IsDelete=false;
                db.Entry(tourDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new{id=tourDetail.TourId});
            }
            ViewBag.TourId = tourDetail.TourId;
            return View(tourDetail);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourDetail tourDetail = db.TourDetails.Find(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            return View(tourDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TourDetail tourDetail = db.TourDetails.Find(id);
			tourDetail.IsDelete=true;
			tourDetail.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new{id=tourDetail.Id});
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
