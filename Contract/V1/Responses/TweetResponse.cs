using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Domain;

namespace Tweeter.Contract.V1.Responses
{
    public class TweetResponse
    {
        public int Id { get; set; }
        public string TweetMessage { get; set; }

        public DateTime TimePosted { get; set; }

        public List<Comment> comments { get; set; }

        public List<Like> Likes { get; set; }
    }
}
