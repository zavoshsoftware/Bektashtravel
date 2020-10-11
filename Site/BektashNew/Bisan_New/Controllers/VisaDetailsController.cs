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
    public class VisaDetailsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid id)
        {
            var visaDetails = db.VisaDetails.Include(v => v.Visa).Where(v => v.VisaId == id && v.IsDelete == false).OrderByDescending(v => v.SubmitDate);
            return View(visaDetails.ToList());
        }


        public ActionResult Create(Guid id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisaDetail visaDetail, Guid id)
        {
            if (ModelState.IsValid)
            {
                visaDetail.VisaId = id;
                visaDetail.IsDelete = false;
                visaDetail.SubmitDate = DateTime.Now;
                visaDetail.Id = Guid.NewGuid();
                db.VisaDetails.Add(visaDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.id = id;
            return View(visaDetail);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaDetail visaDetail = db.VisaDetails.Find(id);
            if (visaDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = visaDetail.Id;
            return View(visaDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VisaDetail visaDetail)
        {
            if (ModelState.IsValid)
            {
                visaDetail.IsDelete = false;
                db.Entry(visaDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = visaDetail.VisaId });
            }
            ViewBag.id = visaDetail.Id;
            return View(visaDetail);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaDetail visaDetail = db.VisaDetails.Find(id);
            if (visaDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = visaDetail.Id;

            return View(visaDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VisaDetail visaDetail = db.VisaDetails.Find(id);
            visaDetail.IsDelete = true;
            visaDetail.DeleteDate = DateTime.Now;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = id });
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

