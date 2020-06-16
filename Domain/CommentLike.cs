using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Domain
{
    public class CommentLike
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Timestamp]
        public DateTime TimeLiked { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
