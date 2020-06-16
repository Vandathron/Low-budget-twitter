using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Domain
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Timestamp]
        public DateTime TimePosted { get; set; }

        public List<CommentLike> CommentLikes { get; set; }

        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
