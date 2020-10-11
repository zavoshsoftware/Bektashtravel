using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;
using ViewModels;
using Helpers;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Configuration;

namespace Bisan.Controllers
{
    [Authorize(Roles = "Administrator,superadmin")]
    public class ToursController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        MenuHelper menu = new MenuHelper();

        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.Airline).Where(t => t.IsDelete == false).OrderByDescending(t => t.SubmitDate).Include(t => t.Country).Where(t => t.IsDelete == false).OrderByDescending(t => t.SubmitDate);
            return View(tours.ToList());
        }

        [AllowAnonymous]
        [Route("tour/{categoryUrlparam}/{code}")]
        public ActionResult Details(string code, string categoryUrlparam)
        {
            Tour tour = db.Tours.Include(current => current.Country).Include(current => current.Airline).Include(current => current.TourCategory).FirstOrDefault(current => current.Code == code);

            if (tour == null)
                return RedirectPermanent("/");

            if (tour.TourCategory.UrlParam != categoryUrlparam)
                return RedirectPermanent("/tour/" + tour.TourCategory.UrlParam + "/" + code);

            TourDetailViewModel tourDetailViewModel = new TourDetailViewModel();
            tourDetailViewModel.Menu = menu.ReturnMenuTours();
            tourDetailViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            tourDetailViewModel.Tour = tour;
            tourDetailViewModel.TourCategory = db.TourCategories.Include(current => current.Parent).FirstOrDefault(current => current.Id == tour.TourCategoryId);
            tourDetailViewModel.Images = db.TourImages.Where(current => current.IsDelete == false && current.TourId == tour.Id).ToList();
            tourDetailViewModel.Footer = menu.ReturnFooter();
            tourDetailViewModel.TourPackages = db.TourPackages
                .Where(current => current.TourId == tour.Id && current.IsDelete == false)
                .Include(current => current.Hotel).OrderByDescending(current => current.Order).ToList();

            tourDetailViewModel.SuggestedTours = ReturnSuggestedTours(tour);

        
                ViewBag.coverImage = !string.IsNullOrEmpty(tour.TourCategory.CoverImage)
                    ? tour.TourCategory.CoverImage
                    : "/Images/header1.jpg";
           
            ViewBag.Canonical = "https://www.bektashtravel.com/tour/" + categoryUrlparam + "/" + code;


            if (!string.IsNullOrEmpty(tour.PageTitle))
                ViewBag.Title = tour.PageTitle + " | بکتاش سیر";
            else
                ViewBag.Title = tour.Title + " | قیمت " + tour.Title;

            if (!string.IsNullOrEmpty(tour.PageDescription))
                ViewBag.Description = tour.PageDescription;
            else
                ViewBag.Description = "برای اطلاع از قیمت " + tour.Title + " وارد وب سایت بکتاش سیر شوید یا با شماره تلفن 02157952 تماس بگیرید. بکتاش ارائه دهنده تورهای متنوع آسیایی و اروپایی";


            return View(tourDetailViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.AirlineId = new SelectList(db.AirLines, "Id", "Title");
            ViewBag.CountryId = new SelectList(db.Countries.OrderBy(current => current.Title), "Id", "Title");
            ViewBag.TourCategoryId = new SelectList(db.TourCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");
            return View();
        }


        [AllowAnonymous]
        public string CodeCleaner()
        {
            List<Tour> tours = db.Tours.OrderBy(current => current.SubmitDate).ToList();
            int id = 1000;
            foreach (Tour tour in tours)
            {
                if (tour.TourCategoryId != null)
                {
                    var tg = db.TourCategories.FirstOrDefault(c => c.Id == tour.TourCategoryId);

                    if (tg != null)
                        if (tg.IsDelete)
                        {
                            tour.IsDelete = true;
                            tour.DeleteDate = DateTime.Now;

                        }
                }

            }
            db.SaveChanges();
            return string.Empty;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tour tour, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadDoc, HttpPostedFileBase fileUploadForm)
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

                    tour.ImageUrl = newFilenameUrl;
                }

                string newFilenameUrlDoc = string.Empty;
                if (fileUploadDoc != null)
                {
                    string filename = Path.GetFileName(fileUploadDoc.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlDoc = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlDoc);

                    fileUploadDoc.SaveAs(physicalFilename);

                    tour.DocumentsFileUrl = newFilenameUrlDoc;
                }


                string newFilenameUrlForm = string.Empty;
                if (fileUploadForm != null)
                {
                    string filename = Path.GetFileName(fileUploadForm.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlForm = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlForm);

                    fileUploadForm.SaveAs(physicalFilename);

                    tour.FormFileUrl = newFilenameUrlForm;
                }

                #endregion

                if (String.IsNullOrEmpty(tour.PageTitle))
                {
                    tour.PageTitle = tour.Title + " | قیمت " + tour.Title + " | بکتاش سیر";
                    tour.PageDescription = "قیمت " + tour.Title + " | رزور آنلاین جدیدترین تورهای آسیایی و اروپایی" +
                                           "آژانس مسافرتی بکتاش سیر ارائه دهنده تورهای آسیایی و اروپایی با تنوع بسیار بالا و قیمت های عالی";
                }

                tour.Code = ReturnCode();
                tour.IsDelete = false;
                tour.SubmitDate = DateTime.Now;
                tour.Id = Guid.NewGuid();
                tour.LastModificationDate = DateTime.Now;

                db.Tours.Add(tour);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            ViewBag.TourCategoryId = new SelectList(db.TourCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title");

            ViewBag.AirlineId = new SelectList(db.AirLines, "Id", "Title", tour.AirlineId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Summary", tour.CountryId);
            return View(tour);
        }
        [AllowAnonymous]
        public string ReturnCode()
        {
            Tour tour = db.Tours.OrderByDescending(current => current.Code).FirstOrDefault();
            if (tour != null)
            {
                return (Convert.ToInt32(tour.Code) + 1).ToString();
            }
            else
            {
                return "1000";
            }
        }
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirlineId = new SelectList(db.AirLines, "Id", "Title", tour.AirlineId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Title", tour.CountryId);
            ViewBag.TourCategoryId = new SelectList(db.TourCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title", tour.TourCategoryId);
            return View(tour);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tour tour, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUploadDoc, HttpPostedFileBase fileUploadForm)
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

                    tour.ImageUrl = newFilenameUrl;
                }

                string newFilenameUrlDoc = string.Empty;
                if (fileUploadDoc != null)
                {
                    string filename = Path.GetFileName(fileUploadDoc.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlDoc = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlDoc);

                    fileUploadDoc.SaveAs(physicalFilename);

                    tour.DocumentsFileUrl = newFilenameUrlDoc;
                }


                string newFilenameUrlForm = string.Empty;
                if (fileUploadForm != null)
                {
                    string filename = Path.GetFileName(fileUploadForm.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrlForm = "/Uploads/Tour/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrlForm);

                    fileUploadForm.SaveAs(physicalFilename);

                    tour.FormFileUrl = newFilenameUrlForm;
                }

                #endregion

                tour.IsDelete = false;
                tour.LastModificationDate = DateTime.Now;

                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AirlineId = new SelectList(db.AirLines, "Id", "Title", tour.AirlineId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Summary", tour.CountryId);
            ViewBag.TourCategoryId = new SelectList(db.TourCategories.Where(current => current.IsDelete == false).OrderBy(current => current.Title), "Id", "Title", tour.TourCategoryId);
            return View(tour);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.category = tour.TourCategoryId;
            return View(tour);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Tour tour = db.Tours.Find(id);
            tour.IsDelete = true;
            tour.DeleteDate = DateTime.Now;

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

        [AllowAnonymous]
        [Route("tour/{urlparam}")]
        public ActionResult List(string urlparam)
        {
            List<Tour> tours = ReturnTourList(urlparam);
            TourListViewModel tourListViewModel = new TourListViewModel();
            tourListViewModel.Menu = menu.ReturnMenuTours();
            tourListViewModel.Tours = tours;
            tourListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();

            TourCategory tourCategory = db.TourCategories.Include(current => current.Parent)
                .FirstOrDefault(current => current.UrlParam == urlparam);

            tourListViewModel.TourCategory = tourCategory;
            tourListViewModel.Footer = menu.ReturnFooter();
            ViewBag.Canonical = "https://www.bektashtravel.com/tour/" + urlparam;

            tourListViewModel.SidebarVisaList = GetSideBarVisaList();

            tourListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            if (tours == null)
                return RedirectPermanent("/tour");


            return View(tourListViewModel);
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
        [Route("tour")]
        public ActionResult PureList()
        {
            TourListViewModel tourListViewModel = new TourListViewModel();
            tourListViewModel.Menu = menu.ReturnMenuTours();
            tourListViewModel.Tours = ReturnTourList(null);
            tourListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();

            tourListViewModel.Footer = menu.ReturnFooter();


            tourListViewModel.SidebarVisaList = GetSideBarVisaList();

            tourListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            ViewBag.Canonical = "https://www.bektashtravel.com/tour/";

            ViewBag.Description =
                "بکتاش سیر ارائه دهنده تورهای متنوع آسیایی و اروپایی و تور کانادا. جهت مشاهده تورهای متنوع و اطلاع از قیمت های تورها به وب سایت بکتاش سیر یا با شماره تلفن 02157952 تماس بگیرید.";
            ViewBag.Title = "تورهای آسیایی و اروپایی و تور کانادا بکتاش سیر";

            return View(tourListViewModel);
        }


        [AllowAnonymous]
        [Route("تور-نوروز")]
        public ActionResult SpecialList(string urlparam)
        {
            TourListViewModel tourListViewModel = new TourListViewModel();
            tourListViewModel.Menu = menu.ReturnMenuTours();
            tourListViewModel.Tours = ReturnSpecialTourList();
            tourListViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            tourListViewModel.TourCategory = db.TourCategories.Include(current => current.Parent)
                .FirstOrDefault(current => current.UrlParam == urlparam);
            tourListViewModel.Footer = menu.ReturnFooter();
            tourListViewModel.SidebarVisaList = GetSideBarVisaList();

            tourListViewModel.SidebarTourCategories = GetSideBarTourCategory();

            ViewBag.Canonical = "https://www.bektashtravel.com/تور-نوروز";

            ViewBag.Description =
                "بکتاش سیر ارائه دهنده تورهای نوروز 99 با بهترین قیمت. جهت مشاهده تورهای متنوع و اطلاع از قیمت های تورها به وب سایت بکتاش سیر یا با شماره تلفن 02157952 تماس بگیرید.";
            ViewBag.Title = "تورهای نوروز 99 در بکتاش سیر";

            return View(tourListViewModel);
        }

        #region Functions
        [AllowAnonymous]
        public List<Tour> ReturnTourList(string urlparam)
        {
            if (urlparam == null)
                return db.Tours.Include(current => current.Airline).Include(current => current.TourCategory).Where(
                        current => current.IsDelete == false && current.IsActive)
                    .ToList();

            List<Tour> tours = null;
            TourCategory tourCategory = db.TourCategories.FirstOrDefault(current => current.UrlParam == urlparam && current.IsDelete == false);

            if (tourCategory != null)
            {
                if (tourCategory.ParentId != null)
                {
                    tours = db.Tours.Include(current => current.Airline).Include(current => current.TourCategory).Where(
                            current =>
                                current.IsDelete == false && current.TourCategoryId == tourCategory.Id &&
                                current.IsActive)
                        .ToList();

                }
                else
                {
                    tours = db.Tours.Include(current => current.Airline).Include(current => current.TourCategory).Where(
                            current =>
                                current.IsDelete == false && current.TourCategory.ParentId == tourCategory.Id &&
                                current.IsActive)
                        .ToList();

                    if (tours.Count() == 0)
                        tours = db.Tours.Include(current => current.Airline).Include(current => current.TourCategory)
                            .Where(current =>
                                current.IsDelete == false && current.TourCategoryId == tourCategory.Id &&
                                current.IsActive)
                            .ToList();
                }

                ViewBag.coverImage = !string.IsNullOrEmpty(tourCategory.CoverImage)
                    ? tourCategory.CoverImage
                    : "/Images/header1.jpg";

                if (!string.IsNullOrEmpty(tourCategory.PageTitle))
                    ViewBag.Title = tourCategory.PageTitle;
                else
                    ViewBag.Title = tourCategory.Title + " | قیمت " + tourCategory.Title + " ویژه نوروز و بهار | بکتاش سیر";

                if (!string.IsNullOrEmpty(tourCategory.PageDescription))
                    ViewBag.Description = tourCategory.PageDescription;
                else
                    ViewBag.Description = "برای اطلاع از قیمت " + tourCategory.Title + " وارد وب سایت بکتاش سیر شوید یا با شماره تلفن 02157952 تماس بگیرید. بکتاش ارائه دهنده تورهای متنوع آسیایی و اروپایی";


                ViewBag.OrginalTitle = tourCategory.Title;
                ViewBag.summery = tourCategory.Summery;
            }
            else
            {
                Models.Type type = db.Types.FirstOrDefault(current => current.UrlParam == urlparam && current.IsDelete == false);

                if (type != null)
                {
                    tours = db.Tours
                        .Where(current => (current.TourCategory.TypeId == type.Id || current.TourCategory.Parent.TypeId == type.Id) && current.IsDelete == false)
                        .Include(current => current.Airline).Include(current => current.TourCategory).ToList();

                    if (!tours.Any())
                    {
                        tours = db.Tours
                            .Where(current => current.TourCategory.Parent.TypeId == type.Id && current.IsDelete == false)
                            .Include(current => current.Airline).Include(current => current.TourCategory).ToList();
                    }

                    if (!string.IsNullOrEmpty(type.PageTitle))
                        ViewBag.Title = type.PageTitle;
                    else
                        ViewBag.Title = type.Title + " | قیمت " + type.Title + " ویژه نوروز و بهار";

                    if (!string.IsNullOrEmpty(type.PageDescription))
                        ViewBag.Description = type.PageDescription;
                    else
                        ViewBag.Description = "برای اطلاع از قیمت " + type.Title + " وارد وب سایت بکتاش سیر شوید یا با شماره تلفن 02157952 تماس بگیرید. بکتاش ارائه دهنده تورهای متنوع آسیایی و اروپایی";

                    ViewBag.OrginalTitle = type.Title;
                    ViewBag.summery = type.Summery;
                }
            }

            return tours;
        }
        [AllowAnonymous]
        public List<Tour> ReturnSpecialTourList()
        {

            List<Tour> tours = db.Tours.Include(current => current.Airline).Include(current => current.TourCategory).Where(
                    current => current.IsDelete == false && current.IsSpecial == true && current.IsActive)
                .ToList();

            return tours;
        }
        //[AllowAnonymous]
        //public List<TourPackageViewModel> ReturnTourHotelPackageList(Guid id)
        //{
        //    List<TourPackageViewModel> oTourPackageViewModel = new List<TourPackageViewModel>();
        //    List<TourPackage> oTourHotelsPackages = db.TourPackages.Where(a => a.TourId == id && a.IsDelete == false).ToList();

        //    foreach (var item in oTourHotelsPackages)
        //    {
        //        List<Hotel> oTourHotels = db.TourHotelPackageDetails.Include(a => a.TourHotel).Where(a => a.TourHotelPackageId == item.Id && a.IsDelete == false).Select(a => a.TourHotel).ToList();
        //        //foreach (TourHotel tourhotel in oTourHotels)
        //        //{
        //        if (oTourHotels.Count() > 0)
        //        {
        //            TourPackagePrice tourPackagePrice = new TourPackagePrice();
        //            if (!item.TwoBedRoomPrice.Any(x => char.IsLetter(x)))
        //            {
        //                tourPackagePrice.ChildWithBed = string.Format("{0:0,0}", Convert.ToInt32(item.ChildWithBedPrice));
        //                tourPackagePrice.TwoBedRoom = string.Format("{0:0,0}", Convert.ToInt32(item.TwoBedRoomPrice));
        //                tourPackagePrice.OneBedRoom = string.Format("{0:0,0}", Convert.ToInt32(item.OneBedRoomPrice));
        //                tourPackagePrice.ChildWithoutBed = string.Format("{0:0,0}", Convert.ToInt32(item.ChildWithoutBedPrice));

        //                if (!string.IsNullOrEmpty(item.TwoBedRoomPrice_ExtraNight))
        //                {

        //                    tourPackagePrice.ChildWithBed_extra = string.Format("{0:0,0}", Convert.ToInt32(item.ChildWithBedPrice_ExtraNight));
        //                    tourPackagePrice.TwoBedRoom_extra = string.Format("{0:0,0}", Convert.ToInt32(item.TwoBedRoomPrice_ExtraNight));
        //                    tourPackagePrice.OneBedRoom_extra = string.Format("{0:0,0}", Convert.ToInt32(item.OneBedRoomPrice_ExtraNight));
        //                    tourPackagePrice.ChildWithoutBed_extra = string.Format("{0:0,0}", Convert.ToInt32(item.ChildWithoutBedPrice_ExtraNight));
        //                }
        //            }
        //            else
        //            {
        //                tourPackagePrice.ChildWithBed = item.ChildWithBedPrice;
        //                tourPackagePrice.ChildWithBed_extra = item.ChildWithBedPrice_ExtraNight;
        //                tourPackagePrice.OneBedRoom = item.OneBedRoomPrice;
        //                tourPackagePrice.OneBedRoom_extra = item.OneBedRoomPrice_ExtraNight;
        //                tourPackagePrice.TwoBedRoom = item.TwoBedRoomPrice;
        //                tourPackagePrice.TwoBedRoom_extra = item.TwoBedRoomPrice_ExtraNight;
        //                tourPackagePrice.ChildWithoutBed = item.ChildWithoutBedPrice;
        //                tourPackagePrice.ChildWithoutBed_extra = item.ChildWithoutBedPrice_ExtraNight;
        //            }
        //            oTourPackageViewModel.Add(new TourPackageViewModel
        //            {
        //                //TourHotelPackage = item,
        //                TourHotels = oTourHotels,
        //                TourPackagePrices = tourPackagePrice
        //                //TourHotel = tourhotel
        //            });
        //        }
        //        //}
        //    }
        //    if (!oTourPackageViewModel.FirstOrDefault().TourPackagePrices.TwoBedRoom.Any(x => char.IsLetter(x)))
        //    {
        //        return oTourPackageViewModel.OrderBy(current => current.TourPackagePrices.TwoBedRoom.Length).ThenBy(current => current.TourPackagePrices.TwoBedRoom).ToList();
        //    }
        //    else
        //    {
        //        return oTourPackageViewModel.OrderBy(current => current.TourPackagePrices.TwoBedRoom).ToList();
        //    }
        //    //return oTourPackageViewModel.OrderBy(current => current.TourHotelPackage.TwoBedRoomPrice).ThenBy(a => a.TourHotels.Max(x => x.Star)).ToList();

        //    //return oTourPackageViewModel.OrderBy(a => a.TourHotels.Max(x => x.Star)).ThenBy(current=>current.TourHotelPackage.TwoBedRoomPrice).ToList();
        //    //return oTourPackageViewModel.OrderBy(current => current.TourHotel.Star).ThenBy(current => current.TourHotelPackage.TwoBedRoomPrice).ToList();
        //}
        [AllowAnonymous]
        public ActionResult RequestPost(string fullname, string email, string mobile, string number, string id)
        {
            if (ModelState.IsValid)
            {
                bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                    return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
                else
                {
                    Reservation reserve = new Reservation()
                    {

                        Email = email,
                        Id = Guid.NewGuid(),
                        IsDelete = false,
                        SubmitDate = DateTime.Now,
                        Fullname = fullname,
                        Number = Convert.ToInt32(number),
                        Mobile = mobile,
                        TourId = new Guid(id)
                    };
                    db.Reservations.Add(reserve);
                    db.SaveChanges();

                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            else

                return Json("false", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public List<Tour> ReturnSuggestedTours(Tour tour)
        {
            List<Tour> suggestedTour = new List<Tour>();

            suggestedTour = db.Tours.Where(current => current.IsDelete == false && current.IsActive && current.Id != tour.Id && current.TourCategoryId == tour.TourCategoryId).Include(current => current.TourCategory).Take(3).ToList();

            if (!suggestedTour.Any())
            {
                TourCategory category = db.TourCategories.Find(tour.TourCategoryId);

                List<TourCategory> tourCategories = db.TourCategories.Where(current => current.IsDelete == false && current.ParentId == category.ParentId).ToList();

                foreach (TourCategory tourCategory in tourCategories)
                {
                    List<Tour> tours = db.Tours.Where(current => current.IsDelete == false
                    && current.IsActive == true
                    && current.TourCategoryId == tourCategory.Id && current.Id != tour.Id).Include(current => current.TourCategory).Take(3).ToList();
                    foreach (Tour oTour in tours)
                    {
                        suggestedTour.Add(oTour);
                    }
                }

                if (!suggestedTour.Any())
                {
                    suggestedTour = db.Tours
                        .Where(current => current.IsInHome && current.IsDelete == false && current.IsActive).Include(current => current.TourCategory).Take(3).ToList();
                }

                //suggestedTour = db.Tours.Include(current=>current.TourCategory)
                //    .Where(current => current.IsDelete == false && current.IsActive == true 
                //    && current.Id != tour.Id && current.TourCategory.ParentId==category.ParentId).ToList();
            }

            int count = suggestedTour.Count();

            if (count >= 3)
                return suggestedTour.Take(3).ToList();


            int diffrence = 3 - count;

            List<Tour> diffrenceTours = db.Tours.Where(c => c.IsInHome && c.IsDelete == false && c.IsActive).Include(c => c.TourCategory).Take(diffrence)
                .ToList();

            foreach (Tour tourdif in diffrenceTours)
            {
                suggestedTour.Add(tourdif);
            }

            return suggestedTour;
        }
        #endregion


        public string ConvertData()
        {


            return "";
        }
    }
}
