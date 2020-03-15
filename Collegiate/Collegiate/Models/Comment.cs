using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Collegiate.Models
{

    public enum CommentType
    {
        UserComment,
        DriverComment,
        RiderComment,
        TripComment
    }
    public class Comment
    {
        public string CommentId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Body { get; set; }
        public CommentType Type { get; set; }
    }
}
