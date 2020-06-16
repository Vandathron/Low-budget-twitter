using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Data;
using Tweeter.Domain;
using Tweeter.Options;

namespace Tweeter.Services
{
    public class AuthService:IAuthService
    {
        private JwtSettings _jwtSettings;
        private ApplicationDbContext _dbContext;
        public AuthService(JwtSettings jwtSettings, ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings;
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResult> LoginUserAsync(string email, string password)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(predicate: x => x.Email == email && x.Password == password);

            if(user != null)
            {
                return GenerateAuthenticationResultForUser(user);
            }

            return new AuthenticationResult
            {
                ErrorMessage = new string[] { "Email or username is incorrect" },
                Success = false,
                User = null
            };
        }

        public async Task<AuthenticationResult> RegisterUserAsync(UserRegistrationRequest user)
        {
            var existingUser = await _dbContext.User.FirstOrDefaultAsync(predicate: x => x.UserName == user.Username.Trim() || x.Email == user.Email.Trim());

            if(existingUser == null)
            {
                var userToCreate = new User
                {
                    Email = user.Email,
                    Bio = user.Bio,
                    Password = user.Password,
                    UserName = user.Username,
                };

                var createdUser = await _dbContext.User.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();

                return GenerateAuthenticationResultForUser(createdUser.Entity);

            }

            if(existingUser.Email == user.Email.Trim())
            {
                return new AuthenticationResult
                {
                    ErrorMessage = new string[] { "This email is already registered" },
                    Success = false,
                    User = null
                };
            }
            return new AuthenticationResult
            {
                ErrorMessage = new string[] { "This username is already registered" },
                Success = false,
                User = null
            };
        }

        public async Task<UserRegistrationResponse> UpdateUserAsync(UserRegistrationRequest user, int userId)
        {
            var userExist = await _dbContext.User.FindAsync(userId);

            if(userExist == null)
            {
                return null;
            }
            else 
            {
                userExist.Bio = user.Bio;
                await _dbContext.SaveChangesAsync();
                return new UserRegistrationResponse { Username = userExist.UserName };
              
            }
        }

        public async Task<UserRegistrationResponse> GetUserByIdAsync(int id)
        {
            var userExist = await _dbContext.User.FindAsync(id);

            if(userExist != null)
            {
                return new UserRegistrationResponse
                {
                    Email = userExist.Email,
                    Id = userExist.Id,
                    Username = userExist.UserName,
                };
            }

            else
            {
                return null;
            }
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(type: "Id", value: user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                User = user
            };
        }
    }
}
