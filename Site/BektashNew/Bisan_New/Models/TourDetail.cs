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
    public class TourDetail:BaseEntity
    {
        public string Title { get; set; }

        public int Order { get; set; }

        [Display(Name = "TripProgram", ResourceType = typeof(Resource.Models.Tour))]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public Guid TourId { get; set; }

        public virtual Tour Tour { get; set; }

        internal class configuration : EntityTypeConfiguration<TourDetail>
        {
            public configuration()
            {
                HasRequired(p => p.Tour).WithMany(t => t.TourDetails).HasForeignKey(p => p.TourId);
            }
        }
    }
}