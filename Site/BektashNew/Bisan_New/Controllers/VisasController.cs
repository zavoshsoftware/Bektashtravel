using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using ViewModels;

namespace Bisan.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class VisasController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Visas.Where(a => a.IsDelete == false).OrderByDescending(a => a.SubmitDate).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Visa visa, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadDoc, HttpPostedFileBase fileUploadForm)
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

                    newFilenameUrl = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    visa.ImageUrl = newFilenameUrl;
                }
                #endregion

                #region Upload and resize image if needed
                string newfileUploadDoc = string.Empty;
                if (fileUploadDoc != null)
                {
                    string filename = Path.GetFileName(fileUploadDoc.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newfileUploadDoc = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newfileUploadDoc);

                    fileUploadDoc.SaveAs(physicalFilename);

                    visa.Document = newfileUploadDoc;
                }
                #endregion
                
                #region Upload and resize image if needed
                string newFilenameUrlForm = string.Empty;
                if (fileUploadForm != null)
                {
                    string filename = Path.GetFileName(fileUploadForm.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlForm = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlForm);

                    fileUploadForm.SaveAs(physicalFilename);

                    visa.Form = newFilenameUrlForm;
                }
                #endregion

                visa.VisitNumber = 0;
                visa.IsDelete = false;
                visa.SubmitDate = DateTime.Now;
                visa.Id = Guid.NewGuid();
                db.Visas.Add(visa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visa);
        }

        // GET: Visas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visa visa = db.Visas.Find(id);
            if (visa == null)
            {
                return HttpNotFound();
            }
            return View(visa);
        }

        // POST: Visas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Visa visa, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadDoc, HttpPostedFileBase fileUploadForm)
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

                    newFilenameUrl = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    visa.ImageUrl = newFilenameUrl;
                }
                #endregion

                #region Upload and resize image if needed
                string newfileUploadDoc = string.Empty;
                if (fileUploadDoc != null)
                {
                    string filename = Path.GetFileName(fileUploadDoc.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newfileUploadDoc = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newfileUploadDoc);

                    fileUploadDoc.SaveAs(physicalFilename);

                    visa.Document = newfileUploadDoc;
                }
                #endregion

                #region Upload and resize image if needed
                string newFilenameUrlForm = string.Empty;
                if (fileUploadForm != null)
                {
                    string filename = Path.GetFileName(fileUploadForm.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlForm = "/Uploads/Visa/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlForm);

                    fileUploadForm.SaveAs(physicalFilename);

                    visa.Form = newFilenameUrlForm;
                }
                #endregion
                visa.IsDelete = false;
                db.Entry(visa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visa);
        }

        // GET: Visas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visa visa = db.Visas.Find(id);
            if (visa == null)
            {
                return HttpNotFound();
            }
            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Visa visa = db.Visas.Find(id);
            visa.IsDelete = true;
            visa.DeleteDate = DateTime.Now;

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
        MenuHelper menu = new MenuHelper();

        [AllowAnonymous]
        [Route("visa")]
        public ActionResult List()
        {
            List<Visa> visas = db.Visas.Where(a => a.IsDelete == false).OrderByDescending(a => a.Order).ToList();

            VisaListViewModel visaList = new VisaListViewModel()
            {
                VisaList = visas,
                Menu = menu.ReturnMenuTours(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                Footer = menu.ReturnFooter(),
            //   SidebarVisaList = GetSideBarVisaList(),
                SidebarTourCategories = GetSideBarTourCategory()
        };

            ViewBag.Description = "راهنمای جامع اخذ ویزاهای کانادا، شینگن و سایر کشور ها را در وب سایت بکتاش سیر گشت مطالعه فرمایید. جهت اطلاعات بیشتر با شماره تلفن 02157952 تماس بگیرید و سوالات خود را از کارشناسان بکتاش بپرسید.";
            ViewBag.Title = "اخذ ویزا تضمینی کانادا و اروپا در بکتاش سیر 02157952";
            return View(visaList);
        }

        [AllowAnonymous]
        public List<Visa> GetSideBarVisaList()
        {
            return db.Visas.Where(current => current.IsDelete == false)
                .OrderByDescending(c => c.Order).ToList();
        }

        [AllowAnonymous]
        public List<TourCategory> GetSideBarTourCategory()
        {
            return db.TourCategories
                .Where(current => current.ParentId == null && current.IsDelete == false)
                .OrderByDescending(c => c.Priority).ToList();
        }

        [AllowAnonymous]
        [Route("visa/{urlParam}")]
        public ActionResult Details(string urlParam)
        {
            if (urlParam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

             Regex isGuid =
            new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            if (isGuid.IsMatch(urlParam))
            {
                return RedirectPermanent("/visa");
            }

            Visa visa = db.Visas.FirstOrDefault(current => current.UrlParam == urlParam);

            List<VisaTour> visaTours = db.VisaTours.Where(current => current.VisaId == visa.Id).Include(c=>c.Tour).Include(c=>c.Tour.TourCategory).ToList();

            List<Tour> tours = new List<Tour>();

            foreach (VisaTour visaTour in visaTours)
            {
                tours.Add(visaTour.Tour);
            }

            VisaDetailViewModel visaDetail = new VisaDetailViewModel()
            {
                Visa = visa,
                VisaDetails = db.VisaDetails.Where(current => current.VisaId == visa.Id && current.IsDelete == false)
                    .OrderBy(current => current.Order).ToList(),
                Menu = menu.ReturnMenuTours(),
                MenuBlogGroups = menu.ReturnBlogGroups(),
                Footer = menu.ReturnFooter(),
                SuggestedTours = tours
            };

            if (!string.IsNullOrEmpty(visa.PageTitle))
                ViewBag.Title = visa.PageTitle;
            else
                ViewBag.Title = visa.Title + " | اخذ ویزا تضمینی توسط بکتاش سیر";

            if (!string.IsNullOrEmpty(visa.PageDescription))
                ViewBag.Description = visa.PageDescription;
            else
                ViewBag.Description = "راهنمای جامع اخذ " + visa.Title +
                                      " به صورت تضمینی. اطلاعات جامع اخذ ویزا تمامی کشورها و ویزا شینگن را در وب سایت بکتاش سیر گشت مطالعه نمایید.";

            ViewBag.Canonical = "https://www.bektashtravel.com/visa/"+visa.UrlParam;
            return View(visaDetail);
        }

    
    }
}
