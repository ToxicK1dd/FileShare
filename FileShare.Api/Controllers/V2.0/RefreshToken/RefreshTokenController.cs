﻿using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.RefreshToken.Interface;
using FileShare.Service.Services.Token.Interface;
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
        /// Get a new access token using the refresh token. This will also rotate the refresh token.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <response code="201">An access token has been generated, and refresh token has been rotated.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Refresh([FromQuery] string oldRefreshToken)
        {
            var isRefreshTokenValid = await _refreshTokenService.ValidateRefreshTokenAsync(oldRefreshToken);
            if (isRefreshTokenValid is false)
                return BadRequest("The refresh token is not valid.");

            var newRefreshToken = await _refreshTokenService.RotateRefreshTokenAsync(oldRefreshToken);
            var newAccessToken = _tokenService.GetAccessTokenFromRefreshToken(newRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return Created(string.Empty, new
            {
                accesstoken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Revoke the specified refresh token, to prevent the user from optaining a new access token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <response code="204">The refresh token has been deleted.</response>
        /// <response code="404">The specified refresh token could not be found.</response>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Revoke([FromQuery] string refreshToken)
        {
            var isRevokedSuccessfully = await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken);
            if (isRevokedSuccessfully is false)
                return NotFound("The refresh token could not be found.");

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