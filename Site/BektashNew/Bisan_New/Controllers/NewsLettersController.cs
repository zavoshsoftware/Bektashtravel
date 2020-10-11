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
    public class NewsLettersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: NewsLetters
        public ActionResult Index()
        {
            return View(db.NewsLetters.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: NewsLetters/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.NewsLetters.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            return View(newsLetter);
        }

        // GET: NewsLetters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsLetters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
				newsLetter.IsDelete=false;
				newsLetter.SubmitDate= DateTime.Now; 
                newsLetter.Id = Guid.NewGuid();
                db.NewsLetters.Add(newsLetter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsLetter);
        }

        // GET: NewsLetters/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.NewsLetters.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            return View(newsLetter);
        }

        // POST: NewsLetters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
				newsLetter.IsDelete=false;
                db.Entry(newsLetter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsLetter);
        }

        // GET: NewsLetters/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetter newsLetter = db.NewsLetters.Find(id);
            if (newsLetter == null)
            {
                return HttpNotFound();
            }
            return View(newsLetter);
        }

        // POST: NewsLetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NewsLetter newsLetter = db.NewsLetters.Find(id);
			newsLetter.IsDelete=true;
			newsLetter.DeleteDate=DateTime.Now;
 
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
