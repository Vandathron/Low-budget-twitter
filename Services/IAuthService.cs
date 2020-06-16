using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Domain;

namespace Tweeter.Services
{
    public interface IAuthService
    {
        public Task<AuthenticationResult> RegisterUserAsync(UserRegistrationRequest user);
        public Task<AuthenticationResult> LoginUserAsync(String email, String password);
        public Task<UserRegistrationResponse> UpdateUserAsync(UserRegistrationRequest user, int userId);
        public Task<UserRegistrationResponse> GetUserByIdAsync(int userId);
    }
}
