using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Login
{
    public record AuthenticateTotpLoginModel
    {
        public AuthenticateTotpLoginModel(string username, string password, string code)
        {
            Username = username;
            Password = password;
            Code = code;
        }


        [Required]
        public string Username { get; init; }

        [Required]
        public string Password { get; init; }

        [Required]
        public string Code { get; init; }
    }
}