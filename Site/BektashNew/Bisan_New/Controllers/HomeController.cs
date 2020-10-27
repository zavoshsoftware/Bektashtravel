using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Helpers;
using Models;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace Bisan.Controllers
{
    public class HomeController : Controller
    {
        MenuHelper menu = new MenuHelper();
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        public ActionResult Index()
        {
            
            MenuHelper menu = new MenuHelper();
            HomeViewModel home = new HomeViewModel();
            home.Menu = menu.ReturnMenuTours();
            home.Footer = menu.ReturnFooter();
            home.MenuBlogGroups = menu.ReturnBlogGroups();
            home.VisaList = db.Visas.Where(current => current.IsDelete == false).OrderByDescending(current => current.Order).Take(3).ToList();
            home.PopularTours = RetrunPopularTour();
            home.EuropeTours = RetrunEuropeTour();
            home.HomeTourCategories = db.TourCategories.Where(current => current.IsDelete == false&&current.IsInHome)
                .OrderByDescending(current => current.Priority).Take(6).ToList();

            return View(home);
        }
        public List<PopularTour> RetrunPopularTour()
        {
            List<PopularTour> popularTours = new List<PopularTour>();

            List<Tour> tours= db.Tours.Where(current => current.IsDelete == false && current.IsActive&&current.IsInHome).Include(current => current.TourCategory).OrderBy(current => current.Priority).Take(3).ToList();
            foreach(Tour tour in tours)
            {
                popularTours.Add(new PopularTour { Tour = tour, AirLine = db.AirLines.Find(tour.AirlineId) });
            }
            return popularTours;
        }
        public List<SpecialTour> RetrunEuropeTour()
        {
            List<SpecialTour> popularTours = new List<SpecialTour>();

            List<Tour> tours = db.Tours.Where(current => current.IsDelete == false && current.IsActive && current.IsEurope).Include(current => current.TourCategory).OrderBy(current => current.Priority).Take(3).ToList();
            foreach (Tour tour in tours)
            {
                popularTours.Add(new SpecialTour { Tour = tour, AirLine = db.AirLines.Find(tour.AirlineId) });
            }
            return popularTours;

        }
        [Route("about")]
        public ActionResult About()
        {
            Guid aboutId = new Guid("d2e6c1a6-769a-41a1-a4b9-37f1c8fc4f8f");
            Guid downTexts = new Guid("6d01e6bb-c33e-4a30-a1ed-10690092c9c3");
            AboutViewModel aboutViewModel = new AboutViewModel();

            aboutViewModel.Menu = menu.ReturnMenuTours();
            aboutViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            aboutViewModel.About = db.TextTypeItems.Where(current => current.TextTypeId == aboutId).FirstOrDefault();
            aboutViewModel.DownPageTexts = db.TextTypeItems.Where(current => current.TextTypeId == downTexts).ToList();
            aboutViewModel.Footer = menu.ReturnFooter();
            return View(aboutViewModel);
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            Guid textId = new Guid("8c8d374e-ff16-490c-83f3-a14b61ed4843");
            MenuHelper menu = new MenuHelper();
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Menu = menu.ReturnMenuTours();
            contactViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            contactViewModel.Footer = menu.ReturnFooter();
            contactViewModel.Description = db.TextTypeItems.Find(textId);
            return View(contactViewModel);
        }
        public ActionResult RequestPost(string fullname, string message, string mobile)
        {
            if (ModelState.IsValid)
            {
                Guid typeId = new Guid("2f4d1d33-5566-48c8-bfdd-1d15f93f7898");
                Comment comment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    IsDelete = false,
                    SubmitDate = DateTime.Now,
                    Fullname = fullname,
                    Description = message,
                    Mobile = mobile,
                    EntityId = typeId,
                    TypeId = typeId,
                    IsShow = false
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else

                return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SubmitNewLetter(string email)
        {
            if (ModelState.IsValid)
            {
                bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                    return Json("InvalidEmail", JsonRequestBehavior.AllowGet);
                else
                {
                    NewsLetter newsLetter = new NewsLetter()
                    {
                        Id = Guid.NewGuid(),
                        IsDelete = false,
                        SubmitDate = DateTime.Now,
                        Email = email

                    };
                    db.NewsLetters.Add(newsLetter);
                    db.SaveChanges();

                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            else

                return Json("false", JsonRequestBehavior.AllowGet);
        }

        //public void ConvertPricesToInt()
        //{
        //    Guid tourId = new Guid("59459f27-6d1e-447f-ba75-8b2e1fbb11bc");
        //    List<TourPackage> tourPackages = db.TourPackages.Where(current => current.IsDelete == false && current.TourId==tourId).ToList();
        //    foreach (TourPackage package in tourPackages)
        //    {
        //        if (package.TwoBedRoomPrice != null)
        //        {
        //            if (!package.TwoBedRoomPrice.Any(x => char.IsLetter(x)))
        //            {
        //                package.TwoBedRoomPrice = Regex.Replace(package.TwoBedRoomPrice, "[^0-9]", "");
        //            }
        //        }
        //        if (package.OneBedRoomPrice != null)
        //        {
        //            if (!package.OneBedRoomPrice.Any(x => char.IsLetter(x)))
        //            {
        //                package.OneBedRoomPrice = Regex.Replace(package.OneBedRoomPrice, "[^0-9]", "");
        //            }
        //        }
        //        if (package.ChildWithBedPrice != null)
        //        {
        //            if (!package.ChildWithBedPrice.Any(x => char.IsLetter(x)))
        //            {
        //                package.ChildWithBedPrice = Regex.Replace(package.ChildWithBedPrice, "[^0-9]", "");
        //            }
        //        }
        //        if (package.ChildWithoutBedPrice != null)
        //        {

        //            if (!package.ChildWithoutBedPrice.Any(x => char.IsLetter(x)))
        //            {
        //                package.ChildWithoutBedPrice = Regex.Replace(package.ChildWithoutBedPrice, "[^0-9]", "");
        //            }
        //        }
        //        db.SaveChanges();

        //    }

        //}



        [Route("sit")]
        public ActionResult Sit()
        {
        
            Guid sitid = new Guid("8fedfbef-9e9a-45f0-b7dc-129c19bbdaad");
        
            SitViewModel aboutViewModel = new SitViewModel();

            aboutViewModel.Menu = menu.ReturnMenuTours();
            aboutViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            aboutViewModel.About = db.TextTypeItems.Where(current => current.Id == sitid).FirstOrDefault();
            aboutViewModel.Footer = menu.ReturnFooter();
            return View(aboutViewModel);
        }

        [Route("CanadaEdu")]
        public ActionResult CanadaEdu()
        {
        
            Guid sitid = new Guid("f5437565-3cd7-41c1-bdee-a12e2252c674");
        
            SitViewModel aboutViewModel = new SitViewModel();

            aboutViewModel.Menu = menu.ReturnMenuTours();
            aboutViewModel.MenuBlogGroups = menu.ReturnBlogGroups();
            aboutViewModel.About = db.TextTypeItems.Where(current => current.Id == sitid).FirstOrDefault();
            aboutViewModel.Footer = menu.ReturnFooter();
            return View(aboutViewModel);
        }
    }
}