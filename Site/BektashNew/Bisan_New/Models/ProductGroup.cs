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
    public class ProductGroup:BaseEntity
    {
        public ProductGroup()
        {
            Products=new List<Product>();
            ProductGroups = new List<ProductGroup>();
        }
    

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "UrlParam")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string UrlParam { get; set; }

        [Display(Name = "PageTitle")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string PageTitle { get; set; }

        [Display(Name = "PageDescription")]
        [StringLength(1000, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }


        [Display(Name = "تصویر هدر")]
        public string HeaderImageUrl { get; set; }



        [Display(Name = "Body")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public virtual ProductGroup Parent { get; set; }
        public Guid? ParentId { get; set; }


        internal class configuration : EntityTypeConfiguration<ProductGroup>
        {
            public configuration()
            {
                HasRequired(p => p.Parent).WithMany(t => t.ProductGroups).HasForeignKey(p => p.ParentId);
            }
        }

    }
}