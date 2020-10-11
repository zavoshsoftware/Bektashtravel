using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Type:BaseEntity
    {
        public Type()
        {
            TourCategories=new List<TourCategory>();
        }

        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string UrlParam { get; set; }
        public int Order { get; set; }
        public virtual ICollection<TourCategory> TourCategories { get; set; }
        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }
    }
}