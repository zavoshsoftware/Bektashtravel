using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class OrderDetail:BaseEntity
    {

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal RowAmount { get; set; }


        [NotMapped]
        public string AmountStr
        {
            get { return Amount.ToString("n0"); }
        }

        internal class configuration : EntityTypeConfiguration<OrderDetail>
        {
            public configuration()
            {
                HasRequired(p => p.Order).WithMany(t => t.OrderDetails).HasForeignKey(p => p.OrderId);
                HasRequired(p => p.Product).WithMany(t => t.OrderDetails).HasForeignKey(p => p.ProductId);
                HasOptional(p => p.VisaType).WithMany(t => t.OrderDetails).HasForeignKey(p => p.VisaTypeId);
            }
        }

        public Guid? VisaTypeId { get; set; }
        public VisaType VisaType { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PasportNumber { get; set; }
        public DateTime? PasportExpireDate { get; set; }
        public string Nationality { get; set; }
        public DateTime? VisitDate1 { get; set; }
        public DateTime? VisitDate2 { get; set; }
        public DateTime? TravelDate { get; set; }
        public string PasportFileUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
    }
}