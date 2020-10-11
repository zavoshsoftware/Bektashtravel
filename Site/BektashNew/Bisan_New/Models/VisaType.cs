using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class VisaType:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}