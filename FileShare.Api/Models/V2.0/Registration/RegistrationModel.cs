using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Registration
{
    /// <summary>
    /// Model for registering a new user.
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record RegistrationModel(
        [Required]
        string Username,
        [Required]
        string Email,
        [Required]
        string Password);
}