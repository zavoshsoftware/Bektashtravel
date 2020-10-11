using System;

namespace Models
{
    public class VisaTour : BaseEntity
    {
        public Guid VisaId { get; set; }

        public Guid TourId { get; set; }

        public virtual Visa Visa { get; set; }

        public virtual Tour Tour { get; set; }
    }
}