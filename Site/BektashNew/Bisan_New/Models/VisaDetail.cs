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
    public class VisaDetail : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource.Models.Visa))]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

       

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        [Display(Name = "نام یکتا")]
        public string UrlParam { get; set; }

        public Guid VisaId { get; set; }
        public virtual Visa Visa { get; set; }
        internal class configuration : EntityTypeConfiguration<VisaDetail>
        {
            public configuration()
            {
                HasRequired(p => p.Visa).WithMany(t => t.VisaDetails).HasForeignKey(p => p.VisaId);
            }
        }
    }
}