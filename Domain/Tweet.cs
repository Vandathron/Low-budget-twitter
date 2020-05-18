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
        public string message { get; set; }

        [Timestamp]
        public DateTime TimePosted { get; set; }
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public User user { get; set; }

        public List<Comment> comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
