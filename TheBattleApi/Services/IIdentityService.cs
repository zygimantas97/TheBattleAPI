using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Models;

namespace TheBattleApi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, string userName);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
