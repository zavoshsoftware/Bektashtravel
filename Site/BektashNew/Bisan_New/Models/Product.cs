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
    public class Product:BaseEntity
    {
        public Product()
        {
            OrderDetails=new List<OrderDetail>();
        }
        [Display(Name = "اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Order { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Code { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

       

        [Display(Name = "توضیحات کوتاه")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "متن کوتاه صفحه")]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string SmallBody { get; set; }

        [Display(Name = "متن طولانی صفحه")]
        [UIHint("RichText")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }

        [NotMapped]
        [Display(Name = "قیمت")]
        public string AmountStr
        {
            get { return Amount.ToString("n0"); }
        }

       
        public Guid ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
 
        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
         
     
        internal class configuration : EntityTypeConfiguration<Product>
        {
            public configuration()
            {
                HasRequired(p => p.ProductGroup).WithMany(t => t.Products).HasForeignKey(p => p.ProductGroupId);
           
            }
        }

  
    }
}