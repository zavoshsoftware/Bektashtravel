using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class TourListViewModel:BaseViewModel
    {
        
        public List<Tour> Tours { get; set; }
        public TourCategory TourCategory { get; set; }
        public List<Visa> SidebarVisaList { get; set; }
        public List<TourCategory> SidebarTourCategories { get; set; }
    }
}