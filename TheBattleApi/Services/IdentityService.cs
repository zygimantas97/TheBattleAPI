using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheBattleApi.Data;
using TheBattleApi.Models;
using TheBattleApi.Options;

namespace TheBattleApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _context;

        public IdentityService(UserManager<IdentityUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            DataContext context)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exists." }
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Incorrect password." }
                };
            }

            return await GenerateUserAuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid Token." }
                };
            }
            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This token hasn't expired yet." }
                };
            }
            var jti = validatedToken.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = _context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            if (storedRefreshToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token does not exist." }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has expired." }
                };
            }
            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been invalidated." }
                };
            }
            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token has been used." }
                };
            }
            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token does not match this JWT." }
                };
            }
            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateUserAuthenticationResultAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, string userName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists." }
                };
            }

            var newUser = new IdentityUser
            {
                NormalizedUserName = userName,
                Email = email,
                UserName = email,
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(e => e.Description)
                };
            }

            return await GenerateUserAuthenticationResultAsync(newUser);
        }

        private async Task<AuthenticationResult> GenerateUserAuthenticationResultAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddYears(1)
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
