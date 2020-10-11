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
    [Authorize(Roles = "superadmin")]
    public class FeaturesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var features = db.Features.Include(f => f.FeatureType).Where(f=>f.IsDelete==false).OrderByDescending(f=>f.SubmitDate);
            return View(features.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.FeatureTypeId = new SelectList(db.FeatureTypes, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Icon,Title,Order,FeatureTypeId,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] Feature feature)
        {
            if (ModelState.IsValid)
            {
				feature.IsDelete=false;
				feature.SubmitDate= DateTime.Now; 
                feature.Id = Guid.NewGuid();
                db.Features.Add(feature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FeatureTypeId = new SelectList(db.FeatureTypes, "Id", "Title", feature.FeatureTypeId);
            return View(feature);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeatureTypeId = new SelectList(db.FeatureTypes, "Id", "Title", feature.FeatureTypeId);
            return View(feature);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Icon,Title,Order,FeatureTypeId,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] Feature feature)
        {
            if (ModelState.IsValid)
            {
				feature.IsDelete=false;
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FeatureTypeId = new SelectList(db.FeatureTypes, "Id", "Title", feature.FeatureTypeId);
            return View(feature);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Feature feature = db.Features.Find(id);
			feature.IsDelete=true;
			feature.DeleteDate=DateTime.Now;
 
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
