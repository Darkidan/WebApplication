using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CatagoryName { get; set; }
        public int NumOfForums { get; set; }
        public virtual ICollection<Forum> Forums { get; set; }
    }
}