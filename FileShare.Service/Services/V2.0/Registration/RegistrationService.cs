using FileShare.Service.Dtos.V2._0.Registration;
using FileShare.Service.Services.V2._0.Registration.Interface;
using FileShare.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileShare.Service.Services.V2._0.Registration
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


        public async Task<RegistrationResultDto> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken)
        {
            var emailValid = IsValidEmailAddress(email);
            if (emailValid is false)
                return new(false, "Email is not valid format.");

            var emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists is not null)
                return new(false, "Email is already taken.");

            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists is not null)
                return new(false, "Username is already taken.");

            DataAccess.Models.Primary.User.User user = new()
            {
                Id = Guid.NewGuid(),
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username,
                Enabled = true,
                Verified = false
            };

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            var result = await _userManager.CreateAsync(user, password);
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