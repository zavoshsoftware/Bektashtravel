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
    public class Tour : BaseEntity
    {
        public Tour()
        {
            TourPackages = new List<TourPackage>();
            TourImages = new List<TourImage>();
            Reservations = new List<Reservation>();
            TourDetails = new List<TourDetail>();
            VisaTours = new List<VisaTour>();
        }

        [Display(Name = "Title", ResourceType = typeof(Resource.Models.Tour))]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Title { get; set; }
        [Display(Name = "TripProgram", ResourceType = typeof(Resource.Models.Tour))]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string TripProgram { get; set; }
        [Display(Name = "Priority", ResourceType = typeof(Resource.Models.Tour))]
        public int? Priority { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource.Models.Tour))]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Price { get; set; }

        public Guid AirlineId { get; set; }
        public AirLine Airline { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [Display(Name = "GoneDate", ResourceType = typeof(Resource.Models.Tour))]
        [Column(TypeName = "datetime2")]
        public DateTime? GoneDate { get; set; }
        [Display(Name = "ReturnDate", ResourceType = typeof(Resource.Models.Tour))]
        [Column(TypeName = "datetime2")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resource.Models.Tour))]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        [Display(Name = "Duration", ResourceType = typeof(Resource.Models.Tour))]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Duration { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Resource.Models.Tour))]
        public bool IsActive { get; set; }
        [Display(Name = "IsInHome", ResourceType = typeof(Resource.Models.Tour))]
        public bool IsInHome { get; set; }


        [Display(Name = "Code", ResourceType = typeof(Resource.Models.Tour))]
        public string Code { get; set; }

        [Display(Name = "خدمات آژانس")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string AgencyService { get; set; }

        [Display(Name = "مدارک لازم ")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Documents { get; set; }

        [Display(Name = "حداکثر بار مجاز")]
        public string MaxWeight { get; set; }

        [Display(Name = "توضیحات")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "امکانات تور")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Features { get; set; }

        [Display(Name = "تور اروپایی است؟")]
        public bool IsEurope { get; set; }

        [Display(Name = "تور ویژه نوروز؟")]
        public bool? IsSpecial { get; set; }


        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        [Display(Name = "فایل مدارک ")]
        public string DocumentsFileUrl { get; set; }


        [Display(Name = "فایل فرم ")]
        public string FormFileUrl { get; set; }

        [Display(Name = "تکمیل شده است")]
        public bool IsSoldOut { get; set; }

        public Guid? TourCategoryId { get; set; }
        public TourCategory TourCategory { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
        public virtual ICollection<TourImage> TourImages { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<TourDetail> TourDetails { get; set; }
        public virtual ICollection<VisaTour> VisaTours { get; set; }
        internal class configuration : EntityTypeConfiguration<Tour>
        {
            public configuration()
            {
                HasOptional(p => p.TourCategory).WithMany(t => t.Tours).HasForeignKey(p => p.TourCategoryId);
                HasRequired(p => p.Airline).WithMany(t => t.Tours).HasForeignKey(p => p.AirlineId);
                HasRequired(p => p.Country).WithMany(t => t.Tours).HasForeignKey(p => p.CountryId);
            }
        }
    }
}