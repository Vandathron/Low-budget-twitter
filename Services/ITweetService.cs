using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Domain;

namespace Tweeter.Services
{
    public interface ITweetService
    { 
        public Task<bool> UserOwnsTweetAsync(int tweetId, int userId);

        public Task<TweetResponse> PostTweetAsync(Tweet tweet);
        public Task<TweetResponse> GetTweetByIdAsync(int tweetId);
        public Task<TweetResponse> GetTweetsByUserIdAsync(int userId);
        public Task<TweetResponse> DeleteTweetAsync(int tweetId);
    }
}
