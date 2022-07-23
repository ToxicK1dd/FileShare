using System.ComponentModel.DataAnnotations;

namespace ImageApi.Models.V2._0.Login
{
    /// <summary>
    ///  Model for authenticating a user.
    /// </summary>
    /// <param name="Username">The username of the user.</param>
    /// <param name="Password">The password of the user.</param>
    public record AuthenticateLoginModel(
        [Required]
        string Username,
        [Required]
        string Password
    );
}