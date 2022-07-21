using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Token.Interface;
using ImageApi.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImageApi.Service.Services.Token
{
    /// <summary>
    /// Methods for generating JWT's and refresh tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public TokenService(IPrimaryUnitOfWork primaryUnitOfWork, IConfiguration configuration)
        {
            _unitOfWork = primaryUnitOfWork;
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

        public async Task<string> GetAccessTokenFromUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var accountId = await _unitOfWork.LoginRepository.GetIdFromUsername(username, cancellationToken);

            return GetAccessToken(accountId);
        }

        public async Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var loginId = await _unitOfWork.LoginRepository.GetIdFromUsername(username, cancellationToken);

            return await GetRefreshTokenAsync(loginId, cancellationToken);
        }

        public async Task<string> GetRefreshTokenAsync(Guid loginId, CancellationToken cancellationToken)
        {
            var refreshTokenString = RandomStringGenerator.Generate();

            var refreshToken = new DataAccess.Models.Primary.RefreshToken.RefreshToken()
            {
                Token = refreshTokenString,
                Expiration = DateTimeOffset.UtcNow.AddDays(30),
                LoginId = loginId
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return refreshToken.Token;
        }
    }
}