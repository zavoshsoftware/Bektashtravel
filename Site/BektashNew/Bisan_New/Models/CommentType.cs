using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class CommentType:BaseEntity
    {
        public CommentType()
        {
            Comments = new List<Comment>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resource.Models.Comment))]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}