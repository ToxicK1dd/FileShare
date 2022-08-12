using FileShare.DataAccess.Models.Primary.RefreshToken;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.RefreshToken.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.RefreshToken
{
    public class RefreshTokenController : BaseController
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;

        public RefreshTokenController(IPrimaryUnitOfWork unitOfWork, IRefreshTokenService refreshTokenService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
        }


        /// <summary>
        /// Get a new JWT, using the refresh token. This will also replace the refresh token with a new one.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <response code="201">A new JWt, and refresh token has been created.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Refresh([FromQuery] string oldRefreshToken)
        {
            var newRefreshToken = await _refreshTokenService.ValidateRefreshTokenAsync(oldRefreshToken);
            if (newRefreshToken is null)
                return NotFound("Refresh token is invalid.");

            var userId = await _unitOfWork.RefreshTokenRepository.GetUserIdFromToken(oldRefreshToken);
            var token = _tokenService.GetAccessTokenFromUserIdAsync(userId);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                token,
                refreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Remove the specified refresh token, to prevent the user from optaining a new JWT.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <response code="204">The refresh token has been deleted.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Revoke([FromQuery] string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetFromTokenAsync(refreshToken);
            if (token is null)
                return NotFound("Refresh token could not be found.");

            _unitOfWork.RefreshTokenRepository.Remove(token);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        ///// <summary>
        ///// Get all the current active refresh tokens for the user.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Tokens()
        //{

        //}
    }
}