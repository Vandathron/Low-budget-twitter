using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Contract;
using Tweeter.Contract.V1.Requests;
using Tweeter.Domain;
using Tweeter.Extensions;
using Tweeter.Services;

namespace Tweeter.Controllers.V1
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet(ApiRoutes.Comment.GetById)]
        public async Task<IActionResult> GetCommentAsync(int commentId)
        {
            var response = await _commentService.GetCommentByIdAsync(commentId);

            if(response.StatusCode == 200)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpGet(ApiRoutes.Comment.GetByTweetId)]
        public async Task<IActionResult> GetCommentByTweetIdAsync([FromRoute] int tweetId)
        {
            var response = await _commentService.GetCommentsByTweetIdAsync(tweetId);

            if (response != null)
            {
                return Ok(response);
            }
            else return NotFound();
        }

        [HttpPost(ApiRoutes.Comment.PostComment)]
        public async Task<IActionResult> PostCommentAsync([FromBody] CommentRequest comment)
        {
            var commentToPost = new Comment
            {
                Message = comment.CommentMessage,
                TweetId = comment.TweetId,
                TimePosted = DateTime.UtcNow,
                UserId = int.Parse(HttpContext.GetUserId())
            };

            var response = await _commentService.PostCommentAsync(commentToPost);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete(ApiRoutes.Comment.Delete)]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            var userOwnsComment = await _commentService.UserOwnsCommentAsync(commentId, int.Parse(HttpContext.GetUserId()));
            if (userOwnsComment)
            {
                var response = await _commentService.DeleteCommentAsync(commentId);
                return Ok(response);
            }
            else
            {
                return Unauthorized(new
                {
                    ErrorMessage = "No permission to delete this comment"
                });
            }

        }

    }
}