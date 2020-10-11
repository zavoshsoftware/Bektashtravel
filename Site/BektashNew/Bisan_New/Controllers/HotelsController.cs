using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using ViewModels;

namespace Bisan_New.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class HotelsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Hotels.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        public ActionResult Create()
        {
            ViewBag.HotelCategoryId = new SelectList(db.HotelCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");

            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
				hotel.IsDelete=false;
				hotel.SubmitDate= DateTime.Now; 
                hotel.Id = Guid.NewGuid();
                hotel.Code = ReturnCode();
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelCategoryId = new SelectList(db.HotelCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");

            return View(hotel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelCategoryId = new SelectList(db.HotelCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
				hotel.IsDelete=false;
                hotel.LastModificationDate=DateTime.Now;

                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelCategoryId = new SelectList(db.HotelCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");
            return View(hotel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Hotel hotel = db.Hotels.Find(id);
			hotel.IsDelete=true;
			hotel.DeleteDate=DateTime.Now;
 
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
        [Route("hotel/{urlparam}")]
        public ActionResult List(string urlparam)
        {
            if (GuidChecker.IsGuid(urlparam))
                return RedirectPermanent("/");

            List<Hotel> hotels = ReturnHotelList(urlparam);
            HotelListViewModel tourListViewModel = new HotelListViewModel();
            tourListViewModel.Menu = menu.ReturnMenuTours();
            tourListViewModel.Hotels = hotels;
            tourListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            tourListViewModel.HotelCategory = db.HotelCategories.Include(current => current.Parent)
                .FirstOrDefault(current => current.UrlParam == urlparam);
            tourListViewModel.Footer = menu.ReturnFooter();
            ViewBag.Canonical = "https://www.bektashtravel.com/hotel/" + urlparam;

            tourListViewModel.SidebarVisaList = GetSideBarVisaList();

            tourListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            if (hotels == null)
                return RedirectPermanent("/hotel");

            return View(tourListViewModel);
        }


        #region Methods

        [AllowAnonymous]
        public List<Hotel> ReturnHotelList(string urlparam)
        {
            if (urlparam == null)
                return db.Hotels.Include(current => current.HotelCategory).Where(current => current.IsDelete == false)
                    .ToList();

            List<Hotel> hotels = null;
            HotelCategory hotelCategory = db.HotelCategories.FirstOrDefault(current => current.UrlParam == urlparam && current.IsDelete == false);

            if (hotelCategory != null)
            {
                if (hotelCategory.ParentId != null)
                {
                    hotels = db.Hotels.Include(current => current.HotelCategory).Where(
                            current =>
                                current.IsDelete == false && current.HotelCategoryId == hotelCategory.Id)
                        .ToList();
                }
                else
                {
                    hotels = db.Hotels.Include(current => current.HotelCategory).Where(
                            current =>
                                current.IsDelete == false && current.HotelCategory.ParentId == hotelCategory.Id)
                        .ToList();

                    if (hotels.Count() == 0)
                        hotels = db.Hotels.Include(current => current.HotelCategory)
                            .Where(current =>
                                current.IsDelete == false && current.HotelCategoryId == hotelCategory.Id)
                            .ToList();
                }

                if (!string.IsNullOrEmpty(hotelCategory.PageTitle))
                    ViewBag.Title = hotelCategory.PageTitle;
                else
                    ViewBag.Title = hotelCategory.Title + " | قیمت " + hotelCategory.Title;

                if (!string.IsNullOrEmpty(hotelCategory.PageDescription))
                    ViewBag.Description = hotelCategory.PageDescription;
                else
                    ViewBag.Description = "برای اطلاع از قیمت " + hotelCategory.Title + " وارد وب سایت بکتاش سیر شوید یا با شماره تلفن 02157952 تماس بگیرید. بکتاش ارائه دهنده تورهای متنوع آسیایی و اروپایی";

                ViewBag.OrginalTitle = hotelCategory.Title;
                ViewBag.summery = hotelCategory.Summery;
            }

            return hotels;
        }

        public List<Visa> GetSideBarVisaList()
        {
            return db.Visas.Where(current => current.IsDelete == false)
                .OrderByDescending(c => c.Order).ToList();
        }

        public List<TourCategory> GetSideBarTourCategory()
        {
            return db.TourCategories
                .Where(current => current.ParentId == null && current.IsDelete == false)
                .OrderByDescending(c => c.Priority).ToList();
        }

        [AllowAnonymous]
        public string ReturnCode()
        {
            Hotel hotel = db.Hotels.OrderByDescending(current => current.Code).FirstOrDefault();
            if (hotel != null)
                return (Convert.ToInt32(hotel.Code) + 1).ToString();

                return "10000";
        }

        public string UpdateHotelCodes()
        {
            List<Hotel> hotels = db.Hotels.OrderByDescending(c => c.SubmitDate).ToList();

            int code = 10000;
            foreach (Hotel hotel in hotels)
            {
                hotel.Code = code.ToString();
                code = code + 1;
            }

            db.SaveChanges();

            return "";
        }
        #endregion
    }
}
