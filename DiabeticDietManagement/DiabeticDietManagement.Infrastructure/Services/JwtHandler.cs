using DiabeticDietManagement.Infrastructure.DTO;
using DiabeticDietManagement.Infrastructure.Extensions;
using DiabeticDietManagement.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private JwtSettings _settings;
        public JwtHandler(JwtSettings settings)
        {
            _settings = settings;
        }
        public JwtDto CreateToken(string email, string role)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimeStamp().ToString(), ClaimValueTypes.Integer64)
            };


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)), SecurityAlgorithms.HmacSha256);

            var expires = now.AddMinutes(_settings.Expiry);

            var jwt = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    claims: claims,
                    notBefore: now,
                    expires: expires,
                    signingCredentials: signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expiry = expires.ToTimeStamp()
            };
        }
    }
}
