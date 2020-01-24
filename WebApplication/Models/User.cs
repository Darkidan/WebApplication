using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int NumOfTopics { get; set; }
        public virtual ICollection<Topic> topics { get; set; }
        public int NumOfComments { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public string UserGroup { get; set; }
    }
}