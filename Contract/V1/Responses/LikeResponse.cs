using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Contract.V1.Responses
{
    public class LikeResponse
    {
        public int StatusCode { get; set; }
        public bool IsLiked { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
