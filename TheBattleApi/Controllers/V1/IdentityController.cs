using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using TheBattleApi.Contracts.V1.Requests;
using TheBattleApi.Contracts.V1.Responses;
using TheBattleApi.Services;
using ErrorModel = TheBattleApi.Contracts.V1.Responses.ErrorModel;

namespace TheBattleApi.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => new ErrorModel { Message = e.ErrorMessage }))
                });
            }

            var _authResponse = await _identityService.RegisterAsync(request.Email, request.Password, request.UserName);
            if (!_authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = _authResponse.Errors.Select(e => new ErrorModel { Message = e})
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequest request)
        {
            var _authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!_authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = _authResponse.Errors.Select(e => new ErrorModel { Message = e})
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var _authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
            if (!_authResponse.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = _authResponse.Errors.Select(e => new ErrorModel { Message = e})
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

    }
}
