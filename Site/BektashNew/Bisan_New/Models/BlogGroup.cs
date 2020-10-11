using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class BlogGroup : BaseEntity
    {
        public BlogGroup()
        {
            Blogs = new List<Blog>();
        }
        [Display(Name = "عنوان گروه بلاگ")]
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
        [Display(Name = "کاور صفحه")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string CoverImage { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }
    }
}