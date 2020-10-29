﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Data;
using Tweeter.Domain;
using Tweeter.Options;

namespace Tweeter.Services
{
    public class CommentService : ICommentService
    {
        private JwtSettings _jwtSettings;
        private ApplicationDbContext _dbContext;
        public CommentService(JwtSettings jwtSettings, ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings;
            _dbContext = dbContext;
        }
        public async Task<CommentResponse> DeleteCommentAsync(int commentId)
        {
            var deletedComment = _dbContext.Comments.Remove(new Comment { Id = commentId });
            await _dbContext.SaveChangesAsync();

            return new CommentResponse
            {
                StatusCode = 200
            };
        }

        public async Task<CommentResponse> GetCommentByIdAsync(int commentId)
        {
            var commentExit = await _dbContext.Comments.FindAsync(commentId);
            if(commentExit == null)
            {
                return new CommentResponse
                {
                    StatusCode = 404,
                    ErrorMessage = "Comment cannot be found"
                };
            }

            return new CommentResponse
            {
                Comment = new Comment { Message = commentExit.Message, TimePosted = commentExit.TimePosted},
                StatusCode = 200,
                ErrorMessage = null
            };
        }

        public async Task<List<Comment>> GetCommentsByTweetIdAsync(int tweetId)
        {
            List<Comment> commentsByTweet = await _dbContext.Comments.Where(x => x.TweetId == tweetId).ToListAsync();

            return commentsByTweet;
        }

        public async Task<CommentResponse> PostCommentAsync(Comment request)
        {
            var tweetExist = await _dbContext.Tweets.SingleAsync(predicate: tweet => tweet.Id == request.TweetId);

            if(tweetExist != null)
            {
                var comment = await _dbContext.Comments.AddAsync(request);
                await _dbContext.SaveChangesAsync();
                return new CommentResponse
                {
                    StatusCode = 200,
                    Comment = new Comment
                    {
                        Message = comment.Entity.Message,
                        TimePosted = comment.Entity.TimePosted,
                        Id= comment.Entity.Id,
                        TweetId = comment.Entity.TweetId,
                        UserId = comment.Entity.UserId
                    }
                };
            }

            return new CommentResponse
            {
                StatusCode = 404,
                ErrorMessage = "Tweet not found"
            };
        }

        public async Task<bool> UserOwnsCommentAsync(int commentId, int userId)
        {
            var comment = await _dbContext.Comments.Where(predicate: x => x.Id == commentId).SingleAsync();

            if(comment == null)
            {
                return false;
            }

            if (comment.UserId == userId) return true;

            return false;
        }
    }
}
