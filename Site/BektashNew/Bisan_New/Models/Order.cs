using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Order:BaseEntity
    {
        public Order()
        {
            OrderDetails=new List<OrderDetail>();
        }
        public int Code { get; set; }
        public decimal TotalAmount { get; set; }


        public bool IsPaid { get; set; }
        public bool IsFinal { get; set; }

        public string DeliverFullName { get; set; }
        public string DeliverCellNumber { get; set; }
        public string DeliverEmail { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string RefId { get; set; }
        [NotMapped]
        public string TotalAmountStr
        {
            get { return TotalAmount.ToString("n0"); }
        }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
 
    }
}