using FileShare.Service.Dtos.Registration;
using FileShare.Service.Services.Registration.Interface;
using FileShare.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileShare.Service.Services.Registration
{
    /// <summary>
    /// Methods for registering accounts.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<DataAccess.Models.Primary.User.User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RegistrationService(
            UserManager<DataAccess.Models.Primary.User.User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<RegistrationResultDto> RegisterAsync(RegisterDto dto)
        {
            var emailValid = IsValidEmailAddress(dto.Email);
            if (emailValid is false)
                return new(false, "Email is not valid format.");

            var emailExists = await _userManager.FindByEmailAsync(dto.Email);
            if (emailExists is not null)
                return new(false, "Email is already taken.");

            var userExists = await _userManager.FindByNameAsync(dto.Username);
            if (userExists is not null)
                return new(false, "Username is already taken.");

            if (string.Equals(dto.Password, dto.ConfirmPassword) is false)
                return new(false, "The passwords doesn't match.");

            DataAccess.Models.Primary.User.User user = new()
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Username,
                IsEnabled = true,
                IsVerified = false
            };

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            var result = await _userManager.CreateAsync(user, dto.Password);
            return new(result.Succeeded, string.Empty);
        }


        #region Helpers
        public static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return new EmailAddressAttribute().IsValid(email);
        }
        #endregion
    }
}