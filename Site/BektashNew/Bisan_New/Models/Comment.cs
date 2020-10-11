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
    public class Comment:BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Fullname { get; set; }

        public Guid TypeId { get; set; }

        public CommentType CommentType { get; set; }

        public Guid EntityId { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource.Models.Comment))]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Mobile", ResourceType = typeof(Resource.Models.Comment))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Mobile { get; set; }
        [Display(Name = "نمایش")]
        public bool IsShow { get; set; }

        public string Response { get; set; }
        internal class configuration : EntityTypeConfiguration<Comment>
        {
            public configuration()
            {
                HasRequired(p => p.CommentType).WithMany(t => t.Comments).HasForeignKey(p => p.TypeId);
            }
        }
    }
}