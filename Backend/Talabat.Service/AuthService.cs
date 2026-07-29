using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class AuthService : IAuthService  //common service"repeated in all projects"
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            // Private Claims"payload or data" (User-Defined)
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            //Secret Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            //Token Object that contains:
            //registered claims like (audience, issuer, expiry,....) ,private claims (user-defined)
            //security key"Secret Key" + algorithm
            var token = new JwtSecurityToken(
                    audience: _configuration["JWT:ValidAudience"], 
                    issuer: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            //Token Generation
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}