using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Role:BaseEntity
    {
        
        [Required]
        public string Title { get; set; }
        public string Name { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
        public virtual ICollection<User> Users { get; set; }
    }
}