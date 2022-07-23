using System.ComponentModel.DataAnnotations;

namespace ImageApi.Models.V2._0.Login
{
    /// <summary>
    /// Model for changing the password of a user.
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// The current password of the user.
        /// </summary>
        [Required]
        public string OldPassword { get; set; }

        /// <summary>
        /// The new password of the user.
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}