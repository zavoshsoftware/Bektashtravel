using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class FeatureType : BaseEntity
    {
        public FeatureType()
        {
            Features=new List<Feature>();
        }
        [Display(Name = "نوع ویژگی")]
        public string Title { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
    }
}