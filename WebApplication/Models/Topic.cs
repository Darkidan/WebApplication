using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Topic
    {
        [Key]
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User Author { get; set; }
        public string Content { get; set; }
        public int NumOfComments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int ForumID { get; set; }
        [ForeignKey("ForumID")]
        public Forum Forum { get; set; }
    }
}