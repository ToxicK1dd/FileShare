using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.RefreshToken.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.AspNetCore.Http;

namespace FileShare.Service.Services.V2._0.RefreshToken
{
    /// <summary>
    /// Methods for managing refresh tokens.
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IRandomGenerator _randomGenerator;

        public RefreshTokenService(IHttpContextAccessor httpContextAccessor, IPrimaryUnitOfWork primaryUnitOfWork, IRandomGenerator randomGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = primaryUnitOfWork;
            _randomGenerator = randomGenerator;
        }


        public async Task<bool> ValidateRefreshTokenAsync(string oldRefreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;
            if (dbRefreshToken.IsExpired)
                return false;
            if (dbRefreshToken.IsRevoked)
                return false;

            return true;
        }

        public async Task<string> RotateRefreshTokenAsync(string oldRefreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return null;

            dbRefreshToken.Token = _randomGenerator.GenerateBase64String();
            dbRefreshToken.Expires = DateTimeOffset.UtcNow.AddDays(30);

            return dbRefreshToken.Token;
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(refreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            dbRefreshToken.IsRevoked = true;
            dbRefreshToken.Revoked = DateTimeOffset.UtcNow;

            return true;
        }
    }
}