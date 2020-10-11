using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    public class TextType:BaseEntity
    {
        public TextType()
        {
            TextTypeItems=new List<TextTypeItem>();
        }
        [Display(Name ="عنوان")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        public ICollection<TextTypeItem> TextTypeItems { get; set; }
    }
}