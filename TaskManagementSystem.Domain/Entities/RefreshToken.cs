using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsRevoked { get; private set; }

        public RefreshToken(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
