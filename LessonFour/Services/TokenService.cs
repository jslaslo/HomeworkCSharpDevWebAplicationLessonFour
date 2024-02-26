using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using LessonFour.Abstractions;
using LessonFour.Securities;
using Microsoft.IdentityModel.Tokens;

namespace LessonFour.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfiguration _jwt;

        public TokenService(JwtConfiguration jwt)
        {
            _jwt = jwt;
        }

        public string GenerateToken(string email, string roleName)
        {
            var credentilas = new SigningCredentials(_jwt.GetSigningKey(), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleName)
            };
            var token = new JwtSecurityToken
                (
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentilas
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

