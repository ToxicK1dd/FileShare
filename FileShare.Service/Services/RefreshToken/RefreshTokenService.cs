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
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository
                .GetFromTokenAsync(HashRefreshToken(oldRefreshToken), _httpContextAccessor.HttpContext.RequestAborted);

            if (dbRefreshToken is null)
                return null;

            if (dbRefreshToken.IsExpired)
                return null;
            if (dbRefreshToken.IsRevoked)
                return null;

            var newRefreshToken = _randomGenerator.GenerateBase64String();

            dbRefreshToken.Token = HashRefreshToken(newRefreshToken);
            dbRefreshToken.Expires = DateTimeOffset.UtcNow.AddDays(30);

            return newRefreshToken;
        }


        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository
                .GetFromTokenAsync(HashRefreshToken(refreshToken), _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            dbRefreshToken.IsRevoked = true;
            dbRefreshToken.Revoked = DateTimeOffset.UtcNow;

            return dbRefreshToken.IsRevoked;
        }

        public async Task<bool> RevokeRefreshTokenFromIdAsync(Guid id)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository
                .GetByIdAsync(id, _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            dbRefreshToken.IsRevoked = true;
            dbRefreshToken.Revoked = DateTimeOffset.UtcNow;

            return dbRefreshToken.IsRevoked;
        }


        public async Task<bool> DeleteRefreshTokenAsync(string refreshToken)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository
                .GetFromTokenAsync(HashRefreshToken(refreshToken), _httpContextAccessor.HttpContext.RequestAborted);
            if (dbRefreshToken is null)
                return false;

            _unitOfWork.RefreshTokenRepository.Remove(dbRefreshToken);
            return true;
        }

        public async Task<bool> DeleteRefreshTokenFromIdAsync(Guid id)
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository
                .GetByIdAsync(id, _httpContextAccessor.HttpContext.RequestAborted);
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


        public async Task<string> GetRefreshTokenFromUsernameAsync(string username)
        {
            var userId = await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, _httpContextAccessor.HttpContext.RequestAborted);
            if (userId == Guid.Empty)
                return null;

            return await GetRefreshTokenFromUserIdAsync(userId);
        }

        public async Task<string> GetRefreshTokenFromUserIdAsync(Guid userId)
        {
            var refreshTokenString = _randomGenerator.GenerateBase64String();

            var refreshToken = new DataAccess.Models.Primary.RefreshToken.RefreshToken()
            {
                Token = HashRefreshToken(refreshTokenString),
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                UserId = userId
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, _httpContextAccessor.HttpContext.RequestAborted);

            return refreshTokenString;
        }


        public async Task<Guid> GetUserIdFromRefreshTokenAsync(string refreshToken)
        {
            return await _unitOfWork.RefreshTokenRepository.GetUserIdFromToken(HashRefreshToken(refreshToken), _httpContextAccessor.HttpContext.RequestAborted);
        }


        #region Helpers
        private static string HashRefreshToken(string refreshToken)
        {
            // I know we shouldn't hardcode a salt,
            // but the refresh token should already be secure by itself.
            return BCrypt.Net.BCrypt.HashPassword(refreshToken, "$2a$11$u6ST0jsWRZm63oHhwkokmu");
        }
        #endregion
    }
}