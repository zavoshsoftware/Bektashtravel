using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HotelListViewModel : BaseViewModel
    {
        
        public List<Hotel> Hotels { get; set; }
        public HotelCategory HotelCategory { get; set; }
        public List<Visa> SidebarVisaList { get; set; }
        public List<TourCategory> SidebarTourCategories { get; set; }
    }
}