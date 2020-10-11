using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Helpers;
using Models;

namespace Bisan_New.Controllers
{
    public class SiteMapGeneratorController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("{language?}/sitemap")]
        public ActionResult Sitemap()
        {
            Sitemap sm = new Sitemap();

            StaticPageSiteMap(sm);
            VisaSiteMap(sm);
            TourTypeSiteMap(sm);
            TourCategorySiteMap(sm);
            TourSiteMap(sm);
            BlogGroupSiteMap(sm);
            BlogSiteMap(sm);

            return new XmlResult(sm);
        }

        public void VisaSiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "https://www.bektashtravel.com/visa", 0.7D, Location.eChangeFrequency.monthly, DateTime.Now.AddDays(-1));

            List<Visa> visas = db.Visas.Where(current => current.IsDelete == false).ToList();

            foreach (Visa visa in visas)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/visa/" + visa.UrlParam, 0.9D, Location.eChangeFrequency.weekly, visa.SubmitDate);
            }
        }
        public void TourTypeSiteMap(Sitemap sm)
        {

            List<Models.Type> types = db.Types.Where(current => current.IsDelete == false).ToList();

            foreach (Models.Type type in types)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/tour/" + type.UrlParam, 0.9D, Location.eChangeFrequency.weekly, type.SubmitDate);
            }
        }


        public void TourCategorySiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "https://www.bektashtravel.com/tour/", 0.9D, Location.eChangeFrequency.daily, DateTime.Now);

            List<Models.TourCategory> tourCategories = db.TourCategories.Where(current => current.IsDelete == false).ToList();

            foreach (Models.TourCategory tourCategory in tourCategories)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/tour/" + tourCategory.UrlParam, 0.9D, Location.eChangeFrequency.weekly, tourCategory.SubmitDate);
            }
        }
        public void TourSiteMap(Sitemap sm)
        {
            List<Models.Tour> tours = db.Tours.Where(current => current.IsDelete == false).ToList();

            foreach (Models.Tour tour in tours)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/tour/" + tour.TourCategory.UrlParam + "/" + tour.Code, 0.7D, Location.eChangeFrequency.weekly, tour.SubmitDate);
            }
        }

        public void StaticPageSiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "https://www.bektashtravel.com/", 1D, Location.eChangeFrequency.weekly, DateTime.Now);

            AddToSiteMap(sm, "https://www.bektashtravel.com/%D8%AA%D9%88%D8%B1-%D9%86%D9%88%D8%B1%D9%88%D8%B2", 0.9D, Location.eChangeFrequency.monthly, DateTime.Now);


            //about
            AddToSiteMap(sm, "https://www.bektashtravel.com/about", 0.5D, Location.eChangeFrequency.monthly, DateTime.Now.AddDays(-20));

            //contact
            AddToSiteMap(sm, "https://www.bektashtravel.com/contact", 0.5D, Location.eChangeFrequency.yearly, DateTime.Now.AddDays(-20));

        }

        public void AddToSiteMap(Sitemap sm, string url, double? priority, Location.eChangeFrequency frequency, DateTime dt)
        {
            sm.Add(new Location()
            {
                // Url = string.Format("http://www.TechnoDesign.ir/Articles/{0}/{1}", 1, "SEO-in-ASP.NET-MVC"),
                Url = url,
                LastModified = dt.ToUniversalTime(),
                Priority = priority,
                ChangeFrequency = frequency
            });
        }


        public void BlogGroupSiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "https://www.bektashtravel.com/blog", 0.7D, Location.eChangeFrequency.daily, DateTime.Now.AddDays(-1));

            List<BlogGroup> blogGroups = db.BlogGroups.Where(current => current.IsDelete == false).ToList();

            foreach (BlogGroup blogGroup in blogGroups)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/blog/" + blogGroup.UrlParam, 0.7D, Location.eChangeFrequency.weekly, blogGroup.LastModificationDate.Value);
            }
        }

        public void BlogSiteMap(Sitemap sm)
        {
            List<Blog> blogs = db.Blogs.Where(current => current.IsDelete == false).Include(c => c.BlogGroup).ToList();

            foreach (Blog blog in blogs)
            {
                AddToSiteMap(sm, "https://www.bektashtravel.com/blog/" + blog.BlogGroup.UrlParam + "/" + blog.UrlParam, 0.9D, Location.eChangeFrequency.monthly, blog.LastModificationDate.Value);
            }
        }
    }
}