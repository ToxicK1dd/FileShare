using FileShare.DataAccess.Models.Primary.User;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public TokenService(IPrimaryUnitOfWork primaryUnitOfWork, IConfiguration configuration, IRandomGenerator randomGenerator, UserManager<User> userManager)
        {
            _unitOfWork = primaryUnitOfWork;
            _configuration = configuration;
            _randomGenerator = randomGenerator;
            _userManager = userManager;
        }


        public async Task<string> GetAccessTokenAsync(Guid userId)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

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

            return await GetAccessTokenAsync(userId);
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