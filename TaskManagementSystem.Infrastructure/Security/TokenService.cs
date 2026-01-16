using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Application.Auth;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateAccessToken(Guid userId, string role)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), 
        new Claim(ClaimTypes.Role, role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],         
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public RefreshToken GenerateRefreshToken()
            => new(Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(7));
    }
}
