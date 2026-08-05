using AuthService.Application.Models;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        public TokenPair GenerateTokenPair(UserAccount user);
        public bool ValidateAccessToken(string token, out AccessTokenPayload accessToken);
    }
}
