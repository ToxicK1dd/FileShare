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


        public async Task<string> ValidateRefreshTokenAsync(string oldRefreshToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (refreshToken is null)
                return null;
            if (refreshToken.IsExpired)
                return null;

            refreshToken.Token = _randomGenerator.GenerateBase64String();
            refreshToken.Expires = DateTimeOffset.UtcNow.AddDays(30);

            return refreshToken.Token;
        }
    }
}