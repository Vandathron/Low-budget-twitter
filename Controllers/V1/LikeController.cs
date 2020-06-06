using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Contract;
using Tweeter.Data;

namespace Tweeter.Controllers.V1
{
  
    [ApiController]
    public class LikeController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public LikeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost(ApiRoutes.Like.LikeTweet)]
        public async Task<IActionResult> LikeTweetAsync(int tweetId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(ApiRoutes.Like.LikeComment)]
        public async Task<IActionResult> LikeCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

    }
}