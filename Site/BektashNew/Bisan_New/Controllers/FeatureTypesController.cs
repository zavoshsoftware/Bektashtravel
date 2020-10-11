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
    public class FeatureTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: FeatureTypes
        public ActionResult Index()
        {
            return View(db.FeatureTypes.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: FeatureTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // GET: FeatureTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeatureTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] FeatureType featureType)
        {
            if (ModelState.IsValid)
            {
				featureType.IsDelete=false;
				featureType.SubmitDate= DateTime.Now; 
                featureType.Id = Guid.NewGuid();
                db.FeatureTypes.Add(featureType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featureType);
        }

        // GET: FeatureTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // POST: FeatureTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] FeatureType featureType)
        {
            if (ModelState.IsValid)
            {
				featureType.IsDelete=false;
                db.Entry(featureType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featureType);
        }

        // GET: FeatureTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featureType = db.FeatureTypes.Find(id);
            if (featureType == null)
            {
                return HttpNotFound();
            }
            return View(featureType);
        }

        // POST: FeatureTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FeatureType featureType = db.FeatureTypes.Find(id);
			featureType.IsDelete=true;
			featureType.DeleteDate=DateTime.Now;
 
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
