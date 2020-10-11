using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            //LastModificationDate = DateTime.Now;
        }
       
        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime SubmitDate { get; set; }
        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastModificationDate { get; set; }
    }
}