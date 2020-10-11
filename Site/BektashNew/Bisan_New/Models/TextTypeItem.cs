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
    public class TextTypeItem:BaseEntity
    {
        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
       
        [Display(Name = "متن توضیحات")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
       
        public string Body { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]

        public string ImageUrl { get; set; }
        [Display(Name = "گروه ")]
        public Guid TextTypeId { get; set; }

        public TextType TextType { get; set; }


      
        internal class configuration : EntityTypeConfiguration<TextTypeItem>
        {
            public configuration()
            {
                HasRequired(p => p.TextType).WithMany(t => t.TextTypeItems).HasForeignKey(p => p.TextTypeId);
            }
        }

         
    }
}