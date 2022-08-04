using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileShare.Service.Services.V2._0.Token
{
    /// <summary>
    /// Methods for generating JWT's and refresh tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IRandomGenerator _randomGenerator;

        public TokenService(IPrimaryUnitOfWork primaryUnitOfWork, IConfiguration configuration, IRandomGenerator randomGenerator)
        {
            _unitOfWork = primaryUnitOfWork;
            _configuration = configuration;
            _randomGenerator = randomGenerator;
        }


        public string GetAccessToken(Guid userId)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);

            var claims = new[]
            {
                new Claim("UserId", $"{userId}"),
            };

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<string> GetAccessTokenFromUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var userId = await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, cancellationToken);

            return GetAccessToken(userId);
        }

        public async Task<string> GetRefreshTokenFromUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var userId = await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, cancellationToken);

            return await GetRefreshTokenAsync(userId, cancellationToken);
        }

        public async Task<string> GetRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            var refreshTokenString = _randomGenerator.GenerateBase64String();

            var refreshToken = new DataAccess.Models.Primary.RefreshToken.RefreshToken()
            {
                Token = refreshTokenString,
                Expiration = DateTimeOffset.UtcNow.AddDays(30),
                UserId = userId
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return refreshToken.Token;
        }
    }
}