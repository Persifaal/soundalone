using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Security
{
    public static class TokenHasher
    {
        public static string Hash(string token) 
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = System.Security.Cryptography.SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }
}
