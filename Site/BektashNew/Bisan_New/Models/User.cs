using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace  Models
{
    public class User:BaseEntity
    {
    
        public string FullName { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

 
        internal class configuration : EntityTypeConfiguration<User>
        {
            public configuration()
            {
                HasRequired(p => p.Role).WithMany(t => t.Users).HasForeignKey(p => p.RoleId);
            }
        }
    }
}