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
        public int id { get; set; }

        [Required]
        public string message { get; set; }

        [Timestamp]
        public DateTime TimePosted { get; set; }

        [Required]
        public int TweetId { get; set; }

        [ForeignKey(nameof(TweetId))]
        public Tweet tweet { get; set; }

        public List<Like> likes { get; set; }
    }
}
