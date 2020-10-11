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
    public class TourCategory:BaseEntity
    {
        public TourCategory()
        {
            TourCategories = new List<TourCategory>();
            Tours = new List<Tour>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resource.Models.TourCategory))]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource.Models.TourCategory))]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Priority", ResourceType = typeof(Resource.Models.TourCategory))]
        public int Priority { get; set; }
        [Display(Name = "ImageUrl", ResourceType = typeof(Resource.Models.Tour))]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        [Display(Name = "کاور صفحه")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string CoverImage { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }
        public Guid? ParentId { get; set; }
        public TourCategory Parent { get; set; }
        public virtual ICollection<TourCategory> TourCategories { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
        public virtual Type Type { get; set; }
        public Guid? TypeId { get; set; }

        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }


        [Display(Name = "بهترین فصل سفر ")]
        public string BestSeason { get; set; }

        [Display(Name = "طول پرواز")]
        public string FlightDuration { get; set; }

        [Display(Name = "واحد پولی")]
        public string Money { get; set; }

        [Display(Name = "زبان رسمی")]
        public string Language { get; set; }

        [Display(Name = "پیش شماره")]
        public string PhonePrefix { get; set; }

        [Display(Name = "اختلاف ساعت")]
        public string TimeDiffrence { get; set; }

        [Display(Name = "نمایش اطلاعات اضافه")]
        public bool ShowAdditionalInfo { get; set; }

        [Display(Name = "کشور")]
        public string Country { get; set; }

        [Display(Name = "در صفحه اصلی باشد")]
        public bool IsInHome { get; set; }

        [Display(Name = "تعداد تور")]
        public int TourCount { get; set; }

        public bool ShowTourCount { get; set; }
        internal class configuration : EntityTypeConfiguration<TourCategory>
        {
            public configuration()
            {
                HasRequired(p => p.Parent).WithMany(t => t.TourCategories).HasForeignKey(p => p.ParentId);
                HasOptional(p => p.Type).WithMany(t => t.TourCategories).HasForeignKey(p => p.TypeId);
            }
        }

    }
}