using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class HotelFeature:BaseEntity
    {
        public Guid FeatureId { get; set; }
        public Guid HotelId { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Hotel Hotel { get; set; }

        internal class configuration : EntityTypeConfiguration<HotelFeature>
        {
            public configuration()
            {
                HasRequired(p => p.Feature).WithMany(t => t.HotelFeatures).HasForeignKey(p => p.FeatureId);
                HasRequired(p => p.Hotel).WithMany(t => t.HotelFeatures).HasForeignKey(p => p.HotelId);
            }
        }
    }
}