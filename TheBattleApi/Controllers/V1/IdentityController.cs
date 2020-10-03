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
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Registers an user in the system
        /// </summary>
        /// <response code="200">An user was successfully registered in the system</response>
        /// <response code="400">Unable to register an user</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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

            return Ok(new AuthenticationResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Logs an user in the system
        /// </summary>
        /// <response code="200">An user was successfully loged in the system</response>
        /// <response code="400">Unable to log an user in the system</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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

            return Ok(new AuthenticationResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Refreshes authentication token (JWT)
        /// </summary>
        /// <response code="200">Authentication token was successfully refreshed</response>
        /// <response code="400">Unable to refresh authentication token</response>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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

            return Ok(new AuthenticationResponse
            {
                Token = _authResponse.Token,
                RefreshToken = _authResponse.RefreshToken
            });
        }

    }
}
