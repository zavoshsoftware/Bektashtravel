using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public List<Blog> Blogs { get; set; }
        public List<PopularTour> PopularTours { get; set; }
        public List<SpecialTour> EuropeTours { get; set; }
        public List<TourCategory> HomeTourCategories { get; set; }
        public List<Visa> VisaList { get; set; }
        
    }
    public class PopularTour
    {
        public Tour Tour { get; set; }
        public AirLine AirLine { get; set; }
    }
    public class SpecialTour
    {
        public Tour Tour { get; set; }
        public AirLine AirLine { get; set; }
    }
}