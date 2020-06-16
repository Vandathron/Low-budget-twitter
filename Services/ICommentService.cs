using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Domain;

namespace Tweeter.Services
{
   public interface ICommentService
    {
        public Task<CommentResponse> PostCommentAsync(Comment request);
        public Task<CommentResponse> DeleteCommentAsync(int commentId);
        public Task<CommentResponse> GetCommentByIdAsync(int commentId);
        public Task<List<Comment>> GetCommentsByTweetIdAsync(int tweetId);
        public Task<bool> UserOwnsCommentAsync(int tweetId, int userId);
    }
}
