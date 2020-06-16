using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Domain
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Timestamp]
        public DateTime TimePosted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Comment> Comments { get; set; }

        public List<TweetLike>  TweetLikes { get; set; }
    }
}
