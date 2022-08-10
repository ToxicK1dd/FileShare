using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Password
{
    public record ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords doesn't match.")]
        public string ConfirmPassword { get; init; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [Required]
        public string Token { get; init; }
    }
}