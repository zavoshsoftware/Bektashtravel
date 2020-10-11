using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Reservation:BaseEntity
    {
        [Display(Name = "Fullname", ResourceType =typeof(Resource.Models.Reservation))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Fullname { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resource.Models.Reservation))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Email { get; set; }
        [Display(Name = "Mobile", ResourceType = typeof(Resource.Models.Reservation))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Mobile { get; set; }
        [Display(Name = "Number", ResourceType = typeof(Resource.Models.Reservation))]
        public int Number { get; set; }
        public Guid TourId { get; set; }
        public Tour Tour { get; set; }
        internal class configuration : EntityTypeConfiguration<Reservation>
        {
            public configuration()
            {
                HasRequired(p => p.Tour).WithMany(t => t.Reservations).HasForeignKey(p => p.TourId);

            }
        }
    }
}