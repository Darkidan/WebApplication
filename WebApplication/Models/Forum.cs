using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Forum
    {
        [Key]
        public int ForumID { get; set; }
        public string ForumName { get; set; }
        public string ForumDescription { get; set; }
        public virtual int NumOfTopics { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category category { get; set; }
        public string background { get; set; }
    }
}