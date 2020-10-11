using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class TourDetailViewModel:BaseViewModel
    {
        public Tour Tour { get; set; }
        public TourCategory TourCategory { get; set; }
        public List<Tour> SuggestedTours { get; set; }
        public List<TourImage> Images { get; set; }
        public List<TourPackage> TourPackages { get; set; }
    }
}