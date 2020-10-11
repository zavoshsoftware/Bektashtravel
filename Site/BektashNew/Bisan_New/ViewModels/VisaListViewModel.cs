using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class VisaListViewModel : BaseViewModel
    {
        public List<Visa> VisaList { get; set; }
        public List<Visa> SidebarVisaList { get; set; }
        public List<TourCategory> SidebarTourCategories { get; set; }
    }
}