using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweeter.Contract.V1.Responses;
using Tweeter.Data;
using Tweeter.Domain;

namespace Tweeter.Services
{
    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext _dbContext;
        public LikeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LikeResponse> LikeComment(int commentId, int userId)
        {
            var comment = await _dbContext.Comments.FindAsync(commentId);

            if (comment != null)
            {
                var like = await _dbContext.CommentLikes.SingleOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
                if (like != null)
                {
                    _dbContext.CommentLikes.Remove(like);
                    return new LikeResponse
                    {
                        Status = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    var likeToAdd = new CommentLike { CommentId = commentId, UserId = userId, TimeLiked = DateTime.UtcNow };
                    await _dbContext.CommentLikes.AddAsync(likeToAdd);
                    await _dbContext.SaveChangesAsync();
                    return new LikeResponse
                    {
                        Status = true,
                        StatusCode = 200
                    };
                }
            }
            else return new LikeResponse
            {
                StatusCode = 404
            };
        }

        public async Task<LikeResponse> LikeTweet(int tweetID, int userId)
        {
            var tweet = await _dbContext.Tweets.FindAsync(tweetID);

            if (tweet != null)
            {
                var like = await _dbContext.TweetLikes.SingleOrDefaultAsync(x => x.TweetId == tweet.Id && x.UserId == userId);
                if (like != null)
                {
                    _dbContext.TweetLikes.Remove(like);
                    return new LikeResponse
                    {
                        Status = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    var likeToAdd = new TweetLike{ TweetId = tweetID, UserId = userId, TimeLiked = DateTime.UtcNow };
                    await _dbContext.TweetLikes.AddAsync(likeToAdd);
                    await _dbContext.SaveChangesAsync();
                    return new LikeResponse
                    {
                        Status = true,
                        StatusCode = 200
                    };
                }
            }
            else return new LikeResponse
            {
                StatusCode = 404
            };
        }
    }
}
