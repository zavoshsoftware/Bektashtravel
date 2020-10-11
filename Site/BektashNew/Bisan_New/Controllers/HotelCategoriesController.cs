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
    public class HotelCategoriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid? id)
        {
            ViewBag.parent = null;

            List<HotelCategory> hotelCategories = new List<HotelCategory>();

            if (id == null)
            {
                hotelCategories = db.HotelCategories.Include(t => t.Parent).Where(t => t.IsDelete == false && t.ParentId == null).OrderByDescending(t => t.SubmitDate).ToList();

            }
            else
            {
                hotelCategories = db.HotelCategories.Include(t => t.Parent).Where(t => t.IsDelete == false && t.ParentId == id).OrderByDescending(t => t.SubmitDate).ToList();
                ViewBag.parent = id;
            }

            return View(hotelCategories);
        }


        // GET: HotelCategories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelCategory hotelCategory = db.HotelCategories.Find(id);
            if (hotelCategory == null)
            {
                return HttpNotFound();
            }
            return View(hotelCategory);
        }

        // GET: HotelCategories/Create

        public ActionResult Create(Guid? id)
        {
            if (id != null)
            {
                ViewBag.parent = id;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelCategory hotelCategory, Guid? id)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.IsDelete = false;
                hotelCategory.SubmitDate = DateTime.Now;
                hotelCategory.Id = Guid.NewGuid();

                if (id != null)
                    hotelCategory.ParentId = id.Value;

                db.HotelCategories.Add(hotelCategory);
                db.SaveChanges();
                if (id != null)
                    return RedirectToAction("Index", new { id = id });
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.HotelCategories, "Id", "Title", hotelCategory.ParentId);
            return View(hotelCategory);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelCategory hotelCategory = db.HotelCategories.Find(id);
            if (hotelCategory == null)
            {
                return HttpNotFound();
            }

            if (hotelCategory.ParentId != null)
                ViewBag.parent = hotelCategory.ParentId;

            ViewBag.ParentId = new SelectList(db.HotelCategories, "Id", "Title", hotelCategory.ParentId);
            return View(hotelCategory);
        }

        // POST: HotelCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                hotelCategory.IsDelete = false;
                db.Entry(hotelCategory).State = EntityState.Modified;
                db.SaveChanges();

                if (hotelCategory.ParentId != null)
                    return RedirectToAction("Index", new { id = hotelCategory.ParentId });

                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.HotelCategories, "Id", "Title", hotelCategory.ParentId);
            return View(hotelCategory);
        }

        // GET: HotelCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelCategory hotelCategory = db.HotelCategories.Find(id);
            if (hotelCategory == null)
            {
                return HttpNotFound();
            }
            return View(hotelCategory);
        }

        // POST: HotelCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HotelCategory hotelCategory = db.HotelCategories.Find(id);
            hotelCategory.IsDelete = true;
            hotelCategory.DeleteDate = DateTime.Now;

            db.SaveChanges();
            if (hotelCategory.ParentId != null)
                return RedirectToAction("Index", new { id = hotelCategory.ParentId });
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
