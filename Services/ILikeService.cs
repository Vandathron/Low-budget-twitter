using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Contract.V1.Responses;

namespace Tweeter.Services
{
    public interface ILikeService
    {
        public Task<LikeResponse> LikeTweet(int tweetID, int userId);
        public Task<LikeResponse> LikeComment(int commentId, int userId);
    }
}
