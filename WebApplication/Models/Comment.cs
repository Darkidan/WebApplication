using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentContent { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User CommentUser { get; set; }
        public int TopicID { get; set; }
        [ForeignKey("TopicID")]
        public Topic CommentTopic { get; set; }
    }
}