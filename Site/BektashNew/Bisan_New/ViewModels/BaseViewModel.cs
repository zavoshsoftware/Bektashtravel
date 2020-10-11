using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BaseViewModel
    {
        public List<TourTypeViewModel> Menu { get; set; }
        public TextTypeItem Footer { get; set; }
        public List<BlogGroup> MenuBlogGroups { get; set; }
    }


    public class MenuTour
    {
        public TourCategory TourCategoryParent { get; set; }
        public List<TourCategory> TourCategory { get; set; }
    }

   public class TourTypeViewModel
    {
        public Models.Type Type { get; set; }
        public List<MenuTour> TourCategories { get; set; }
    }
}