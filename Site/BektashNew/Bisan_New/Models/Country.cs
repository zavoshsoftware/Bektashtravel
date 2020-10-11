using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Country
    {
        public Country()
        {
            Tours = new List<Tour>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Summary", ResourceType = typeof(Resource.Models.Country))]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Summary { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resource.Models.Country))]
        [MaxLength(30, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}