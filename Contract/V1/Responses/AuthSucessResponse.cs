using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Contract.V1.Responses
{
    public class AuthSucessResponse
    {
        public string Token { get; set; }
        public string Status { get; set; }
        public UserRegistrationResponse User { get; set; }
        
    }
}
