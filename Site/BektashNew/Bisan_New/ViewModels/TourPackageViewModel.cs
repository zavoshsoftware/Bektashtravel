using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class TourPackageViewModel
    {
        public TourPackage TourHotelPackage { get; set; }
        public List<Hotel> TourHotels { get; set; }
        public TourPackagePrice TourPackagePrices { get; set; }
    }

    public class TourPackagePrice
    {
        public string TwoBedRoom { get; set; }
        public string TwoBedRoom_extra { get; set; }
        public string OneBedRoom { get; set; }
        public string OneBedRoom_extra { get; set; }
        public string ChildWithBed { get; set; }
        public string ChildWithBed_extra { get; set; }
        public string ChildWithoutBed { get; set; }
        public string ChildWithoutBed_extra { get; set; }
    }
}