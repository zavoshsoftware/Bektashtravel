using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class TourCategoryListViewModel:BaseViewModel
    {
        public List<TourCategory> TourCategories { get; set; }
        public TourCategory Parent { get; set; }
    }
}