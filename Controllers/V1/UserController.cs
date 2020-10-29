using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Contract;
using Tweeter.Contract.V1.Requests;
using Tweeter.Contract.V1.Responses;
using Tweeter.Extensions;
using Tweeter.Services;

namespace Tweeter.Controllers.V1
{

    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.User.Login)]
        public async Task<IActionResult> LoginAsync(String email, String password)
        {
            var response = await _authService.LoginUserAsync(email, password);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(new AuthFailedResponse
            {
                ErrorMessages = response.ErrorMessage
            });
        }

        [HttpPost(ApiRoutes.User.Register)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var response = await _authService.RegisterUserAsync(User);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest( new AuthFailedResponse
            {
                ErrorMessages = response.ErrorMessage
            });
        }

        [HttpGet(ApiRoutes.User.Get)]
        public async Task<IActionResult> GetAsync([FromRoute] int UserId)
        {
            var response = await _authService.GetUserByIdAsync(UserId);

            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

       [HttpDelete(ApiRoutes.User.Delete)]
        public async Task<IActionResult> DeleteUserAsync([FromBody] int UserId)
        {
            return Ok();
        }

        [HttpPut(ApiRoutes.User.Update)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserRegistrationRequest request)
        {
            int userId = int.Parse(HttpContext.GetUserId());
            var response = await _authService.UpdateUserAsync(request, userId);

            if (response != null)
            {
                return Ok(response);
            }
            else return BadRequest();
        }

    }
}