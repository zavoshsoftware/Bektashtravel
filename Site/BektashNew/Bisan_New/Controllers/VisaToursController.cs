using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Models;

namespace Bisan_New.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class VisaToursController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var visaTours = db.VisaTours.Include(v => v.Tour).Where(v=>v.IsDelete==false).OrderByDescending(v=>v.SubmitDate).Include(v => v.Visa).Where(v=>v.IsDelete==false).OrderByDescending(v=>v.SubmitDate);
            return View(visaTours.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTour visaTour = db.VisaTours.Find(id);
            if (visaTour == null)
            {
                return HttpNotFound();
            }
            return View(visaTour);
        }

        public ActionResult Create()
        {
            ViewBag.TourId = new SelectList(db.Tours.Where(cu=>cu.IsDelete==false), "Id", "Title");
            ViewBag.VisaId = new SelectList(db.Visas.Where(cu => cu.IsDelete == false), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VisaId,TourId,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] VisaTour visaTour)
        {
            if (ModelState.IsValid)
            {
				visaTour.IsDelete=false;
				visaTour.SubmitDate= DateTime.Now; 
                visaTour.Id = Guid.NewGuid();
                db.VisaTours.Add(visaTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourId = new SelectList(db.Tours.Where(cu=>cu.IsDelete==false), "Id", "Title", visaTour.TourId);
            ViewBag.VisaId = new SelectList(db.Visas.Where(cu => cu.IsDelete == false), "Id", "Title", visaTour.VisaId);
            return View(visaTour);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTour visaTour = db.VisaTours.Find(id);
            if (visaTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = new SelectList(db.Tours.Where(cu=>cu.IsDelete==false), "Id", "Title", visaTour.TourId);
            ViewBag.VisaId = new SelectList(db.Visas.Where(cu=>cu.IsDelete==false), "Id", "Title", visaTour.VisaId);
            return View(visaTour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VisaId,TourId,IsDelete,DeleteDate,SubmitDate,LastModificationDate")] VisaTour visaTour)
        {
            if (ModelState.IsValid)
            {
				visaTour.IsDelete=false;
                db.Entry(visaTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourId = new SelectList(db.Tours.Where(cu=>cu.IsDelete==false), "Id", "Title", visaTour.TourId);
            ViewBag.VisaId = new SelectList(db.Visas.Where(cu => cu.IsDelete == false), "Id", "Title", visaTour.VisaId);
            return View(visaTour);
        }

        // GET: VisaTours/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTour visaTour = db.VisaTours.Find(id);
            if (visaTour == null)
            {
                return HttpNotFound();
            }
            return View(visaTour);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            VisaTour visaTour = db.VisaTours.Find(id);
			visaTour.IsDelete=true;
			visaTour.DeleteDate=DateTime.Now;
 
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
