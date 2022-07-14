using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Token.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImageApi.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IPrimaryUnitOfWork _primaryUnitOfWork;
        private readonly IConfiguration _configuration;

        public TokenService(IPrimaryUnitOfWork primaryUnitOfWork, IConfiguration configuration)
        {
            _primaryUnitOfWork = primaryUnitOfWork;
            _configuration = configuration;
        }


        public string GetAccessToken(Guid accountId)
        {
            // Create claims based user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AccountId", $"{accountId}"),
                    };

            // Create credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create token based on credentials and claims
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GetRefreshTokenAsync(Guid loginId, CancellationToken cancellationToken)
        {
            var refreshTokenString = GenerateRefreshToken();

            var refreshToken = new DataAccess.Models.Primary.RefreshToken.RefreshToken()
            {
                Token = refreshTokenString,
                LoginId = loginId
            };
            await _primaryUnitOfWork.RefreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return refreshTokenString;
        }


        #region Helpers
        private static string GenerateRefreshToken(int length = 64)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#¤%&/()=?~^*-_<>\\";
            Random random = new();

            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(randomString));
        }
        #endregion
    }
}