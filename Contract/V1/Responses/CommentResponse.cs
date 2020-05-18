using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Domain;

namespace Tweeter.Contract.V1.Responses
{
    public class CommentResponse
    {
       public Comment comment { get; set; }
    }
}
