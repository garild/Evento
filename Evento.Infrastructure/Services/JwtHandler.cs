using System;
using System.Security.Claims;
using Evento.Infrastructure.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Evento.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;
        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

        }
        public JwtDTO CreateToken(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
               new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
               new Claim(JwtRegisteredClaimNames.UniqueName,userId.ToString()),
               new Claim(ClaimTypes.Role,role),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Iat,now.ToTimestamp().ToString()),
            };

            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           

            var jwt = new JwtSecurityToken(
            
              issuer: _jwtSettings.Issure,
              audience: _jwtSettings.Issure,
              claims: claims,
              notBefore: now,
              expires: expires,
              signingCredentials: creds

             );
             var token = new JwtSecurityTokenHandler().WriteToken(jwt);
             return new JwtDTO
             {
                 Token = token,
                 Expires = expires.ToTimestamp()
             };
        }
    }
}