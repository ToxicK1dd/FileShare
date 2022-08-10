using FileShare.Api.Models.V2._0.Password;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Password.Interface;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Controllers.V2._0.Password
{
    /// <summary>
    /// Endpoints for managing passwords.
    /// </summary>
    public class PasswordController : BaseController
    {
        private readonly IPasswordService _passwordService;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IFluentEmailFactory _emailFactory;

        public PasswordController(IPasswordService passwordService, IPrimaryUnitOfWork unitOfWork, IFluentEmailFactory emailFactory)
        {
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _emailFactory = emailFactory;
        }


        /// <summary>
        /// Change the current password of the user.
        /// </summary>
        /// <param name="model"></param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            await _passwordService.ChangePasswordAsync(model.NewPassword, model.OldPassword);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Request a password reset token.
        /// </summary>
        /// <param name="email"></param>
        [HttpPost]
        [AllowAnonymous]
        [ActionName("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RequestResetPassword([FromQuery] string email)
        {
            var token = await _passwordService.RequestResetPasswordAsync(email);
            if (token is null)
                return NotFound("User not found");

            // TODO: Implement email sending.
            //var mail = _emailFactory.Create();
            //await mail.To(email)
            //    .Subject("Reset password")
            //    .Body("<p>Click <a href=\"#\">here</a> to reset your password.</p>")
            //    .SendAsync();

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Reset the password of a user using a password reset token.
        /// </summary>
        /// <param name="model"></param>
        [HttpPut]
        [AllowAnonymous]
        [ActionName("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ResetPasswordModel model)
        {
            var isSuccessful = await _passwordService
                .ConfirmResetPasswordAsync(model.Email, model.Password, model.ConfirmPassword, model.Token);
            if (isSuccessful is false)
                return BadRequest("Email or token is invalid.");

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}