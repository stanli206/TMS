using Microsoft.EntityFrameworkCore;
using System;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Application.Abstractions;


namespace TaskManagementSystem.Application.Auth
{
    public class LoginHandler
    {
        private readonly IAuthDbContext _db;
        private readonly ITokenService _tokenService;

        public LoginHandler(IAuthDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public async Task<LoginResult?> Handle(LoginRequest req)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == req.Email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return null;

            var accessToken =
                _tokenService.GenerateAccessToken(user.Id, user.Role);

            var refreshToken =
                _tokenService.GenerateRefreshToken();

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            return new LoginResult(accessToken, refreshToken.Token);
        }
    }
}
