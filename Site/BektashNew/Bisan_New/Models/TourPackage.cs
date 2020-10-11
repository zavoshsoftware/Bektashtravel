using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class TourPackage : BaseEntity
    {
        public TourPackage()
        {
            OrderDetails=new List<OrderDetail>();
        }

        [Display(Name="اولویت")]
        public int? Order { get; set; }

        [Display(Name = "TwoBedRoomPrice", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string TwoBedRoomPrice { get; set; }

        [Display(Name = "OneBedRoomPrice", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string OneBedRoomPrice { get; set; }

        [Display(Name = "ChildWithBedPrice", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string ChildWithBedPrice { get; set; }

        [Display(Name = "ChildWithoutBedPrice", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string ChildWithoutBedPrice { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "TwoBedRoomPrice_ExtraNight", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string TwoBedRoomPrice_ExtraNight { get; set; }

        [Display(Name = "OneBedRoomPrice_ExtraNight", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string OneBedRoomPrice_ExtraNight { get; set; }

        [Display(Name = "ChildWithBedPrice_ExtraNight", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string ChildWithBedPrice_ExtraNight { get; set; }

        [Display(Name = "ChildWithoutBedPrice_ExtraNight", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        public string ChildWithoutBedPrice_ExtraNight { get; set; }

        public Guid TourId { get; set; }
        public Tour Tour { get; set; }

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        internal class configuration : EntityTypeConfiguration<TourPackage>
        {
            public configuration()
            {
                HasRequired(p => p.Tour).WithMany(t => t.TourPackages).HasForeignKey(p => p.TourId);
                HasRequired(p => p.Hotel).WithMany(t => t.TourPackages).HasForeignKey(p => p.HotelId);
            }
        }



      
    }
}