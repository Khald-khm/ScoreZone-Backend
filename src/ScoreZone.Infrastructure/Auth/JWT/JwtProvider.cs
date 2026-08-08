using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace ScoreZone.Infrastructure.Auth.JWT
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        

        public Task<string> CreateToken(string identityId, Guid userId, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("IdentityId", identityId),
                new Claim("UserId", userId.ToString())
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Key)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken
            (
                issuer : _jwtOptions.Issuer,
                audience : _jwtOptions.Audience,
                claims : claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
                signingCredentials : credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(jwtToken);

        }


        public string CreateRefreshToken()
        {
            var randBytes = new byte [64];

            using var randGen = RandomNumberGenerator.Create();

            randGen.GetBytes(randBytes);

            var token = Convert.ToBase64String(randBytes);

            return token;
        }
    }
}