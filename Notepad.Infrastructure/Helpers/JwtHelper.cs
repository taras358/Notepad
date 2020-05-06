using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notepad.Core.Entities;
using Notepad.Core.Helpers;
using Notepad.Core.Models.Responses;
using Notepad.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.Infrastructure.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly AuthTokenOption _tokenOptions;

        public JwtHelper(IOptions<AuthTokenOption> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public JwtAuthResponse GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions?.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessTokenClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var refreshTokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            DateTime accessTokenExpires = DateTime.UtcNow.Add(_tokenOptions.AccessTokenExpiration);
            DateTime refreshTokenExpires = DateTime.UtcNow.Add(_tokenOptions.RefreshTokenExpiration);
            JwtSecurityToken accessToken = GenerateToken(null, accessTokenExpires, accessTokenClaims, credentials);
            JwtSecurityToken refreshToken = GenerateToken(null, refreshTokenExpires, refreshTokenClaims, credentials);
            string encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            string encodedRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return new JwtAuthResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                AccessToken = encodedAccessToken,
                RefreshToken = encodedRefreshToken,
            };
        }

        private static JwtSecurityToken GenerateToken(string audience, DateTime expiration, List<Claim> claims, SigningCredentials credentials)
        {
            return new JwtSecurityToken(
              audience: audience,
              claims: claims,
              expires: expiration,
              signingCredentials: credentials);
        }
    }
}
