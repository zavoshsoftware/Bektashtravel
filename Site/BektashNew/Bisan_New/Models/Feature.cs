using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Models
{
    public class Feature:BaseEntity
    {
        [Display(Name = "آیکن")]
        public string Icon { get; set; }

        [Display(Name = "ویژگی هتل")]
        public string Title { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int Order { get; set; }
        public Guid FeatureTypeId { get; set; }

        public virtual FeatureType FeatureType { get; set; }
        public virtual ICollection<HotelFeature> HotelFeatures { get; set; }

        internal class configuration : EntityTypeConfiguration<Feature>
        {
            public configuration()
            {
                HasRequired(p => p.FeatureType).WithMany(t => t.Features).HasForeignKey(p => p.FeatureTypeId);
            }
        }
    }
}