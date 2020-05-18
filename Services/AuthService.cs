using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tweeter.Contract.V1.Requests;
using Tweeter.Domain;

namespace Tweeter.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthenticationResult> RegisterUserAsync(UserRegistrationRequest user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                return new AuthenticationResult
                {
                    ErrorMessage = "User already exist"
                };
            }
            else return new AuthenticationResult { ErrorMessage = "No user with this email" };
        }
    }
}
