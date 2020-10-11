using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class AirLine:BaseEntity
    {
        public AirLine()
        {
            Tours=new List<Tour>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resource.Models.AirLine))]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Title { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resource.Models.AirLine))]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string ImageUrl { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}