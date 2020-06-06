using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Domain;

namespace Tweeter.Contract.V1.Requests
{
    public class CommentRequest
    {
        public string CommentMessage { get; set; }
        public int TweetId { get; set; }
    }
}
