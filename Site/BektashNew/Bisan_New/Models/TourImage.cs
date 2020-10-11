using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class TourImage:BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [MaxLength(200)]
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        public Guid TourId { get; set; }
        public Tour Tour { get; set; }
        internal class configuration : EntityTypeConfiguration<TourImage>
        {
            public configuration()
            {
                HasRequired(p => p.Tour).WithMany(t => t.TourImages).HasForeignKey(p => p.TourId);

            }
        }
    }
}