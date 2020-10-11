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
    public class VisaTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: VisaTypes
        public ActionResult Index()
        {
            return View(db.VisaTypes.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: VisaTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaType visaType = db.VisaTypes.Find(id);
            if (visaType == null)
            {
                return HttpNotFound();
            }
            return View(visaType);
        }

        // GET: VisaTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VisaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] VisaType visaType)
        {
            if (ModelState.IsValid)
            {
				visaType.IsDelete=false;
				visaType.SubmitDate= DateTime.Now; 
                visaType.Id = Guid.NewGuid();
                db.VisaTypes.Add(visaType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visaType);
        }

        // GET: VisaTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaType visaType = db.VisaTypes.Find(id);
            if (visaType == null)
            {
                return HttpNotFound();
            }
            return View(visaType);
        }

        // POST: VisaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] VisaType visaType)
        {
            if (ModelState.IsValid)
            {
				visaType.IsDelete=false;
                db.Entry(visaType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visaType);
        }

        // GET: VisaTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaType visaType = db.VisaTypes.Find(id);
            if (visaType == null)
            {
                return HttpNotFound();
            }
            return View(visaType);
        }

        // POST: VisaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VisaType visaType = db.VisaTypes.Find(id);
			visaType.IsDelete=true;
			visaType.DeleteDate=DateTime.Now;
 
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
