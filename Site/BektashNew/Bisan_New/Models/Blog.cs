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
    public class Blog : BaseEntity
    {


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [MaxLength(200)]
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        [Display(Name = "تصویر هدر")]
        public string HeaderImageUrl { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitNumber { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }


        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }
        public Guid BlogGroupId { get; set; }
        public BlogGroup BlogGroup { get; set; }



        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        internal class configuration : EntityTypeConfiguration<Blog>
        {
            public configuration()
            {
                HasRequired(p => p.BlogGroup).WithMany(t => t.Blogs).HasForeignKey(p => p.BlogGroupId);
            }
        }
    }



}