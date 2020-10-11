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
    public class CommentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Comments
        public ActionResult Index(Guid id)
        {
            return View(db.Comments.Where(a=>a.IsDelete==false && a.TypeId==id).OrderByDescending(a=>a.SubmitDate).ToList());
        }

       

        // GET: Comments/Create
        public ActionResult Create(Guid id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment,Guid id)
        {
            if (ModelState.IsValid)
            {
				comment.IsDelete=false;
				comment.SubmitDate= DateTime.Now; 
                comment.Id = Guid.NewGuid();
                comment.TypeId = id;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=id});
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = comment.TypeId;
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
				comment.IsDelete=false;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=comment.TypeId});
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = comment.TypeId;
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);
			comment.IsDelete=true;
			comment.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index",new { id=comment.TypeId});
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
