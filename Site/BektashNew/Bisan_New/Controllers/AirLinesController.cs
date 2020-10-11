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
    public class AirLinesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AirLines
        public ActionResult Index()
        {
            return View(db.AirLines.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: AirLines/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirLine airLine = db.AirLines.Find(id);
            if (airLine == null)
            {
                return HttpNotFound();
            }
            return View(airLine);
        }

        // GET: AirLines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AirLine airLine)
        {
            if (ModelState.IsValid)
            {
				airLine.IsDelete=false;
				airLine.SubmitDate= DateTime.Now; 
                airLine.Id = Guid.NewGuid();
                db.AirLines.Add(airLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airLine);
        }

        // GET: AirLines/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirLine airLine = db.AirLines.Find(id);
            if (airLine == null)
            {
                return HttpNotFound();
            }
            return View(airLine);
        }

        // POST: AirLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AirLine airLine)
        {
            if (ModelState.IsValid)
            {
				airLine.IsDelete=false;
                db.Entry(airLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airLine);
        }

        // GET: AirLines/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirLine airLine = db.AirLines.Find(id);
            if (airLine == null)
            {
                return HttpNotFound();
            }
            return View(airLine);
        }

        // POST: AirLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AirLine airLine = db.AirLines.Find(id);
			airLine.IsDelete=true;
			airLine.DeleteDate=DateTime.Now;
 
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
