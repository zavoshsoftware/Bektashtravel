using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Visa:BaseEntity
    {
        public Visa()
        {
            VisaDetails=new List<VisaDetail>();
            VisaTours=new List<VisaTour>();
        }
        [Display(Name = "Title",ResourceType =typeof(Resource.Models.Visa))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource.Models.Visa))]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [MaxLength(200)]
        [Display(Name = "ImageUrl", ResourceType = typeof(Resource.Models.Visa))]
        public string ImageUrl { get; set; }

     
        [Display(Name = "تعداد بازدید")]
        public int? VisitNumber { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        public string UrlParam { get; set; }

        [Display(Name = "خلاصه")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "مدارک مورد نیاز")]
        public string Document { get; set; }

        [Display(Name = "فرم ها")]
        public string Form { get; set; }

        public virtual ICollection<VisaDetail> VisaDetails { get; set; }

        public virtual ICollection<VisaTour> VisaTours { get; set; }

        public string PageTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

    }
}