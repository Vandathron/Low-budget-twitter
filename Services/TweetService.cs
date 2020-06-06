using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tweeter.Contract.V1.Responses;
using Tweeter.Data;
using Tweeter.Domain;

namespace Tweeter.Services
{
    public class TweetService : ITweetService
    {
        private readonly ApplicationDbContext _dbContext;
        public TweetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TweetResponse> DeleteTweetAsync(int tweetId)
        {
             _dbContext.Tweets.Remove(new Tweet {Id = tweetId });
            await _dbContext.SaveChangesAsync();

            return new TweetResponse
            {
                StatusCode = 200,
                Id = tweetId
            };
        }

        public async Task<TweetResponse> GetTweetByIdAsync(int tweetId)
        {
            var tweet = await _dbContext.Tweets.FindAsync(tweetId);

            if(tweet == null)
            {
                return new TweetResponse
                {
                    StatusCode = 404,
                    ErrorMessage = "Tweet not found",
                    TweetMessage = null
                };
            }

            return TweetToTweetResponse(tweet);
        }

        public Task<TweetResponse> GetTweetsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TweetResponse> PostTweetAsync(Tweet tweet)
        {
            var tweetToPost = await _dbContext.Tweets.AddAsync(tweet);
            await _dbContext.SaveChangesAsync();
            return TweetToTweetResponse(tweetToPost.Entity);
        }

        public async Task<bool> UserOwnsTweetAsync(int tweetId, int userId)
        {
            var tweet =await _dbContext.Tweets.SingleAsync(x => x.Id == tweetId);

            if(tweet == null)
            {
                return false;
            }

            if (tweet.UserId != userId) return false;

            return true;
        }

        public TweetResponse TweetToTweetResponse(Tweet tweet)
        {
            return new TweetResponse
            {
                Id = tweet.Id,
                TweetMessage = tweet.Message,
                TimePosted = tweet.TimePosted
            };
        }
    }
}
