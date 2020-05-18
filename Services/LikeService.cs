using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweeter.Contract.V1.Responses;
using Tweeter.Data;

namespace Tweeter.Services
{
    public class LikeService : ILikeService
    {
        private ApplicationDbContext _dbContext;
        public LikeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<LikeResponse> LikeComment(int commentId)
        {
            throw new NotImplementedException();
        }


    }
}
