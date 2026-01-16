using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Auth
{
    public interface ITokenService
    {
        string GenerateAccessToken(Guid userId, string role);
        RefreshToken GenerateRefreshToken();
    }
}
