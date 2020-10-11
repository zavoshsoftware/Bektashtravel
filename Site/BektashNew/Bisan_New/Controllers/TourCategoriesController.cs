using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;
using Helpers;
using System.IO;

namespace Bisan.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class TourCategoriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TourCategories
        public ActionResult Index(Guid? id)
        {
            ViewBag.parent = null;
            List<TourCategory> tourCategories = new List<TourCategory>();
            if (id == null)
            {
                tourCategories = db.TourCategories.Include(t => t.Parent).Where(t => t.IsDelete == false && t.ParentId == null).OrderByDescending(t => t.SubmitDate).ToList();

            }
            else
            {
                tourCategories = db.TourCategories.Include(t => t.Parent).Where(t => t.IsDelete == false && t.ParentId == id).OrderByDescending(t => t.SubmitDate).ToList();
                ViewBag.parent = id;
            }
            return View(tourCategories);
        }



        // GET: TourCategories/Create
        public ActionResult Create(Guid? id)
        {
            if (id != null)
            {
                ViewBag.parent = id;
            }
            else
                ViewBag.TypeId = new SelectList(db.Types.Where(current => current.IsDelete == false), "Id", "Title");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TourCategory tourCategory, Guid? id, HttpPostedFileBase fileUpload, HttpPostedFileBase coverUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    tourCategory.ImageUrl = newFilenameUrl;
                }
                #endregion
                #region Upload and resize cover if needed
                string newCovernameUrl = string.Empty;
                if (coverUpload != null)
                {
                    string covername = Path.GetFileName(coverUpload.FileName);
                    string newCovername = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(covername);

                    newCovernameUrl = "/Uploads/Tour/" + newCovername;
                    string physicalCovername = Server.MapPath(newCovernameUrl);

                    coverUpload.SaveAs(physicalCovername);

                    tourCategory.CoverImage = newCovernameUrl;
                }
                #endregion
                tourCategory.IsDelete = false;
                tourCategory.SubmitDate = DateTime.Now;
                tourCategory.Id = Guid.NewGuid();
                tourCategory.LastModificationDate = DateTime.Now;


                if (id != null)
                    tourCategory.ParentId = id.Value;
                else
                    tourCategory.TypeId = tourCategory.TypeId;
                db.TourCategories.Add(tourCategory);
                db.SaveChanges();
                if (id != null)
                    return RedirectToAction("Index", new { id = id });
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.TourCategories, "Id", "Title", tourCategory.ParentId);
            return View(tourCategory);
        }

        // GET: TourCategories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourCategory tourCategory = db.TourCategories.Find(id);
            if (tourCategory == null)
            {
                return HttpNotFound();
            }
            if (tourCategory.ParentId != null)
                ViewBag.parent = tourCategory.ParentId;
            else
                ViewBag.TypeId = new SelectList(db.Types.Where(current => current.IsDelete == false), "Id", "Title", tourCategory.TypeId);

            return View(tourCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TourCategory tourCategory, HttpPostedFileBase fileUpload, HttpPostedFileBase coverUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = tourCategory.ImageUrl;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    tourCategory.ImageUrl = newFilenameUrl;
                }
                #endregion
                #region Upload and resize cover if needed
                string newCovernameUrl = tourCategory.CoverImage;
                if (coverUpload != null)
                {
                    string covername = Path.GetFileName(coverUpload.FileName);
                    string newCovername = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(covername);

                    newCovernameUrl = "/Uploads/Tour/" + newCovername;
                    string physicalCovername = Server.MapPath(newCovernameUrl);

                    coverUpload.SaveAs(physicalCovername);

                    tourCategory.CoverImage = newCovernameUrl;
                }
                #endregion
                tourCategory.IsDelete = false;
                tourCategory.LastModificationDate = DateTime.Now;

                db.Entry(tourCategory).State = EntityState.Modified;
                db.SaveChanges();


                if (tourCategory.ParentId != null)
                    return RedirectToAction("Index", new { id = tourCategory.ParentId });

                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Types.Where(current => current.IsDelete == false), "Id", "Title", tourCategory.TypeId);

            return View(tourCategory);
        }

        // GET: TourCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourCategory tourCategory = db.TourCategories.Find(id);
            if (tourCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.parent = tourCategory.ParentId;
            return View(tourCategory);
        }

        // POST: TourCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TourCategory tourCategory = db.TourCategories.Find(id);
            tourCategory.IsDelete = true;
            tourCategory.DeleteDate = DateTime.Now;

            db.SaveChanges();
            if (tourCategory.ParentId != null)
                return RedirectToAction("Index", new { id = tourCategory.ParentId });
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
        [AllowAnonymous]

        [Route("tourCategory/list/{id:Guid}")]
        public ActionResult List(Guid id)
        {
            MenuHelper menu = new MenuHelper();
            TourCategoryListViewModel tourListViewModel = new TourCategoryListViewModel();
            tourListViewModel.Menu = menu.ReturnMenuTours();
            tourListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            tourListViewModel.TourCategories = ReturnTourCategoryList(id);
            tourListViewModel.Parent = db.TourCategories.Find(id);
            tourListViewModel.Footer = menu.ReturnFooter();
            return View(tourListViewModel);
        }
        [AllowAnonymous]
        public List<TourCategory> ReturnTourCategoryList(Guid id)
        {
            List<TourCategory> tourCategories = db.TourCategories.Where(current => current.IsDelete == false && current.ParentId == id).ToList();
            return tourCategories;
        }
    }
}
