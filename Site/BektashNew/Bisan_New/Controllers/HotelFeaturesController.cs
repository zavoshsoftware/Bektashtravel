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
    public class HotelFeaturesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        
        public ActionResult Index(Guid id)
        {
            var hotelFeatures = db.HotelFeatures.Include(h => h.Feature).Where(h => h.HotelId == id && h.IsDelete == false).OrderByDescending(h => h.SubmitDate);
            ViewBag.Title = "لیست ویژگی های هتل " + db.Hotels.Find(id)?.Title;
            return View(hotelFeatures.ToList());
        }
 
        public ActionResult Create(Guid id)
        {
            ViewBag.FeatureId = new SelectList(db.Features, "Id", "Icon");
            ViewBag.HotelId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HotelFeature hotelFeature,Guid id)
        {
            if (ModelState.IsValid)
            {
                hotelFeature.HotelId = id;
                hotelFeature.IsDelete = false;
                hotelFeature.SubmitDate = DateTime.Now;
                hotelFeature.Id = Guid.NewGuid();
                db.HotelFeatures.Add(hotelFeature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FeatureId = new SelectList(db.Features, "Id", "Icon", hotelFeature.FeatureId);
            ViewBag.HotelId =id;
            return View(hotelFeature);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelFeature hotelFeature = db.HotelFeatures.Find(id);
            if (hotelFeature == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeatureId = new SelectList(db.Features, "Id", "Icon", hotelFeature.FeatureId);
            ViewBag.HotelId = hotelFeature.HotelId;
            return View(hotelFeature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HotelFeature hotelFeature)
        {
            if (ModelState.IsValid)
            {
                hotelFeature.IsDelete = false;
                db.Entry(hotelFeature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FeatureId = new SelectList(db.Features, "Id", "Icon", hotelFeature.FeatureId);
            ViewBag.HotelId = hotelFeature.HotelId;
            return View(hotelFeature);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelFeature hotelFeature = db.HotelFeatures.Find(id);
            if (hotelFeature == null)
            {
                return HttpNotFound();
            }
            return View(hotelFeature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HotelFeature hotelFeature = db.HotelFeatures.Find(id);
            hotelFeature.IsDelete = true;
            hotelFeature.DeleteDate = DateTime.Now;

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
