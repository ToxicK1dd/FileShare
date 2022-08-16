using FileShare.DataAccess.Models.Primary.RefreshToken;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.RefreshToken;
using FileShare.Service.Services.RefreshToken.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace FileShare.Service.Services.RefreshToken
{
    /// <summary>
    /// Methods for managing refresh tokens.
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClaimsHelper _identityClaimsHelper;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IMapper _mapper;

        public RefreshTokenService(
            IHttpContextAccessor httpContextAccessor,
            IIdentityClaimsHelper identityClaimsHelper,
            IPrimaryUnitOfWork primaryUnitOfWork,
            IRandomGenerator randomGenerator,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClaimsHelper = identityClaimsHelper;
            _unitOfWork = primaryUnitOfWork;
            _randomGenerator = randomGenerator;
            _mapper = mapper;
        }


        public async Task<string> RotateRefreshTokenAsync(string oldRefreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(oldRefreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return null;

            if (dbRefreshToken.IsExpired)
                return null;
            if (dbRefreshToken.IsRevoked)
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

            return dbRefreshToken.IsRevoked;
        }

        public async Task<bool> RevokeRefreshTokenFromIdAsync(Guid id)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetByIdAsync(id, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            dbRefreshToken.IsRevoked = true;
            dbRefreshToken.Revoked = DateTimeOffset.UtcNow;

            return dbRefreshToken.IsRevoked;
        }

        public async Task<bool> DeleteRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(refreshToken, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            _unitOfWork.RefreshTokenRepository.Remove(dbRefreshToken);
            return true;
        }

        public async Task<bool> DeleteRefreshTokenFromIdAsync(Guid id)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetByIdAsync(id, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            _unitOfWork.RefreshTokenRepository.Remove(dbRefreshToken);
            return true;
        }

        public async Task<IEnumerable<RefreshTokenDto>> GetRefreshTokensAsync(int page, int size)
        {
            if (page <= 0 || size <= 0)
                return Enumerable.Empty<RefreshTokenDto>();

            var username = _identityClaimsHelper.GetUsernameFromHttpContext(_httpContextAccessor.HttpContext);
            var userId = await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, _httpContextAccessor.HttpContext.RequestAborted);

            var tokens = await _unitOfWork.RefreshTokenRepository.GetAllByUserIdPaginatedAsync(userId, page, size, _httpContextAccessor.HttpContext.RequestAborted);

            return _mapper.Map<IEnumerable<RefreshTokenDto>>(tokens);
        }
    }
}