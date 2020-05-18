using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweeter.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public Boolean Success { get; set; }
        public IEnumerable<string> ErrorMessage { get; set; }

        public User User;
    }
}
