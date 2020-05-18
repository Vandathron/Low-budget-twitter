using System;
using System.Collections.Generic;
using System.Linq;
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
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TweetController : ControllerBase
    {
        private ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet(ApiRoutes.Tweet.GetTweet)]
        public async Task<IActionResult> GetTweetByIdAsync(int tweetId)
        {
            var response = await _tweetService.GetTweetByIdAsync(tweetId);
            if(response.Id == null)
            {
                return NotFound(new
                {
                    status = false,
                    ErrorMessage = "Tweet not found"
                });
            }
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Tweet.PostTweet)]
        public async Task<IActionResult> PostTweetAsync([FromBody] TweetRequest tweet)
        {
            var tweetToPost = new Tweet
            {
                comments = null,
                Likes = null,
                message = tweet.TweetMessage,
                UserId = int.Parse(HttpContext.GetUserId()),
                TimePosted = DateTime.Now
            };

            var response = await _tweetService.PostTweetAsync(tweetToPost);

            return Ok(response);

        }

        [HttpGet(ApiRoutes.Tweet.GetTweetsByUser)]
        public async Task<IActionResult> GetTweetByUserIdAsync(int UserId)
        {
            return Ok();
        }


        [HttpDelete(ApiRoutes.Tweet.DeleteTweet)]
        public async Task<IActionResult> DeleteTweetAsync(int tweetId)
        {
            var userOwnsTweet =await _tweetService.UserOwnsTweetAsync(tweetId, int.Parse(HttpContext.GetUserId()));

            if (userOwnsTweet)
            {
                var response = await _tweetService.DeleteTweetAsync(tweetId);
                if(response.Id != null)
                {
                    return Ok(new
                    {
                        status = true,
                    });
                }
                return NotFound();
            }
            else
            {
                return Unauthorized(new
                {
                    status = false,
                    ErrorMessage = "No permission to delete this tweet"
                });
            }
        }
    }
}