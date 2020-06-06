using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Domain
{
    public class TweetLike
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Timestamp]
        public DateTime TimeLiked { get; set; }

        [Required]
        public int TweetId { get; set; }

        [ForeignKey(nameof(TweetId))]
        public Tweet Tweet { get; set; }
    }
}
