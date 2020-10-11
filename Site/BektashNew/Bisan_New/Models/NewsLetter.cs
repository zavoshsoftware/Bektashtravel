using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class NewsLetter:BaseEntity
    {
        [Display(Name = "Email", ResourceType = typeof(Resource.Models.Reservation))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Email { get; set; }
    }
}