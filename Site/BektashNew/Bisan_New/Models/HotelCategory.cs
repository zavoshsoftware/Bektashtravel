using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class HotelCategory:BaseEntity
    {
        public string Title { get; set; }
        public int Order { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        public string PageTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PageDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }
        public Guid? ParentId { get; set; }
        public HotelCategory Parent { get; set; }
        public virtual ICollection<HotelCategory> HotelCategories { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }

        internal class configuration : EntityTypeConfiguration<HotelCategory>
        {
            public configuration()
            {
                HasRequired(p => p.Parent).WithMany(t => t.HotelCategories).HasForeignKey(p => p.ParentId);
            }
        }

    }
}