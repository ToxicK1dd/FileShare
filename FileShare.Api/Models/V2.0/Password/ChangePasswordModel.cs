using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Password
{
    /// <summary>
    /// Model for changing the password of a user.
    /// </summary>
    public record ChangePasswordModel
    {
        /// <summary>
        /// The current password of the user.
        /// </summary>
        [Required]
        public string OldPassword { get; init; }

        /// <summary>
        /// The new password of the user.
        /// </summary>
        [Required]
        public string NewPassword { get; init; }
    }
}