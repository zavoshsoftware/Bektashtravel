using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class VisaDetailViewModel : BaseViewModel
    {
        public Visa Visa { get; set; }
        public List<VisaDetail> VisaDetails { get; set; }
        public List<Tour> SuggestedTours { get; set; }
    }
}