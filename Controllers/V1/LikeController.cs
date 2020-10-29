using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Contract;
using Tweeter.Data;
using Tweeter.Extensions;
using Tweeter.Services;

namespace Tweeter.Controllers.V1
{
  
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost(ApiRoutes.Like.LikeTweet)]
        public async Task<IActionResult> LikeTweetAsync(int tweetId)
        {
            var response = await _likeService.LikeComment(tweetId, int.Parse(HttpContext.GetUserId()));
            if (response.StatusCode == 200) return Ok(response.Status); return NotFound();
        }

        [HttpPost(ApiRoutes.Like.LikeComment)]
        public async Task<IActionResult> LikeCommentAsync(int commentId)
        {
            var response = await _likeService.LikeComment(commentId, int.Parse(HttpContext.GetUserId()));
            if (response.StatusCode == 200) return Ok(response.Status);  return NotFound();
        }

    }
}