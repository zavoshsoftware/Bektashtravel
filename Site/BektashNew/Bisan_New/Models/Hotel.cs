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
    public class Hotel:BaseEntity
    {
        public Hotel()
        {
            TourPackages = new List<TourPackage>();
        }

        [Display(Name = "Title", ResourceType = typeof(Resource.Models.TourHotel))]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleEn { get; set; }

        [Display(Name = "Star", ResourceType = typeof(Resource.Models.TourHotel))]
        public int Star { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resource.Models.TourHotel))]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Country { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resource.Models.TourHotel))]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string City { get; set; }

        [Display(Name = "Priority", ResourceType = typeof(Resource.Models.TourHotel))]
        public int? Priority { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource.Models.TourHotelsPackage))]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
        public virtual ICollection<HotelFeature> HotelFeatures { get; set; }

        public Guid? HotelCategoryId { get; set; }
        public virtual HotelCategory HotelCategory { get; set; }

        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        public string Code { get; set; }
        internal class configuration : EntityTypeConfiguration<Hotel>
        {
            public configuration()
            {
                HasRequired(p => p.HotelCategory).WithMany(t => t.Hotels).HasForeignKey(p => p.HotelCategoryId);
            }
        }

    }
}